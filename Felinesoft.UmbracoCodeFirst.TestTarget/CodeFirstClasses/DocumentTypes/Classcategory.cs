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
    [DocumentType(@"Class Category", @"ClassCategory", new Type[] { typeof(Class) }, @".sprTreeFolder", false, false, @"")]
    [Template(true, "Class Category", "ClassCategory")]
    public class Classcategory : Seopage
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Menu Title", @"menuTitle", true, @"The title that will appear in the menu. 18 Character limit.", 0, false)]
            public Textstring Menutitle { get; set; }

            [ContentProperty(@"Menu Title Superscript", @"menuTitleSuperscript", false, @"Optional superscript content e.g. TM", 1, false)]
            public Textstring Menutitlesuperscript { get; set; }

            [ContentProperty(@"Page Description Heading Bold", @"pageDescriptionHeadingBold", false, @"BOLD heading text for this category page description.", 4, false)]
            public Textstring Pagedescriptionheadingbold { get; set; }

            [ContentProperty(@"Page Description Heading Normal", @"pageDescriptionHeadingNormal", false, @"Normal font weight page heading", 5, false)]
            public Textstring Pagedescriptionheadingnormal { get; set; }

            [ContentProperty(@"Page Description Body", @"pageDescriptionBody", false, @"", 7, false)]
            public Textstring Pagedescriptionbody { get; set; }

            [ContentProperty(@"Menu Title Colour", @"menuTitleColour", false, @"", 2, false)]
            public LMI.BusinessLogic.CodeFirst.ColourPicker Menutitlecolour { get; set; }

            [ContentProperty(@"Menu Background Colour", @"menuBackgroundColour", false, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.ColourPicker Menubackgroundcolour { get; set; }

            [ContentProperty(@"Page Description Heading Colour", @"pageDescriptionHeadingColour", false, @"", 6, false)]
            public LMI.BusinessLogic.CodeFirst.ColourPicker Pagedescriptionheadingcolour { get; set; }

            [ContentProperty(@"Page Description Body Colour", @"pageDescriptionBodyColour", false, @"", 8, false)]
            public LMI.BusinessLogic.CodeFirst.ColourPicker Pagedescriptionbodycolour { get; set; }

            [ContentProperty(@"Page Description Background Colour", @"pageDescriptionBackgroundColour", false, @"", 9, false)]
            public LMI.BusinessLogic.CodeFirst.ColourPicker Pagedescriptionbackgroundcolour { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}