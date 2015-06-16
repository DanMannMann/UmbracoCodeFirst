using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Content.Interfaces;

namespace Felinesoft.UmbracoCodeFirst.Content.Factories
{
    /// <summary>
    /// <para>Creates an instance of a document type using values supplied as a strongly-typed model.</para>
    /// <para>The RootContentFactory and ChildContentFactory[T] implementations work with the IAutoContent interface
    /// and the [AutoContent] attribute to place the content creation logic into methods on the code-first document type model.
    /// The recommended route to content generation is to implement IAutoContent on the document type model and apply
    /// an [AutoContent] attribute to the document type model.</para>
    /// <para>Implementing a custom factory may be required for some scenarios such as creating a collection
    /// of content items or creating a nested structure in a single factory. A custom factory type can be
    /// specified using a [ContentFactory] attribute.</para>
    /// </summary>
    public abstract class ContentFactoryBase : IContentFactory
    {
        /// <summary>
        /// The values to use to create the document
        /// </summary>
        protected DocumentTypeBase Values { get; private set; }

        /// <summary>
        /// <para>Creates an instance of a document type using values supplied as a strongly-typed model.</para>
        /// <para>The RootContentFactory and ChildContentFactory[T] implementations work with the IAutoContent interface
        /// and the [AutoContent] attribute to place the content creation logic into methods on the code-first document type model.
        /// The recommended approach for content creation is to implement IAutoContent on the document type model and apply
        /// an [AutoContent] attribute to the document type model.</para>
        /// <para>Implementing a custom factory may be required for some scenarios such as creating a collection
        /// of content items or creating a nested structure in a single factory. A custom factory type can be
        /// specified using a [ContentFactory] attribute.</para>
        /// </summary>
        /// <param name="values">The values to use to create the document</param>
        protected ContentFactoryBase(DocumentTypeBase values)
        {
            Values = values;
        }

        /// <summary>
        /// Gets the IContent instance if it already exists, otherwise creates it
        /// </summary>
        /// <returns>the IContent instance</returns>
        public abstract Umbraco.Core.Models.IContent GetOrCreate();

        /// <summary>
        /// Gets the IContent instance if it exists, otherwise returns null
        /// </summary>
        /// <returns>the IContent instance if it exists, otherwise null</returns>
        public abstract Umbraco.Core.Models.IContent GetIfExists();

        /// <summary>
        /// Gets the IPublishedContent instance if it exists, otherwise returns null
        /// </summary>
        /// <returns>the IPublishedContent instance if it exists, otherwise null</returns>
        public abstract IPublishedContent GetPublishedIfExists();

        /// <summary>
        /// Gets the IContent instance if it already exists, otherwise creates it
        /// </summary>
        /// <param name="parentId">The ID of the parent node</param>
        /// <returns>the IContent instance</returns>
        protected Umbraco.Core.Models.IContent GetOrCreate(int parentId)
        {
            var result = GetIfExists();
            if (result == null)
            {
                //No node. Create it.
                result = Values.CreateContent(parentId);
            }
            return result;
        }

        /// <summary>
        /// Gets the IContent instance if it exists, otherwise returns null
        /// </summary>
        /// <param name="rootContent">The collection of content to search for the existing node.</param>
        /// <returns>the IContent instance if it exists, otherwise null</returns>
        protected Umbraco.Core.Models.IContent GetIfExists(IEnumerable<IContent> rootContent)
        {
            return rootContent.FirstOrDefault(x => x.ContentType.Alias == Values.GetDocumentTypeAlias() && x.Name == Values.NodeDetails.Name);
        }

        /// <summary>
        /// Gets the IPublishedContent instance if it exists, otherwise returns null
        /// </summary>
        /// <param name="rootContent">The collection of content to search for the existing node.</param>
        /// <returns>the IPublishedContent instance if it exists, otherwise null</returns>
        protected IPublishedContent GetPublishedIfExists(IEnumerable<IPublishedContent> rootContent)
        {
            return rootContent.FirstOrDefault(x => x.ContentType.Alias == Values.GetDocumentTypeAlias() && x.Name == Values.NodeDetails.Name);
        }
    }
}