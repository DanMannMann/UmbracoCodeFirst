
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
    [DataType("Umbraco.RadioButtonList", "Article Promo Picker")]
    [PreValue("1", @"Article")]
    [PreValue("2", @"Promo")]
    public class ArticlePromoPicker : RadioButtonList
    {
        }
}