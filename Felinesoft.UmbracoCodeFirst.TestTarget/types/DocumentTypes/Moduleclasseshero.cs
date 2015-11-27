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
    [DocumentType(@"MODULE: Class - Hero", @"MODULEClassesHero", null, @".sprTreeFolder", false, false, @"")]
    public class Moduleclasseshero : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Button Text", @"heroButtonText", true, @"Max Character limit: 22", 9, false)]
            public Textstring Herobuttontext { get; set; }

            [ContentProperty(@"Background image", @"backgroundImage", false, @"", 8, false)]
            public LegacyMediaPicker Backgroundimage { get; set; }

            [ContentProperty(@"Hero heading", @"heroHeading", true, @"Max Character limit: 25", 0, false)]
            public Textstring Heroheading { get; set; }

            [ContentProperty(@"CTA 1 Text", @"heroCTA1Text", false, @"Max Character limit: 38", 11, false)]
            public Textstring Herocta1text { get; set; }

            [ContentProperty(@"CTA 2 Text", @"heroCTA2Text", false, @"Max Character limit: 38", 14, false)]
            public Textstring Herocta2text { get; set; }

            [ContentProperty(@"CTA 1 Url Text", @"heroCTA1UrlText", false, @"Max Character limit: 38", 12, false)]
            public Textstring Herocta1urltext { get; set; }

            [ContentProperty(@"CTA 2 Url Text", @"heroCTA2UrlText", false, @"Max Character limit: 38", 15, false)]
            public Textstring Herocta2urltext { get; set; }

            [ContentProperty(@"Hero subheading", @"heroSubheading", true, @"Max Character limit: 65", 2, false)]
            public Textstring Herosubheading { get; set; }

            [ContentProperty(@"Background colour", @"heroBackgroundColour", false, @"", 4, false)]
            public UmbracoCodeFirst.GeneratedTypes.ColourPicker Herobackgroundcolour { get; set; }

            [ContentProperty(@"Footer Background Colour", @"heroFooterBackgroundColour", false, @"", 5, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Herofooterbackgroundcolour { get; set; }

            [ContentProperty(@"Footer Item Background Colour", @"heroFooterItemBackgroundColour", false, @"", 6, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Herofooteritembackgroundcolour { get; set; }

            [ContentProperty(@"Button Url", @"heroButtonUrl", true, @"", 7, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Herobuttonurl { get; set; }

            [ContentProperty(@"CTA 1 Url", @"heroCTA1Url", false, @"", 13, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Herocta1url { get; set; }

            [ContentProperty(@"CTA 2 Url", @"heroCTA2Url", false, @"", 16, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Herocta2url { get; set; }

            [ContentProperty(@"Hero Heading Colour", @"heroHeadingColour", true, @"", 1, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Heroheadingcolour { get; set; }

            [ContentProperty(@"Hero Subheading Colour", @"heroSubheadingColour", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Herosubheadingcolour { get; set; }

            [ContentProperty(@"Class Summary Tick Icon", @"classSummaryTickIcon", false, @"", 10, false)]
            public LegacyMediaPicker Classsummarytickicon { get; set; }

        }
        public class ClassSummary1Tab : TabBase
        {
            [ContentProperty(@"Heading", @"heroClassSummary1Heading", true, @"Max Character limit: 35", 0, false)]
            public Textstring Heroclasssummary1heading { get; set; }

            [ContentProperty(@"Subheading", @"heroClassSummary1Subheading", true, @"Max Character limit: 17", 1, false)]
            public Textstring Heroclasssummary1subheading { get; set; }

        }
        public class ClassSummary2Tab : TabBase
        {
            [ContentProperty(@"Heading", @"heroClassSummary2Heading", true, @"Max Character limit: 35", 0, false)]
            public Textstring Heroclasssummary2heading { get; set; }

            [ContentProperty(@"Subheading", @"heroClassSummary2Subheading", true, @"Max Character limit: 17", 1, false)]
            public Textstring Heroclasssummary2subheading { get; set; }

        }
        public class ClassSummary3Tab : TabBase
        {
            [ContentProperty(@"Heading", @"heroClassSummary3Heading", true, @"Max Character limit: 35", 0, false)]
            public Textstring Heroclasssummary3heading { get; set; }

            [ContentProperty(@"Subheading", @"heroClassSummary3Subheading", true, @"Max Character limit: 17", 1, false)]
            public Textstring Heroclasssummary3subheading { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }

        [ContentTab(@"Class Summary 1", 1)]
        public ClassSummary1Tab ClassSummary1 { get; set; }

        [ContentTab(@"Class Summary 2", 2)]
        public ClassSummary2Tab ClassSummary2 { get; set; }

        [ContentTab(@"Class Summary 3", 3)]
        public ClassSummary3Tab ClassSummary3 { get; set; }
    }
}