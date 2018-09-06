using Marsman.UmbracoCodeFirst.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marsman.UmbracoCodeFirst.Converters
{
    public abstract class DataTypeConverterBase<Tdb, Tentity> : IDataTypeConverter<Tdb, Tentity>
    {
        public abstract Tentity Create(Tdb input, Action<object> contextAction = null);

        public abstract Tdb Serialise(Tentity input);

        public object Create(object input, Action<object> registerContext = null)
        {
            if (input == null) return default(Tentity);
            return Create((Tdb)input, registerContext);
        }

        public object Serialise(object input)
        {
            if (input == null) return default(Tdb);
            return Serialise((Tentity)input);
        }
    }
}
