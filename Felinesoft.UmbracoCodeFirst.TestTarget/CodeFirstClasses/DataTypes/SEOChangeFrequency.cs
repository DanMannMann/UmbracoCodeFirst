
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