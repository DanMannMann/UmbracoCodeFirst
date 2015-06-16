using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.Converters
{
    public abstract class DataTypeConverterBase<Tdb, Tentity> : IDataTypeConverter<Tdb, Tentity>
    {
        public abstract Tentity Create(Tdb input);

        public abstract Tdb Serialise(Tentity input);

        public object Create(object input)
        {
            return Create((Tdb)input);
        }

        public object Serialise(object input)
        {
            return Serialise((Tentity)input);
        }
    }
}
