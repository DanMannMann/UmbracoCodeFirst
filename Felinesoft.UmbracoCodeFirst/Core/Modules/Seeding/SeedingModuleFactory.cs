using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.Core.Resolver;
using System;
using Umbraco.Core;
using Umbraco.Core.Services;

namespace Marsman.UmbracoCodeFirst.Core.Modules
{
    public class SeedingModuleFactory : ModuleFactoryBase<ISeedingModule, IDocumentModelModule, IMediaModelModule, IMemberModelModule>
    {
		public override ISeedingModule CreateInstance(IDocumentModelModule documentModule, IMediaModelModule mediaModule, IMemberModelModule memberModule)
        {
			ServiceContext _svc = ApplicationContext.Current.Services;
			return new SeedingModule(documentModule, mediaModule, memberModule, _svc.ContentService, _svc.MediaService, _svc.MemberService);
        }

        public override System.Collections.Generic.IEnumerable<Type> GetAttributeTypesToFilterOn()
        {
            return new Type[] { typeof(SeedFactoryAttribute) };
        }
    }
}