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