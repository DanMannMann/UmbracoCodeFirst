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
    [DocumentType(@"Instructors Landing", @"InstructorsLanding", new Type[] { typeof(Modules), typeof(Articletopic), typeof(Webpage), typeof(Redirectpage), typeof(Events) }, @"icon-operator", false, false, @"")]
    [Template(true, "Webpage", "Webpage")]
    public class Instructorslanding : Seopage
    {
        public class MenuTab : TabBase
        {
            [ContentProperty(@"Has Flyout Menu", @"hasFlyoutMenu", false, @"Tick this if this is a main menu link with flyout menu", 0, false)]
            public TrueFalse Hasflyoutmenu { get; set; }

            [ContentProperty(@"Show in Flyout Menu", @"showInFlyoutMenu", false, @"Tick this if you want this page to appear as a child in a flyout menu", 1, false)]
            public TrueFalse Showinflyoutmenu { get; set; }

            [ContentProperty(@"Hide In Sub Nav", @"hideInSubNav", false, @"Hide this from sub navigation", 2, false)]
            public TrueFalse Hideinsubnav { get; set; }

        }

        [ContentTab(@"Menu", 0)]
        public MenuTab Menu { get; set; }
    }
}