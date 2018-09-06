using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.Core.Resolver;
using System;

namespace Marsman.UmbracoCodeFirst.Core.Modules
{
    public class DocumentModelModuleFactory : ModuleFactoryBase<IDocumentModelModule, IDataTypeModule, IDocumentTypeModule>
    {
        public override System.Collections.Generic.IEnumerable<Type> GetAttributeTypesToFilterOn()
        {
            return new Type[] { typeof(DocumentTypeAttribute) };
        }

        public override IDocumentModelModule CreateInstance(IDataTypeModule dataTypeModule, IDocumentTypeModule documentTypeModule)
        {
            return new DocumentModelModule(dataTypeModule, documentTypeModule);
        }
    }

}