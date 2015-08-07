using System;

namespace Felinesoft.UmbracoCodeFirst.Converters
{
    public sealed class PassThroughConverter<T> : DataTypeConverterBase<T, T>
    {

        public override T Create(T input, Action<object> contextAction = null)
        {
            return input;
        }

        public override T Serialise(T input)
        {
            return input;
        }
    }
}