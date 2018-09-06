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