
using System;
using Marsman.UmbracoCodeFirst.Extensions;

namespace Marsman.UmbracoCodeFirst.Attributes
{
    /// <summary>
    /// Specifies that a property should be used as a document tab on a document type.
    /// Any properties which are not set will be inferred from the property metadata and the
    /// data type metadata if possible.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ContentTabAttribute : CodeFirstAttribute, IInitialisablePropertyAttribute
    {
        /// <summary>
        /// The name of the tab
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The sort order of the tab
        /// </summary>
        public int SortOrder { get; protected set; }

        /// <summary>
        /// <para>Used if the tab is renamed to preserve the aliases of existing properties</para>
        /// <para>If it is not null then this property's value will be used as the postfix when addTabNameToPropertyAlias is true. If this value is null then Name is used.</para>
        /// </summary>
        public string OriginalName { get; protected set; }

        /// <summary>
        /// Specifies that a property should be used as a document tab on a document type.
        /// Any properties which are not set will be inferred from the property metadata and the
        /// data type metadata if possible.
        /// </summary>
        /// <param name="name">The name of the tab</param>
        /// <param name="sortOrder">The sort order of the tab</param>
        public ContentTabAttribute(string name = null, int sortOrder = 0, string originalName = null, bool preserveOriginalNameFormat = false)
        {
            Name = name;
            SortOrder = sortOrder;
            OriginalName = originalName == null ? null : preserveOriginalNameFormat ? originalName : originalName.ToProperCase();
        }

        /// <summary>
        /// Initialises the attribute properties based on the type to which the attribute is applied.
        /// </summary>
        /// <param name="propertyTarget">The property to which the attribute is applied</param>
        public void Initialise(System.Reflection.PropertyInfo propertyTarget)
        {
            if (string.IsNullOrEmpty(Name))
            {
                Name = propertyTarget.Name.ToProperCase();
            }
            Initialised = true;
        }

        /// <summary>
        /// Returns true if the attribute instance has been initialised.
        /// Until the attribute is initialised any inferred properties (i.e. those not set explicitly in the constructor) will still be null.
        /// </summary>
        public bool Initialised
        {
            get;
            private set;
        }
    }
}
