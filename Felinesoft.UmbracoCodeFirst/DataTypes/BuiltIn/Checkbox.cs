
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
    [DataType("Umbraco.TrueFalse", "Checkbox")]
    [DoNotSyncDataType][BuiltInDataType]
    public class Checkbox : IUmbracoIntegerDataType
    {

        public bool Value { get; set; }

		public static implicit operator Checkbox(bool value)
		{
			return new Checkbox() { Value = value };
		}

		/// <summary>
		/// Initialises the instance from the db value
		/// </summary>
		public void Initialise(int dbValue)
        {
            if (dbValue > 0)
            {
                Value = true;
            }
            else
            {
                Value = false;
            }
        }

        /// <summary>
        /// Serialises the instance to the db value
        /// </summary>
        public int Serialise()
        {
            return Value ? 1 : 0;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}