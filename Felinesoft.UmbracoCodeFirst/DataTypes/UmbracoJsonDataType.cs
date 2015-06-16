using Felinesoft.UmbracoCodeFirst.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.DataTypes
{
    /// <summary>
    /// A data type base which allows the derived type to be serialised to and deserialised from JSON using JsonConvert
    /// </summary>
    public abstract class UmbracoJsonDataType : IUmbracoStringDataType
    {
        private JsonSerializerSettings _serialiserSettings;

        /// <summary>
        /// Constructs a new instance of <see cref="UmbracoJsonDataType"/>
        /// </summary>
        /// <param name="nullHandling">Defines how null properties are serialised</param>
        protected UmbracoJsonDataType(NullValueHandling nullHandling = NullValueHandling.Include)
        {
            _serialiserSettings = new JsonSerializerSettings
            {
                NullValueHandling = nullHandling
            };
        }

        /// <summary>
        /// Defines how null properties are serialised
        /// </summary>
        protected NullValueHandling NullHandling
        {
            get
            {
                return _serialiserSettings.NullValueHandling;
            }
            set
            {
                _serialiserSettings = new JsonSerializerSettings
                {
                    NullValueHandling = value
                };
            }
        }

        /// <summary>
        /// Initialises the current instance with the 
        /// values deserialised from the given JSON string
        /// </summary>
        /// <param name="dbValue">The JSON string</param>
        public virtual void Initialise(string dbValue)
        {
            JsonConvert.PopulateObject(dbValue, this);
        }

        /// <summary>
        /// Serialises the current instance to a JSON string
        /// </summary>
        /// <returns>The serialised instance</returns>
        public virtual string Serialise()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, _serialiserSettings);
        }
    }

    [DataType(propertyEditorAlias: "CodeFirst.JsonAddressEditor")]
    public class AddressDataType : IUmbracoNtextDataType<string>
    {
        public int PropertyNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }

        [JsonIgnore]
        public string Region
        {
            get
            {
                return string.Format("{0}, {1}", City, PostalCode);
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}\r\n{2}\r\n{3}\r\n\r\n{4}", PropertyNumber, AddressLine1, AddressLine2, City, PostalCode);
        }

        public void Initialise(string dbValue)
        {
            JsonConvert.PopulateObject(dbValue, this);
        }

        public string Serialise()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
