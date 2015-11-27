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
    [DocumentType(@"MODULE: New Form", @"MODULENewForm", null, @"icon-inbox", false, false, @"")]
    [Template(true, "MODULE JotForm", "MODULEJotForm")]
    public class Modulenewform : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Module header", @"moduleHeader", false, @"Max Character limit: 54", 0, false)]
            public Textstring Moduleheader { get; set; }

            [ContentProperty(@"Module subheading", @"moduleSubheading", false, @"Max Character limit: 79", 1, false)]
            public Textstring Modulesubheading { get; set; }

            [ContentProperty(@"Text", @"text", false, @"", 5, false)]
            public TextboxMultiple Text { get; set; }

            [ContentProperty(@"Text header", @"textHeader", true, @"Max Character limit: 56", 4, false)]
            public Textstring Textheader { get; set; }

            [ContentProperty(@"Play Video Button", @"playVideoButton", false, @"Max Character limit: 28", 3, false)]
            public Textstring Playvideobutton { get; set; }

            [ContentProperty(@"Video", @"video", false, @"", 2, false)]
            public LegacyMediaPicker Video { get; set; }

            [ContentProperty(@"BackgroundColour", @"backgroundColour", false, @"", 8, false)]
            public UmbracoCodeFirst.GeneratedTypes.ColourPicker Backgroundcolour { get; set; }

            [ContentProperty(@"JotForm ID", @"jotform", false, @"ID number of your jotform (Not the URL)", 7, false)]
            public Textstring Jotform { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}