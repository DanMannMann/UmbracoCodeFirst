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
    [DocumentType(@"Root", @"Root", new Type[] { typeof(Region), typeof(Search) }, @"icon-sitemap", true, false, @"")]
    [Template(true, "Webpage", "Webpage")]
    public class Root : Master
    {
        public class TestTab : TabBase
        {
            [ContentProperty(@"Module 1", @"dictPicker", false, @"", 0, false)]
            public LMI.BusinessLogic.CodeFirst.Module1 Dictpicker { get; set; }

            [ContentProperty(@"Module 2", @"module2", false, @"", 1, false)]
            public LMI.BusinessLogic.CodeFirst.Module1 Module2 { get; set; }

        }

        [ContentTab(@"Test", 0)]
        public TestTab Test { get; set; }
    }
}