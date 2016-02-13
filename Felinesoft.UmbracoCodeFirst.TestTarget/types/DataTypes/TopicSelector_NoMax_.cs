
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
    [DataType("FS.Epithet", "Topic Selector (No Max)")]
    [PreValue("1", @"No no no no no no no no no no no no there's no limit")]
    [PreValue("2", @"topics")]
    public class TopicSelector_NoMax_ : IUmbracoNtextDataType
    {
        //TODO implement the properties and serialisation logic for the FS.Epithet property editor's values

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