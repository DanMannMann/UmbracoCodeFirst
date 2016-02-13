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