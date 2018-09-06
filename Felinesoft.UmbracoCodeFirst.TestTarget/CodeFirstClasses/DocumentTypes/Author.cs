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
    [DocumentType(@"Author", @"Author", null, @"icon-people", false, false, @"")]
    public class Author : Authorfolder
    {
        public class SettingsTab : TabBase
        {
            [ContentProperty(@"User", @"user", false, @"", 0, false)]
            public LMI.BusinessLogic.CodeFirst.UserPicker User { get; set; }

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