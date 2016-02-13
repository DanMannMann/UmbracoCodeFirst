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
    [DocumentType(@"Root", @"Root", new Type[] { typeof(Region), typeof(Search) }, @"icon-sitemap", true, false, @"")]
    [Template(true, "Webpage", "Webpage")]
    public class Root : Master
    {
        public class TestTab : TabBase
        {
            [ContentProperty(@"Module 1", @"dictPicker", false, @"", 0, false)]
            public UmbracoCodeFirst.GeneratedTypes.Module1 Dictpicker { get; set; }

            [ContentProperty(@"Module 2", @"module2", false, @"", 1, false)]
            public UmbracoCodeFirst.GeneratedTypes.Module1 Module2 { get; set; }

        }

        [ContentTab(@"Test", 0)]
        public TestTab Test { get; set; }
    }
}