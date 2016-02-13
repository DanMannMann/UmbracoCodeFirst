using Felinesoft.UmbracoCodeFirst.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Felinesoft.UmbracoCodeFirst.ContentTypes
{
    /// <summary>
    /// Represents the details of an Umbraco content node
    /// </summary>
    public class MemberNodeDetails : ContentNodeDetails<IMember>
    {
        /// <summary>
        /// The IMember instance used to construct this instance
        /// </summary>
        public IMember Content { get; private set; }

        /// <summary>
        /// Returns true if this instance was constructed from an IPublishedInstance instance
        /// </summary>
        public bool IsPublishedInstance { get; private set; }

        /// <summary>
        /// Constructs a new instance of <see cref="DocumentNodeDetails"/>
        /// </summary>
        public MemberNodeDetails() { UmbracoId = -1; }

        /// <summary>
        /// Constructs a new instance of <see cref="DocumentNodeDetails"/>
        /// </summary>
        /// <param name="content">The content instance to describe</param>
        public MemberNodeDetails(IPublishedContent content)
        {
            Initialise(content);
        }

        /// <summary>
        /// Constructs a new instance of <see cref="DocumentNodeDetails"/>
        /// </summary>
        /// <param name="content">The content instance to describe</param>
        public MemberNodeDetails(IMember content)
        {
            Initialise(content);
        }

        public override void Initialise(IPublishedContent content)
        {
            base.Initialise(content);
            IsPublishedInstance = true;
        }

        public override void Initialise(IMember content, string contentTypeAlias = null)
        {
            base.Initialise(content, content.ContentType.Alias);
            this.Content = content;
            IsPublishedInstance = false;
        }
    }

}
