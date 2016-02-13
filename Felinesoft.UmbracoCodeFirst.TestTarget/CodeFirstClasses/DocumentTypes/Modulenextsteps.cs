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
    [DocumentType(@"MODULE: Next Steps", @"MODULENextSteps", new Type[] { typeof(Submodulenextstepsitem) }, @".sprTreeFolder", false, false, @"")]
    public class Modulenextsteps : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Module header", @"moduleHeader", false, @"Max Character limit: 54", 0, false)]
            public Textstring Moduleheader { get; set; }

            [ContentProperty(@"Module subheader", @"moduleSubheader", false, @"Max Character limit: 79", 3, false)]
            public Textstring Modulesubheader { get; set; }

            [ContentProperty(@"Hero header", @"heroHeader", false, @"Max Character limit: 42", 4, false)]
            public Textstring Heroheader { get; set; }

            [ContentProperty(@"Hero subtext", @"heroSubtext", false, @"Max Character limit: 135", 6, false)]
            public Textstring Herosubtext { get; set; }

            [ContentProperty(@"Background image", @"backgroundImage", false, @"", 2, false)]
            public LegacyMediaPicker Backgroundimage { get; set; }

            [ContentProperty(@"Second section header", @"secondSectionHeader", false, @"Max Character limit: 65", 9, false)]
            public Textstring Secondsectionheader { get; set; }

            [ContentProperty(@"Hero header Colour", @"heroHeaderColour", false, @"", 5, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Heroheadercolour { get; set; }

            [ContentProperty(@"Hero subtext colour", @"heroSubtextColour", false, @"", 7, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Herosubtextcolour { get; set; }

            [ContentProperty(@"Background colour", @"backgroundColour", false, @"", 1, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Backgroundcolour { get; set; }

            [ContentProperty(@"Video", @"video", false, @"", 8, false)]
            public LegacyMediaPicker Video { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}