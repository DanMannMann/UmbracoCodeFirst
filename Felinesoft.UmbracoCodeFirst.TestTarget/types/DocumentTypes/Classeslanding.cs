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
    [DocumentType(@"Classes Landing", @"ClassesLanding", new Type[] { typeof(Classcategory) }, @"icon-podcast", false, false, @"")]
    [Template(true, "Classes Landing", "ClassesLanding")]
    public class Classeslanding : Seopage
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"All Classes Page", @"allClassesPage", true, @"Please choose the all classes node for this section. Visitors will be sent to all classes when they hit this page.", 4, false)]
            public LegacyContentPicker Allclassespage { get; set; }

            [ContentProperty(@"Class Hero Image", @"classHeroImage", false, @"The image that will appear above the category page menu", 5, false)]
            public LegacyMediaPicker Classheroimage { get; set; }

            [ContentProperty(@"Class Hero Title", @"classHeroTitle", false, @"The class category hero title. 16 Character limit.", 0, false)]
            public Textstring Classherotitle { get; set; }

            [ContentProperty(@"Class Hero Sub-Title", @"classHeroSubTitle", false, @"45 Character limit.", 2, false)]
            public Textstring Classherosubtitle { get; set; }

            [ContentProperty(@"Class Hero Background Colour", @"classHeroBackgroundColour", false, @"", 6, false)]
            public UmbracoCodeFirst.GeneratedTypes.ColourPicker Classherobackgroundcolour { get; set; }

            [ContentProperty(@"ClassHeroTitleColour", @"classHeroTitleColour", false, @"", 1, false)]
            public UmbracoCodeFirst.GeneratedTypes.ColourPicker Classherotitlecolour { get; set; }

            [ContentProperty(@"Class Hero Sub-Title Colour", @"classHeroSubTitleColour", false, @"Default to white", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.ColourPicker Classherosubtitlecolour { get; set; }

            [ContentProperty(@"Footer Title", @"footerTitle", false, @"Max 63 characters", 7, false)]
            public Textstring Footertitle { get; set; }

            [ContentProperty(@"Footer Body", @"footerBody", false, @"Max 154 characters", 8, false)]
            public TextboxMultiple Footerbody { get; set; }

            [ContentProperty(@"Footer Link Text", @"footerLinkText", false, @"22 Character limit", 9, false)]
            public Textstring Footerlinktext { get; set; }

            [ContentProperty(@"Footer URL", @"footerUrl", false, @"", 10, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Footerurl { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}