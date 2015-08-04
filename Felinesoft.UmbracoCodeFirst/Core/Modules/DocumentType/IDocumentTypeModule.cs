using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public interface IDocumentTypeModule : IContentTypeModuleBase, ICodeFirstEntityModule
    {
        bool TryGetDocumentType(string alias, out DocumentTypeRegistration registration);
        bool TryGetDocumentType(Type type, out DocumentTypeRegistration registration);
        bool TryGetDocumentType<T>(out DocumentTypeRegistration registration) where T : DocumentTypeBase;
    }
}
