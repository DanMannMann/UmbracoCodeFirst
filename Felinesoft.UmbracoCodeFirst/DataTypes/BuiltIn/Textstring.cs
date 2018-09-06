
using Marsman.UmbracoCodeFirst;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Marsman.UmbracoCodeFirst.DataTypes;
using Marsman.UmbracoCodeFirst.DataTypes.BuiltIn;
using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Umbraco.Core.Models;
using System;
using Marsman.UmbracoCodeFirst.Core;

namespace Marsman.UmbracoCodeFirst.DataTypes.BuiltIn
{
    [DataType("Umbraco.Textbox", "Textstring")]
    [DoNotSyncDataType][BuiltInDataType]
    public class Textstring : IUmbracoNvarcharDataType
    {
        public string Value { get; set; }

		public static implicit operator Textstring(string value)
		{
			return new Textstring() { Value = value };
		}

		/// <summary>
		/// Initialises the instance from the db value
		/// </summary>
		public void Initialise(string dbValue)
        {
            Value = dbValue;
        }

        /// <summary>
        /// Serialises the instance to the db value
        /// </summary>
        public string Serialise()
        {
            return Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}