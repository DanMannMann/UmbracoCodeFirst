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
    [DocumentType(@"MODULE: Music Tracklist", @"MODULEMusicTracklist", new Type[] { typeof(Modulemusictracklist) }, @".sprTreeFolder", false, false, @"")]
    public class Modulemusictracklist : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Module heading", @"musicPlaylistModuleHeading", false, @"Max Character limit: 58", 0, false)]
            public Textstring Musicplaylistmoduleheading { get; set; }

            [ContentProperty(@"Module subheading", @"musicPlaylistModuleSubheading", false, @"Max Character limit: 58", 2, false)]
            public Textstring Musicplaylistmodulesubheading { get; set; }

            [ContentProperty(@"Background Colour", @"musicPlaylistbackgroundColour", false, @"", 4, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Musicplaylistbackgroundcolour { get; set; }

            [ContentProperty(@"Buy Now Button Text", @"musicPlaylistBuyNowButtonText", false, @"Max Character limit: 22", 5, false)]
            public Textstring Musicplaylistbuynowbuttontext { get; set; }

            [ContentProperty(@"Music Playlist", @"musicPlaylist", false, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.PlaylistPicker Musicplaylist { get; set; }

            [ContentProperty(@"Buy Now Button Url", @"musicPlaylistBuyNowButtonUrl", false, @"", 1, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Musicplaylistbuynowbuttonurl { get; set; }

            [ContentProperty(@"CTA Text", @"musicPlaylistCTAText", false, @"Max Character limit: 100 (including HTML tags)", 6, false)]
            public LMI.BusinessLogic.CodeFirst.TextWithLink Musicplaylistctatext { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}