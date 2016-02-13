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
    [DocumentType(@"MODULE: Do More", @"MODULEDoMore", null, @".sprTreeFolder", false, false, @"")]
    public class Moduledomore : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Module heading", @"doMoreModuleHeading", false, @"Max Character limit: 58", 0, false)]
            public Textstring Domoremoduleheading { get; set; }

            [ContentProperty(@"Module subheading", @"doMoreModuleSubheading", false, @"Max Character limit: 79", 2, false)]
            public Textstring Domoremodulesubheading { get; set; }

            [ContentProperty(@"Module Heading Colour", @"doMoreModuleHeadingColour", false, @"", 1, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Domoremoduleheadingcolour { get; set; }

            [ContentProperty(@"Module subheading Colour", @"doMoreModuleSubheadingColour", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Domoremodulesubheadingcolour { get; set; }

            [ContentProperty(@"Background Colour", @"backgroundColour", false, @"", 4, false)]
            public UmbracoCodeFirst.GeneratedTypes.ColourPicker Backgroundcolour { get; set; }

        }
        public class Item1Tab : TabBase
        {
            [ContentProperty(@"Header", @"doMoreHeaderText1", true, @"Max Character limit: 23", 0, false)]
            public Textstring Domoreheadertext1 { get; set; }

            [ContentProperty(@"Copy", @"doMoreCopyText1", true, @"Max Character limit: 54", 1, false)]
            public Textstring Domorecopytext1 { get; set; }

            [ContentProperty(@"Url", @"doMoreUrl1", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Domoreurl1 { get; set; }

            [ContentProperty(@"Icon", @"doMoreIcon1", false, @"", 5, false)]
            public LegacyMediaPicker Domoreicon1 { get; set; }

            [ContentProperty(@"Copy Colour", @"doMoreCopyText1Colour", false, @"", 2, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Domorecopytext1colour { get; set; }

            [ContentProperty(@"Open In New Window 1", @"openInNewWindow1", false, @"", 4, false)]
            public TrueFalse Openinnewwindow1 { get; set; }

        }
        public class Item2Tab : TabBase
        {
            [ContentProperty(@"Header", @"doMoreHeaderText2", true, @"Max Character limit: 23", 0, false)]
            public Textstring Domoreheadertext2 { get; set; }

            [ContentProperty(@"Copy", @"doMoreCopyText2", true, @"Max Character limit: 54", 1, false)]
            public Textstring Domorecopytext2 { get; set; }

            [ContentProperty(@"Url", @"doMoreUrl2", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Domoreurl2 { get; set; }

            [ContentProperty(@"Icon", @"doMoreIcon2", false, @"", 5, false)]
            public LegacyMediaPicker Domoreicon2 { get; set; }

            [ContentProperty(@"Copy Colour", @"doMoreCopyText2Colour", false, @"", 2, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Domorecopytext2colour { get; set; }

            [ContentProperty(@"Open In New Window 2", @"openInNewWindow2", false, @"", 4, false)]
            public TrueFalse Openinnewwindow2 { get; set; }

        }
        public class Item3Tab : TabBase
        {
            [ContentProperty(@"Header", @"doMoreHeaderText3", true, @"Max Character limit: 23", 0, false)]
            public Textstring Domoreheadertext3 { get; set; }

            [ContentProperty(@"Copy", @"doMoreCopyText3", true, @"Max Character limit: 54", 1, false)]
            public Textstring Domorecopytext3 { get; set; }

            [ContentProperty(@"Url", @"doMoreUrl3", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Domoreurl3 { get; set; }

            [ContentProperty(@"Icon", @"doMoreIcon3", false, @"", 5, false)]
            public LegacyMediaPicker Domoreicon3 { get; set; }

            [ContentProperty(@"Copy Colour", @"doMoreCopyText3Colour", false, @"", 2, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Domorecopytext3colour { get; set; }

            [ContentProperty(@"Open In New Window 3", @"openInNewWindow3", false, @"", 4, false)]
            public TrueFalse Openinnewwindow3 { get; set; }

        }
        public class Item4Tab : TabBase
        {
            [ContentProperty(@"Header", @"doMoreHeaderText4", true, @"Max Character limit: 23", 0, false)]
            public Textstring Domoreheadertext4 { get; set; }

            [ContentProperty(@"Copy", @"doMoreCopyText4", true, @"Max Character limit: 54", 1, false)]
            public Textstring Domorecopytext4 { get; set; }

            [ContentProperty(@"Url", @"doMoreUrl4", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Domoreurl4 { get; set; }

            [ContentProperty(@"Icon", @"doMoreIcon4", false, @"", 5, false)]
            public LegacyMediaPicker Domoreicon4 { get; set; }

            [ContentProperty(@"Copy Colour", @"doMoreCopyText4Colour", false, @"", 2, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Domorecopytext4colour { get; set; }

            [ContentProperty(@"Open In New Window 4", @"openInNewWindow4", false, @"", 4, false)]
            public TrueFalse Openinnewwindow4 { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }

        [ContentTab(@"Item 1", 1)]
        public Item1Tab Item1 { get; set; }

        [ContentTab(@"Item 2", 2)]
        public Item2Tab Item2 { get; set; }

        [ContentTab(@"Item 3", 3)]
        public Item3Tab Item3 { get; set; }

        [ContentTab(@"Item 4", 4)]
        public Item4Tab Item4 { get; set; }
    }
}