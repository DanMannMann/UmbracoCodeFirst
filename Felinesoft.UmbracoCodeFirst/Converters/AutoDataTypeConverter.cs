using Felinesoft.UmbracoCodeFirst.Core;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.Converters
{
    /// <summary>
    /// A converter which can convert between a storage type of Tdb and
    /// a C# type of Tentity automatically when the Tentity type implements
    /// IUmbracoDataType[Tdb]
    /// </summary>
    /// <typeparam name="Tdb">The database storage type (int, string or DateTime)</typeparam>
    /// <typeparam name="Tentity">The code-first data type which implements IUmbracoDataType[Tdb]</typeparam>
    public sealed class AutoDataTypeConverter<Tdb, Tentity> : DataTypeConverterBase<Tdb, Tentity> where Tentity : IUmbracoDataType<Tdb>
    {
        /// <summary>
        /// Deserialises a Tdb value by calling IUmbracoDataType[Tdb].Initialise on
        /// an instance of Tentity, passing in the input
        /// </summary>
        public override Tentity Create(Tdb input, Action<object> registerContext = null)
        {
            var result = ((Tentity)Activator.CreateInstance<Tentity>());
            registerContext.Invoke(result);
            result.Initialise((Tdb)input);
            return result;
        }

        /// <summary>
        /// Serialises a Tentity value to type Tdb by calling IUmbracoDataType[Tdb].Serialise on
        /// the input
        /// </summary>
        public override Tdb Serialise(Tentity input)
        {
            var result = (Tentity)input;
            return result.Serialise();
        }
    }
}
