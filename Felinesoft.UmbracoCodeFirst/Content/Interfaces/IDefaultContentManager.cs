using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Felinesoft.UmbracoCodeFirst.Content.Interfaces
{
    /// <summary>
    /// Manages the creation of default documents
    /// </summary>
    internal interface IDefaultContentManager
    {
        /// <summary>
        /// Gets an existing instance if it exists, otherwise creates a default instance.
        /// </summary>
        /// <typeparam name="T">The document type to get or create</typeparam>
        /// <returns>The document instance</returns>
        IContent GetOrCreateDefaultContent<T>() where T : DocumentTypeBase;

        /// <summary>
        /// Gets an existing instance if it exists, otherwise creates a default instance.
        /// </summary>
        /// <param name="docType">The document type to get or create</param>
        /// <returns>The document instance</returns>
        IContent GetOrCreateDefaultContent(Type docType);

        /// <summary>
        /// Creates a default instance.
        /// </summary>
        /// <param name="docType">The document type to create</param>
        /// <returns>The document instance</returns>
        IContent CreateDefaultContent(Type docType);
    }
}
