using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System;
using System.Collections.Generic;
using Umbraco.Core;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public class TemplateModuleFactory : ModuleFactoryBase<ITemplateModule, IDocumentTypeModule>
    {
        public override IEnumerable<Type> GetAttributeTypesToFilterOn()
        {
            return new Type[] { typeof(TemplateAttribute) };
        }

        public override ITemplateModule CreateInstance(IDocumentTypeModule documentTypeModule)
        {
            return new TemplateModule(documentTypeModule, ApplicationContext.Current.Services.FileService, ApplicationContext.Current.Services.ContentTypeService);
        }
    }
}