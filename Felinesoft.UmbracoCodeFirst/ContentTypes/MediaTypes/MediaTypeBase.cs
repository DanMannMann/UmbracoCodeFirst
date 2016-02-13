using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Umbraco.Web;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Converters;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Felinesoft.UmbracoCodeFirst.Core.Modules;
using System.Web;
using Felinesoft.UmbracoCodeFirst.Core;
using Felinesoft.UmbracoCodeFirst.Events;
using System.IO;

namespace Felinesoft.UmbracoCodeFirst.ContentTypes
{
    /// <summary>
    /// A base class for code-first document types
    /// </summary>
    public abstract class MediaTypeBase : CodeFirstContentBase<MediaNodeDetails>, IHtmlString
    {
        private Lazy<IMediaModelModule> _modelModule;
		private FileStream _file;

		/// <summary>
		/// A base class for code-first document types.
		/// This constructor initialises the NodeDetails property with an empty instance of <see cref="DocumentNodeDetails"/>
		/// </summary>
		public MediaTypeBase()
        {
            NodeDetails = new MediaNodeDetails();
            _modelModule = new Lazy<IMediaModelModule>(() => CodeFirstManager.Current.Modules.MediaModelModule);
        }

        public override string ToString()
        {
            try
            {
                return NodeDetails.IsPublishedInstance ?
                    NodeDetails.PublishedContent.Url
                    :
                    "#";
            }
            catch
            {
                return "#";
            }
        }

        public string ToHtmlString()
        {
            try
            {
                return NodeDetails.IsPublishedInstance ?
                    GetHtml()
                    :
                    "#";
            }
            catch
            {
                return "#";
            }
        }

        private string GetHtml()
        {
            var prop = CodeFirstModelContext.GetContext(this).ContentType.Properties.FirstOrDefault(x => x.Alias == SpecialAliases.FileUpload);
            if (prop == null)
            {
                return string.Empty;
            }
            else
            {
                var val = prop.Metadata.GetValue(this);
                if (val == null)
                {
                    return string.Empty;
                }
                else
                {
                    if (val is IHtmlString)
                    {
                        return (val as IHtmlString).ToHtmlString();
                    }
                    else
                    {
                        return HttpUtility.HtmlEncode(val.ToString());
                    }
                }
            }
        }

        public void Project(IMedia target)
        {
            _modelModule.Value.ProjectModelToContent(this, target);
        }

		#region Create and update IContent
		/// <summary>
		/// Persists the current values of the instance back to the database
		/// </summary>
		/// <param name="contentId">Id of the Umbraco Document</param>
		/// <param name="parentId">Id of the parent Umbraco Document. Only applied when creating new content. At present code-first cannot change the parent of an existing node.</param>
		/// <param name="userId">The user ID for the audit trail</param>
		/// <param name="raiseEvents">True to raise Umbraco content service events</param>
		public void Persist(int parentId = -1, int userId = 0, bool raiseEvents = false)
		{
			IMedia content;
			if (_modelModule.Value.TryConvertToContent(this, out content, parentId))
			{
				//persist object into umbraco database
				ApplicationContext.Current.Services.MediaService.Save(content, userId, raiseEvents);

				//update the node details
				NodeDetails = new MediaNodeDetails(content);
			}
			else
			{
				throw new CodeFirstException("Failed to convert model to content.");
			}
		}

		public void SetMediaFile(FileStream file)
		{
			_file = file;
		}

		internal FileStream MediaFile
		{
			get
			{
				return _file;
			}
		}
		#endregion
	}
}