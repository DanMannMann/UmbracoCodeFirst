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
    [MediaType(@"Image", @"Image", null, @"icon-picture", false, false, @"")]
    public class Image : MediaTypeBase
    {
        public class ImageTab : TabBase
        {
            [ContentProperty(@"Upload image", @"umbracoFile", false, @"", 0, false)]
            public Upload Umbracofile { get; set; }

            [ContentProperty(@"Alt Tag", @"altTag", true, @"Max Character limit: 50", 1, false)]
            public Textstring Alttag { get; set; }

            [ContentProperty(@"Description", @"description", false, @"Max Character limit: 160", 2, false)]
            public Textstring Description { get; set; }

            [ContentProperty(@"Copyright", @"copyright", false, @"Max Character limit: 107", 3, false)]
            public Textstring Copyright { get; set; }

        }

        [ContentTab(@"Image", 1)]
        public ImageTab ImageTabProperty { get; set; }

        [ContentProperty(@"Width", "umbracoWidth", false, @"", 1, false)]
        public Label Umbracowidth { get; set; }

        [ContentProperty(@"Height", "umbracoHeight", false, @"", 2, false)]
        public Label Umbracoheight { get; set; }

        [ContentProperty(@"Size", "umbracoBytes", false, @"", 3, false)]
        public Label Umbracobytes { get; set; }

        [ContentProperty(@"Type", "umbracoExtension", false, @"", 0, false)]
        public Label Umbracoextension { get; set; }

        [ContentComposition]
        public Seoimage SeoimageComposition { get; set; }
    }
}