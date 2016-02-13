using Felinesoft.UmbracoCodeFirst.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Core;
using Umbraco.Core.Models;

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

        public override Tenum Create(string input, Action<object> registerContext = null)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return default(Tenum);
            }

            var ids = input.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var preValues = GetPreValues();
            Tenum result;
            int currentId;

            if (ids.Count() > 0 && ids.All(x => int.TryParse(x, out currentId) && preValues.Any(y => y.Id == currentId)))
            {
                var items = new List<string>();
                var idList = new List<int>();
                idList.AddRange(ids.Select(x => int.Parse(x)));

                foreach (var id in idList)
                {
                    items.Add(preValues.Single(x => x.Id == id).Value.ToPascalCase());
                }
                if (Enum.TryParse<Tenum>(string.Join(",", items), true, out result))
                {
                    return result;
                }
                else
                {
                    throw new CodeFirstException("invalid prevalue id list: " + input);
                }
            }
            else if (Enum.TryParse<Tenum>(input.ToPascalCase(), true, out result))
            {
                return result;
            }
            else
            {
                throw new CodeFirstException(input + " is not a member of enum " + typeof(Tenum).Name);
            }
        }

        public override string Serialise(Tenum input)
        {
            var preValues = GetPreValues();
            var ids = new List<int>();
            foreach (var val in preValues)
            {
                Tenum result;
                if (Enum.TryParse<Tenum>(val.Value.ToPascalCase(), out result) && (input as Enum).HasFlag(result as Enum))
                {
                    if ((result.ToString().Equals("none", StringComparison.InvariantCultureIgnoreCase) || result.ToString().Equals("all", StringComparison.InvariantCultureIgnoreCase)) && (int)Convert.ChangeType((result as Enum), (result as Enum).GetTypeCode()) == 0)
                    {
                        continue;
                    }
                    else
                    {
                        ids.Add(val.Id);
                    }
                }
            }
            return string.Join(",", ids);
        }

        private IReadOnlyList<PreValue> GetPreValues()
        {
            return CodeFirstManager.Current.Modules.PreValueCacheModule.Get(CodeFirstManager.Current.Modules.DataTypeModule.DataTypeRegister.Registrations.First(x => x.ClrType == typeof(Tenum)));
        }
    }
}
