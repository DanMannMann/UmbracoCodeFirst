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
    [DocumentType(@"Local Settings", @"LocalSettings", null, @"icon-umb-settings color-yellow", false, false, @"")]
    public class Localsettings : Master
    {
        public class UsersTab : TabBase
        {
            [ContentProperty(@"Local Market Admins", @"localMarketAdmins", false, @"", 0, false)]
            public LMI.BusinessLogic.CodeFirst.Multi_UserPicker Localmarketadmins { get; set; }

            [ContentProperty(@"Local Market Managers", @"localMarketManagers", false, @"", 1, false)]
            public LMI.BusinessLogic.CodeFirst.Multi_UserPicker Localmarketmanagers { get; set; }

        }

        [ContentTab(@"Users", 0)]
        public UsersTab Users { get; set; }
    }
}