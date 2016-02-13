
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System.Reflection;
using Felinesoft.UmbracoCodeFirst.Converters;
using Umbraco.Core.Models;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    /// <summary>
    /// Specifies that the decorated enum is a code-first enum data type
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false)]
    public class EnumDataTypeAttribute : DataTypeAttribute, IInitialisableAttribute
    {
        /// <summary>
        /// Specifies that the decorated enum is a code-first enum data type
        /// </summary>
        /// <param name="propertyEditorAlias">The property editor alias for the data type</param>
        /// <param name="name">The instance name for the data type</param>
        /// <param name="converterType">The default converter type which can convert between the code-first class and an Umbraco property of that data type</param>
        /// <param name="dbType">The storage type used to store the data type value in the database</param>
        /// <param name="useConverter">False to use no converter if converterType is null, true to attempt to use an automatic converter.
        /// Automatic converters are supported for any enum.</param>
        public EnumDataTypeAttribute(string propertyEditorAlias = null, string name = null)
            : base(propertyEditorAlias, name, null, DatabaseType.Nvarchar, true)
        {

        }

        /// <summary>
        /// Initialises the Factory property based on the type to which the attribute is applied.
        /// </summary>
        /// <param name="decoratedType">The type to which the attribute is applied</param>
        public override void Initialise(Type decoratedType)
        {
            if (!decoratedType.IsEnum)
            {
                throw new CodeFirstException("EnumDataTypeAttribute can only be applied to an enum type. " + decoratedType.FullName + " is not an enum");
            }

            if (string.IsNullOrEmpty(Name))
            {
                Name = decoratedType.Name.ToProperCase();
            }

            if (string.IsNullOrEmpty(PropertyEditorAlias))
            {
                if (decoratedType.GetCustomAttribute<FlagsAttribute>() != null)
                {
                    PropertyEditorAlias = BuiltInPropertyEditorAliases.CheckBoxList;
                }
                else
                {
                    PropertyEditorAlias = BuiltInPropertyEditorAliases.DropDown;
                }
            }

            ConverterType = typeof(EnumDataTypeConverter<>).MakeGenericType(decoratedType);

            DbType = DataTypeDatabaseType.Ntext;

            Initialised = true;
        }
    }
}
