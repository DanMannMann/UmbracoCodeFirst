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
    [DocumentType(@"Country Contact", @"CountryContact", null, @"icon-operator", false, false, @"")]
    public class Countrycontact : Contourdatafolder
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Email", @"email", true, @"", 8, false)]
            public Textstring Email { get; set; }

            [ContentProperty(@"Country Website Url", @"countryWebsiteUrl", false, @"", 10, false)]
            public UmbracoCodeFirst.GeneratedTypes.UrlPicker Countrywebsiteurl { get; set; }

            [ContentProperty(@"Country Code", @"countryCode", false, @"", 9, false)]
            public Textstring Countrycode { get; set; }

            [ContentProperty(@"Name", @"name", false, @"", 0, false)]
            public Textstring Name { get; set; }

            [ContentProperty(@"Street Address 1", @"streetAddress1", false, @"", 1, false)]
            public Textstring Streetaddress1 { get; set; }

            [ContentProperty(@"Street Address 2", @"streetAddress2", false, @"", 2, false)]
            public Textstring Streetaddress2 { get; set; }

            [ContentProperty(@"City", @"city", false, @"", 3, false)]
            public Textstring City { get; set; }

            [ContentProperty(@"State \ Province", @"stateProvince", false, @"", 4, false)]
            public Textstring Stateprovince { get; set; }

            [ContentProperty(@"Country", @"country", false, @"", 5, false)]
            public Textstring Country { get; set; }

            [ContentProperty(@"Contact Phone", @"contactPhone", false, @"", 6, false)]
            public Textstring Contactphone { get; set; }

            [ContentProperty(@"Fax", @"fax", false, @"", 7, false)]
            public Textstring Fax { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}