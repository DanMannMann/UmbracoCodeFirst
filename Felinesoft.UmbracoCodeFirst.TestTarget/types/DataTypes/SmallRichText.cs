
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
    [DataType("Umbraco.TinyMCEv3", "Small Rich Text")]
    [PreValue("1", @"{
  ""toolbar"": [
    ""code"",
    ""styleselect"",
    ""bold"",
    ""italic"",
    ""alignleft"",
    ""aligncenter"",
    ""alignright"",
    ""bullist"",
    ""numlist"",
    ""outdent"",
    ""indent"",
    ""link"",
    ""umbmediapicker"",
    ""umbmacro"",
    ""umbembeddialog""
  ],
  ""stylesheets"": [],
  ""dimensions"": {
    ""height"": 50,
    ""width"": 600
  }
}")]
    public class SmallRichText : RichtextEditor
    {
        }
}