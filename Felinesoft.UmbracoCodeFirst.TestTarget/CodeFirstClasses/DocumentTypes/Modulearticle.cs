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

namespace LMI.BusinessLogic.CodeFirst
{
    [DocumentType(@"MODULE: Article", @"ModuleArticle", null, @".sprTreeFolder", false, false, @"")]
    [Template(true, "MODULE: Article", "MODULEArticle")]
    public class Modulearticle : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Article Title", @"articleTitle", true, @"Max Character limit: 36", 0, false)]
            public Textstring Articletitle { get; set; }

            [ContentProperty(@"Heading", @"heading", true, @"Max Character limit: 61 ", 1, false)]
            public Textstring Heading { get; set; }

            [ContentProperty(@"Snippet", @"snippet", true, @"Max Character limit: 357", 2, false)]
            public TextboxMultiple Snippet { get; set; }

            [ContentProperty(@"Body", @"body", false, @"", 3, false)]
            public RichtextEditor Body { get; set; }

        }
        public class GalleryTab : TabBase
        {
            [ContentProperty(@"Images", @"images", false, @"", 0, false)]
            public LMI.BusinessLogic.CodeFirst.MultipleMediaPicker Images { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }

        [ContentTab(@"Gallery", 1)]
        public GalleryTab Gallery { get; set; }
    }
}