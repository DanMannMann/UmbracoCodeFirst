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
    [DocumentType(@"Local Settings", @"LocalSettings", null, @"icon-umb-settings color-yellow", false, false, @"")]
    public class Localsettings : Master
    {
        public class UsersTab : TabBase
        {
            [ContentProperty(@"Local Market Admins", @"localMarketAdmins", false, @"", 0, false)]
            public UmbracoCodeFirst.GeneratedTypes.Multi_UserPicker Localmarketadmins { get; set; }

            [ContentProperty(@"Local Market Managers", @"localMarketManagers", false, @"", 1, false)]
            public UmbracoCodeFirst.GeneratedTypes.Multi_UserPicker Localmarketmanagers { get; set; }

        }

        [ContentTab(@"Users", 0)]
        public UsersTab Users { get; set; }
    }
}