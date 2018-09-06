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
            public LMI.BusinessLogic.CodeFirst.UrlPicker Buttonurl { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}