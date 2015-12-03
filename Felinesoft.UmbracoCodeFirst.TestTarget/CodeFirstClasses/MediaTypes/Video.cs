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
    [MediaType(@"Video", @"video", null, @"icon-video", false, false, @"")]
    public class Video : MediaTypeBase
    {
        public class VideoTab : TabBase
        {
            [ContentProperty(@"Youtube URL", @"youtubeURL", false, @"Youtube video ID, can be found in URL of video, example: 1Asy_U5ZOdc", 0, false)]
            public Textstring Youtubeurl { get; set; }

            [ContentProperty(@"Vimeo URL", @"vimeoURL", false, @"Vimeo video ID, can be found in URL of video, example: 41989773", 1, false)]
            public Textstring Vimeourl { get; set; }

            [ContentProperty(@"Width", @"width", true, @"Width in pixels, whole numbers only", 4, false)]
            public Numeric Width { get; set; }

            [ContentProperty(@"Height", @"height", true, @"Height in pixels, whole numbers only", 5, false)]
            public Numeric Height { get; set; }

            [ContentProperty(@"Video Overlay Image", @"umbracoFile", true, @"", 2, false)]
            public Upload Umbracofile { get; set; }

            [ContentProperty(@"Video Overlay Image Alt Tag", @"videoOverlayImageAltTag", true, @"Max Character limit: 50", 3, false)]
            public Textstring Videooverlayimagealttag { get; set; }

            [ContentProperty(@"Description", @"description", false, @"Max Character limit: 145", 6, false)]
            public Textstring Description { get; set; }

            [ContentProperty(@"Copyright", @"copyright", false, @"Max Character limit: 80", 7, false)]
            public Textstring Copyright { get; set; }

            [ContentProperty(@"Display Duration", @"displayDuration", false, @"", 8, false)]
            public Textstring Displayduration { get; set; }

        }

        [ContentTab(@"Video", -1)]
        public VideoTab VideoTabProperty { get; set; }

        [ContentComposition]
        public Seovideo SeovideoComposition { get; set; }
    }
}