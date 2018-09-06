using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.Core.Resolver;
using System;

namespace Marsman.UmbracoCodeFirst.Core.Modules
{
    public class MemberModelModuleFactory : ModuleFactoryBase<IMemberModelModule, IDataTypeModule, IMemberTypeModule>
    {
        public override System.Collections.Generic.IEnumerable<Type> GetAttributeTypesToFilterOn()
        {
            return new Type[] { typeof(MemberTypeAttribute) };
        }

        public override IMemberModelModule CreateInstance(IDataTypeModule dataTypeModule, IMemberTypeModule memberTypeModule)
        {
            return new MemberModelModule(dataTypeModule, memberTypeModule);
        }
    }
}