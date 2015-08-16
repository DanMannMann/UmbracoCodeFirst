using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public interface IMemberTypeModule : IContentTypeModuleBase, ICodeFirstEntityModule
    {
        bool TryGetMemberType(string alias, out MemberTypeRegistration registration);
        bool TryGetMemberType(Type type, out MemberTypeRegistration registration);
        bool TryGetMemberType<T>(out MemberTypeRegistration registration) where T : MemberTypeBase;
    }
}
