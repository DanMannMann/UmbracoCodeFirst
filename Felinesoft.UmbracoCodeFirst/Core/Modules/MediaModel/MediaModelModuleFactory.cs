using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using System;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public class MediaModelModuleFactory : ModuleFactoryBase<IMediaModelModule, IDataTypeModule, IMediaTypeModule>
    {
        public override System.Collections.Generic.IEnumerable<Type> GetAttributeTypesToFilterOn()
        {
            return new Type[] { typeof(MediaTypeAttribute) };
        }

        public override IMediaModelModule CreateInstance(IDataTypeModule dataTypeModule, IMediaTypeModule mediaTypeModule)
        {
            return new MediaModelModule(dataTypeModule, mediaTypeModule);
        }
    }
}