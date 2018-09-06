
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

namespace LMI.BusinessLogic.CodeFirst
{
    [DataType("Umbraco.TinyMCEv3", "Small Rich Text")]
    [PreValue("editor", @"{
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