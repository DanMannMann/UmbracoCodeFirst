
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
    [DataType("Umbraco.DropDownMultiple", "SEO Change Frequency")]
    [PreValue("0", @"Never")]
    [PreValue("1", @"Yearly")]
    [PreValue("2", @"Monthly")]
    [PreValue("3", @"Weekly")]
    [PreValue("4", @"Daily")]
    [PreValue("5", @"Hourly")]
    [PreValue("6", @"Always")]
    public class SEOChangeFrequency : DropdownMultiple
    {
        }
}