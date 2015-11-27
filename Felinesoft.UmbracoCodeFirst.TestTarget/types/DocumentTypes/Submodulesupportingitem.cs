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
    [DocumentType(@"SUB MODULE: Supporting Item", @"SUBMODULESupportingItem", null, @".sprTreeFolder", false, false, @"")]
    public class Submodulesupportingitem : Submodules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Header", @"header", true, @"Max Character limit: 41", 1, false)]
            public Textstring Header { get; set; }

            [ContentProperty(@"Image or Video", @"image", false, @"", 0, false)]
            public LegacyMediaPicker Image { get; set; }

            [ContentProperty(@"Body Text", @"bodyText", true, @"", 3, false)]
            public RichtextEditor Bodytext { get; set; }

            [ContentProperty(@"CTA", @"cTA", false, @"", 5, false)]
            public Textstring Cta { get; set; }

            [ContentProperty(@"CTA URL", @"cTAURL", false, @"", 6, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Ctaurl { get; set; }

            [ContentProperty(@"Header Colour", @"headerColour", false, @"", 2, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Headercolour { get; set; }

            [ContentProperty(@"Body Text Colour", @"bodyTextColour", false, @"", 4, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Bodytextcolour { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}