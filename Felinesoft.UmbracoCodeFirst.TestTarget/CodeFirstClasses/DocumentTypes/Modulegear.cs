using Marsman.UmbracoCodeFirst;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Marsman.UmbracoCodeFirst.DataTypes;
using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System;
using Marsman.UmbracoCodeFirst.DataTypes.BuiltIn;

namespace LMI.BusinessLogic.CodeFirst
{
    [DocumentType(@"MODULE: Gear", @"MODULEGear", null, @".sprTreeFolder", false, false, @"")]
    public class Modulegear : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Module heading", @"gearModuleHeading", false, @"Max Character limit: 58", 0, false)]
            public Textstring Gearmoduleheading { get; set; }

            [ContentProperty(@"Module subheading", @"gearModuleSubheading", false, @"Max Character limit: 79", 1, false)]
            public Textstring Gearmodulesubheading { get; set; }

            [ContentProperty(@"Image", @"gearImage", false, @"", 3, false)]
            public LegacyMediaPicker Gearimage { get; set; }

            [ContentProperty(@"Header", @"gearHeader", false, @"Max Character limit: 35", 4, false)]
            public Textstring Gearheader { get; set; }

            [ContentProperty(@"Subheading", @"gearSubheading", false, @"Max Character limit: 90", 5, false)]
            public Textstring Gearsubheading { get; set; }

            [ContentProperty(@"Image gallery heading", @"gearImageGalleryHeading", false, @"Max Character limit: 70", 7, false)]
            public Textstring Gearimagegalleryheading { get; set; }

            [ContentProperty(@"Background Colour", @"gearBackgroundColour", false, @"", 6, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Gearbackgroundcolour { get; set; }

            [ContentProperty(@"Module Subheading Colour", @"moduleSubheadingColour", false, @"", 2, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Modulesubheadingcolour { get; set; }

            [ContentProperty(@"Module Heading Colour", @"gearModuleHeadingColour", false, @"", 8, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Gearmoduleheadingcolour { get; set; }

        }
        public class GalleryTab : TabBase
        {
            [ContentProperty(@"Image 1", @"gearImage1", false, @"", 0, false)]
            public LegacyMediaPicker Gearimage1 { get; set; }

            [ContentProperty(@"Image 2", @"gearImage2", false, @"", 4, false)]
            public LegacyMediaPicker Gearimage2 { get; set; }

            [ContentProperty(@"Image 3", @"gearImage3", false, @"", 8, false)]
            public LegacyMediaPicker Gearimage3 { get; set; }

            [ContentProperty(@"Image 4", @"gearImage4", false, @"", 12, false)]
            public LegacyMediaPicker Gearimage4 { get; set; }

            [ContentProperty(@"Image 5", @"gearImage5", false, @"", 16, false)]
            public LegacyMediaPicker Gearimage5 { get; set; }

            [ContentProperty(@"Image 6", @"gearImage6", false, @"", 20, false)]
            public LegacyMediaPicker Gearimage6 { get; set; }

            [ContentProperty(@"Image 1 Description", @"gearImageDescription1", false, @"Max Character limit: 21", 1, false)]
            public Textstring Gearimagedescription1 { get; set; }

            [ContentProperty(@"Image 2 Description", @"gearImageDescription2", false, @"Max Character limit:21", 5, false)]
            public Textstring Gearimagedescription2 { get; set; }

            [ContentProperty(@"Image 3 Description", @"gearImageDescription3", false, @"Max Character limit: 21", 9, false)]
            public Textstring Gearimagedescription3 { get; set; }

            [ContentProperty(@"Image 4 Description", @"gearImageDescription4", false, @"Max Character limit: 21", 13, false)]
            public Textstring Gearimagedescription4 { get; set; }

            [ContentProperty(@"Image 5 Description", @"gearImageDescription5", false, @"Max Character limit: 21", 17, false)]
            public Textstring Gearimagedescription5 { get; set; }

            [ContentProperty(@"Image 6 Description", @"gearImageDescription6", false, @"Max Character limit: 21", 21, false)]
            public Textstring Gearimagedescription6 { get; set; }

            [ContentProperty(@"CTA 1 Text", @"gearImageCTAText1", false, @"Max Character limit: 24", 2, false)]
            public Textstring Gearimagectatext1 { get; set; }

            [ContentProperty(@"CTA 2 Text", @"gearImageCTAText2", false, @"Max Character limit: 24", 6, false)]
            public Textstring Gearimagectatext2 { get; set; }

            [ContentProperty(@"CTA 3 Text", @"gearImageCTAText3", false, @"Max Character limit: 24", 10, false)]
            public Textstring Gearimagectatext3 { get; set; }

            [ContentProperty(@"CTA 4 Text", @"gearImageCTAText4", false, @"Max Character limit: 24", 14, false)]
            public Textstring Gearimagectatext4 { get; set; }

            [ContentProperty(@"CTA 5 Text", @"gearImageCTAText5", false, @"Max Character limit: 24", 18, false)]
            public Textstring Gearimagectatext5 { get; set; }

            [ContentProperty(@"CTA 1 Url", @"gearCtaUrl1", false, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Gearctaurl1 { get; set; }

            [ContentProperty(@"CTA 2 Url", @"gearCtaUrl2", false, @"", 7, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Gearctaurl2 { get; set; }

            [ContentProperty(@"CTA 3 Url", @"gearCtaUrl3", false, @"", 11, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Gearctaurl3 { get; set; }

            [ContentProperty(@"CTA 4 Url", @"gearCtaUrl4", false, @"", 15, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Gearctaurl4 { get; set; }

            [ContentProperty(@"CTA 5 Url", @"gearCtaUrl5", false, @"", 19, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Gearctaurl5 { get; set; }

            [ContentProperty(@"CTA 6 Url", @"gearCtaUrl6", false, @"", 23, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Gearctaurl6 { get; set; }

            [ContentProperty(@"CTA 6 Text", @"gearImageCTAText6", false, @"Max Character limit: 24", 22, false)]
            public Textstring Gearimagectatext6 { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }

        [ContentTab(@"Gallery", 1)]
        public GalleryTab Gallery { get; set; }
    }
}