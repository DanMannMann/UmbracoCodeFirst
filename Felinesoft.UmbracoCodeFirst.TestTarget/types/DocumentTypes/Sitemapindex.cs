using Felinesoft.UmbracoCodeFirst;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;

namespace UmbracoCodeFirst.GeneratedTypes
{
    [DocumentType(@"Sitemap Index", @"SitemapIndex", null, @"icon-sitemap", false, false, @"")]
    public class Sitemapindex : Master
    {
        public class ContentTab : TabBase
        {
        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}