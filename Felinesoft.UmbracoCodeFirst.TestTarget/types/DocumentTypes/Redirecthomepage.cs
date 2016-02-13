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
    [DocumentType(@"Redirect Homepage", @"RedirectHomepage", null, @".sprTreeFolder", false, false, @"")]
    [Template(true, "Redirect Homepage", "RedirectHomepage")]
    public class Redirecthomepage : Master
    {

        [ContentProperty(@"Redirect To", "redirectTo", true, @"Pick the URL that this page will forward to", 0, false)]
        public UmbracoCodeFirst.GeneratedTypes.UrlPicker Redirectto { get; set; }

        [ContentProperty(@"Dictionary Name", "dictionaryName", true, @"Please copy & paste the dictionary alias for this site's title", 1, false)]
        public Textstring Dictionaryname { get; set; }
    }
}