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
    [MediaType(@"File", @"File", null, @"icon-document", false, false, @"")]
    public class File : MediaTypeBase
    {
        public class FileTab : TabBase
        {
            [ContentProperty(@"Upload file", @"umbracoFile", false, @"", 0, false)]
            public Upload Umbracofile { get; set; }

            [ContentProperty(@"Type", @"umbracoExtension", false, @"", 1, false)]
            public Label Umbracoextension { get; set; }

            [ContentProperty(@"Size", @"umbracoBytes", false, @"", 2, false)]
            public Label Umbracobytes { get; set; }

        }

        [ContentTab(@"File", 1)]
        public FileTab FileTabProperty { get; set; }
    }
}