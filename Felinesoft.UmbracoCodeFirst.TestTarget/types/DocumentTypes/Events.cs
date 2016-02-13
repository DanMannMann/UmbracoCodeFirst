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
    [DocumentType(@"Events", @"Events", new Type[] { typeof(Eventlisting) }, @"icon-map-alt", false, false, @"")]
    [Template(true, "Debug", "Debug")]
    public class Events : Seopage
    {
        public class SettingsTab : TabBase
        {
            [ContentProperty(@"All Events Page", @"umbracoRedirect", false, @"", 0, false)]
            public UmbracoCodeFirst.GeneratedTypes.EventlistingsPicker Umbracoredirect { get; set; }

        }

        [ContentTab(@"Settings", 0)]
        public SettingsTab Settings { get; set; }
    }
}