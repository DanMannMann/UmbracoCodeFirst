using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Felinesoft.InitialisableAttributes;
using Umbraco.Core;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Content.Interfaces;

namespace Felinesoft.UmbracoCodeFirst.Content.Factories
{
    internal class DefaultContentManager : IDefaultContentManager
    {
        public IContent GetOrCreateDefaultContent<T>() where T : DocumentTypeBase
        {
            return GetOrCreateDefaultContent(typeof(T));
        }

        public IContent GetOrCreateDefaultContent(Type docType)
        {
            //Create default content using the registered factory
            var attr = docType.GetInitialisedAttribute<ContentFactoryAttribute>();
            if (attr == null)
            {
                return null;
            }

            var content = attr.Factory.GetOrCreate();
            return content;
        }

        public IContent CreateDefaultContent(Type docType)
        {
            //Create default content using all registered factories
            var attr = docType.GetInitialisedAttribute<ContentFactoryAttribute>();
            if (attr == null)
            {
                return null;
            }

            var content = GetOrCreateDefaultContent(docType);
            if (content != null)
            {
                content.SortOrder = attr.SortOrder;
                var attempt = ApplicationContext.Current.Services.ContentService.SaveAndPublishWithStatus(content, 0, false);
            }
            return content;
        }
    }
}
