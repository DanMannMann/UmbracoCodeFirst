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
    [DocumentType(@"SUB MODULE: Next Steps Item", @"SUBMODULENextStepsItem", null, @".sprTreeFolder", false, false, @"")]
    public class Submodulenextstepsitem : Submodules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Header", @"header", true, @"Max Character limit: 34", 0, false)]
            public Textstring Header { get; set; }

            [ContentProperty(@"Body text", @"bodytext", true, @"Max Character limit: 145", 1, false)]
            public Textstring Bodytext { get; set; }

            [ContentProperty(@"Link Url text", @"linkUrlText", false, @"", 2, false)]
            public Textstring Linkurltext { get; set; }

            [ContentProperty(@"Link Url", @"linkUrl", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Linkurl { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}