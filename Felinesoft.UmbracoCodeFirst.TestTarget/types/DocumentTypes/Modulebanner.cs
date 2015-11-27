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
    [DocumentType(@"MODULE: Banner", @"ModuleBanner", null, @".sprTreeFolder", false, false, @"")]
    [Template(true, "MODULE: Banner", "MODULEBanner")]
    public class Modulebanner : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Module heading", @"moduleHeading", false, @"", 0, false)]
            public Textstring Moduleheading { get; set; }

            [ContentProperty(@"Module subheading", @"moduleSubheading", false, @"", 1, false)]
            public Textstring Modulesubheading { get; set; }

            [ContentProperty(@"Button text", @"buttonText", false, @"", 5, false)]
            public Textstring Buttontext { get; set; }

            [ContentProperty(@"Button Url", @"buttonUrl", false, @"", 6, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Buttonurl { get; set; }

            [ContentProperty(@"Background colour", @"backgroundColour", false, @"", 4, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Backgroundcolour { get; set; }

            [ContentProperty(@"Button colour", @"buttonColour", false, @"", 7, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Buttoncolour { get; set; }

            [ContentProperty(@"Heading", @"heading", false, @"", 2, false)]
            public Textstring Heading { get; set; }

            [ContentProperty(@"Subheading", @"subHeading", false, @"", 3, false)]
            public Textstring Subheading { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}