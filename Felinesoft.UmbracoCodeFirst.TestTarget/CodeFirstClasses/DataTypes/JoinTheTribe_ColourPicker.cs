
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
    [DataType("Umbraco.ColorPickerAlias", "Join The Tribe  - Colour Picker")]
    [PreValue("0", @"4099ff")]
    [PreValue("1", @"3b5998")]
    [PreValue("2", @"58B947")]
    [PreValue("3", @"ffc425")]
    public class JoinTheTribe_ColourPicker : ColorPicker
    {
        }
}