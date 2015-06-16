using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.Exceptions
{
    /// <summary>
    /// Thrown when a circular dependency is detected in a content dependency tree
    /// </summary>
    [Serializable]
    public class ContentCircularDependencyException : ContentDependencyException
    {
        /// <summary>
        /// Constructs a new instance of <see cref="ContentCircularDependencyException"/>
        /// </summary>
        /// <param name="message">The message describing the exceptional condition</param>
        public ContentCircularDependencyException(string message)
            : base(message) { }

        /// <summary>
        /// Constructs a new instance of <see cref="ContentCircularDependencyException"/>
        /// </summary>
        /// <param name="message">The message describing the exceptional condition</param>
        /// <param name="dependents">The list of types being analysed when the exception occurred</param>
        public ContentCircularDependencyException(string message, List<Type> dependents)
            : base(string.Format("{0} - affected types: {1}", message, GetNames(dependents))) { }

        private static string GetNames(List<Type> dependents)
        {
            var result = string.Empty;
            dependents.ForEach(x => { if (dependents.IndexOf(x) != 0) { result += ", "; } result += x.Name; });
            return result;
        }
    }
}
