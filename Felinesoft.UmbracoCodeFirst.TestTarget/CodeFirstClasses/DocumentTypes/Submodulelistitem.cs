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
    [DocumentType(@"SUB MODULE: List Item", @"SUBMODULEListItem", null, @".sprTreeFolder", false, false, @"")]
    public class Submodulelistitem : Submodules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Heading", @"heading", true, @"Max Character limit: 54", 1, false)]
            public Textstring Heading { get; set; }

            [ContentProperty(@"Body Text", @"bodyText", true, @"", 2, false)]
            public RichtextEditor Bodytext { get; set; }

            [ContentProperty(@"Media", @"media", false, @"[ THIS IS A PLACEHOLDER FOR THE CUSTOM MEDIA PICKER ]", 0, false)]
            public LegacyMediaPicker Media { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}