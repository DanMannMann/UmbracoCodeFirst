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