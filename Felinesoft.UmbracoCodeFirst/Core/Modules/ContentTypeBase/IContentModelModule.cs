using Umbraco.Core.Models;

namespace Marsman.UmbracoCodeFirst.Core.Modules
{
    public interface IContentModelModule
    {
        object ConvertToModel(IPublishedContent content, CodeFirstModelContext parentContext = null);
    }
}