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
    [DocumentType(@"Class", @"Class", new Type[] { typeof(Modules) }, @"icon-award", false, false, @"")]
    [Template(true, "Webpage", "Webpage")]
    public class Class : Seopage
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Class image", @"classImage", false, @"", 0, false)]
            public LegacyMediaPicker Classimage { get; set; }

            [ContentProperty(@"Class Programme Colour", @"classProgrammeColour", false, @"", 1, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Classprogrammecolour { get; set; }

        }
        public class GridTab : TabBase
        {
            [ContentProperty(@"Class Body Colour", @"classBodyColour", false, @"", 0, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Classbodycolour { get; set; }

            [ContentProperty(@"Shop CTA", @"gearshopCTA1", false, @"Max Character limit: 24", 1, false)]
            public Textstring Gearshopcta1 { get; set; }

            [ContentProperty(@"Class Title Colour", @"classTitleColour", false, @"", 2, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Classtitlecolour { get; set; }

            [ContentProperty(@"Class background colour", @"classBackgroundColour", false, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Classbackgroundcolour { get; set; }

            [ContentProperty(@"Class header", @"classHeader", true, @"Max Character limit: 27", 4, false)]
            public Textstring Classheader { get; set; }

            [ContentProperty(@"Class subheader", @"classSubheader", false, @"Max Character limit: 38", 5, false)]
            public Textstring Classsubheader { get; set; }

            [ContentProperty(@"Class description on rollover", @"classDescriptionOnRollover", true, @"Max Character limit: 81", 6, false)]
            public Textstring Classdescriptiononrollover { get; set; }

            [ContentProperty(@"Class conditional description", @"classConditionalDescription", false, @"Max Character limit: 56", 7, false)]
            public Textstring Classconditionaldescription { get; set; }

            [ContentProperty(@"Class CTA", @"classCTA", true, @"Max Character limit: 24", 8, false)]
            public Textstring Classcta { get; set; }

            [ContentProperty(@"Hide in class grid", @"hideInClassGrid", false, @"", 9, false)]
            public TrueFalse Hideinclassgrid { get; set; }

        }
        public class ClassListTab : TabBase
        {
            [ContentProperty(@"Class List Logo Heading", @"classListLogoHeading", true, @"A Logo heading for the class list.", 0, false)]
            public LegacyMediaPicker Classlistlogoheading { get; set; }

            [ContentProperty(@"Class List Description", @"classListDescription", true, @"144 Character limit", 1, false)]
            public Textstring Classlistdescription { get; set; }

            [ContentProperty(@"Class List Option Text", @"classListOptionText", false, @"85 Character limit", 2, false)]
            public Textstring Classlistoptiontext { get; set; }

            [ContentProperty(@"Calories", @"calories", false, @"Maximum 17 characters", 3, false)]
            public Textstring Calories { get; set; }

            [ContentProperty(@"Exercise Type", @"exerciseType", false, @"Maximum 52 characters", 4, false)]
            public Textstring Exercisetype { get; set; }

            [ContentProperty(@"Class Link Arrow", @"classLinkArrow", true, @"", 5, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Classlinkarrow { get; set; }

        }
        public class ConfigTab : TabBase
        {
            [ContentProperty(@"CSS Class Names", @"cssClassNames", true, @"", 1, false)]
            public LMI.BusinessLogic.CodeFirst.MultipleTextBox Cssclassnames { get; set; }

            [ContentProperty(@"Abbreviation", @"abbreviation", false, @"", 0, false)]
            public Textstring Abbreviation { get; set; }

            [ContentProperty(@"CSS Background Colour", @"cssBackgroundColour", false, @"", 2, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Cssbackgroundcolour { get; set; }

            [ContentProperty(@"CSS Foreground Colour", @"cssForegroundColour", false, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Cssforegroundcolour { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }

        [ContentTab(@"Grid", 9)]
        public GridTab Grid { get; set; }

        [ContentTab(@"Class List", 10)]
        public ClassListTab ClassList { get; set; }

        [ContentTab(@"Config", 11)]
        public ConfigTab Config { get; set; }
    }
}