using Marsman.UmbracoCodeFirst.ContentTypes;
using Marsman.UmbracoCodeFirst.Core.Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marsman.UmbracoCodeFirst.Core.Modules
{
    public interface IMemberTypeModule : IContentTypeModuleBase, ICodeFirstEntityModule
    {
        bool TryGetMemberType(string alias, out MemberTypeRegistration registration);
        bool TryGetMemberType(Type type, out MemberTypeRegistration registration);
        bool TryGetMemberType<T>(out MemberTypeRegistration registration) where T : MemberTypeBase;
		MemberTypeRegistration GetMemberType<T>() where T : MemberTypeBase;
		MemberTypeRegistration GetMemberType(Type type);
		MemberTypeRegistration GetMemberType(string alias);
	}
}
