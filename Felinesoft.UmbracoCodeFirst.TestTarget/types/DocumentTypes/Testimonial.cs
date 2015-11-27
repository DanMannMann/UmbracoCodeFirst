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
    [DocumentType(@"Testimonial", @"Testimonial", new Type[] { typeof(Modules) }, @".sprTreeFolder", false, false, @"")]
    [Template(true, "Webpage", "Webpage")]
    public class Testimonial : Seopage
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Testimonial Name", @"testimonialName", true, @"Maximum length 32 characters", 0, false)]
            public Textstring Testimonialname { get; set; }

            [ContentProperty(@"Testimonial Role", @"testimonialRole", true, @"Maximum length 32 characters", 1, false)]
            public Textstring Testimonialrole { get; set; }

            [ContentProperty(@"Testimonial Quote", @"testimonialQuote", true, @"Maximum length 110 characters", 3, false)]
            public Textstring Testimonialquote { get; set; }

            [ContentProperty(@"Testimonial CTA", @"testimonialCTA", false, @"Maximum length 30 characters", 5, false)]
            public Textstring Testimonialcta { get; set; }

            [ContentProperty(@"Testimonial CTA Link", @"testimonialCTALink", false, @"", 6, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Testimonialctalink { get; set; }

            [ContentProperty(@"Testimonial CTA Video", @"testimonialCTAVideo", false, @"", 8, false)]
            public LegacyMediaPicker Testimonialctavideo { get; set; }

            [ContentProperty(@"Testimonial Image", @"testimonialImage", false, @"", 2, false)]
            public LegacyMediaPicker Testimonialimage { get; set; }

            [ContentProperty(@"Testimonial CTA Link Text", @"TestimonialCTALinkText", false, @"", 7, false)]
            public Textstring Testimonialctalinktext { get; set; }

            [ContentProperty(@"Background colour", @"testimonialBackgroundColour", false, @"", 4, false)]
            public UmbracoCodeFirst.GeneratedTypes.ColourPicker Testimonialbackgroundcolour { get; set; }

        }
        public class MenuTab : TabBase
        {
            [ContentProperty(@"Hide In Sub Nav", @"hideInSubNav", false, @"Hide this from sub navigation", 0, false)]
            public TrueFalse Hideinsubnav { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }

        [ContentTab(@"Menu", 1)]
        public MenuTab Menu { get; set; }
    }
}