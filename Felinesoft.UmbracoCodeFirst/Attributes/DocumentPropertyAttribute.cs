using Felinesoft.UmbracoCodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Felinesoft.InitialisableAttributes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Exceptions;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    /// <summary>
    /// Specifies that a property should be used as a document property on a document type.
    /// Any properties which are not set will be inferred from the property metadata and the
    /// data type metadata if possible.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DocumentPropertyAttribute : CodeFirstAttribute, IInitialisablePropertyAttribute
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
        /// The property editor alias of the property
        /// </summary>
        public string PropertyEditorAlias { get; set; }

        /// <summary>
        /// The instance name of the property's data type
        /// </summary>
        public string DataTypeInstanceName { get; set; }

        /// <summary>
        /// Whether or not the property is required to be set before the document can be saved
        /// </summary>
        public bool Mandatory { get; set; }

        /// <summary>
        /// A description of the property, shown under the title
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A type which inherits TypeConverter and can convert between the database storage type and the
        /// code-first property type.
        /// </summary>
        public Type ConverterType { get; set; }

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
        public DocumentPropertyAttribute(string name = null, string alias = null, string propertyEditorAlias = null, string dataTypeInstanceName = null, Type converterType = null, bool mandatory = false, string description = "", int sortOrder = 0, bool addTabAliasToPropertyAlias = true, bool useConverter = true)
        {
            Name = name;
            Alias = alias;
            PropertyEditorAlias = propertyEditorAlias;
            DataTypeInstanceName = dataTypeInstanceName;
            ConverterType = converterType;
            Mandatory = mandatory;
            Description = description;
            SortOrder = sortOrder;
            AddTabAliasToPropertyAlias = addTabAliasToPropertyAlias;
            _useConverter = useConverter;
        }

        /// <summary>
        /// Initialises the attribute properties based on the property to which the attribute is applied.
        /// </summary>
        /// <param name="target">The property to which the attribute is applied</param>
        public virtual void Initialise(PropertyInfo target)
        {
            var dataType = target.PropertyType;
            var attr = dataType.GetCodeFirstAttribute<DataTypeAttribute>();

            /* *
             * If the data type is registered then check if any of its' essential properties are null.
             * If they are then copy them in from the data type's attribute.
             * If the type is not registered but there is a data type attribute then use it's values if needed.
             * * 
             * */
            if (DataTypeRegister.Current.IsRegistered(dataType))
            {
                if (DataTypeInstanceName == null)
                {
                    DataTypeInstanceName = DataTypeRegister.Current.GetDataTypeInstanceName(dataType);
                }
                if (DataTypeInstanceName == null && PropertyEditorAlias == null)
                {
                    PropertyEditorAlias = DataTypeRegister.Current.GetPropertyEditorAlias(dataType);
                }
                if (ConverterType == null && _useConverter)
                {
                    ConverterType = DataTypeRegister.Current.GetConverterType(dataType);
                }
            }
            else if (attr != null)
            {
                if (DataTypeInstanceName == null)
                {
                    DataTypeInstanceName = attr.Name;
                }
                if (DataTypeInstanceName == null && PropertyEditorAlias == null)
                {
                    PropertyEditorAlias = attr.PropertyEditorAlias;
                }
                if (ConverterType == null && _useConverter)
                {
                    ConverterType = attr.ConverterType;
                }
            }
            else if (DataTypeInstanceName == null && PropertyEditorAlias == null)
            {
                throw new CodeFirstException("When a property has a [DocumentProperty] attribute its type must either be one of the supported types (string, int, bool and DateTime), be registered in the DataTypeRegister or have a [DataType] attribute. " + dataType.Name + " is not in the DataTypeRegister and does not have a valid [DataType] attribute.");
            }

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
            private set;
        }
    }
}
