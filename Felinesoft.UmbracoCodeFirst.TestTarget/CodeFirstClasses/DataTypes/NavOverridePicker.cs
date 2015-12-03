
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
    [DataType("Umbraco.DropDown", "Nav Override Picker")]
    [PreValue("0", @"Classes Landing Page")]
    [PreValue("1", @"Club Landing Page")]
    [PreValue("2", @"Instructors Landing Page")]
    [PreValue("3", @"News Landing Page")]
    [PreValue("4", @"Community Landing Page")]
    [PreValue("5", @"Shop Landing Page")]
    public class NavOverridePicker : DropdownList
    {
        }
}