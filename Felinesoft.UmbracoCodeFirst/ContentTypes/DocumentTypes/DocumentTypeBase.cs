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

using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Converters;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Felinesoft.UmbracoCodeFirst.Core.Modules;

namespace Felinesoft.UmbracoCodeFirst.ContentTypes
{
    /// <summary>
    /// A base class for code-first document types
    /// </summary>
    public abstract class DocumentTypeBase : CodeFirstContentBase<DocumentNodeDetails>
    {
        private Lazy<IDocumentModelModule> _modelModule;

        /// <summary>
        /// A base class for code-first document types.
        /// This constructor initialises the NodeDetails property with an empty instance of <see cref="DocumentNodeDetails"/>
        /// </summary>
        public DocumentTypeBase()
        {
            NodeDetails = new DocumentNodeDetails();
            _modelModule = new Lazy<IDocumentModelModule>(() => CodeFirstManager.Current.Modules.DocumentModelModule);
        }

        #region Create and update IContent
        /// <summary>
        /// Persists the current values of the instance back to the database
        /// </summary>
        /// <param name="contentId">Id of the Umbraco Document</param>
        /// <param name="parentId">Id of the parent Umbraco Document. Only applied when creating new content. At present code-first cannot change the parent of an existing node.</param>
        /// <param name="userId">The user ID for the audit trail</param>
        /// <param name="raiseEvents">True to raise Umbraco content service events</param>
        public void Persist(int parentId = -1, int userId = 0, bool raiseEvents = false, bool publish = false)
        {
            IContent content;
            if (_modelModule.Value.TryConvertToContent(this, out content, parentId))
            {
                //persist object into umbraco database
                if (publish)
                {
                    ApplicationContext.Current.Services.ContentService.SaveAndPublishWithStatus(content, userId, raiseEvents);
                }
                else
                {
                    ApplicationContext.Current.Services.ContentService.Save(content, userId, raiseEvents);
                }

                //update the node details
                NodeDetails = new DocumentNodeDetails(content);
            }
            else
            {
                throw new CodeFirstException("Failed to convert model to content.");
            }
        }

        public void Project(IContent target)
        {
            _modelModule.Value.ProjectModelToContent(this, target);
        }
        #endregion
    }
}