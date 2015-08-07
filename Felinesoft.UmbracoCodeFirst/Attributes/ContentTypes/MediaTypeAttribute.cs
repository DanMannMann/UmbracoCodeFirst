using Felinesoft.UmbracoCodeFirst.ContentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Core;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    /// <summary>
    /// Specifies that a class should be used as a media type by UmbracoCodeFirst.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MediaTypeAttribute : ContentTypeAttribute, IInitialisableAttribute
    {
        /// <summary>
        /// Specifies that a class should be used as a media type by UmbracoCodeFirst.
        /// </summary>
        /// <param name="mediaTypeName">The name of the media type</param>
        /// <param name="mediaTypeAlias">The alias of the media type</param>
        /// <param name="allowedChildren">An array of code-first media types which are valid children of this media type</param>
        /// <param name="icon">The icon to display in the content tree (see UmbracoCodeFirst.BuiltInIcons for constants representing Umbraco's default icon set)</param>
        /// <param name="allowAtRoot">True to allow this type of media to be created at the root of the content tree</param>
        /// <param name="enableListView">True to enable list view for this media type</param>
        /// <param name="description">The Description of the media type</param>
        public MediaTypeAttribute(string mediaTypeName = null, string mediaTypeAlias = null, Type[] allowedChildren = null,
            string icon = BuiltInIcons.IconPicture, bool allowAtRoot = false, bool enableListView = false, string description = "", UmbracoIconColor iconColor = UmbracoIconColor.Black)
        {
            Name = mediaTypeName;
            Alias = mediaTypeAlias;
            EnableListView = enableListView;
            AllowedAtRoot = allowAtRoot;
            AllowedChildren = allowedChildren;
            Icon = icon;
            Description = description;
            IconColor = iconColor;
        }

        /// <summary>
        /// Returns true if the attribute instance has been initialised.
        /// Until the attribute is initialised any inferred properties (i.e. any not explicitly set by the caller) will still be null.
        /// </summary>
        public bool Initialised
        {
            get;
            private set;
        }

        /// <summary>
        /// Initialises the Factory property based on the type to which the attribute is applied.
        /// </summary>
        /// <param name="decoratedType">The type to which the attribute is applied</param>
        public virtual void Initialise(Type decoratedType)
        {
            if (Initialised)
            {
                throw new InvalidOperationException("Already initialised!");
            }

            var name = decoratedType.Name.ToProperCase();
            var alias = decoratedType.Name.ToCamelCase();
            if (Name == null)
            {
                Name = name;
            }
            if (Alias == null)
            {
                Alias = alias;
            }
            Initialised = true;
        }
    }
}
