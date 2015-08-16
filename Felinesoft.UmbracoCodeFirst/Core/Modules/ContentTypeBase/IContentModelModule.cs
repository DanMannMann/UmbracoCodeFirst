using Umbraco.Core.Models;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public interface IContentModelModule
    {
        object ConvertToModel(IPublishedContent content, CodeFirstModelContext parentContext = null);
    }
}