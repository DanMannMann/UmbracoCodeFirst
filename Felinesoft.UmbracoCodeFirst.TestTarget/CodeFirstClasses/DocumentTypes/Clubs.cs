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
    [DocumentType(@"Clubs", @"Clubs", new Type[] { typeof(Modules), typeof(Webpage), typeof(Clubs), typeof(Redirectpage) }, @".sprTreeFolder", false, false, @"")]
    [Template(true, "Webpage", "Webpage")]
    public class Clubs : Seopage
    {
        public class MenuTab : TabBase
        {
            [ContentProperty(@"Has Flyout Menu", @"hasFlyoutMenu", false, @"", 0, false)]
            public TrueFalse Hasflyoutmenu { get; set; }

            [ContentProperty(@"Show in Flyout Menu", @"showInFlyoutMenu", false, @"", 1, false)]
            public TrueFalse Showinflyoutmenu { get; set; }

            [ContentProperty(@"Hide In Sub Nav", @"hideInSubNav", false, @"Hide this from sub navigation", 2, false)]
            public TrueFalse Hideinsubnav { get; set; }

        }

        [ContentTab(@"Menu", 0)]
        public MenuTab Menu { get; set; }
    }
}