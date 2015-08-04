
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
using System.Web;

namespace Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn
{
    [DataType(propertyEditorAlias: "Umbraco.UploadField", name: "Upload")]
    [DoNotSyncDataType][BuiltInDataType]
    public class Upload : IUmbracoNtextDataType
    {
        public string Url { get; set; }

        /// <summary>
        /// Initialises the instance from the db value
        /// </summary>
        public void Initialise(string dbValue)
        {
            Url = dbValue;
        }

        /// <summary>
        /// Serialises the instance to the db value
        /// </summary>
        public string Serialise()
        {
            return Url;
        }

        public override string ToString()
        {
            return Url;
        }
    }
}