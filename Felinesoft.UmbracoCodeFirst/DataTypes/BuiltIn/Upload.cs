
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
using System.Web;

namespace Marsman.UmbracoCodeFirst.DataTypes.BuiltIn
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