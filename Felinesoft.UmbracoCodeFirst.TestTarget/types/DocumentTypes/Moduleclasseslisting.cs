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
    [DocumentType(@"MODULE: Classes - Listing", @"MODULEClassesListing", null, @".sprTreeFolder", false, false, @"")]
    public class Moduleclasseslisting : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Header", @"header", false, @"Max Character limit: 68", 0, false)]
            public Textstring Header { get; set; }

            [ContentProperty(@"Subheading", @"subheading", false, @"Max Character limit: 160", 1, false)]
            public Textstring Subheading { get; set; }

            [ContentProperty(@"Background colour", @"backgroundColour", false, @"", 2, false)]
            public UmbracoCodeFirst.GeneratedTypes.ColourPicker Backgroundcolour { get; set; }

            [ContentProperty(@"Listing image", @"listingImage", false, @"[ THIS IS A PLACEHOLDER FOR THE CUSTOM MEDIA PICKER ]", 3, false)]
            public Label Listingimage { get; set; }

            [ContentProperty(@"Description", @"description", false, @"Max Character limit: 144", 4, false)]
            public Textstring Description { get; set; }

            [ContentProperty(@"Option", @"option", false, @"Max Character limit: 85", 5, false)]
            public Textstring Option { get; set; }

            [ContentProperty(@"Calories 1", @"calories1", false, @"Max Character limit: 17", 6, false)]
            public Textstring Calories1 { get; set; }

            [ContentProperty(@"Calories 2", @"calories2", false, @"Max Character limit: 17", 7, false)]
            public Textstring Calories2 { get; set; }

            [ContentProperty(@"Exercise type 1", @"exerciseType1", false, @"Max Character limit: 52", 8, false)]
            public Textstring Exercisetype1 { get; set; }

            [ContentProperty(@"Exercise type 2", @"exerciseType2", false, @"Max Character limit: 52", 9, false)]
            public Textstring Exercisetype2 { get; set; }

            [ContentProperty(@"Link", @"link", false, @"Please enter the URL with http:// for external or without for internal.", 10, false)]
            public Textstring Link { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}