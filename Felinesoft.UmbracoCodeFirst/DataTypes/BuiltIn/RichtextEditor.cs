
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
using System.Web.Mvc;
using System.Web;

namespace Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn
{
    [DataType("Umbraco.TinyMCEv3", "Richtext editor", null, DataTypeDatabaseType.Ntext)]
    [BuiltInDataType]
    public class RichtextEditor : IUmbracoNtextDataType<HtmlString>
    {
        private HtmlString _html;

        public HtmlString Html
        {
            get { return _html; }
            set { _html = value; }
        }

        public void Initialise(HtmlString dbValue)
        {
            _html = dbValue;
        }

        public HtmlString Serialise()
        {
            return _html;
        }

        public override string ToString()
        {
            return _html.ToString();
        }
    }
}