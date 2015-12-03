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
    [DocumentType(@"SUB MODULE: Calculator", @"SUBMODULECalculator", null, @".sprTreeFolder", false, false, @"")]
    public class Submodulecalculator : Submodules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Header", @"header", true, @"Max Character limit: 37", 0, false)]
            public Textstring Header { get; set; }

            [ContentProperty(@"Description", @"description", true, @"Max Character limit: 160", 1, false)]
            public Textstring Description { get; set; }

            [ContentProperty(@"Link", @"link", false, @"", 2, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Link { get; set; }

            [ContentProperty(@"Header Colour", @"headerColour", false, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Headercolour { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}