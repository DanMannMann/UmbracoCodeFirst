
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
    [DataType("Umbraco.DropDownMultiple", "SEO Change Frequency")]
    [PreValue("1", @"Never")]
    [PreValue("2", @"Yearly")]
    [PreValue("3", @"Monthly")]
    [PreValue("4", @"Weekly")]
    [PreValue("5", @"Daily")]
    [PreValue("6", @"Hourly")]
    [PreValue("7", @"Always")]
    public class SEOChangeFrequency : DropdownMultiple
    {
        }
}