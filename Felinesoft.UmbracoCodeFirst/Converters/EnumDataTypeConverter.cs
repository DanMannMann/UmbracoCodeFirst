using Felinesoft.UmbracoCodeFirst.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Core;

namespace Felinesoft.UmbracoCodeFirst.Converters
{
    /// <summary>
    /// A converter which can convert between a storage type of string and
    /// a C# enum of type Tenum automatically. This only works when the property
    /// editor used stores the *value* of the selected item, not its ID. 
    /// DropDown, CheckboxList and DropdownMultiple work out-of-the-box. Radiobox does not.
    /// </summary>
    /// <typeparam name="Tenum">An enum type to convert to and from</typeparam>
    public sealed class EnumDataTypeConverter<Tenum> : DataTypeConverterBase<string, Tenum> where Tenum : struct, IConvertible
    {
        private Type _enumType;

        /// <summary>
        /// Constructs a new instance and checks if the type argument is an enum
        /// </summary>
        /// <exception cref="CodeFirstException">Thrown when Tenum is not an enum type</exception>
        public EnumDataTypeConverter()
        {
            _enumType = typeof(Tenum);
            if (!_enumType.IsEnum)
            {
                throw new CodeFirstException("EnumDataTypeConverter can only be used to convert enum types. " + _enumType.Name + " is not an enum type.");
            }
        }

        /// <summary>
        /// Creates an instance of Tenum by converting the input to pascal case
        /// and parsing it. This works for comma-separated lists of values if the specified
        /// enum is a bit-field with a [Flags] attribute.
        /// </summary>
        public override Tenum Create(string input, Action<object> registerContext = null)
        {
            Tenum result;
            if (Enum.TryParse<Tenum>(input.ToPascalCase(), true, out result))
            {
                return result;
            }
            else
            {
                throw new CodeFirstException(input + " is not a member of enum " + typeof(Tenum).Name);
            }
        }

        /// <summary>
        /// Serialises the input enum by calling ToString then converting the result to proper case.
        /// This produces comma-separated lists of values if the specified enum is a bit-field with a [Flags] attribute.
        /// </summary>
        public override string Serialise(Tenum input)
        {
            return input.ToString().ToProperCase();
        }
    }
}
