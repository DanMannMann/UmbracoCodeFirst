using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using Umbraco.Core;
using Umbraco.Core.Models;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using Felinesoft.UmbracoCodeFirst.Core.Modules;
using System.Reflection;
using Umbraco.Core.Services;
using Felinesoft.UmbracoCodeFirst.Exceptions;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public class TemplateModule : ITemplateModule
    {
        private IDocumentTypeModule _documentTypeModule;
        private IFileService _fileService;
        private IContentTypeService _contentTypeService;

        public TemplateModule(IDocumentTypeModule documentTypeModule, IFileService fileService, IContentTypeService contentTypeService)
        {
            _fileService = fileService;
            _contentTypeService = contentTypeService;
            _documentTypeModule = documentTypeModule;
        }
        public void Initialise(IEnumerable<Type> classes)
        {
            foreach (var t in classes)
            {
                RegisterTemplates(t);
            }
        }

        private object _lock = new object();

        /// <summary>
        /// Registers the specified templates for the given doctype. Creates a basic default cshtml file if none exists at the specified path.
        /// </summary>
        public void RegisterTemplates(Type docType)
        {
            if (CodeFirstManager.Current.Features.InitialisationMode == InitialisationMode.Passive)
            {
                return;
            }

            DocumentTypeRegistration reg;
            if (_documentTypeModule.TryGetDocumentType(docType, out reg))
            {
                var attributes = docType.GetCodeFirstAttributes<TemplateAttribute>();
                var type = _contentTypeService.GetContentType(reg.Alias);
                List<ITemplate> templateList = new List<ITemplate>();
                ITemplate defaultTemplate = null;

                foreach (var attribute in attributes)
                {
                    if (templateList.Any(x => x.Alias == attribute.TemplateAlias))
                    {
                        throw new CodeFirstException("Duplicate template aliases specified on " + docType.FullName);
                    }
                    var template = ConfigureTemplate(docType, ref defaultTemplate, attribute);
                    templateList.Add(template);
                }

                if (defaultTemplate != null)
                {
                    type.SetDefaultTemplate(defaultTemplate);
                }
                type.AllowedTemplates = templateList;
                _contentTypeService.Save(type);
            }
            else
            {
                throw new CodeFirstException(docType.Name + " is not a registered document type. [Template] can only be applied to document types.");
            }
        }

        private ITemplate ConfigureTemplate(Type docType, ref ITemplate defaultTemplate, TemplateAttribute attribute)
        {
            var template = _fileService.GetTemplates().FirstOrDefault(x => x.Alias == attribute.TemplateAlias);
            if (template == null)
            {
                template = CreateTemplate(attribute);
            }
            if (attribute.IsDefault)
            {
                if (defaultTemplate == null)
                {
                    defaultTemplate = template;
                }
                else
                {
                    throw new CodeFirstException("More than one default template specified for " + docType.FullName);
                }
            }
            if (attribute.TemplateName != template.Name)
            {
                var t = new umbraco.cms.businesslogic.template.Template(template.Id);
                t.Text = attribute.TemplateName;
                t.Save(); 
                template = _fileService.GetTemplates().FirstOrDefault(x => x.Alias == attribute.TemplateAlias); //re-get the template to pick up the changes
            }
            return template;
        }

        private ITemplate CreateTemplate(TemplateAttribute attribute)
        {
            var path = "~/Views/" + attribute.TemplateAlias + ".cshtml";
            var template = new Template(path, attribute.TemplateName, attribute.TemplateAlias);
            lock (_lock)
            {
                if (System.IO.File.Exists(HostingEnvironment.MapPath(path)))
                {
                    //get the existing content from the file so it isn't overwritten when we save the template.
                    template.Content = System.IO.File.ReadAllText(HostingEnvironment.MapPath(path));
                }
                else
                {
                    //TODO get this from a file resource containing a default view
                    var content = "@inherits Felinesoft.UmbracoCodeFirst.Views.UmbracoDocumentViewPage<" + attribute.DecoratedTypeFullName + ">" + Environment.NewLine + Environment.NewLine;
                    template.Content = content;
                }

                _fileService.SaveTemplate(template);
            }
            return template;
        }
    }

}

namespace Felinesoft.UmbracoCodeFirst.Extensions
{
    public static class TemplateModuleExtensions
    {
        public static void AddDefaultTemplateModule(this CodeFirstModuleResolver resolver)
        {
            resolver.RegisterModule<ITemplateModule>(new TemplateModuleFactory());
        }
    }
}