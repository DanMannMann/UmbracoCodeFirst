using System;

namespace Felinesoft.UmbracoCodeFirst.Converters
{
    public sealed class BoolTrueFalseConverter : DataTypeConverterBase<int, bool>
    {
        public override bool Create(int input, Action<object> contextAction = null)
        {
            return input == 1;
        }

        public override int Serialise(bool input)
        {
            return input ? 1 : 0;
        }
    }
}