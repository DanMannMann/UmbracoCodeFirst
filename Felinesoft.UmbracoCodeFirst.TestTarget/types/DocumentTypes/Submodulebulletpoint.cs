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
    [DocumentType(@"SUB MODULE: Bullet Point", @"SUBMODULEBulletPoint", null, @".sprTreeFolder", false, false, @"")]
    public class Submodulebulletpoint : Submodules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Header", @"header", false, @"Max Character limit: 27", 0, false)]
            public Textstring Header { get; set; }

            [ContentProperty(@"Body Text", @"bodyText", false, @"Max Character limit: 38", 1, false)]
            public Textstring Bodytext { get; set; }

            [ContentProperty(@"Link", @"link", false, @"", 2, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Link { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}