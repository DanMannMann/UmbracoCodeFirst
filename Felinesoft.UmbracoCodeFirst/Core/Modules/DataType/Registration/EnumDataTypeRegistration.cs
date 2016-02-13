using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Felinesoft.UmbracoCodeFirst.Converters;

namespace Felinesoft.UmbracoCodeFirst
{
    /// <summary>
    /// Represents the information needed to map an Umbraco select or multiselect data type to a .NET enum type
    /// </summary>
    /// <typeparam name="Tenum">The enum type to map</typeparam>
    public class EnumDataTypeRegistration<Tenum> : DataTypeRegistration where Tenum : struct, IConvertible
    {
        /// <summary>
        /// Represents the information needed to map an Umbraco select or multiselect data type to a .NET enum type.
        /// If no data type instance or property editor is specified then checkboxlist will be used for [Flags] enums and dropdown will be used for regular enums.
        /// </summary>
        /// <param name="instanceName">the data type instance name</param>
        /// <param name="propertyEditorAlias">the data type property editor alias</param>
        /// <param name="converterType">the type converter to use to transform the Umbraco value into the enum</param>
        /// <param name="useConverter">True to use an automatic converter if none is specified. False to use no converter.</param>
        public EnumDataTypeRegistration(string instanceName = null, string propertyEditorAlias = null, Type converterType = null, bool useConverter = true)
        {
            Type t = typeof(Tenum);
            if (!t.IsEnum)
            {
                throw new CodeFirstException("EnumDataTypeRegistration can only be used to define enum-based data types. " + t.Name + " is not an enum type.");
            }

            if (string.IsNullOrEmpty(instanceName))
            {
                DataTypeInstanceName = t.FullName;
            }
            else
            {
                DataTypeInstanceName = instanceName;
            }

            if (string.IsNullOrEmpty(propertyEditorAlias))
            {
                if (t.GetCustomAttribute<FlagsAttribute>() != null)
                {
                    PropertyEditorAlias = BuiltInPropertyEditorAliases.CheckBoxList;
                }
                else
                {
                    PropertyEditorAlias = BuiltInPropertyEditorAliases.DropDown;
                }
            }
            else
            {
                PropertyEditorAlias = propertyEditorAlias;
            }

            if (converterType == null && useConverter)
            {
                ConverterType = typeof(EnumDataTypeConverter<Tenum>);
            }
            else
            {
                ConverterType = converterType;
            }
        }
    }
}
