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
    [DocumentType(@"Redirect Page", @"RedirectPage", null, @"icon-axis-rotation-2", false, false, @"")]
    [Template(true, "Redirect Page", "RedirectPage")]
    public class Redirectpage : Master
    {

        [ContentProperty(@"Redirect To", "redirectTo", true, @"", 0, false)]
        public LMI.BusinessLogic.CodeFirst.UrlPicker Redirectto { get; set; }

        [ContentProperty(@"Show In Flyout Menu", "showInFlyoutMenu", false, @"", 1, false)]
        public Checkbox Showinflyoutmenu { get; set; }

        [ContentProperty(@"Hide In Sub Nav", "hideInSubNav", false, @"", 2, false)]
        public Checkbox Hideinsubnav { get; set; }
    }
}