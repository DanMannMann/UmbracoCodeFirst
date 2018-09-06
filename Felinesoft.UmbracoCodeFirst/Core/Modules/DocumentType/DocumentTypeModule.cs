using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Marsman.UmbracoCodeFirst.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

using Marsman.UmbracoCodeFirst.Extensions;
using System.Collections.Concurrent;
using Umbraco.Web.Mvc;
using System.Collections.ObjectModel;
using Marsman.UmbracoCodeFirst.Core.Resolver;
using Marsman.UmbracoCodeFirst.Core.Modules;
using Umbraco.Web.Models.Trees;
using Marsman.UmbracoCodeFirst.Diagnostics;
using System.IO;
using Marsman.UmbracoCodeFirst.Core.ClassFileGeneration;
using Marsman.UmbracoCodeFirst.Core.Modules.ContentTypeBase.T4;

namespace Marsman.UmbracoCodeFirst.Core.Modules
{
    public class DocumentTypeModule : ContentTypeModuleBase, IDocumentTypeModule, IClassFileGenerator
    {
        private IPropertyModule _propertyModule;
        private IContentTypeService _service;

        public DocumentTypeModule(IPropertyModule propertyModule, IContentTypeService service)
            : base(propertyModule, Timing.DocTypeModuleTimer)
        {
            _service = service;
            _propertyModule = propertyModule;
        }

        #region IDocumentTypeModule
        public bool TryGetDocumentType(string alias, out DocumentTypeRegistration registration)
        {
            ContentTypeRegistration reg;
            if (ContentTypeRegister.TryGetContentType(alias, out reg))
            {
                registration = reg as DocumentTypeRegistration;
                return registration != null;
            }
            else
            {
                registration = null;
                return false;
            }
        }

        public bool TryGetDocumentType(Type type, out DocumentTypeRegistration registration)
        {
            ContentTypeRegistration reg;
            if (ContentTypeRegister.TryGetContentType(type, out reg))
            {
                registration = reg as DocumentTypeRegistration;
                return registration != null;
            }
            else
            {
                registration = null;
                return false;
            }
        }

        public bool TryGetDocumentType<T>(out DocumentTypeRegistration registration) where T : DocumentTypeBase
        {
            return TryGetDocumentType(typeof(T), out registration);
        }
		public DocumentTypeRegistration GetDocumentType<T>() where T : DocumentTypeBase
		{
			return ContentTypeRegister.Registrations.FirstOrDefault(x => x.ClrType == typeof(T)) as DocumentTypeRegistration;
		}
		public DocumentTypeRegistration GetDocumentType(Type type)
		{
			return ContentTypeRegister.Registrations.FirstOrDefault(x => x.ClrType == type) as DocumentTypeRegistration;
		}
		public DocumentTypeRegistration GetDocumentType(string alias)
		{
			return ContentTypeRegister.Registrations.FirstOrDefault(x => x.Alias == alias) as DocumentTypeRegistration;
		}
		#endregion

		#region IEntityTreeFilter
		public override bool IsFilter(string treeAlias)
        {
			//old = nodeTypes, new = documentTypes
			return treeAlias.Equals("documentTypes", StringComparison.InvariantCultureIgnoreCase) || treeAlias.Equals("nodeTypes", StringComparison.InvariantCultureIgnoreCase);
        }
        #endregion

        #region IClassFileGenerator
        public void GenerateClassFiles(string nameSpace, string folderPath)
        {
            string dataDirectory = Path.Combine(folderPath, "DocumentTypes");

            if (!System.IO.Directory.Exists(dataDirectory))
            {
                System.IO.Directory.CreateDirectory(dataDirectory);
            }

            GenerateContentTypes(nameSpace, dataDirectory, "DocumentType", () => ApplicationContext.Current.Services.ContentTypeService.GetAllContentTypes(), (x, y) =>
                {
                    ConfigureTemplates(x as IContentType, y);
                    if (x.ParentId == -1)
                    {
                        y.ParentAlias = "DocumentTypeBase";
                        y.ParentClassName = "DocumentTypeBase";
                    }
                    else
                    {
                        var parent = ApplicationContext.Current.Services.ContentTypeService.GetContentType(x.ParentId);
                        y.ParentAlias = parent == null ? "DocumentTypeBase" : parent.Alias;
                        y.ParentClassName = parent == null ? "DocumentTypeBase" : TypeGeneratorUtils.GetFormattedMemberName(parent.Alias);
                    }
                });
        }

        private void ConfigureTemplates(IContentType node, ContentTypeDescription type)
        {
            type.Templates = new List<TemplateDescription>();
            foreach (var template in node.AllowedTemplates)
            {
                var templateModel = new TemplateDescription();
                templateModel.Alias = template.Alias;
                templateModel.Name = template.Name;
                templateModel.IsDefault = template == node.DefaultTemplate ? "true" : "false";
                type.Templates.Add(templateModel);
            }
        }
        #endregion

        #region Content Type Service Adapter
        protected override IEnumerable<IContentTypeBase> GetAllContentTypes()
        {
            return _service.GetAllContentTypes();
        }

        protected override void SaveContentType(IContentTypeBase contentType)
        {
            _service.Save((IContentType)contentType);
        }

        protected override void DeleteContentType(IContentTypeBase contentType)
        {
            _service.Delete((IContentType)contentType);
        }

        protected override IContentTypeComposition GetContentTypeByAlias(string alias)
        {
            return _service.GetContentType(alias);
        }

        protected override IContentTypeComposition CreateContentType(IContentTypeBase parent)
        {
            return parent == null ? new ContentType(-1) : new ContentType((IContentType)parent);
        }

        protected override ContentTypeRegistration CreateRegistration(Type type)
        {
            var documentTypeAttribute = type.GetCodeFirstAttribute<DocumentTypeAttribute>();
            if (documentTypeAttribute != null)
            {
                var props = new List<PropertyRegistration>();
                var tabs = new List<TabRegistration>();
                var comps = new List<ContentTypeCompositionRegistration>();
                DocumentTypeRegistration registration = new DocumentTypeRegistration(
                                                                                props,
                                                                                tabs,
                                                                                comps,
                                                                                documentTypeAttribute.Alias,
                                                                                documentTypeAttribute.Name,
                                                                                type,
                                                                                documentTypeAttribute);

                return registration;
            }
            else
            {
                throw new CodeFirstException("The specified type does not have a DocumentTypeAttribute. Type: " + type.Name);
            }
        }

        protected override IEnumerable<IContentTypeBase> GetChildren(IContentTypeBase contentType)
        {
            return _service.GetContentTypeChildren(contentType.Id);
        }
		#endregion
	}
}

namespace Marsman.UmbracoCodeFirst.Extensions
{
    public static class DocumentTypeModuleExtensions
    {
        public static void AddDefaultDocumentTypeModule(this CodeFirstModuleResolver resolver)
        {
            resolver.RegisterModule<IDocumentTypeModule>(new DocumentTypeModuleFactory());
        }
    }
}
