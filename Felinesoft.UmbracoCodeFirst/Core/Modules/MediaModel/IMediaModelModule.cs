using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using Umbraco.Core.Models;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public interface IMediaModelModule : ICodeFirstEntityModule, IContentModelModule
    {
        bool TryConvertToModel<T>(IPublishedContent content, out T model) where T : MediaTypeBase;

        bool TryConvertToModel<T>(IMedia media, out T model) where T : MediaTypeBase;

        T ConvertToModel<T>(IMedia media, CodeFirstModelContext parentContext = null) where T : MediaTypeBase;

        T ConvertToModel<T>(IPublishedContent content, CodeFirstModelContext parentContext = null) where T : MediaTypeBase;

        bool TryConvertToContent<T>(T model, out IMedia content, int parentId = -1) where T : MediaTypeBase;

        IMedia ConvertToContent(MediaTypeBase model, int parentId = -1);

        void ProjectModelToContent(MediaTypeBase model, IMedia content);
    }
}