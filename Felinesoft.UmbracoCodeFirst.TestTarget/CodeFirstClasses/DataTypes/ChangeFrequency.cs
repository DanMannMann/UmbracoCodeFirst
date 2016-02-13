
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

namespace LMI.BusinessLogic.CodeFirst
{
    [DataType("Umbraco.DropdownlistPublishingKeys", "Change Frequency")]
    [PreValue("0", @"always")]
    [PreValue("1", @"hourly")]
    [PreValue("2", @"daily")]
    [PreValue("3", @"monthly")]
    [PreValue("4", @"yearly")]
    [PreValue("5", @"never")]
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