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
    [DocumentType(@"MODULE: On Demand Hero", @"MODULEOnDemandHero", null, @".sprTreeFolder", false, false, @"")]
    public class Moduleondemandhero : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Hero image", @"heroImage", false, @"", 0, false)]
            public LegacyMediaPicker Heroimage { get; set; }

            [ContentProperty(@"Hero header", @"heroHeader", false, @"Max Character limit: 55", 1, false)]
            public Textstring Heroheader { get; set; }

            [ContentProperty(@"Hero subheader", @"heroSubheader", false, @"", 2, false)]
            public RichtextEditor Herosubheader { get; set; }

            [ContentProperty(@"Hero Header Colour", @"heroHeaderColour", false, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Heroheadercolour { get; set; }

            [ContentProperty(@"Hero Background colour", @"heroBackgroundColour", false, @"", 4, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Herobackgroundcolour { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}