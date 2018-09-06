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
    [DocumentType(@"SUB MODULE: Media Gallery Image Item", @"SUBMODULEMediaGalleryImageItem", null, @".sprTreeFolder", false, false, @"")]
    public class Submodulemediagalleryimageitem : Submodules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Image", @"image", false, @"", 0, false)]
            public LegacyMediaPicker Image { get; set; }

            [ContentProperty(@"Description", @"description", false, @"Max Character limit: 160", 1, false)]
            public Textstring Description { get; set; }

            [ContentProperty(@"Copyright", @"copyright", false, @"Max Character limit: 107", 2, false)]
            public Textstring Copyright { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}