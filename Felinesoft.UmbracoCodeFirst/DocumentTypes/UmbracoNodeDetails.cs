using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Felinesoft.UmbracoCodeFirst.DocumentTypes
{
    /// <summary>
    /// Represents the details of an Umbraco content node
    /// </summary>
    public class UmbracoNodeDetails
    {
        /// <summary>
        /// The name of the node
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The alias of the node type
        /// </summary>
        public string DocumentTypeAlias { get; set; }

        /// <summary>
        /// The id of the node
        /// </summary>
        public int UmbracoId { get; private set; }

        public int SortOrder { get; set; }

        /// <summary>
        /// The IPublishedContent instance used to construct this instance
        /// </summary>
        public IPublishedContent PublishedContent { get; private set; }

        /// <summary>
        /// The IContent instance used to construct this instance
        /// </summary>
        public IContent Content { get; private set; }

        /// <summary>
        /// Returns true if this instance was constructed from an IPublishedInstance instance
        /// </summary>
        public bool IsPublishedInstance { get; private set; }
        //TODO add other basic properties of a node (last update date etc)

        /// <summary>
        /// Constructs a new instance of <see cref="UmbracoNodeDetails"/>
        /// </summary>
        public UmbracoNodeDetails() { UmbracoId = -1; }

        /// <summary>
        /// Constructs a new instance of <see cref="UmbracoNodeDetails"/>
        /// </summary>
        /// <param name="content">The content instance to describe</param>
        public UmbracoNodeDetails(IPublishedContent content)
        {
            this.PublishedContent = content;
            this.UmbracoId = content.Id;
            this.Name = content.Name;
            this.DocumentTypeAlias = content.DocumentTypeAlias;
            this.SortOrder = content.SortOrder;
            IsPublishedInstance = true;
        }

        /// <summary>
        /// Constructs a new instance of <see cref="UmbracoNodeDetails"/>
        /// </summary>
        /// <param name="content">The content instance to describe</param>
        public UmbracoNodeDetails(IContent content)
        {
            this.Content = content;
            this.UmbracoId = content.Id;
            this.Name = content.Name;
            this.DocumentTypeAlias = content.ContentType.Alias;
            this.SortOrder = content.SortOrder;
            IsPublishedInstance = false;
        }
    }
}
