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
    [DocumentType(@"MODULE: Supporting", @"MODULESupporting", new Type[] { typeof(Submodulesupportingitem) }, @".sprTreeFolder", false, false, @"")]
    public class Modulesupporting : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Module heading", @"moduleHeading", false, @"Max Character limit: 55", 0, false)]
            public Textstring Moduleheading { get; set; }

            [ContentProperty(@"Module subheading", @"moduleSubheading", false, @"Max Character limit: 79", 2, false)]
            public Textstring Modulesubheading { get; set; }

            [ContentProperty(@"Button", @"button", false, @"Max Character limit: 22", 7, false)]
            public Textstring Button { get; set; }

            [ContentProperty(@"Button Url", @"buttonUrl", false, @"", 8, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Buttonurl { get; set; }

            [ContentProperty(@"ShowImages", @"showImages", true, @"", 4, false)]
            public TrueFalse Showimages { get; set; }

            [ContentProperty(@"Footer Title", @"footerTitle", false, @"", 9, false)]
            public LMI.BusinessLogic.CodeFirst.SmallRichText Footertitle { get; set; }

            [ContentProperty(@"Footer Body", @"footerBody", false, @"", 10, false)]
            public Textstring Footerbody { get; set; }

            [ContentProperty(@"Module heading Colour", @"moduleHeadingColour", false, @"", 1, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Moduleheadingcolour { get; set; }

            [ContentProperty(@"Module subheading colour", @"moduleSubheadingColour", false, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Modulesubheadingcolour { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}