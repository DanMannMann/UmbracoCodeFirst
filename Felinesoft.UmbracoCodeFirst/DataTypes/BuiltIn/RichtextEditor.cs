
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
using System.Web.Mvc;
using System.Web;

namespace Marsman.UmbracoCodeFirst.DataTypes.BuiltIn
{
    [DataType("Umbraco.TinyMCEv3", "Richtext editor")]
    [DoNotSyncDataType][BuiltInDataType]
    public class RichtextEditor : IHtmlString, IUmbracoNtextDataType
    {
        private string _raw;

		public static implicit operator RichtextEditor(string value)
		{
			return new RichtextEditor() { Value = value };
		}

		public string Value
        {
            get
            {
                return _raw;
            }
            set
            {
                _raw = value;
            }
        }

        public void Initialise(string dbValue)
        {
            _raw = dbValue;
        }

        public string Serialise()
        {
            return _raw;
        }

        public override string ToString()
        {
            return _raw;
        }

        public string ToHtmlString()
        {
            var toAdd = DataTypeUtils.GetHtmlTagContentFromContextualAttributes(this);
			return "<div" + toAdd + ">" + _raw + "</div>";
        }
    }
}