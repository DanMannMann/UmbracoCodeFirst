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

	public abstract class Seed
	{
		public static DocumentSeed Document<T>(string nodeName, T document, params DocumentSeed[] children) where T : DocumentTypeBase
		{
			return new DocumentSeed(nodeName, document, children);
		}

		public static DocumentSeed Document<T>(string nodeName, T document, IEnumerable<DocumentSeed> children) where T : DocumentTypeBase
		{
			return new DocumentSeed(nodeName, document, children);
		}

		public static MediaSeed Media<T>(string nodeName, T media, params MediaSeed[] children) where T : MediaTypeBase
		{
			return new MediaSeed(nodeName, media, children);
		}

		public static MediaSeed Media<T>(string nodeName, T media, IEnumerable<MediaSeed> children) where T : MediaTypeBase
		{
			return new MediaSeed(nodeName, media, children);
		}

		public static MemberSeed Member<T>(string nodeName, T member) where T : MemberTypeBase
		{
			return new MemberSeed(nodeName, member);
		}
	}

}