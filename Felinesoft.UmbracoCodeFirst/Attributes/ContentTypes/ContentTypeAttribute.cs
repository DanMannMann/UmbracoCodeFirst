
using System;
namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    public abstract class ContentTypeAttribute : CodeFirstAttribute
    {
        /// <summary>
        /// The name of the content type
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The alias of the content type
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// An array of code-first document types which are valid children of this document type
        /// </summary>
        public Type[] AllowedChildren { get; set; }

        /// <summary>
        /// True to allow this type of document to be created at the root of the content tree
        /// </summary>
        public bool AllowedAtRoot { get; set; }

        /// <summary>
        /// True to enable list view for this document type
        /// </summary>
        public bool EnableListView { get; set; }

        /// <summary>
        /// The icon to display in the content tree (see UmbracoCodeFirst.BuiltInIcons for constants
        /// representing Umbraco's default icon set)
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// The Description of the document type
        /// </summary>
        public string Description { get; set; }

        public string CssClasses { get; set; }
    }
}