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
    [DocumentType(@"Music Item", @"MusicItem", null, @"icon-music", false, false, @"")]
    public class Musicitem : Musicplaylist
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Track Number", @"trackNumber", false, @"Max Character limit: 3", 0, false)]
            public Textstring Tracknumber { get; set; }

            [ContentProperty(@"Track Type", @"trackType", false, @"Max Character limit: 42", 2, false)]
            public Textstring Tracktype { get; set; }

            [ContentProperty(@"Track Name", @"trackName", false, @"Max Character limit: 53", 1, false)]
            public Textstring Trackname { get; set; }

            [ContentProperty(@"Artist name", @"artistName", false, @"", 3, false)]
            public Textstring Artistname { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}