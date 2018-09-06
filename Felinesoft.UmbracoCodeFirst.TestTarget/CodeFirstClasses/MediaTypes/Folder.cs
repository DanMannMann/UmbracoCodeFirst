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
    [MediaType(@"Folder", @"Folder", new Type[] { typeof(Folder), typeof(Image), typeof(File), typeof(Video) }, @"icon-folder", true, false, @"")]
    public class Folder : MediaTypeBase
    {
        public class ContentsTab : TabBase
        {
            [ContentProperty(@"Contents:", @"contents", false, @"", 0, false)]
            public FolderBrowser Contents { get; set; }

        }

        [ContentTab(@"Contents", 1)]
        public ContentsTab Contents { get; set; }
    }
}