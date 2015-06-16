using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo.DataTypes
{
    [DataType(BuiltInPropertyEditorAliases.Grid, "Textpage Grid", null, Umbraco.Core.Models.DataTypeDatabaseType.Ntext)]
    [PreValueFile("~/App_Data/TextpagePreValues.xml")]
    #region PreValues
//    [PreValue("1", @"{
//  ""styles"": [
//    {
//      ""label"": ""Set a background image"",
//      ""description"": ""Set a row background"",
//      ""key"": ""background-image"",
//      ""view"": ""imagepicker"",
//      ""modifier"": ""url({0})""
//    }
//  ],
//  ""config"": [
//    {
//      ""label"": ""Class"",
//      ""description"": ""Set a css class"",
//      ""key"": ""class"",
//      ""view"": ""textstring""
//    }
//  ],
//  ""columns"": 12,
//  ""templates"": [
//    {
//      ""name"": ""1 column layout"",
//      ""sections"": [
//        {
//          ""grid"": 12
//        }
//      ]
//    }
//  ],
//  ""layouts"": [
//    {
//      ""name"": ""Headline"",
//      ""areas"": [
//        {
//          ""grid"": 12,
//          ""editors"": [
//            ""headline""
//          ]
//        }
//      ]
//    },
//    {
//      ""name"": ""Article"",
//      ""areas"": [
//        {
//          ""grid"": 4
//        },
//        {
//          ""grid"": 8
//        }
//      ]
//    },
//    {
//      ""name"": ""3col"",
//      ""areas"": [
//        {
//          ""grid"": 4
//        },
//        {
//          ""grid"": 4
//        },
//        {
//          ""grid"": 4
//        }
//      ]
//    }
//  ]
//}")]
//    [PreValue("2", @"{
//  ""toolbar"": [
//    ""code"",
//    ""styleselect"",
//    ""bold"",
//    ""italic"",
//    ""alignleft"",
//    ""aligncenter"",
//    ""alignright"",
//    ""bullist"",
//    ""numlist"",
//    ""outdent"",
//    ""indent"",
//    ""link"",
//    ""umbmediapicker"",
//    ""umbmacro"",
//    ""umbembeddialog""
//  ],
//  ""stylesheets"": [],
//  ""dimensions"": {
//    ""height"": 500
//  },
//  ""maxImageSize"": 500
//}")]
    #endregion
    public class TextpageGrid : GridBase
    {
    }
}