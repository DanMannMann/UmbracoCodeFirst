using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using System;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public class DocumentModelModuleFactory : ModuleFactoryBase<IDocumentModelModule, IDataTypeModule, IDocumentTypeModule>
    {
        public override System.Collections.Generic.IEnumerable<Type> GetAttributeTypesToFilterOn()
        {
            return new Type[] { };
        }

        public override IDocumentModelModule CreateInstance(IDataTypeModule dataTypeModule, IDocumentTypeModule documentTypeModule)
        {
            return new DocumentModelModule(dataTypeModule, documentTypeModule);
        }
    }

}