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
    [DocumentType(@"MODULE: Music Archive CTA", @"MODULEMusicArchiveCTA", null, @".sprTreeFolder", false, false, @"")]
    public class Modulemusicarchivecta : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Header", @"header", false, @"Max Character limit: 63", 0, false)]
            public Textstring Header { get; set; }

            [ContentProperty(@"Subheader", @"subheader", false, @"Max Character limit: 154", 1, false)]
            public Textstring Subheader { get; set; }

            [ContentProperty(@"Button", @"button", false, @"Max Character limit: 22", 2, false)]
            public Textstring Button { get; set; }

            [ContentProperty(@"Button Url", @"buttonUrl", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Buttonurl { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}