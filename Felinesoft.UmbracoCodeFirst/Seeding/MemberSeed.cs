using System;
using System.Linq;
using System.Collections.Generic;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Felinesoft.UmbracoCodeFirst.Seeding
{
	public sealed class MemberSeed : Seed<MemberTypeBase>
	{
		internal MemberSeed(string nodeName, MemberTypeBase member)
			: base(nodeName, member)
		{
		}
	}
}