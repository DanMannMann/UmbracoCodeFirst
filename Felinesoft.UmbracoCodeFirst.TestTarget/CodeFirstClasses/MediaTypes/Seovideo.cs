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
    [MediaType(@"SEO Video", @"SEOVideo", null, @"icon-video", false, false, @"")]
    public class Seovideo : MediaTypeBase
    {
        public class SEOTab : TabBase
        {
            [ContentProperty(@"Optional Author", @"optionalAuthor", false, @"", 0, false)]
            public Textstring Optionalauthor { get; set; }

            [ContentProperty(@"Geolocation", @"geolocation", false, @"", 2, false)]
            public Textstring Geolocation { get; set; }

            [ContentProperty(@"Title", @"title", false, @"Max Character limit: 100", 3, false)]
            public Textstring Title { get; set; }

            [ContentProperty(@"Duration", @"duration", false, @"Please enter a value between 0 and 28800 (8 hours)", 4, false)]
            public Numeric Duration { get; set; }

            [ContentProperty(@"Expiration Date", @"expirationDate", false, @"", 5, false)]
            public DatePickerWithTime Expirationdate { get; set; }

            [ContentProperty(@"Rating", @"rating", false, @"Please enter a value between 0.0 and 5.0", 6, false)]
            public Textstring Rating { get; set; }

            [ContentProperty(@"Publication Date", @"publicationDate", false, @"", 7, false)]
            public DatePickerWithTime Publicationdate { get; set; }

            [ContentProperty(@"Family Friendly", @"familyFriendly", false, @"", 8, false)]
            public Checkbox Familyfriendly { get; set; }

            [ContentProperty(@"Tags", @"tags", false, @"", 9, false)]
            public Tags Tags { get; set; }

            [ContentProperty(@"Category", @"category", false, @"Max Character limit: 256", 10, false)]
            public Textstring Category { get; set; }

            [ContentProperty(@"Gallery Loc", @"galleryLoc", false, @"", 11, false)]
            public Textstring Galleryloc { get; set; }

            [ContentProperty(@"Platform", @"platform", false, @"A list of space-delimited platforms where the video may or may not be played. Allowed values are web, mobile, and tv.", 12, false)]
            public Textstring Platform { get; set; }

            [ContentProperty(@"Video Type", @"videoType", false, @"mime type", 13, false)]
            public Textstring Videotype { get; set; }

            [ContentProperty(@"optionalAuthorURL", @"optionalAuthorURL", false, @"", 1, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Optionalauthorurl { get; set; }

        }

        [ContentTab(@"SEO", 0)]
        public SEOTab SEO { get; set; }
    }
}