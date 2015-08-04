
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
using Felinesoft.UmbracoCodeFirst.Core;
using Felinesoft.UmbracoCodeFirst.Demo.MediaTypes;

namespace Felinesoft.UmbracoCodeFirst.Demo.DocTypes
{
    [DocumentType(icon: BuiltInIcons.SprTreeFolder)]
    [Template(isDefault: true)]
    public class Genericpage : Master
    {
        public class ContentTab : TabBase
        {
            [ContentProperty("Subheader", "subheader", false, "", 0, false)]
            public Textstring Subheader { get; set; }

            [ContentProperty("Content", "content", false, "", 1, false)]
            public RichtextEditor Content { get; set; }
        }

        [ContentTab("Content", 0)]
        public ContentTab Content { get; set; }

        [MediaPickerProperty]
        public Helicropter Image { get; set; }

        [DocumentPickerProperty]
        public TestPage PickedPage { get; set; }
    }
}