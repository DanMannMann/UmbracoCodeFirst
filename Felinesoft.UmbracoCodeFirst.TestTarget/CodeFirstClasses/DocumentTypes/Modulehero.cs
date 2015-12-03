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
    [DocumentType(@"MODULE: Hero", @"MODULEHero", null, @".sprTreeFolder", false, false, @"")]
    public class Modulehero : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Hero heading", @"heroHeading", false, @"Max Character limit: 42", 0, false)]
            public Textstring Heroheading { get; set; }

            [ContentProperty(@"Hero subheading", @"heroSubheading", false, @"Max Character limit: 120", 2, false)]
            public Textstring Herosubheading { get; set; }

            [ContentProperty(@"Button", @"button", false, @"Max Character limit: 22", 3, false)]
            public Textstring Button { get; set; }

            [ContentProperty(@"Background image", @"backgroundImage", false, @"", 9, false)]
            public LegacyMediaPicker Backgroundimage { get; set; }

            [ContentProperty(@"Button Url", @"buttonUrl", false, @"", 4, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Buttonurl { get; set; }

            [ContentProperty(@"Background Colour", @"backgroundColour", false, @"", 10, false)]
            public LMI.BusinessLogic.CodeFirst.ColourPicker Backgroundcolour { get; set; }

            [ContentProperty(@"Hero header colour", @"heroHeaderColour", false, @"", 1, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Heroheadercolour { get; set; }

            [ContentProperty(@"Instructor text", @"instructorText", false, @"Max Character limit: 36", 5, false)]
            public Textstring Instructortext { get; set; }

            [ContentProperty(@"Instructor CTA", @"instructorCTA", false, @"Max Character limit: 10", 6, false)]
            public Textstring Instructorcta { get; set; }

            [ContentProperty(@"CTA Colour", @"cTAColour", false, @"", 8, false)]
            public LMI.BusinessLogic.CodeFirst.ColourPicker Ctacolour { get; set; }

            [ContentProperty(@"Video", @"video", false, @"", 11, false)]
            public LegacyMediaPicker Video { get; set; }

            [ContentProperty(@"Video subheader", @"videoSubheader", false, @"Max Character limit: 125", 12, false)]
            public Textstring Videosubheader { get; set; }

            [ContentProperty(@"Video header", @"videoHeader", false, @"Max Character limit: 24", 13, false)]
            public Textstring Videoheader { get; set; }

            [ContentProperty(@"Video Subject", @"videoSubject", false, @"Max Character limit: 125", 14, false)]
            public Textstring Videosubject { get; set; }

            [ContentProperty(@"Instructor CTA URL", @"instructorCtaUrl", false, @"", 7, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Instructorctaurl { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}