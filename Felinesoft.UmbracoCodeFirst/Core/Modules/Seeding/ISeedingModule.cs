using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Felinesoft.UmbracoCodeFirst.Seeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public interface ISeedingModule : ICodeFirstEntityModule
    {
		void SeedDocuments(DocumentSeed root, int userId = 0, bool publishOnCreate = false, bool raiseEventsOnCreate = false);

		void SeedMedia(MediaSeed root, int userId = 0, bool raiseEventsOnCreate = false);

		void SeedMember(MemberSeed member, bool raiseEventsOnCreate = false);
	}

	public interface ISeedFactory<out T> where T : Seed
	{
		T GetSeed();
		int SortOrder { get; }
	}

	public class test : ISeedFactory<DocumentSeed>
	{
		public int SortOrder
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public DocumentSeed GetSeed()
		{
			throw new NotImplementedException();
		}
	}
}
