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
    [DocumentType(@"MODULE: On Demand Images", @"MODULEOnDemandImages", null, @".sprTreeFolder", false, false, @"")]
    public class Moduleondemandimages : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Tribe member header", @"tribeMemberHeader", true, @"Max Character limit: 60", 0, false)]
            public Textstring Tribememberheader { get; set; }

            [ContentProperty(@"Tribe member CTA Text", @"tribeMemberCTAText", false, @"Max Character limit: 34", 1, false)]
            public Textstring Tribememberctatext { get; set; }

            [ContentProperty(@"Tribe member header colour", @"tribeMemberHeaderColour", false, @"", 2, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Tribememberheadercolour { get; set; }

            [ContentProperty(@"Tribe Member Background Colour", @"tribeMemberBackgroundColour", false, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Tribememberbackgroundcolour { get; set; }

            [ContentProperty(@"Tribe member CTA Url Text", @"tribeMemberCTAUrlText", false, @"", 4, false)]
            public Textstring Tribememberctaurltext { get; set; }

            [ContentProperty(@"Tribe member CTA URL", @"tribeMemberCTAUrl", false, @"", 5, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Tribememberctaurl { get; set; }

            [ContentProperty(@"Tribe CTA Text Colour", @"tribeCTATextColour", false, @"", 6, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Tribectatextcolour { get; set; }

        }
        public class Thumbnail1Tab : TabBase
        {
            [ContentProperty(@"Thumbnail header 1", @"thumbnailHeader1", true, @"Max Character limit: 26", 0, false)]
            public Textstring Thumbnailheader1 { get; set; }

            [ContentProperty(@"Thumbnail text 1", @"thumbnailText1", true, @"Max Character limit: 135", 1, false)]
            public Textstring Thumbnailtext1 { get; set; }

            [ContentProperty(@"Thumbnail Image 1", @"thumbnailImage1", true, @"", 2, false)]
            public LegacyMediaPicker Thumbnailimage1 { get; set; }

            [ContentProperty(@"Thumbnail 1 Link", @"thumbnail1Link", false, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Thumbnail1link { get; set; }

        }
        public class Thumbnail2Tab : TabBase
        {
            [ContentProperty(@"Thumbnail Image 2", @"thumbnailImage2", true, @"", 0, false)]
            public LegacyMediaPicker Thumbnailimage2 { get; set; }

            [ContentProperty(@"Thumbnail Header 2", @"thumbnailHeader2", true, @"Max Character limit: 26", 1, false)]
            public Textstring Thumbnailheader2 { get; set; }

            [ContentProperty(@"Thumbnail text 2", @"thumbnailText2", true, @"Max Character limit: 135", 2, false)]
            public Textstring Thumbnailtext2 { get; set; }

            [ContentProperty(@"Thumbnail 2 Link", @"thumbnail2Link", false, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Thumbnail2link { get; set; }

        }
        public class Thumbnail3Tab : TabBase
        {
            [ContentProperty(@"Thumbnail Image 3", @"thumbnailImage3", true, @"", 0, false)]
            public LegacyMediaPicker Thumbnailimage3 { get; set; }

            [ContentProperty(@"Thumbnail text 3", @"thumbnailText3", true, @"Max Character limit: 135", 1, false)]
            public Textstring Thumbnailtext3 { get; set; }

            [ContentProperty(@"Thumbnail Header 3", @"thumbnailHeader3", true, @"Max Character limit: 26", 2, false)]
            public Textstring Thumbnailheader3 { get; set; }

            [ContentProperty(@"Thumbnail 3 Link", @"thumbnail3Link", false, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Thumbnail3link { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }

        [ContentTab(@"Thumbnail 1", 1)]
        public Thumbnail1Tab Thumbnail1 { get; set; }

        [ContentTab(@"Thumbnail 2", 2)]
        public Thumbnail2Tab Thumbnail2 { get; set; }

        [ContentTab(@"Thumbnail 3", 3)]
        public Thumbnail3Tab Thumbnail3 { get; set; }
    }
}