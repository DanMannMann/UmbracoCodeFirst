
using Felinesoft.UmbracoCodeFirst;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Umbraco.Core.Models;
using System;
using Felinesoft.UmbracoCodeFirst.Core;

namespace Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn
{
    [DataType("Umbraco.Date", "Date Picker")]
    [DoNotSyncDataType][BuiltInDataType]
    public class DatePicker : IUmbracoDateDataType
    {
        public DateTime Value { get; set; }

        /// <summary>
        /// Initialises the instance from the db value
        /// </summary>
        public void Initialise(DateTime dbValue)
        {
            Value = dbValue;
        }

        /// <summary>
        /// Serialises the instance to the db value
        /// </summary>
        public DateTime Serialise()
        {
            return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}