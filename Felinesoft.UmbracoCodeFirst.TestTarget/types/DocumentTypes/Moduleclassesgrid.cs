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
    [DocumentType(@"MODULE: Classes - Grid", @"MODULEClassesGrid", null, @"icon-thumbnails-small", false, false, @"")]
    [Template(true, "MODULEClassesGrid", "MODULEClassesGrid")]
    public class Moduleclassesgrid : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"More details square CTA", @"moreDetailsSquareCTA", true, @"Max Character limit: 27", 3, false)]
            public Textstring Moredetailssquarecta { get; set; }

            [ContentProperty(@"More details square subheading", @"moreDetailsSquareSubheading", true, @"Max Character limit: 54", 2, false)]
            public Textstring Moredetailssquaresubheading { get; set; }

            [ContentProperty(@"Module heading", @"moduleHeading", false, @"Max Character limit: 54", 0, false)]
            public Textstring Moduleheading { get; set; }

            [ContentProperty(@"Module subheading", @"moduleSubheading", false, @"Max Character limit: 79", 1, false)]
            public Textstring Modulesubheading { get; set; }

            [ContentProperty(@"More details square Url", @"moreDetailsSquareUrl", false, @"", 4, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Moredetailssquareurl { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}