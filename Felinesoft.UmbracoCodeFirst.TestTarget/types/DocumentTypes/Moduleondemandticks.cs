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
    [DocumentType(@"MODULE: On Demand Ticks", @"MODULEOnDemandTicks", null, @".sprTreeFolder", false, false, @"")]
    public class Moduleondemandticks : Modules
    {
        public class AdvanceYourCareer1Tab : TabBase
        {
            [ContentProperty(@"Header", @"advanceYourCareerHeader1", true, @"Max Character limit: 32", 0, false)]
            public Textstring Advanceyourcareerheader1 { get; set; }

            [ContentProperty(@"Body text", @"advanceYourCareerbodyText1", true, @"Recommended Character limit: 105", 1, false)]
            public RichtextEditor Advanceyourcareerbodytext1 { get; set; }

            [ContentProperty(@"Header colour", @"advanceYourCareerHeaderColour1", false, @"", 2, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Advanceyourcareerheadercolour1 { get; set; }

            [ContentProperty(@"Bodytext colour", @"advanceYourCareerBodytextColour1", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Advanceyourcareerbodytextcolour1 { get; set; }

        }
        public class AdvanceYourCareer2Tab : TabBase
        {
            [ContentProperty(@"Header", @"advanceYourCareerHeader2", true, @"Max Character limit: 32", 0, false)]
            public Textstring Advanceyourcareerheader2 { get; set; }

            [ContentProperty(@"Body text", @"advanceYourCareerbodyText2", true, @"Recommended Character limit: 105", 1, false)]
            public RichtextEditor Advanceyourcareerbodytext2 { get; set; }

            [ContentProperty(@"Header Colour", @"advanceYourCareerHeaderColour2", false, @"", 2, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Advanceyourcareerheadercolour2 { get; set; }

            [ContentProperty(@"Body text colour", @"advanceYourCareerBodytextColour2", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Advanceyourcareerbodytextcolour2 { get; set; }

        }
        public class AdvanceYourCareer3Tab : TabBase
        {
            [ContentProperty(@"Header", @"advanceYourCareerHeader3", true, @"Max Character limit: 32", 0, false)]
            public Textstring Advanceyourcareerheader3 { get; set; }

            [ContentProperty(@"Body text", @"advanceYourCareerbodyText3", true, @"Recommended Character limit: 105", 1, false)]
            public RichtextEditor Advanceyourcareerbodytext3 { get; set; }

            [ContentProperty(@"Header colour", @"advanceYourCareerHeaderColour3", false, @"", 2, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Advanceyourcareerheadercolour3 { get; set; }

            [ContentProperty(@"Body text colour", @"advanceYourCareerBodytextColour3", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Advanceyourcareerbodytextcolour3 { get; set; }

        }
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Second section subheader", @"secondSectionSubheader", false, @"Max Character limit: 64", 0, false)]
            public Textstring Secondsectionsubheader { get; set; }

            [ContentProperty(@"Second section subheader Colour", @"secondSectionSubheaderColour", false, @"", 1, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Secondsectionsubheadercolour { get; set; }

            [ContentProperty(@"Second section bodytext", @"secondSectionBodytext", false, @"Max Character limit: 160", 2, false)]
            public RichtextEditor Secondsectionbodytext { get; set; }

            [ContentProperty(@"Second section bodytext Colour", @"secondSectionBodytextColour", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Secondsectionbodytextcolour { get; set; }

            [ContentProperty(@"Get more info header", @"getMoreInfoHeader", false, @"Max Character limit: 52", 4, false)]
            public Textstring Getmoreinfoheader { get; set; }

            [ContentProperty(@"Get more info button text", @"getMoreInfoButtonText", false, @"Max Character limit: 22", 5, false)]
            public Textstring Getmoreinfobuttontext { get; set; }

            [ContentProperty(@"Get more info button Url", @"getMoreInfoButtonUrl", false, @"", 6, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Getmoreinfobuttonurl { get; set; }

        }

        [ContentTab(@"Advance Your Career 1", 4)]
        public AdvanceYourCareer1Tab AdvanceYourCareer1 { get; set; }

        [ContentTab(@"Advance Your Career 2", 5)]
        public AdvanceYourCareer2Tab AdvanceYourCareer2 { get; set; }

        [ContentTab(@"Advance Your Career 3", 6)]
        public AdvanceYourCareer3Tab AdvanceYourCareer3 { get; set; }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}