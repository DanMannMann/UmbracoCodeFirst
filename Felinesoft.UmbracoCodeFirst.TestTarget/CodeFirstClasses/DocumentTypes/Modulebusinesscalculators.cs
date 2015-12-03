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
    [DocumentType(@"MODULE: Business Calculators", @"MODULEBusinessCalculators", new Type[] { typeof(Submodulecalculator) }, @".sprTreeFolder", false, false, @"")]
    [Template(true, "MODULE Business Calculators", "MODULEBusinessCalculators")]
    public class Modulebusinesscalculators : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Module header", @"moduleHeader", false, @"Max Character limit: 68", 0, false)]
            public Textstring Moduleheader { get; set; }

            [ContentProperty(@"Module subheading", @"moduleSubheading", false, @"Max Character limit: 79", 1, false)]
            public Textstring Modulesubheading { get; set; }

            [ContentProperty(@"Header", @"header", true, @"Max Character limit: 26", 3, false)]
            public Textstring Header { get; set; }

            [ContentProperty(@"Text", @"text", true, @"", 4, false)]
            public TextboxMultiple Text { get; set; }

            [ContentProperty(@"Background Colour", @"backgroundColour", false, @"", 2, false)]
            public LMI.BusinessLogic.CodeFirst.ColourPicker Backgroundcolour { get; set; }

            [ContentProperty(@"Module header colour", @"moduleHeaderColour", false, @"", 5, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Moduleheadercolour { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}