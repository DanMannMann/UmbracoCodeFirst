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
    [DocumentType(@"MODULE: Secondary", @"MODULESecondary", null, @".sprTreeFolder", false, false, @"")]
    public class Modulesecondary : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Module header", @"moduleHeader", false, @"Max Character limit: 62", 0, false)]
            public Textstring Moduleheader { get; set; }

            [ContentProperty(@"Module bodytext 2", @"moduleBodytext2", false, @"", 2, false)]
            public UmbracoCodeFirst.GeneratedTypes.SmallRichText Modulebodytext2 { get; set; }

            [ContentProperty(@"Module bodytext 1", @"moduleBodytext1", false, @"", 1, false)]
            public UmbracoCodeFirst.GeneratedTypes.SmallRichText Modulebodytext1 { get; set; }

            [ContentProperty(@"Module header", @"moduleHeaderColour", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Moduleheadercolour { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}