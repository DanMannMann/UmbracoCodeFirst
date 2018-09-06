using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.Core.Resolver;
using System;
using Umbraco.Core;

namespace Marsman.UmbracoCodeFirst.Core.Modules
{
    public class DataTypeModuleFactory : ModuleFactoryBase<IDataTypeModule>
    {
        public override System.Collections.Generic.IEnumerable<Type> GetAttributeTypesToFilterOn()
        {
            return new Type[] { typeof(DataTypeAttribute) };
        }

        public override IDataTypeModule CreateInstance(System.Collections.Generic.IEnumerable<ICodeFirstEntityModule> dependencies)
        {
            return new DataTypeModule(ApplicationContext.Current.Services.DataTypeService);
        }
    }
}