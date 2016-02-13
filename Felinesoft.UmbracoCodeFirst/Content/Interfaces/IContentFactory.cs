using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;

namespace Felinesoft.UmbracoCodeFirst.Content.Interfaces
{
    /// <summary>
    /// <para>Creates an instance of a document type</para>
    /// <para>In order to be consumed by the initialiser the implementing class needs to have a parameterless constructor.</para>
    /// <para>The RootContentFactory and ChildContentFactory[T] implementations work with the IAutoContent interface
    /// and the [AutoContent] attribute to place the content creation logic into methods on the code-first document type model.
    /// The recommended route to content generation is to implement IAutoContent on the document type model and apply
    /// an [AutoContent] attribute to the document type model.</para>
    /// <para>Implementing a custom factory may be required for some scenarios such as creating a collection
    /// of content items or creating a nested structure in a single factory.</para>
    /// </summary>
    public interface IContentFactory
    {
        /// <summary>
        /// <para>Creates and returns the default document, or returns the existing instance if there is one.</para>
        /// <para>If the node already exists it should still be returned in order to ensure its' sort order and permissions are updated.</para>
        /// <para>Note: It is the implementor's responsibility to ensure content doesn't already exist. Exceptions will be thrown by Umbraco after this method ends if attempting to create content which already exists, so do check.</para>
        /// </summary>
        IContent GetOrCreate();

        /// <summary>
        /// <para>Gets the existing instance of this default document, if it exists in the database.</para>
        /// <para>This method returns any node of the correct document type with the correct alias found at the correct tree location, otherwise null</para>
        /// </summary>
        /// <returns>Any node of the correct document type with the correct alias found at the correct tree location, otherwise null</returns>
        IContent GetIfExists();

        /// <summary>
        /// <para>Gets the existing instance of this default content, if it exists in the published content cache.</para>
        /// <para>This method returns any node of the correct document type with the correct alias found at the correct tree location, otherwise null</para>
        /// </summary>
        /// <returns>Any node of the correct document type with the correct alias found at the correct tree location, otherwise null</returns>
        IPublishedContent GetPublishedIfExists();
    }

    /// <summary>
    /// <para>Creates an instance of a document type, allowing the parent document to be specified using a generic type parameter.</para>
    /// <para>In order to be consumed by the initialiser the implementing class needs to have a parameterless constructor.</para>
    /// <para>The RootContentFactory and ChildContentFactory[T] implementations work with the IAutoContent interface
    /// and the [AutoContent] attribute to place the content creation logic into methods on the code-first document type model.
    /// The recommended route to content generation is to implement IAutoContent on the document type model and apply
    /// an [AutoContent] attribute to the document type model.</para>
    /// <para>Implementing a custom factory may be required for some scenarios such as creating a collection
    /// of content items or creating a nested structure in a single factory.</para>
    /// </summary>
    /// <typeparam name="TparentDocType">The DocumentTypeBase whose default document will be the parent of this factory's created document.</typeparam>
    public interface IContentFactory<TparentDocType> : IContentFactory where TparentDocType : DocumentTypeBase 
    {

    }
}
