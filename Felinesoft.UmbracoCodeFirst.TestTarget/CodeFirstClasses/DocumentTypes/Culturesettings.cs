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
    [DocumentType(@"Culture Settings", @"CultureSettings", null, @"icon-wrench color-orange", false, false, @"")]
    public class Culturesettings : Master
    {
        public class UsersTab : TabBase
        {
            [ContentProperty(@"Translator", @"translator", false, @"", 0, false)]
            public LMI.BusinessLogic.CodeFirst.Multi_UserPicker Translator { get; set; }

        }

        [ContentTab(@"Users", 0)]
        public UsersTab Users { get; set; }
    }
}