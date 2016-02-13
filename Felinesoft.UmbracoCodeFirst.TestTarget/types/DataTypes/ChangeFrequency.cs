
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

namespace UmbracoCodeFirst.GeneratedTypes
{
    [DataType("Umbraco.DropdownlistPublishingKeys", "Change Frequency")]
    [PreValue("1", @"always")]
    [PreValue("2", @"hourly")]
    [PreValue("3", @"daily")]
    [PreValue("4", @"monthly")]
    [PreValue("5", @"yearly")]
    [PreValue("6", @"never")]
    public class ChangeFrequency : IUmbracoIntegerDataType
    {
        //TODO implement the properties and serialisation logic for the Umbraco.DropdownlistPublishingKeys property editor's values

        /// <summary>
        /// Initialises the instance from the db value
        /// </summary>
        public void Initialise(int dbValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Serialises the instance to the db value
        /// </summary>
        public int Serialise()
        {
            throw new NotImplementedException();
        }
    }
}