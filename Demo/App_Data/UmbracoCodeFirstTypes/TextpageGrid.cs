
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

namespace Demo.GeneratedUmbracoTypes
{
    [DataType("Umbraco.Grid", "Textpage Grid", null, DataTypeDatabaseType.Ntext)]
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
    ""height"": 500
  },
  ""maxImageSize"": 500
}")]
    [PreValue("2", @"{
  ""styles"": [
    {
      ""label"": ""Set a background image"",
      ""description"": ""Set a row background"",
      ""key"": ""background-image"",
      ""view"": ""imagepicker"",
      ""modifier"": ""url({0})""
    }
  ],
  ""config"": [
    {
      ""label"": ""Class"",
      ""description"": ""Set a css class"",
      ""key"": ""class"",
      ""view"": ""textstring""
    }
  ],
  ""columns"": 12,
  ""templates"": [
    {
      ""name"": ""1 column layout"",
      ""sections"": [
        {
          ""grid"": 12
        }
      ]
    }
  ],
  ""layouts"": [
    {
      ""name"": ""Headline"",
      ""areas"": [
        {
          ""grid"": 12,
          ""editors"": [
            ""headline""
          ]
        }
      ]
    },
    {
      ""name"": ""Article"",
      ""areas"": [
        {
          ""grid"": 4
        },
        {
          ""grid"": 8
        }
      ]
    },
    {
      ""name"": ""3col"",
      ""areas"": [
        {
          ""grid"": 4
        },
        {
          ""grid"": 4
        },
        {
          ""grid"": 4
        }
      ]
    }
  ]
}")]
    public class TextpageGrid : GridBase
    {
        }
}