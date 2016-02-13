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
    [DocumentType(@"MODULE: Instructor", @"MODULEInstructor", null, @"icon-operator", false, false, @"")]
    [Template(true, "MODULE_Instructor", "MODULE_Instructor")]
    public class Moduleinstructor : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Button", @"button", true, @"Max Character limit: 22", 4, false)]
            public Textstring Button { get; set; }

            [ContentProperty(@"Instructor text", @"instructorText", true, @"Max Character limit: 36", 6, false)]
            public Textstring Instructortext { get; set; }

            [ContentProperty(@"Instructor CTA", @"instructorCTA", true, @"Max Character limit: 10", 7, false)]
            public Textstring Instructorcta { get; set; }

            [ContentProperty(@"Instructor CTA URL", @"instructorCTAURL", false, @"", 8, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Instructorctaurl { get; set; }

            [ContentProperty(@"Background image", @"backgroundImage", false, @"", 10, false)]
            public LegacyMediaPicker Backgroundimage { get; set; }

            [ContentProperty(@"Background colour", @"backgroundColour", false, @"", 11, false)]
            public UmbracoCodeFirst.GeneratedTypes.ColourPicker Backgroundcolour { get; set; }

            [ContentProperty(@"Video header", @"videoHeader", false, @"Max Character limit: 24", 14, false)]
            public Textstring Videoheader { get; set; }

            [ContentProperty(@"Video subheader", @"videoSubheader", false, @"Max Character limit: 125", 13, false)]
            public Textstring Videosubheader { get; set; }

            [ContentProperty(@"Video", @"video", false, @"", 12, false)]
            public LegacyMediaPicker Video { get; set; }

            [ContentProperty(@"Hero heading", @"heroHeading", true, @"Max Character limit: 28", 2, false)]
            public Textstring Heroheading { get; set; }

            [ContentProperty(@"Hero subheading", @"heroSubheading", false, @"Max Character limit: 70", 3, false)]
            public Textstring Herosubheading { get; set; }

            [ContentProperty(@"Module heading", @"moduleHeading", false, @"Max Character limit: 54", 0, false)]
            public Textstring Moduleheading { get; set; }

            [ContentProperty(@"Module subheading", @"moduleSubheading", false, @"Max Character limit: 79", 1, false)]
            public Textstring Modulesubheading { get; set; }

            [ContentProperty(@"Video Subject", @"videoSubject", false, @"Max Character limit: 125", 15, false)]
            public Textstring Videosubject { get; set; }

            [ContentProperty(@"Button Url", @"buttonUrl", false, @"", 5, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Buttonurl { get; set; }

            [ContentProperty(@"CTA Colour", @"cTAColour", false, @"", 9, false)]
            public UmbracoCodeFirst.GeneratedTypes.ColourPicker Ctacolour { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}