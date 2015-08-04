using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using System;
using System.Reflection;
using Umbraco.Core;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DocumentPickerPropertyAttribute : ContentPropertyAttribute, IDataTypeRedirect, IInitialisablePropertyAttribute
    {
        /// <summary>
        /// Specifies that a property should be used as a document property on a document type and that the SingleDocumentPicker should be
        /// used in Umbraco to choose the value for this property.
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
        public DocumentPickerPropertyAttribute(string name = null, string alias = null, bool mandatory = false, string description = "", int sortOrder = 0, bool addTabAliasToPropertyAlias = true, string dataType = null)
            : base(name, alias, mandatory, description, sortOrder, addTabAliasToPropertyAlias, dataType) { }

        public Type DocumentType { get; private set; }

        public override void Initialise(PropertyInfo propertyTarget)
        {
            base.Initialise(propertyTarget);
            if (!propertyTarget.PropertyType.Inherits<DocumentTypeBase>())
            {
                throw new AttributeInitialisationException("[DocumentPickerProperty] can only be applied to properties who's type inherits DocumentTypeBase. Affected property: " + propertyTarget.DeclaringType.Name + "." + propertyTarget.Name);
            }
            DocumentType = propertyTarget.PropertyType;
            Initialised = true;
        }

        public bool Initialised
        {
            get;
            private set;
        }

        public Type Redirect(PropertyInfo property)
        {
            return typeof(SingleDocumentPicker<>).MakeGenericType(property.PropertyType);
        }

        public object GetValue(object data)
        {
            if (data == null)
            {
                return null;
            }
            return data.GetType().GetProperty("PickedItem").GetValue(data);
        }
    }
}