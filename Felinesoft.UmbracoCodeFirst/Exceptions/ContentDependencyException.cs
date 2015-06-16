using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Felinesoft.UmbracoCodeFirst.Exceptions
{
    /// <summary>
    /// Thrown when an exception occurs during content dependency ordering
    /// </summary>
    [Serializable]
    public class ContentDependencyException : InvalidOperationException
	{
        /// <summary>
        /// Constructs a new instance of <see cref="ContentDependencyException"/>
        /// </summary>
        /// <param name="message">The message describing the exceptional condition</param>
        public ContentDependencyException(string message)
            : base(message) { }
	}
}