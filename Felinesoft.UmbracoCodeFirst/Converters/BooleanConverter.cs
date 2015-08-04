using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.Converters
{
    public sealed class BooleanConverter : DataTypeConverterBase<string,bool>
    {
        public override bool Create(string input, DataTypeRegistration registration)
        {
            return bool.Parse(input.ToString());
        }

        public override string Serialise(bool input)
        {
            return input ? "1" : "0";
        }
    }
}
