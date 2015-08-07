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

namespace Felinesoft.UmbracoCodeFirst.ContentTypes
{
    /// <summary>
    /// A base class for code-first document types
    /// </summary>
    public abstract class MediaTypeBase : CodeFirstContentBase<MediaNodeDetails>, IHtmlString
    {
        private Lazy<IMediaModelModule> _modelModule;

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
    }
}