using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public interface IMediaTypeModule : IContentTypeModuleBase, ICodeFirstEntityModule
    {
        bool TryGetMediaType(string alias, out MediaTypeRegistration registration);
        bool TryGetMediaType(Type type, out MediaTypeRegistration registration);
        bool TryGetMediaType<T>(out MediaTypeRegistration registration) where T : MediaTypeBase;
		MediaTypeRegistration GetMediaType<T>() where T : MediaTypeBase;
		MediaTypeRegistration GetMediaType(Type type);
		MediaTypeRegistration GetMediaType(string alias);
	}
}
