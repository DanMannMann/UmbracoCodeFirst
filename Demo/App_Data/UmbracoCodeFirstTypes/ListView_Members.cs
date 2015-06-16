
using Felinesoft.UmbracoCodeFirst;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Umbraco.Core.Models;
using System;

namespace Demo.GeneratedUmbracoTypes
{
    [DataType("Umbraco.ListView", "List View - Members", null, DataTypeDatabaseType.Nvarchar)]
    [PreValue("1", @"10")]
    [PreValue("2", @"Name")]
    [PreValue("3", @"asc")]
    [PreValue("4", @"[{""alias"":""email"",""isSystem"":1},{""alias"":""username"",""isSystem"":1},{""alias"":""updateDate"",""header"":""Last edited"",""isSystem"":1}]")]
    public class ListView_Members : IUmbracoStringDataType
    {
        //TODO implement the properties and serialisation logic for the Umbraco.ListView property editor's values

        /// <summary>
        /// Initialises the instance from the db value
        /// </summary>
        public void Initialise(string dbValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Serialises the instance to the db value
        /// </summary>
        public string Serialise()
        {
            throw new NotImplementedException();
        }
    }
}