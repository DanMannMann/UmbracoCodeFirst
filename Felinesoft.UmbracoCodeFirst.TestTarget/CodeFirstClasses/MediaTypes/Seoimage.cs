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
    [MediaType(@"SEO Image", @"SEOImage", null, @"icon-picture", false, false, @"")]
    public class Seoimage : MediaTypeBase
    {
        public class SEOTab : TabBase
        {
            [ContentProperty(@"Title", @"title", false, @"", 0, false)]
            public Textstring Title { get; set; }

            [ContentProperty(@"Optional Author", @"optionalAuthor", false, @"", 1, false)]
            public LMI.BusinessLogic.CodeFirst.AuthorPicker Optionalauthor { get; set; }

            [ContentProperty(@"Caption", @"caption", false, @"", 2, false)]
            public Textstring Caption { get; set; }

            [ContentProperty(@"Geolocation", @"geolocation", false, @"", 3, false)]
            public Textstring Geolocation { get; set; }

            [ContentProperty(@"License", @"license", false, @"", 4, false)]
            public Textstring License { get; set; }

        }

        [ContentTab(@"SEO", 0)]
        public SEOTab SEO { get; set; }
    }
}