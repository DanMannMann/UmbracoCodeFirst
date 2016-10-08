using Felinesoft.UmbracoCodeFirst;
using System;
using Felinesoft.UmbracoCodeFirst.Core;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Umbraco.Web;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System.Drawing;

namespace Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn
{
    /// <summary>
    /// Represents Umbraco's built-in color picker data type
    /// </summary>
    [DataType(name: BuiltInDataTypes.ApprovedColor, propertyEditorAlias: BuiltInPropertyEditorAliases.ColorPicker)]
    [DoNotSyncDataType][BuiltInDataType]
    public class ColorPicker : IUmbracoNtextDataType
    {
        private Color _color = Color.Black;

		public static implicit operator ColorPicker(Color color)
		{
			return new ColorPicker() { Color = color };
		}

		public static implicit operator ColorPicker(string hex)
		{
			return new ColorPicker() { Color = ColorTranslator.FromHtml(hex.StartsWith("#") ? hex : "#" + hex) };
		}

		/// <summary>
		/// Gets the selected colour
		/// </summary>
		public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        /// <summary>
        /// Gets and sets the hex code of the selected color.
        /// A # is always prefixed to the returned value. Prefixing a # to input values is optional.
        /// </summary>
        public string HexCode
        {
            get
            {
                return "#" + Serialise();
            }
            set
            {
                _color = System.Drawing.ColorTranslator.FromHtml(value.StartsWith("#") ? value : "#" + value);
            }
        }

        /// <summary>
        /// Initialises the instance from a hex code without a # prefix
        /// </summary>
        public void Initialise(string dbValue)
        {
            if (string.IsNullOrWhiteSpace(dbValue))
            {
                Color = System.Drawing.Color.Empty;
            }
            else
            {
                try
                {
                    Color = System.Drawing.ColorTranslator.FromHtml("#" + dbValue);
                }
                catch
                {
                    Color = System.Drawing.Color.Empty;
                }
            }
        }

        /// <summary>
        /// Serialises the instance to a hex code without a # prefix
        /// </summary>
        public string Serialise()
        {
            return String.Format("{0:X2}{1:X2}{2:X2}", _color.R, _color.G, _color.B);
        }

        public override string ToString()
        {
            return HexCode;
        }
    }
}