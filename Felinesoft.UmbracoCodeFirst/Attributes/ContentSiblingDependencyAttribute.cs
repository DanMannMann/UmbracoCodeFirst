using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Exceptions;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    /// <summary>
    /// Specifies a document type whose default document is a sibling of this
    /// document and should be created before this document
    /// </summary>
    public class ContentSiblingDependencyAttribute : Attribute
    {
        /// <summary>
        /// Specifies a document type whose default document is a sibling of this
        /// document and should be created before this document
        /// </summary>
        /// <param name="dependency">The document type</param>
        public ContentSiblingDependencyAttribute(Type dependency)
        {
            Dependency = dependency;
        }

        /// <summary>
        /// The dependency document type
        /// </summary>
        public Type Dependency { get; private set; }

        /// <summary>
        /// The instance of the dependency
        /// </summary>
        /// <exception cref="ContentDependencyException">Thrown if the dependency does not exist</exception>
        public IContent DependencyContent
        {
            get
            {
                var attr = Dependency.GetContentFactoryAttribute();
                if(attr == null)
                {
                    throw new ContentDependencyException("The specified content dependency type does not have a [ContentFactory] attribute");
                }
                return attr.Factory.GetIfExists();
            }
        }
    }
}
