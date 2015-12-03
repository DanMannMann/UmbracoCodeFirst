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
    [DocumentType(@"SUB MODULE: FAQ Item", @"SubModuleFaqItem", null, @".sprTreeFolder", false, false, @"")]
    public class Submodulefaqitem : Submodules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Heading", @"heading", true, @"Maximum of 120 Characters", 0, false)]
            public Textstring Heading { get; set; }

            [ContentProperty(@"Heading Colour", @"headingColour", false, @"", 1, false)]
            public LMI.BusinessLogic.CodeFirst.ColourPicker Headingcolour { get; set; }

            [ContentProperty(@"Answer", @"answer", false, @"", 2, false)]
            public RichtextEditor Answer { get; set; }

            [ContentProperty(@"Answer Colour", @"answerColour", false, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.ColourPicker Answercolour { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}