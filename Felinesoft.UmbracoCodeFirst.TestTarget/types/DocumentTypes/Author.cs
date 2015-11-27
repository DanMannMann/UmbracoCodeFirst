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
    [DocumentType(@"Author", @"Author", null, @"icon-people", false, false, @"")]
    public class Author : Authorfolder
    {
        public class SettingsTab : TabBase
        {
            [ContentProperty(@"User", @"user", false, @"", 0, false)]
            public UmbracoCodeFirst.GeneratedTypes.UserPicker User { get; set; }

            [ContentProperty(@"Image", @"image", false, @"", 1, false)]
            public LegacyMediaPicker Image { get; set; }

        }
        public class SocialTab : TabBase
        {
            [ContentProperty(@"Twitter Handle", @"twitterHandle", false, @"", 0, false)]
            public Textstring Twitterhandle { get; set; }

        }

        [ContentTab(@"Settings", 2)]
        public SettingsTab Settings { get; set; }

        [ContentTab(@"Social", 1)]
        public SocialTab Social { get; set; }
    }
}