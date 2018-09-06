using Marsman.UmbracoCodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Marsman.UmbracoCodeFirst.Extensions;
using Marsman.UmbracoCodeFirst.Exceptions;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Umbraco.Core.Models;
using Umbraco.Core;
using Marsman.UmbracoCodeFirst.DataTypes.BuiltIn;

namespace Marsman.UmbracoCodeFirst.Attributes
{
    /// <summary>
    /// Specifies that a property should be used as a document property on a document type.
    /// Any properties which are not set will be inferred from the property metadata and the
    /// data type metadata if possible.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ContentPropertyAttribute : CodeFirstAttribute, IInitialisablePropertyAttribute
    {
        private bool _useConverter;

        /// <summary>
        /// The name of the property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The alias of the property
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Whether or not the property is required to be set before the document can be saved
        /// </summary>
        public bool Mandatory { get; set; }

        /// <summary>
        /// A description of the property, shown under the title
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The sort order of the property
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// The validation regex for the property
        /// </summary>
        public string ValidationRegularExpression { get; set; }

        /// <summary>
        /// True to add the tab alias to the property alias when creating the document type.
        /// This prevents accidental alias naming collisions when two different tabs have a property
        /// with the same name.
        /// </summary>
        public bool AddTabAliasToPropertyAlias { get; set; }

         /// <summary>
        /// <para>The name of the data type to use for this property.</para> 
        /// <para>This value is ignored if the property type is a code-first data type. It should be used
        /// when the property type matches the output type of the relevant property editor value converter.
        /// The specified data type must already exist in Umbraco, it will not be created or updated when
        /// specified using this value.</para>
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// Specifies that a property should be used as a document property on a document type.
        /// Any properties which are not set will be inferred from the property metadata and the
        /// data type metadata if possible.
        /// </summary>
        /// <param name="name">Friendly name of the property</param>
        /// <param name="alias">Alias of the property</param>
        /// <param name="propertyEditorAlias">Alias of your propertyEditor</param>
        /// <param name="dataTypeInstanceName">Name of the instance of your property editor, leave empty if you are using a built-in</param>
        /// <param name="converterType">Converter class that inherits TypeConverter</param>
        /// <param name="mandatory">if set to <c>true</c> [mandatory].</param>
        /// <param name="description">The description.</param>
        /// <param name="sortOrder">The sort order.</param>
        /// <param name="addTabAliasToPropertyAlias">if set to <c>true</c> add's the tab's alias as a suffix to the property alias.</param>
        /// <param name="dataType">
        /// <para>The name of the data type to use for this property.</para> 
        /// <para>This property is ignored if the property type is a code-first data type. It should be used
        /// when the property type matches the output type of the relevant property editor value converter.
        /// The specified data type must already exist in Umbraco, it will not be created or updated when
        /// specified using this value.</para>
        /// </param>
        public ContentPropertyAttribute(string name = null, string alias = null, bool mandatory = false, string description = "", int sortOrder = 0, bool addTabAliasToPropertyAlias = true, string dataType = null)
        {
            Name = name;
            Alias = alias;
            Mandatory = mandatory;
            Description = description;
            SortOrder = sortOrder;
            AddTabAliasToPropertyAlias = addTabAliasToPropertyAlias;
            DataType = dataType;
        }

        /// <summary>
        /// Initialises the attribute properties based on the property to which the attribute is applied.
        /// </summary>
        /// <param name="target">The property to which the attribute is applied</param>
        public virtual void Initialise(PropertyInfo target)
        {
            if (Name == null)
            {
                Name = target.Name.ToProperCase();
            }
            if (Alias == null)
            {
                Alias = target.Name.ToCamelCase();
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
            protected set;
        }
    }

}
