
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
    [DataType("Umbraco.ColorPickerAlias", "Black or White")]
    [PreValue("1", @"000000")]
    [PreValue("2", @"ffffff")]
    public class BlackOrWhite : ColorPicker
    {
        }
}