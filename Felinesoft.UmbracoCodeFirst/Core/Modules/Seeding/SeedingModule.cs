using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Core.Modules;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Seeding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public class SeedingModule : ISeedingModule
	{
		private IDocumentModelModule _documentModule;
		private IMediaModelModule _mediaModule;
		private IMemberModelModule _memberModule;
		private IContentService _contentService;
		private IMediaService _mediaService;
		private IMemberService _memberService;

		public bool SkipChildrenIfParentExists { get; set; } = false;

		public SeedingModule(IDocumentModelModule documentModule, IMediaModelModule mediaModule, IMemberModelModule memberModule, IContentService contentService, IMediaService mediaService, IMemberService memberService)
        {
			_documentModule = documentModule;
			_mediaModule = mediaModule;
			_memberModule = memberModule;
			_contentService = contentService;
			_mediaService = mediaService;
			_memberService = memberService;
		}

        public void Initialise(IEnumerable<Type> classes)
        {
			var tuples = new List<Tuple<int, SeedFactoryAttribute, Seed>>();

			foreach(var type in classes)
			{
				var factory = (ISeedFactory<Seed>)Activator.CreateInstance(type);
				var attr = type.GetCodeFirstAttribute<SeedFactoryAttribute>();
				tuples.Add(new Tuple<int, SeedFactoryAttribute, Seed>(factory.SortOrder, attr, factory.GetSeed()));
			}

			foreach(var tuple in tuples.OrderBy(x => x.Item1))
			{
				if (tuple.Item3 is DocumentSeed)
				{
					SeedDocuments(tuple.Item3 as DocumentSeed, tuple.Item2.UserId, tuple.Item2.PublishOnCreate, tuple.Item2.RaiseEventsOnCreate);
				}
				else if (tuple.Item3 is MediaSeed)
				{
					SeedMedia(tuple.Item3 as MediaSeed, tuple.Item2.UserId, tuple.Item2.RaiseEventsOnCreate);
				}
				else if (tuple.Item3 is MemberSeed)
				{
					SeedMember(tuple.Item3 as MemberSeed, tuple.Item2.RaiseEventsOnCreate);
				}
				else
				{
					throw new CodeFirstException($"Unknown seed type '{tuple.Item3.GetType().Name}'");
				}
			}
		}

		public void SeedDocuments(DocumentSeed root, int userId = 0, bool publishOnCreate = false, bool raiseEventsOnCreate = false)
		{
			SeedDocuments(root, null, userId, publishOnCreate, raiseEventsOnCreate);
		}

		private void SeedDocuments(DocumentSeed document, DocumentSeed parent, int userId, bool publishOnCreate, bool raiseEventsOnCreate)
		{
			var content = (parent?.Content?.NodeDetails?.Content?.Children().Where(x => string.Equals(x.Name, document.NodeName, StringComparison.InvariantCultureIgnoreCase)) 
								?? 
						  _contentService.GetRootContent().Where(x => string.Equals(x.Name, document.NodeName, StringComparison.InvariantCultureIgnoreCase))).FirstOrDefault();
			var contentExisted = content == null;

			if (!contentExisted)
			{
				document.Content.NodeDetails.Name = document.NodeName;
				document.Content.Persist(parent?.Content?.NodeDetails?.UmbracoId ?? -1, userId, raiseEventsOnCreate, publishOnCreate);
				content = document.Content.NodeDetails.Content;
			}
			
			if (!SkipChildrenIfParentExists || !contentExisted)
			{
				foreach(var child in document.Children)
				{
					SeedDocuments(child, document, userId, publishOnCreate, raiseEventsOnCreate);
				}
			}
		}

		public void SeedMedia(MediaSeed root, int userId = 0, bool raiseEventsOnCreate = false)
		{
			SeedMedia(root, null, userId, raiseEventsOnCreate);
		}

		private void SeedMedia(MediaSeed media, MediaSeed parent, int userId, bool raiseEventsOnCreate)
		{
			var content = (parent?.Content?.NodeDetails?.Content?.Children().Where(x => string.Equals(x.Name, media.NodeName, StringComparison.InvariantCultureIgnoreCase))
								??
						  _mediaService.GetRootMedia().Where(x => string.Equals(x.Name, media.NodeName, StringComparison.InvariantCultureIgnoreCase))).FirstOrDefault();
			var contentExisted = content == null;

			if (!contentExisted)
			{
				media.Content.NodeDetails.Name = media.NodeName;
				media.Content.Persist(parent?.Content?.NodeDetails?.UmbracoId ?? -1, userId, raiseEventsOnCreate);
				content = media.Content.NodeDetails.Content;
			}

			if (!SkipChildrenIfParentExists || !contentExisted)
			{
				foreach (var child in media.Children)
				{
					SeedMedia(child, media, userId, raiseEventsOnCreate);
				}
			}
		}

		public void SeedMember(MemberSeed member, bool raiseEventsOnCreate = false)
		{
			var content = _memberService.GetByUsername(member.Content.Username);
			var contentExisted = content == null;

			if (!contentExisted)
			{
				member.Content.NodeDetails.Name = member.NodeName;
				member.Content.Persist(raiseEventsOnCreate);
				content = member.Content.NodeDetails.Content;
			}
		}
	}
}

namespace Felinesoft.UmbracoCodeFirst.Extensions
{
    public static class SeedingModuleExtensions
    {
        public static void AddDefaultSeedingModule(this CodeFirstModuleResolver resolver)
        {
            resolver.RegisterModule<ISeedingModule>(new SeedingModuleFactory());
        }
    }
}