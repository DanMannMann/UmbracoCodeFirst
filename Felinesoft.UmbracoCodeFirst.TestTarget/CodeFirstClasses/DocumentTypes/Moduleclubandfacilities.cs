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
    [DocumentType(@"MODULE: Club and Facilities", @"MODULEClubAndFacilities", null, @".sprTreeFolder", false, false, @"")]
    public class Moduleclubandfacilities : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Background image", @"backgroundImage", false, @"", 2, false)]
            public LegacyMediaPicker Backgroundimage { get; set; }

            [ContentProperty(@"Headline", @"heading", true, @"Max Character limit: 39", 3, false)]
            public Textstring Heading { get; set; }

            [ContentProperty(@"Subheading", @"subheading", false, @"Max Character limit: 110", 4, false)]
            public Textstring Subheading { get; set; }

            [ContentProperty(@"Clubs Text", @"text", false, @"Max Character Limit: 20", 7, false)]
            public Textstring Text { get; set; }

            [ContentProperty(@"Clubs CTA", @"cTA", true, @"Max Character limit:  20", 8, false)]
            public Textstring Cta { get; set; }

            [ContentProperty(@"Module heading", @"moduleHeading", false, @"Max Character limit: 54", 0, false)]
            public Textstring Moduleheading { get; set; }

            [ContentProperty(@"Module subheading", @"moduleSubheading", false, @"Max Character limit: 79", 1, false)]
            public Textstring Modulesubheading { get; set; }

            [ContentProperty(@"Button", @"buttonText", false, @"", 5, false)]
            public Textstring Buttontext { get; set; }

            [ContentProperty(@"Button Url", @"buttonUrl", false, @"", 6, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Buttonurl { get; set; }

            [ContentProperty(@"Clubs CTA URL", @"clubsCTAURL", false, @"", 9, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Clubsctaurl { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}