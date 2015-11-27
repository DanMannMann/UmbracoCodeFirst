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
    [DocumentType(@"MODULE: Shop", @"MODULEShop", null, @"icon-shopping-basket", false, false, @"")]
    [Template(true, "MODULE_Shop", "MODULE_Shop")]
    public class Moduleshop : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Module heading", @"moduleHeading", true, @"Max Character limit: 54", 0, false)]
            public Textstring Moduleheading { get; set; }

            [ContentProperty(@"Module subheading", @"moduleSubheading", true, @"Max Character limit: 79", 1, false)]
            public Textstring Modulesubheading { get; set; }

        }
        public class LargePromoTab : TabBase
        {
            [ContentProperty(@"Heading", @"largePromoAreaHeading", false, @"Max Character limit: 56", 2, false)]
            public Textstring Largepromoareaheading { get; set; }

            [ContentProperty(@"Subheading", @"largePromoAreaSubheading", false, @"Max Character limit: 125", 3, false)]
            public Textstring Largepromoareasubheading { get; set; }

            [ContentProperty(@"Button", @"largePromoAreaButton", true, @"Max Character limit: 22", 4, false)]
            public Textstring Largepromoareabutton { get; set; }

            [ContentProperty(@"Image", @"largePromoAreaImage", true, @"", 0, false)]
            public LegacyMediaPicker Largepromoareaimage { get; set; }

            [ContentProperty(@"Background colour", @"largePromoBackgroundColour", false, @"", 1, false)]
            public UmbracoCodeFirst.GeneratedTypes.ColourPicker Largepromobackgroundcolour { get; set; }

            [ContentProperty(@"Button Url", @"largePromoAreaButtonUrl", false, @"", 5, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Largepromoareabuttonurl { get; set; }

        }
        public class SmallPromoTab : TabBase
        {
            [ContentProperty(@"Image", @"smallPromoAreaImage", true, @"", 0, false)]
            public LegacyMediaPicker Smallpromoareaimage { get; set; }

            [ContentProperty(@"Background colour", @"smallPromoBackgroundColour", false, @"", 1, false)]
            public UmbracoCodeFirst.GeneratedTypes.ColourPicker Smallpromobackgroundcolour { get; set; }

            [ContentProperty(@"Heading", @"smallPromoAreaHeading", false, @"Max Character limit: 65", 2, false)]
            public Textstring Smallpromoareaheading { get; set; }

            [ContentProperty(@"Subheading", @"smallPromoAreaSubheading", false, @"Max Character limit: 84", 3, false)]
            public Textstring Smallpromoareasubheading { get; set; }

            [ContentProperty(@"Button", @"smallPromoAreaButton", true, @"Max Character limit: 36", 4, false)]
            public Textstring Smallpromoareabutton { get; set; }

            [ContentProperty(@"Button Url", @"smallPromoAreaButtonUrl", false, @"", 5, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Smallpromoareabuttonurl { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }

        [ContentTab(@"Large Promo", 1)]
        public LargePromoTab LargePromo { get; set; }

        [ContentTab(@"Small Promo", 2)]
        public SmallPromoTab SmallPromo { get; set; }
    }
}