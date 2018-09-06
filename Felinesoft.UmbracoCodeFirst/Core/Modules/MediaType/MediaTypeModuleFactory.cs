using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.Core.Resolver;
using System;
using Umbraco.Core;
using System.Linq;

namespace Marsman.UmbracoCodeFirst.Core.Modules
{
    public class MediaTypeModuleFactory : ModuleFactoryBase<IMediaTypeModule,IPropertyModule>
    {
        public override System.Collections.Generic.IEnumerable<Type> GetAttributeTypesToFilterOn()
        {
            return new Type[] { typeof(MediaTypeAttribute) };
        }

        public override IMediaTypeModule CreateInstance(IPropertyModule dependency)
        {
            return new MediaTypeModule(dependency, ApplicationContext.Current.Services.ContentTypeService);
        }
    }
}