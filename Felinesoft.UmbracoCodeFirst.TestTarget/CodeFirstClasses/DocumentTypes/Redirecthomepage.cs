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
    [DocumentType(@"Redirect Homepage", @"RedirectHomepage", null, @".sprTreeFolder", false, false, @"")]
    [Template(true, "Redirect Homepage", "RedirectHomepage")]
    public class Redirecthomepage : Master
    {

        [ContentProperty(@"Redirect To", "redirectTo", true, @"Pick the URL that this page will forward to", 0, false)]
        public LMI.BusinessLogic.CodeFirst.UrlPicker Redirectto { get; set; }

        [ContentProperty(@"Dictionary Name", "dictionaryName", true, @"Please copy & paste the dictionary alias for this site's title", 1, false)]
        public Textstring Dictionaryname { get; set; }
    }
}