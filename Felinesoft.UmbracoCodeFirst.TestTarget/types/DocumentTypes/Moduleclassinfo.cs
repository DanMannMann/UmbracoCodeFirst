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
    [DocumentType(@"MODULE: Class - Info", @"MODULEClassInfo", null, @".sprTreeFolder", false, false, @"")]
    public class Moduleclassinfo : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Heading", @"infoHeading", true, @"Max Character limit: 58", 0, false)]
            public Textstring Infoheading { get; set; }

            [ContentProperty(@"Body Text", @"infoBodyText", false, @"", 4, false)]
            public RichtextEditor Infobodytext { get; set; }

            [ContentProperty(@"Secondary Header", @"infoSecondaryHeader", false, @"Max Character limit: 56", 6, false)]
            public Textstring Infosecondaryheader { get; set; }

            [ContentProperty(@"Secondary Subheader", @"infoSecondarySubheader", false, @"Max Character limit: 172", 7, false)]
            public Textstring Infosecondarysubheader { get; set; }

            [ContentProperty(@"Benefits header", @"infoBenefitsHeader", false, @"Max Character limit: 36", 8, false)]
            public Textstring Infobenefitsheader { get; set; }

            [ContentProperty(@"Benefits Bodytext", @"infoBenefitsBodytext", false, @"", 9, false)]
            public RichtextEditor Infobenefitsbodytext { get; set; }

            [ContentProperty(@"Science Header", @"infoScienceHeader", false, @"Max Character limit: 36", 10, false)]
            public Textstring Infoscienceheader { get; set; }

            [ContentProperty(@"Science Bodytext", @"infoScienceBodytext", false, @"", 12, false)]
            public RichtextEditor Infosciencebodytext { get; set; }

            [ContentProperty(@"Subheading", @"infoSubheading", false, @"Max Character limit: 58", 1, false)]
            public Textstring Infosubheading { get; set; }

            [ContentProperty(@"Sub Heading Colour", @"infoSubHeadingColour", false, @"", 2, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Infosubheadingcolour { get; set; }

            [ContentProperty(@"Science Header Colour", @"infoScienceHeaderColour", false, @"", 11, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Infoscienceheadercolour { get; set; }

            [ContentProperty(@"Background colour", @"infoBackgroundColour", false, @"", 5, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Infobackgroundcolour { get; set; }

            [ContentProperty(@"Lead Text", @"infoLeadText", false, @"", 3, false)]
            public TextboxMultiple Infoleadtext { get; set; }

            [ContentProperty(@"Header Colour", @"headerColour", false, @"", 13, false)]
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Headercolour { get; set; }

        }
        public class ModuleLayoutTab : TabBase
        {
            [ContentProperty(@"Hide Top Section", @"hideTopSection", false, @"", 0, false)]
            public TrueFalse Hidetopsection { get; set; }

            [ContentProperty(@"Hide Bottom Section", @"hideBottomSection", false, @"", 1, false)]
            public TrueFalse Hidebottomsection { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }

        [ContentTab(@"Module Layout", 1)]
        public ModuleLayoutTab ModuleLayout { get; set; }
    }
}