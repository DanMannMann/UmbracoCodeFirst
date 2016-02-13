
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
    [DataType("Umbraco.ColorPickerAlias", "Colour Picker")]
    [PreValue("0", @"ffffff")]
    [PreValue("1", @"dddddd")]
    [PreValue("2", @"f7f7f7")]
    [PreValue("3", @"060606")]
    [PreValue("4", @"212121")]
    [PreValue("5", @"778d99")]
    [PreValue("6", @"283d50")]
    [PreValue("7", @"004d66")]
    [PreValue("8", @"006384")]
    [PreValue("9", @"00b4ae")]
    [PreValue("10", @"593371")]
    [PreValue("11", @"743f95")]
    [PreValue("12", @"21423a")]
    [PreValue("13", @"00a077")]
    [PreValue("14", @"32b57c")]
    [PreValue("15", @"585858")]
    public class ColourPicker : ColorPicker
    {
        }
}