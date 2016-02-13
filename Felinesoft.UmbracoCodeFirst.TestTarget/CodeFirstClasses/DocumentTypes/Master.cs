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
    [DocumentType(@"master", @"master", null, @".sprTreeFolder", false, false, @"")]
    public class Master : DocumentTypeBase
    {

        [ContentProperty(@"check-in comment", "checkInComment", false, @"", 0, false)]
        public LMI.BusinessLogic.CodeFirst.Check_InComment Checkincomment { get; set; }
    }
}