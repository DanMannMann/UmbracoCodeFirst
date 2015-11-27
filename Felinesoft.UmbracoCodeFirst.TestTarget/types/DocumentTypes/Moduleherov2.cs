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
    [DocumentType(@"MODULE: Hero v2", @"ModuleHeroV2", null, @"icon-width", false, false, @"")]
    public class Moduleherov2 : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Subheader", @"subheader", false, @"", 0, false)]
            public Textstring Subheader { get; set; }

            [ContentProperty(@"Header", @"header", false, @"", 1, false)]
            public Textstring Header { get; set; }

            [ContentProperty(@"CTA 1 Text", @"cta1Text", false, @"", 2, false)]
            public Textstring Cta1text { get; set; }

            [ContentProperty(@"CTA 2 Text", @"cta2Text", false, @"", 3, false)]
            public Textstring Cta2text { get; set; }

            [ContentProperty(@"CTA 1 Url", @"cta1Url", false, @"", 4, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Cta1url { get; set; }

            [ContentProperty(@"CTA 2 Url", @"cta2Url", false, @"", 5, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Cta2url { get; set; }

            [ContentProperty(@"Background Image", @"backgroundImage", false, @"", 6, false)]
            public LegacyMediaPicker Backgroundimage { get; set; }

            [ContentProperty(@"Hero Background Colour", @"heroBackgroundColour", false, @"", 7, false)]
            public UmbracoCodeFirst.GeneratedTypes.ColourPicker Herobackgroundcolour { get; set; }

            [ContentProperty(@"Video", @"video", false, @"", 8, false)]
            public UmbracoCodeFirst.GeneratedTypes.VideoPicker Video { get; set; }

            [ContentProperty(@"Video Header", @"videoHeader", false, @"", 9, false)]
            public Textstring Videoheader { get; set; }

            [ContentProperty(@"Video Subheader", @"videoSubheader", false, @"", 10, false)]
            public Textstring Videosubheader { get; set; }

            [ContentProperty(@"Video Subject", @"videoSubject", false, @"", 11, false)]
            public Textstring Videosubject { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}