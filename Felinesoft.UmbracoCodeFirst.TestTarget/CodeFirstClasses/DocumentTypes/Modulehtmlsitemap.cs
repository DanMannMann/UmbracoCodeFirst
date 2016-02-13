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

namespace LMI.BusinessLogic.CodeFirst
{
    [DocumentType(@"MODULE: HTML Sitemap", @"ModuleHtmlSitemap", null, @".sprTreeFolder", false, false, @"")]
    public class Modulehtmlsitemap : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Heading", @"heading", true, @"", 0, false)]
            public Textstring Heading { get; set; }

            [ContentProperty(@"Search Text", @"searchText", true, @"", 1, false)]
            public Textstring Searchtext { get; set; }

            [ContentProperty(@"Search Go Button Text", @"searchGoButtonText", true, @"", 2, false)]
            public Textstring Searchgobuttontext { get; set; }

            [ContentProperty(@"CTA Text", @"ctaText", false, @"", 3, false)]
            public Textstring Ctatext { get; set; }

            [ContentProperty(@"CTA Link", @"ctaLink", false, @"", 4, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Ctalink { get; set; }

            [ContentProperty(@"CTA Link Text", @"ctaLinkText", false, @"", 5, false)]
            public Textstring Ctalinktext { get; set; }

            [ContentProperty(@"Classes Title", @"classesTitle", false, @"", 6, false)]
            public Textstring Classestitle { get; set; }

            [ContentProperty(@"Sitemap Sections", @"sitemapSections", false, @"Pick the parent pages that you want to show on the HTML sitemap", 7, false)]
            public LMI.BusinessLogic.CodeFirst.MultipleNodeHTMLSitemapPicker Sitemapsections { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}