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
    [DocumentType(@"MODULE: Benefits", @"MODULEBenefits", null, @".sprTreeFolder", false, false, @"")]
    public class Modulebenefits : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Hero image", @"heroImage", false, @"", 0, false)]
            public LegacyMediaPicker Heroimage { get; set; }

            [ContentProperty(@"Hero header", @"heroHeader", false, @"Max Character limit: 55", 1, false)]
            public Textstring Heroheader { get; set; }

            [ContentProperty(@"Tribe member header", @"tribeMemberHeader", true, @"Max Character limit: 60", 5, false)]
            public Textstring Tribememberheader { get; set; }

            [ContentProperty(@"Tribe member CTA Text", @"tribeMemberCTAText", false, @"Max Character limit: 34", 8, false)]
            public Textstring Tribememberctatext { get; set; }

            [ContentProperty(@"Second section subheader", @"secondSectionSubheader", false, @"Max Character limit: 64", 12, false)]
            public Textstring Secondsectionsubheader { get; set; }

            [ContentProperty(@"Second section bodytext", @"secondSectionBodytext", false, @"Max Character limit: 160", 14, false)]
            public Textstring Secondsectionbodytext { get; set; }

            [ContentProperty(@"Get more info header", @"getMoreInfoHeader", false, @"Max Character limit: 52

Please surround the text you want highlighted with curly braces e.g. 
Lorem {Ipsum} Dollar", 17, false)]
            public Textstring Getmoreinfoheader { get; set; }

            [ContentProperty(@"Get more info button text", @"getMoreInfoButtonText", false, @"Max Character limit: 22", 18, false)]
            public Textstring Getmoreinfobuttontext { get; set; }

            [ContentProperty(@"Get more info button Url", @"getMoreInfoButtonUrl", false, @"", 19, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Getmoreinfobuttonurl { get; set; }

            [ContentProperty(@"Hero Background colour", @"heroBackgroundColour", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Herobackgroundcolour { get; set; }

            [ContentProperty(@"Hero Header Colour", @"heroHeaderColour", false, @"", 4, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Heroheadercolour { get; set; }

            [ContentProperty(@"Tribe member header colour", @"tribeMemberHeaderColour", false, @"", 6, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Tribememberheadercolour { get; set; }

            [ContentProperty(@"Tribe Member Background Colour", @"tribeMemberBackgroundColour", false, @"", 7, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Tribememberbackgroundcolour { get; set; }

            [ContentProperty(@"Tribe member CTA Url Text", @"tribeMemberCTAUrlText", false, @"", 9, false)]
            public Textstring Tribememberctaurltext { get; set; }

            [ContentProperty(@"Tribe member CTA URL", @"tribeMemberCTAUrl", false, @"", 10, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Tribememberctaurl { get; set; }

            [ContentProperty(@"Tribe CTA Text Colour", @"tribeCTATextColour", false, @"", 11, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Tribectatextcolour { get; set; }

            [ContentProperty(@"Second section subheader Colour", @"secondSectionSubheaderColour", false, @"", 13, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Secondsectionsubheadercolour { get; set; }

            [ContentProperty(@"Second section bodytext Colour", @"secondSectionBodytextColour", false, @"", 15, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Secondsectionbodytextcolour { get; set; }

            [ContentProperty(@"Hero subheader", @"heroSubheader", false, @"", 2, false)]
            public RichtextEditor Herosubheader { get; set; }

        }
        public class AdvanceYourCareer1Tab : TabBase
        {
            [ContentProperty(@"Header", @"advanceYourCareerHeader1", true, @"Max Character limit: 32", 0, false)]
            public Textstring Advanceyourcareerheader1 { get; set; }

            [ContentProperty(@"Body text", @"advanceYourCareerbodyText1", true, @"Recommended Character limit: 105", 2, false)]
            public RichtextEditor Advanceyourcareerbodytext1 { get; set; }

            [ContentProperty(@"Header colour", @"advanceYourCareerHeaderColour1", false, @"", 1, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Advanceyourcareerheadercolour1 { get; set; }

            [ContentProperty(@"Bodytext colour", @"advanceYourCareerBodytextColour1", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Advanceyourcareerbodytextcolour1 { get; set; }

        }
        public class AdvanceYourCareer2Tab : TabBase
        {
            [ContentProperty(@"Header", @"advanceYourCareerHeader2", true, @"Max Character limit: 32", 0, false)]
            public Textstring Advanceyourcareerheader2 { get; set; }

            [ContentProperty(@"Body text", @"advanceYourCareerbodyText2", true, @"Recommended Character limit: 105", 2, false)]
            public RichtextEditor Advanceyourcareerbodytext2 { get; set; }

            [ContentProperty(@"Header Colour", @"advanceYourCareerHeaderColour2", false, @"", 1, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Advanceyourcareerheadercolour2 { get; set; }

            [ContentProperty(@"Body text colour", @"advanceYourCareerBodytextColour2", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Advanceyourcareerbodytextcolour2 { get; set; }

        }
        public class AdvanceYourCareer3Tab : TabBase
        {
            [ContentProperty(@"Header", @"advanceYourCareerHeader3", true, @"Max Character limit: 32", 0, false)]
            public Textstring Advanceyourcareerheader3 { get; set; }

            [ContentProperty(@"Body text", @"advanceYourCareerbodyText3", true, @"Recommended Character limit: 105", 2, false)]
            public RichtextEditor Advanceyourcareerbodytext3 { get; set; }

            [ContentProperty(@"Header colour", @"advanceYourCareerHeaderColour3", false, @"", 1, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Advanceyourcareerheadercolour3 { get; set; }

            [ContentProperty(@"Body text colour", @"advanceYourCareerBodytextColour3", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Advanceyourcareerbodytextcolour3 { get; set; }

        }
        public class Thumbnail1Tab : TabBase
        {
            [ContentProperty(@"Thumbnail header 1", @"thumbnailHeader1", true, @"Max Character limit: 26", 1, false)]
            public Textstring Thumbnailheader1 { get; set; }

            [ContentProperty(@"Thumbnail text 1", @"thumbnailText1", true, @"Max Character limit: 135", 2, false)]
            public Textstring Thumbnailtext1 { get; set; }

            [ContentProperty(@"Thumbnail Image 1", @"thumbnailImage1", true, @"", 0, false)]
            public LegacyMediaPicker Thumbnailimage1 { get; set; }

            [ContentProperty(@"Thumbnail 1 Link", @"thumbnail1Link", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Thumbnail1link { get; set; }

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
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Thumbnail2link { get; set; }

        }
        public class Thumbnail3Tab : TabBase
        {
            [ContentProperty(@"Thumbnail Image 3", @"thumbnailImage3", true, @"", 0, false)]
            public LegacyMediaPicker Thumbnailimage3 { get; set; }

            [ContentProperty(@"Thumbnail text 3", @"thumbnailText3", true, @"Max Character limit: 135", 2, false)]
            public Textstring Thumbnailtext3 { get; set; }

            [ContentProperty(@"Thumbnail Header 3", @"thumbnailHeader3", true, @"Max Character limit: 26", 1, false)]
            public Textstring Thumbnailheader3 { get; set; }

            [ContentProperty(@"Thumbnail 3 Link", @"thumbnail3Link", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Thumbnail3link { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }

        [ContentTab(@"Advance Your Career 1", 4)]
        public AdvanceYourCareer1Tab AdvanceYourCareer1 { get; set; }

        [ContentTab(@"Advance Your Career 2", 5)]
        public AdvanceYourCareer2Tab AdvanceYourCareer2 { get; set; }

        [ContentTab(@"Advance Your Career 3", 6)]
        public AdvanceYourCareer3Tab AdvanceYourCareer3 { get; set; }

        [ContentTab(@"Thumbnail 1", 1)]
        public Thumbnail1Tab Thumbnail1 { get; set; }

        [ContentTab(@"Thumbnail 2", 2)]
        public Thumbnail2Tab Thumbnail2 { get; set; }

        [ContentTab(@"Thumbnail 3", 3)]
        public Thumbnail3Tab Thumbnail3 { get; set; }
    }
}