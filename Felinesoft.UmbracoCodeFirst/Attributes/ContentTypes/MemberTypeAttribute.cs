using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MemberTypeAttribute : ContentTypeAttribute, IInitialisableAttribute
    {
        /// <summary>
        /// Specifies that a class should be used as a member type by UmbracoCodeFirst.
        /// </summary>
        /// <param name="memberTypeName">The name of the member type</param>
        /// <param name="memberTypeAlias">The alias of the member type</param>
        /// <param name="icon">The icon to display in the content tree (see UmbracoCodeFirst.BuiltInIcons for constants representing Umbraco's default icon set)</param>
        /// <param name="iconColor">The icon color to show in the tree</param>
        /// <param name="description">The Description of the member type</param>
        public MemberTypeAttribute(string memberTypeName = null, string memberTypeAlias = null,
                    string icon = BuiltInIcons.IconDocument, string description = "", UmbracoIconColor iconColor = UmbracoIconColor.Black)
        {
            Name = memberTypeName;
            Alias = memberTypeAlias;
            EnableListView = false;
            AllowedAtRoot = false;
            AllowedChildren = new Type[] { };
            Icon = icon;
            Description = description;
            IconColor = iconColor;
        }

        /// <summary>
        /// <para>Specifies that a class should be used as a member type by UmbracoCodeFirst.</para>
        /// <para>
        /// THIS CTR is included for compatability with the output of the T4 generators, as it has the same signature as the 
        /// other content attribute types. It should not be used. Use the overload with 5 parameters for human-written code.
        /// </para>
        /// </summary>
        /// <param name="memberTypeName">The name of the member type</param>
        /// <param name="memberTypeAlias">The alias of the member type</param>
        /// <param name="allowedChildren">Should always be null - no inheritance in member types.</param>
        /// <param name="icon">The icon to display in the content tree (see UmbracoCodeFirst.BuiltInIcons for constants representing Umbraco's default icon set)</param>
        /// <param name="allowAtRoot">Should always be false - no folders in members tree</param>
        /// <param name="enableListView">Should always be false - no folder structure in members tree</param>
        /// <param name="iconColor">The icon color to show in the tree</param>
        /// <param name="description">The Description of the member type</param>
        public MemberTypeAttribute(string memberTypeName, string memberTypeAlias, object allowedChildren,
                    string icon, bool allowAtRoot, bool enableListView, string description, UmbracoIconColor iconColor = UmbracoIconColor.Black)
        {
            Name = memberTypeName;
            Alias = memberTypeAlias;
            EnableListView = false;
            AllowedAtRoot = allowAtRoot;
            AllowedChildren = new Type[] { };
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