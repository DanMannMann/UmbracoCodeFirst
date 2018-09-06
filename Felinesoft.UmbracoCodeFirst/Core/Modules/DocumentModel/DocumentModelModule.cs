using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.Converters;
using Marsman.UmbracoCodeFirst.Core.Modules;
using Marsman.UmbracoCodeFirst.Core.Resolver;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Marsman.UmbracoCodeFirst.Extensions;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;
using Umbraco.Web;
using Umbraco.Core;
using System.Web.Mvc;
using System.Collections.Generic;
using Marsman.UmbracoCodeFirst.Exceptions;
using System.Web;
using Umbraco.Core.Services;
using Umbraco.Core.Events;
using Marsman.UmbracoCodeFirst.Events;

namespace Marsman.UmbracoCodeFirst.Core.Modules
{
    public class DocumentModelModule : ContentModelModuleBase<DocumentNodeDetails,IContentService,IContent>, IDocumentModelModule
    {
        private IDataTypeModule _dataTypeModule;
        private IDocumentTypeModule _documentTypeModule;

		public DocumentModelModule(IDataTypeModule dataTypeModule, IDocumentTypeModule documentTypeModule)
            : base(dataTypeModule, 
				  documentTypeModule, 
				  x => x.ContentType.Alias,
				  (x, type) => { if (type == SubscribeType.Subscribe) { ContentService.Trashing += x; } else { ContentService.Trashing -= x; } }, //trashing
				  (x, type) => { }, //deleting
				  (x, type) => { if (type == SubscribeType.Subscribe) { ContentService.Created += x; } else { ContentService.Created -= x; } }, //creating
				  (x, type) => { if (type == SubscribeType.Subscribe) { ContentService.Saving += x; } else { ContentService.Saving -= x; } }, //saving
				  (x, type) => { if (type == SubscribeType.Subscribe) { ContentService.Moving += x; } else { ContentService.Moving -= x; } }, //moving
				  (x, type) => { if (type == SubscribeType.Subscribe) { ContentService.Copying += x; } else { ContentService.Copying -= x; } }, //copying
				  (x, type) => { if (type == SubscribeType.Subscribe) { ContentService.Publishing += x; } else { ContentService.Publishing -= x; } }, //publishing
				  (x, type) => { if (type == SubscribeType.Subscribe) { ContentService.UnPublishing += x; } else { ContentService.UnPublishing -= x; } }) //unpublishing
        {
            _dataTypeModule = dataTypeModule;
            _documentTypeModule = documentTypeModule;
        }

        #region Convert Model to Content
        public bool TryConvertToContent<Tmodel>(Tmodel model, out IContent content, int parentId = -1) where Tmodel : DocumentTypeBase
        {
            try
            {
                content = ConvertToContent(model, parentId);
                return true;
            }
            catch (Exception ex)
            {
                content = null;
                return false;
            }
        }

        public IContent ConvertToContent(DocumentTypeBase model, int parentId = -1)
        {
            var contentId = model.NodeDetails.UmbracoId;
            DocumentTypeRegistration registration;
            if (!_documentTypeModule.TryGetDocumentType(model.GetType(), out registration))
            {
                throw new CodeFirstException("Document type not registered. Type: " + model.GetType());
            }

            //Create or update object
            if (contentId == -1)
            {
                return CreateContent(parentId, model, registration);
            }
            else
            {
                return UpdateContent(model, registration);
            }
        }

        /// <summary>
        /// <para>Creates an IContent populated with the current values of the model</para>
        /// </summary>
        private IContent CreateContent(int parentId, DocumentTypeBase model, ContentTypeRegistration registration)
        {
            //Get the type alias and create the content
            var typeAlias = registration.Alias;
            var node = ApplicationContext.Current.Services.ContentService.CreateContent(model.NodeDetails.Name, parentId, typeAlias);
            MapModelToContent(node, model, registration);
            return node;
        }

        /// <summary>
        /// Updates an existing IContent item with the current values of the model
        /// </summary>
        /// <returns></returns>
        private IContent UpdateContent(DocumentTypeBase model, ContentTypeRegistration registration)
        {
            if (model.NodeDetails == null || model.NodeDetails.UmbracoId == -1)
            {
                throw new ArgumentException("Can't update content for a model with no ID. Try calling CreateContent instead. Check that the NodeDetails.UmbracoId property is set before calling UpdateContent.");
            }
            var node = ApplicationContext.Current.Services.ContentService.GetById(model.NodeDetails.UmbracoId);
            MapModelToContent(node, model, registration);
            return node;
        }
        #endregion

        #region Convert Content To Model
        public bool TryConvertToModel<T>(IContent content, out T model) where T : DocumentTypeBase
        {
            //TODO move to derived
            DocumentTypeRegistration docType;
            if (_documentTypeModule.TryGetDocumentType(content.ContentType.Alias, out docType) && docType.ClrType == typeof(T))
            {
                try
                {
                    model = ConvertToModel<T>(content);
                    return true;
                }
                catch
                {
                    model = default(T);
                    return false;
                }
            }
            else
            {
                model = default(T);
                return false;
            }
        }

        /// <summary>
        /// Extension used to convert an IPublishedContent back to a Typed model instance.
        /// Your model does need to inherit from UmbracoGeneratedBase and contain the correct attributes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public T ConvertToModel<T>(IContent content, CodeFirstModelContext parentContext = null) where T : DocumentTypeBase
        {
            //TODO move to derived
            DocumentTypeRegistration registration;
            if (!_documentTypeModule.TryGetDocumentType(content.ContentType.Alias, out registration))
            {
                throw new CodeFirstException("Could not find document type registration for document type alias " + content.ContentType.Alias);
            }
            if (registration.ClrType != typeof(T))
            {
                if (registration.ClrType.Inherits(typeof(T)))
                {
                    //Redirect to the underlying type and make one of those instead
                    if (!_documentTypeModule.TryGetDocumentType(typeof(T), out registration))
                    {
                        throw new CodeFirstException("Could not find document type registration for underlying type " + typeof(T).FullName);
                    }
                }
                else
                {
                    throw new CodeFirstException("Registered type for document type " + content.ContentType.Alias + " is " + registration.ClrType.Name + ", not " + typeof(T).Name);
                }
            }

            T instance = (T)CreateInstanceFromContent(content, registration, parentContext);
            (instance as DocumentTypeBase).NodeDetails = new DocumentNodeDetails(content);
            return instance;
        }

        public bool TryConvertToModel<T>(IPublishedContent content, out T model) where T : DocumentTypeBase
        {
            return base.TryConvertToModelInternal(content, out model);
        }

        public T ConvertToModel<T>(IPublishedContent content, CodeFirstModelContext parentContext = null) where T : DocumentTypeBase
        {
            return base.ConvertToModelInternal<T>(content, parentContext);
        }
        #endregion

        public void ProjectModelToContent(DocumentTypeBase model, IContent content)
        {
            var type = model.GetType();
            DocumentTypeRegistration reg;
            if (_documentTypeModule.TryGetDocumentType(type, out reg) && (reg.ClrType == type || reg.ClrType.Inherits(type)))
            {
                MapModelToContent(content, model, reg);
            }
        }
    }

}

namespace Marsman.UmbracoCodeFirst.Extensions
{
    public static class DocumentModelModuleExtensions
    {
        public static void AddDefaultDocumentModelModule(this CodeFirstModuleResolver resolver)
        {
            resolver.RegisterModule<IDocumentModelModule>(new DocumentModelModuleFactory());
        }
    }
}