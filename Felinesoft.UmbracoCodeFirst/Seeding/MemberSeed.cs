using System;
using System.Linq;
using System.Collections.Generic;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Marsman.UmbracoCodeFirst.Core.Resolver;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Marsman.UmbracoCodeFirst.Seeding
{
	public sealed class MemberSeed : Seed<MemberTypeBase>
	{
		internal MemberSeed(string nodeName, MemberTypeBase member)
			: base(nodeName, member)
		{
		}
	}
}