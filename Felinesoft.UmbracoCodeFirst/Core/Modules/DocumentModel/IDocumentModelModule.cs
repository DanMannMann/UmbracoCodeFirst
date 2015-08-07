using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public interface IDocumentModelModule : ICodeFirstEntityModule, IContentModelModule
    {
        bool TryConvertToModel<T>(IPublishedContent content, out T model) where T : DocumentTypeBase;

        bool TryConvertToModel<T>(IContent content, out T model) where T : DocumentTypeBase;

        T ConvertToModel<T>(IContent content, CodeFirstModelContext parentContext = null) where T : DocumentTypeBase;

        T ConvertToModel<T>(IPublishedContent content, CodeFirstModelContext parentContext = null) where T : DocumentTypeBase;

        bool TryConvertToContent<T>(T model, out IContent content, int parentId = -1) where T : DocumentTypeBase;

        IContent ConvertToContent(DocumentTypeBase model, int parentId = -1);

        void ProjectModelToContent(DocumentTypeBase model, IContent content);
    }

}
