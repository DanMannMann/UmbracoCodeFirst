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
    [DocumentType(@"SUB MODULE: Proxy Class", @"SubModuleProxyClass", null, @"icon-award color-orange", false, false, @"")]
    public class Submoduleproxyclass : Submodules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Image", @"image", false, @"", 8, false)]
            public LegacyMediaPicker Image { get; set; }

            [ContentProperty(@"Header", @"header", true, @"", 0, false)]
            public Textstring Header { get; set; }

            [ContentProperty(@"Subheader", @"subheader", false, @"", 2, false)]
            public Textstring Subheader { get; set; }

            [ContentProperty(@"Url", @"url", false, @"", 9, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Url { get; set; }

            [ContentProperty(@"Background Colour", @"backgroundColour", false, @"", 7, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Backgroundcolour { get; set; }

            [ContentProperty(@"Header Colour", @"headerColour", false, @"", 1, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Headercolour { get; set; }

            [ContentProperty(@"Subheader Colour", @"subheaderColour", false, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Subheadercolour { get; set; }

            [ContentProperty(@"Description", @"description", true, @"", 4, false)]
            public Textstring Description { get; set; }

            [ContentProperty(@"Description Rollover", @"descriptionRollover", true, @"", 5, false)]
            public Textstring Descriptionrollover { get; set; }

            [ContentProperty(@"CTA", @"cta", true, @"", 6, false)]
            public Textstring Cta { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}