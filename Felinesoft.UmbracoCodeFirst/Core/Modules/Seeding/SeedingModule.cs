using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Marsman.UmbracoCodeFirst.Core.Modules;
using Marsman.UmbracoCodeFirst.Core.Resolver;
using Marsman.UmbracoCodeFirst.Exceptions;
using Marsman.UmbracoCodeFirst.Extensions;
using Marsman.UmbracoCodeFirst.Seeding;
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

namespace Marsman.UmbracoCodeFirst.Core.Modules
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
			var dict = new Dictionary<SeedFactoryAttribute, Seed>();

			foreach(var type in classes)
			{
				var factory = (ISeedFactory<Seed>)Activator.CreateInstance(type);
				var attr = type.GetCodeFirstAttribute<SeedFactoryAttribute>();
				dict.Add(attr, factory.GetSeed());
			}

			using (new HttpContextFaker(System.Web.HttpContext.Current)) //Will preserve the current context if there is one, otherwise it makes a fake one
			{
				//Disable content events during seeding
				var previous = CodeFirstManager.Current.Features.EnableContentEvents;
				CodeFirstManager.Current.Features.EnableContentEvents = false;

				foreach (var tuple in dict.OrderBy(x => x.Key.SortOrder))
				{
					if (tuple.Value is DocumentSeed)
					{
						SeedDocuments(tuple.Value as DocumentSeed, null, tuple.Key.UserId, tuple.Key.PublishOnCreate, tuple.Key.RaiseEventsOnCreate);
					}
					else if (tuple.Value is MediaSeed)
					{
						SeedMedia(tuple.Value as MediaSeed, null, tuple.Key.UserId, tuple.Key.RaiseEventsOnCreate);
					}
					else if (tuple.Value is MemberSeed)
					{
						SeedMember(tuple.Value as MemberSeed, tuple.Key.RaiseEventsOnCreate);
					}
					else
					{
						throw new CodeFirstException($"Unknown seed type '{tuple.Value.GetType().Name}'");
					}
				}

				CodeFirstManager.Current.Features.EnableContentEvents = previous;
			}
		}

		public void SeedDocuments(DocumentSeed document, IContent parent = null, int userId = 0, bool publishOnCreate = false, bool raiseEventsOnCreate = false)
		{
			var content = (parent?.Children().Where(x => string.Equals(x.Name, document.NodeName, StringComparison.InvariantCultureIgnoreCase)) 
								?? 
						  _contentService.GetRootContent().Where(x => string.Equals(x.Name, document.NodeName, StringComparison.InvariantCultureIgnoreCase))).FirstOrDefault();
			var contentExisted = content != null;

			if (!contentExisted)
			{
				document.Content.NodeDetails.Name = document.NodeName;
				document.Content.Persist(parent?.Id ?? -1, userId, raiseEventsOnCreate, publishOnCreate);
				content = document.Content.NodeDetails.Content;
			}
			
			if (!SkipChildrenIfParentExists || !contentExisted)
			{
				foreach(var child in document.Children)
				{
					SeedDocuments(child, content, userId, publishOnCreate, raiseEventsOnCreate);
				}
			}
		}

		public void SeedMedia(MediaSeed media, IMedia parent = null, int userId = 0, bool raiseEventsOnCreate = false)
		{
			var content = (parent?.Children().Where(x => string.Equals(x.Name, media.NodeName, StringComparison.InvariantCultureIgnoreCase))
								??
						  _mediaService.GetRootMedia().Where(x => string.Equals(x.Name, media.NodeName, StringComparison.InvariantCultureIgnoreCase))).FirstOrDefault();
			var contentExisted = content == null;

			if (!contentExisted)
			{
				media.Content.NodeDetails.Name = media.NodeName;
				media.Content.Persist(parent?.Id ?? -1, userId, raiseEventsOnCreate);
				content = media.Content.NodeDetails.Content;
			}

			if (!SkipChildrenIfParentExists || !contentExisted)
			{
				foreach (var child in media.Children)
				{
					SeedMedia(child, content, userId, raiseEventsOnCreate);
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

namespace Marsman.UmbracoCodeFirst.Extensions
{
    public static class SeedingModuleExtensions
    {
        public static void AddDefaultSeedingModule(this CodeFirstModuleResolver resolver)
        {
            resolver.RegisterModule<ISeedingModule>(new SeedingModuleFactory());
        }
    }
}