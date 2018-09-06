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
            public LMI.BusinessLogic.CodeFirst.UrlPicker Link { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}