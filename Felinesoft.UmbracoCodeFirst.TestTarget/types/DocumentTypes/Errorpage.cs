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
    [DocumentType(@"Error Page", @"ErrorPage", null, @"icon-alert-alt", false, false, @"")]
    [Template(true, "Error404", "Error404")]
    public class Errorpage : Seopage
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Title", @"title", true, @"Max Character limit: 36", 0, false)]
            public Textstring Title { get; set; }

            [ContentProperty(@"Heading", @"heading", true, @"Max Character limit: 61 ", 1, false)]
            public Textstring Heading { get; set; }

            [ContentProperty(@"Snippet", @"snippet", true, @"Max Character limit: 357", 2, false)]
            public TextboxMultiple Snippet { get; set; }

            [ContentProperty(@"Body", @"body", false, @"", 3, false)]
            public RichtextEditor Body { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}