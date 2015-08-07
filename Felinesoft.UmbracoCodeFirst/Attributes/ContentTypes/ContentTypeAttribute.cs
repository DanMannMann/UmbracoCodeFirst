
using System;
namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    public abstract class ContentTypeAttribute : CodeFirstAttribute
    {
        private string _icon = string.Empty;

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
        public string Icon
        {
            get
            {
                return _icon + (IconColor == UmbracoIconColor.Black ? string.Empty : (" color-" + IconColor.ToString().ToLower()));
            }
            set
            {
                _icon = value;
            }
        }

        /// <summary>
        /// The colour to use for the icon in the content tree (defaults to black)
        /// </summary>
        public UmbracoIconColor IconColor { get; set; }

        /// <summary>
        /// The Description of the document type
        /// </summary>
        public string Description { get; set; }
    }

    public enum UmbracoIconColor
    {
        Black = 0,
        Green = 1,
        Yellow = 2,
        Orange = 3,
        Blue = 4,
        Red = 5
    }
}