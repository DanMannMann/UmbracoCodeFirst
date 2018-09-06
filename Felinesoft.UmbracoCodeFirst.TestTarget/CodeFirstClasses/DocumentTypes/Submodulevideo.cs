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
    [DocumentType(@"SUB MODULE: Video", @"SUBMODULEVideo", null, @".sprTreeFolder", false, false, @"")]
    public class Submodulevideo : Submodules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Video title", @"videoTitle", true, @"Max Character limit: 22", 0, false)]
            public Textstring Videotitle { get; set; }

            [ContentProperty(@"Video Picker", @"videoPicker", false, @"", 1, false)]
            public LMI.BusinessLogic.CodeFirst.VideoPicker Videopicker { get; set; }

            [ContentProperty(@"Video Id", @"videoId", false, @"", 2, false)]
            public Textstring Videoid { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}