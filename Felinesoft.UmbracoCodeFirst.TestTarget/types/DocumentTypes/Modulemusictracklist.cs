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
            public UmbracoCodeFirst.GeneratedTypes.SpectrumColorPicker Musicplaylistbackgroundcolour { get; set; }

            [ContentProperty(@"Buy Now Button Text", @"musicPlaylistBuyNowButtonText", false, @"Max Character limit: 22", 5, false)]
            public Textstring Musicplaylistbuynowbuttontext { get; set; }

            [ContentProperty(@"Music Playlist", @"musicPlaylist", false, @"", 3, false)]
            public UmbracoCodeFirst.GeneratedTypes.PlaylistPicker Musicplaylist { get; set; }

            [ContentProperty(@"Buy Now Button Url", @"musicPlaylistBuyNowButtonUrl", false, @"", 1, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Musicplaylistbuynowbuttonurl { get; set; }

            [ContentProperty(@"CTA Text", @"musicPlaylistCTAText", false, @"Max Character limit: 100 (including HTML tags)", 6, false)]
            public UmbracoCodeFirst.GeneratedTypes.TextWithLink Musicplaylistctatext { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}