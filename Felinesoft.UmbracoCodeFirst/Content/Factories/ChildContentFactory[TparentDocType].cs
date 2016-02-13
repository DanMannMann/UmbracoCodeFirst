using Felinesoft.UmbracoCodeFirst.Content.Interfaces;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.Content.Factories
{
    /// <summary>
    /// Creates AutoContent which specifies a parent document. Allows the parent type to be specified as a generic type parameter.
    /// </summary>
    /// <typeparam name="TparentDocType">The parent document's type</typeparam>
    public class ChildContentFactory<TparentDocType> : ChildContentFactory, IContentFactory<TparentDocType>
    where TparentDocType : DocumentTypeBase
    {
        /// <summary>
        /// Creates AutoContent which specifies a parent document
        /// </summary>
        /// <param name="values">The document property values to use</param>
        public ChildContentFactory(DocumentTypeBase values)
            : base(values, typeof(TparentDocType)) { }
    }
}
