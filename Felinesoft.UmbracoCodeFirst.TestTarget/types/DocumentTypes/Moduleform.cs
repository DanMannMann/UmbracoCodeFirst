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
    [DocumentType(@"MODULE: Form", @"MODULEForm", null, @".sprTreeFolder", false, false, @"")]
    [Template(true, "MODULEForm", "MODULEForm")]
    public class Moduleform : Modules
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

            [ContentProperty(@"Form", @"form", true, @"", 7, false)]
            public UmbracoCodeFirst.GeneratedTypes.FormPicker Form { get; set; }

            [ContentProperty(@"Video", @"video", false, @"", 2, false)]
            public LegacyMediaPicker Video { get; set; }

            [ContentProperty(@"BackgroundColour", @"backgroundColour", false, @"", 8, false)]
            public UmbracoCodeFirst.GeneratedTypes.ColourPicker Backgroundcolour { get; set; }

            [ContentProperty(@"Sub Text", @"subText", false, @"", 6, false)]
            public TextboxMultiple Subtext { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}