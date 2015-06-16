
using Felinesoft.UmbracoCodeFirst;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Demo.DataTypes;

namespace Demo.GeneratedUmbracoTypes
{
    [DocumentType(null, "TextPage", "umbTextPage", null, ".sprTreeDoc", false, false, false, "")]
    public class Umbtextpage : DocumentTypeBase
    {
        public class ContentTab : TabBase
        {
            [DocumentProperty("Title", "title", "Umbraco.Textbox", "Textstring", null, false, "", 0, false)]
            public Textstring Title { get; set; }

            [DocumentProperty("Sub title", "subTitle", "Umbraco.Textbox", "Textstring", null, false, "", 1, false)]
            public Textstring SubTitle { get; set; }

            [DocumentProperty("Abstract", "bodyText", "Umbraco.TinyMCEv3", "Richtext editor", null, false, "", 2, false)]
            public RichtextEditor Abstract { get; set; }

            [DocumentProperty(sortOrder: 3)]
            public TextpageGrid Grid { get; set; }
        }

        [DocumentTab("Content", 0)]
        public ContentTab Content { get; set; }
    }
}