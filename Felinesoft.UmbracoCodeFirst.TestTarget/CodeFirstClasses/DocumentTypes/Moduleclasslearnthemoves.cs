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
    [DocumentType(@"MODULE: Class - Learn the Moves", @"MODULEClassLearnTheMoves", new Type[] { typeof(Submodulevideo) }, @".sprTreeFolder", false, false, @"")]
    public class Moduleclasslearnthemoves : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Module heading", @"LTMModuleHeading", false, @"Max Character limit: 58", 0, false)]
            public Textstring Ltmmoduleheading { get; set; }

            [ContentProperty(@"Module subheading", @"LTMModuleSubheading", false, @"Max Character limit: 79", 2, false)]
            public Textstring Ltmmodulesubheading { get; set; }

            [ContentProperty(@"Video Header", @"LTMVideoHeader", false, @"Max Character limit: 24", 5, false)]
            public Textstring Ltmvideoheader { get; set; }

            [ContentProperty(@"Video subheader", @"LTMVideoSubheader", false, @"Max Character limit: 125", 7, false)]
            public Textstring Ltmvideosubheader { get; set; }

            [ContentProperty(@"Play button", @"LTMPlayButtonText", true, @"Max Character limit: 15", 10, false)]
            public Textstring Ltmplaybuttontext { get; set; }

            [ContentProperty(@"Play button colour", @"LTMPlayButtonColour", false, @"", 11, false)]
            public LMI.BusinessLogic.CodeFirst.BlackOrWhite Ltmplaybuttoncolour { get; set; }

            [ContentProperty(@"Background Colour", @"LTMHerobackgroundColour", false, @"", 9, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Ltmherobackgroundcolour { get; set; }

            [ContentProperty(@"Video", @"LTMVideo", false, @"", 4, false)]
            public LegacyMediaPicker Ltmvideo { get; set; }

            [ContentProperty(@"Related Vidoes", @"LTMRelatedVidoes", false, @"", 13, false)]
            public LMI.BusinessLogic.CodeFirst.MultipleMediaPicker Ltmrelatedvidoes { get; set; }

            [ContentProperty(@"Video Header Colour", @"LTMVideoHeaderColour", false, @"", 6, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Ltmvideoheadercolour { get; set; }

            [ContentProperty(@"Video Subheader Colour", @"LTMVideoSubheaderColour", false, @"", 8, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Ltmvideosubheadercolour { get; set; }

            [ContentProperty(@"HeadingColour", @"headingcolour", false, @"", 1, false)]
            public LMI.BusinessLogic.CodeFirst.ColourPicker Headingcolour { get; set; }

            [ContentProperty(@"Subheading Colour", @"subheadingColour", false, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.ColourPicker Subheadingcolour { get; set; }

            [ContentProperty(@"Related Video Background Colour", @"relatedVideoBackgroundColour", false, @"", 14, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Relatedvideobackgroundcolour { get; set; }

            [ContentProperty(@"LTM Related Video Header", @"LTMRelatedVideoHeader", false, @"", 12, false)]
            public Textstring Ltmrelatedvideoheader { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}