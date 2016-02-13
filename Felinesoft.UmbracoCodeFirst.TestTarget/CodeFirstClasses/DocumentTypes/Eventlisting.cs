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
    [DocumentType(@"Event Listing", @"EventListing", new Type[] { typeof(Eventdetail) }, @"icon-bulleted-list", false, false, @"")]
    [Template(true, "EventListing", "EventListing")]
    public class Eventlisting : Seopage
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Footer", @"footer", false, @"", 0, false)]
            public RichtextEditor Footer { get; set; }

        }
        public class GridTab : TabBase
        {
            [ContentProperty(@"Event 1", @"event1", false, @"", 0, false)]
            public LMI.BusinessLogic.CodeFirst.EventPicker Event1 { get; set; }

            [ContentProperty(@"Event 2", @"event2", false, @"", 1, false)]
            public LMI.BusinessLogic.CodeFirst.EventPicker Event2 { get; set; }

            [ContentProperty(@"Event 3", @"event3", false, @"", 2, false)]
            public LMI.BusinessLogic.CodeFirst.EventPicker Event3 { get; set; }

            [ContentProperty(@"Event 4", @"event4", false, @"", 3, false)]
            public LMI.BusinessLogic.CodeFirst.EventPicker Event4 { get; set; }

            [ContentProperty(@"Event 5", @"event5", false, @"", 4, false)]
            public LMI.BusinessLogic.CodeFirst.EventPicker Event5 { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }

        [ContentTab(@"Grid", 1)]
        public GridTab Grid { get; set; }
    }
}