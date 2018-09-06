
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
using Umbraco.Web;
using Umbraco.Core;
using System.Xml.XPath;
using Marsman.UmbracoCodeFirst.Exceptions;

namespace Marsman.UmbracoCodeFirst.DataTypes.BuiltIn
{
    [DataType("Umbraco.MediaPicker", "Media Picker")]
    [DoNotSyncDataType][BuiltInDataType]
    public class LegacyMediaPicker : MediaItem, IUmbracoIntegerDataType
    {
		public static implicit operator LegacyMediaPicker(int value)
		{
			return new LegacyMediaPicker() { MediaNodeId = value };
		}

		public static implicit operator LegacyMediaPicker(MediaTypeBase value)
		{
			try
			{
				return new LegacyMediaPicker() { MediaNodeId = value.NodeDetails.UmbracoId };
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Media Node Id (MediaTypeBase.NodeDetails.UmbracoId) must be set", ex);
			}
		}

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