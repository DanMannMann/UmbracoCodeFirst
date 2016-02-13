
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

namespace LMI.BusinessLogic.CodeFirst
{
    [DataType("Umbraco.TinyMCEv3", "Text With Link")]
    [PreValue("editor", @"{
  ""toolbar"": [
    ""bold"",
    ""italic"",
    ""underline"",
    ""strikethrough"",
    ""alignleft"",
    ""aligncenter"",
    ""alignright"",
    ""link""
  ],
  ""stylesheets"": [],
  ""dimensions"": {
    ""height"": 100
  }
}")]
    public class TextWithLink : RichtextEditor
    {
        }
}