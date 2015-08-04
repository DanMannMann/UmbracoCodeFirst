
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
using Umbraco.Web;
using Umbraco.Core;
using System.Xml.XPath;
using Felinesoft.UmbracoCodeFirst.Exceptions;

namespace Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn
{
    [DataType("Umbraco.MediaPicker", "Media Picker")]
    [DoNotSyncDataType][BuiltInDataType]
    public class LegacyMediaPicker : MediaItem, IUmbracoIntegerDataType
    {
        /// <summary>
        /// Initialises the instance from the db value
        /// </summary>
        public void Initialise(int dbValue)
        {
            MediaNodeId = dbValue;
        }

        /// <summary>
        /// Serialises the instance to the db value
        /// </summary>
        public int Serialise()
        {
            return MediaNodeId;
        }

        
    }
}