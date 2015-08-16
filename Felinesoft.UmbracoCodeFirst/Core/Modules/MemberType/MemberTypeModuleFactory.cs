using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using System;
using Umbraco.Core;
using System.Linq;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public class MemberTypeModuleFactory : ModuleFactoryBase<IMemberTypeModule, IPropertyModule>
    {
        public override System.Collections.Generic.IEnumerable<Type> GetAttributeTypesToFilterOn()
        {
            return new Type[] { typeof(MemberTypeAttribute), typeof(MemberGroupAttribute) };
        }

        public override IMemberTypeModule CreateInstance(IPropertyModule dependency)
        {
            return new MemberTypeModule(dependency, ApplicationContext.Current.Services.MemberTypeService, ApplicationContext.Current.Services.MemberService);
        }
    }
}