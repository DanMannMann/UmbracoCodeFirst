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
            public LMI.BusinessLogic.CodeFirst.UrlPicker Linkurl { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}