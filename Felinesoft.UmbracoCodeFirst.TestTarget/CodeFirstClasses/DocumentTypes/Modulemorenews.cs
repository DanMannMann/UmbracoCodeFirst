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
    [DocumentType(@"MODULE: More News", @"ModuleMoreNews", null, @".sprTreeFolder", false, false, @"")]
    [Template(true, "MODULE: More News", "MODULEMoreNews1")]
    public class Modulemorenews : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"CTA Text", @"ctaText", true, @"", 2, false)]
            public Textstring Ctatext { get; set; }

            [ContentProperty(@"CTA Url", @"ctaUrl", true, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Ctaurl { get; set; }

            [ContentProperty(@"Article 1", @"article1", true, @"", 4, false)]
            public LMI.BusinessLogic.CodeFirst.ArticlePicker Article1 { get; set; }

            [ContentProperty(@"Article 2", @"article2", true, @"", 5, false)]
            public LMI.BusinessLogic.CodeFirst.ArticlePicker Article2 { get; set; }

            [ContentProperty(@"Article 3", @"article3", true, @"", 6, false)]
            public LMI.BusinessLogic.CodeFirst.ArticlePicker Article3 { get; set; }

            [ContentProperty(@"Article 4", @"article4", true, @"", 7, false)]
            public LMI.BusinessLogic.CodeFirst.ArticlePicker Article4 { get; set; }

            [ContentProperty(@"Header", @"header", true, @"Max character length 25", 0, false)]
            public Textstring Header { get; set; }

            [ContentProperty(@"Sub Header", @"subHeader", false, @"Max character length 65", 1, false)]
            public Textstring Subheader { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}