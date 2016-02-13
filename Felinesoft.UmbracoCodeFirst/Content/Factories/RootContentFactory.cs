
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using Umbraco.Web;

namespace Felinesoft.UmbracoCodeFirst.Content.Factories
{
    /// <summary>
    /// Creates AutoContent which does not specify a parent document
    /// </summary>
    public class RootContentFactory : ContentFactoryBase
    {
        /// <summary>
        /// Creates AutoContent which does not specify a parent document
        /// </summary>
        /// <param name="values">The document property values to use</param>
        public RootContentFactory(DocumentTypeBase values)
            : base(values)
        { }

        /// <summary>
        /// Gets the IContent instance if it already exists, otherwise creates it
        /// </summary>
        /// <returns>the IContent instance</returns>
        public override Umbraco.Core.Models.IContent GetOrCreate()
        {
            return GetOrCreate(-1);
        }

        /// <summary>
        /// Gets the IContent instance if it exists, otherwise returns null
        /// </summary>
        /// <returns>the IContent instance if it exists, otherwise null</returns>
        public override Umbraco.Core.Models.IContent GetIfExists()
        {
            return GetIfExists(ApplicationContext.Current.Services.ContentService.GetRootContent());
        }

        /// <summary>
        /// Gets the IPublishedContent instance if it exists, otherwise returns null
        /// </summary>
        /// <returns>the IPublishedContent instance if it exists, otherwise null</returns>
        public override IPublishedContent GetPublishedIfExists()
        {
            var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            return GetPublishedIfExists(umbracoHelper.TypedContentAtRoot());
        }
    }
}