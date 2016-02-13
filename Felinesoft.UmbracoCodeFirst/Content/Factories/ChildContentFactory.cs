using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using System.Reflection;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.InitialisableAttributes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Content.Interfaces;

namespace Felinesoft.UmbracoCodeFirst.Content.Factories
{
    /// <summary>
    /// Creates AutoContent which specifies a parent document
    /// </summary>
    public class ChildContentFactory : ContentFactoryBase
    {
        private IContentFactory _parentFactory;

        /// <summary>
        /// The parent document's IContent instance if it exists, otherwise null
        /// </summary>
        protected IContent Parent { get { return _parentFactory.GetIfExists(); } }

        /// <summary>
        /// The parent document's IPublishedContent instance if it exists, otherwise null
        /// </summary>
        protected IPublishedContent PublishedParent { get { return _parentFactory.GetPublishedIfExists(); } }

        /// <summary>
        /// Creates AutoContent which specifies a parent document
        /// </summary>
        /// <param name="values">The document property values to use</param>
        /// <param name="parentDocType">The type of the parent document</param>
        public ChildContentFactory(DocumentTypeBase values, Type parentDocType)
            : base(values)
        {
            if(parentDocType == null)
            {
                throw new ArgumentNullException("parentDocType", "ChildContentFactory requires a parent document type");
            }

            //Get the factory type of the parent type
            var attr = parentDocType.GetInitialisedAttribute<ContentFactoryAttribute>();
            if (attr == null)
            {
                throw new InvalidOperationException("Parent type '" + parentDocType.Name + "' specified for Umbraco content '" + values.NodeDetails.Name + "' does not have a [ContentFactory] or [AutoContent] attribute");
            }
            else
            {
                _parentFactory = attr.Factory;
            }         
        }

        /// <summary>
        /// Gets the IContent instance if it already exists, otherwise creates it
        /// </summary>
        /// <returns>the IContent instance</returns>
        public override Umbraco.Core.Models.IContent GetOrCreate()
        {
            if (Parent == null)
            {
                throw new InvalidOperationException("Parent node does not exist for " + Values.NodeDetails.Name + " (doc type alias: " + Values.GetDocumentTypeAlias() + ")");
            }
            return GetOrCreate(Parent.Id);
        }

        /// <summary>
        /// Gets the IContent instance if it exists, otherwise returns null
        /// </summary>
        /// <returns>the IContent instance if it exists, otherwise null</returns>
        public override Umbraco.Core.Models.IContent GetIfExists()
        {
            if (Parent == null)
            {
                return null;
            }
            else
            {
                return GetIfExists(Parent.Children());
            }
        }

        /// <summary>
        /// Gets the IPublishedContent instance if it exists, otherwise returns null
        /// </summary>
        /// <returns>the IPublishedContent instance if it exists, otherwise null</returns>
        public override IPublishedContent GetPublishedIfExists()
        {
            if (PublishedParent == null)
            {
                return null;
            }
            else
            {
                return GetPublishedIfExists(PublishedParent.Children);
            }
        }
    }
}