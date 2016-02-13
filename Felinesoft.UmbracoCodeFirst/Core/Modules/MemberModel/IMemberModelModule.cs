using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using Umbraco.Core.Models;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public interface IMemberModelModule : ICodeFirstEntityModule, IContentModelModule
    {
        bool TryConvertToModel<T>(IPublishedContent content, out T model) where T : MemberTypeBase;

        bool TryConvertToModel<T>(IMember member, out T model) where T : MemberTypeBase;

        T ConvertToModel<T>(IMember member, CodeFirstModelContext parentContext = null) where T : MemberTypeBase;

        T ConvertToModel<T>(IPublishedContent content, CodeFirstModelContext parentContext = null) where T : MemberTypeBase;

        bool TryConvertToContent<T>(T model, out IMember content) where T : MemberTypeBase;

        IMember ConvertToContent(MemberTypeBase model);

        void ProjectModelToContent(MemberTypeBase model, IMember content);
    }
}