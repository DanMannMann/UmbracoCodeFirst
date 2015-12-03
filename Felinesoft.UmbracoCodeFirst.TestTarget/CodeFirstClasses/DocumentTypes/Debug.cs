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
    [DocumentType(@"Debug", @"Debug", null, @"icon-bug color-green", false, false, @"")]
    [Template(true, "Debug", "Debug")]
    public class Debug : Master
    {
        public class RepointTestTab : TabBase
        {
        }
        public class NodeRepointerTab : TabBase
        {
        }
        public class RepointTab : TabBase
        {
            [ContentProperty(@"RJPMultiUrlPicker", @"rjpmultiurlpicker", false, @"", 1, false)]
            public LMI.BusinessLogic.CodeFirst.MultipleURLPicker Rjpmultiurlpicker { get; set; }

            [ContentProperty(@"FS Url Picker", @"fsUrlPicker", false, @"", 0, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Fsurlpicker { get; set; }

            [ContentProperty(@"Single Node Tree Picker", @"singleNodeTreePicker", false, @"", 2, false)]
            public LMI.BusinessLogic.CodeFirst.SingleNodeTreepicker Singlenodetreepicker { get; set; }

        }

        [ContentTab(@"Repoint Test", 0)]
        public RepointTestTab RepointTest { get; set; }

        [ContentTab(@"Node Repointer", 0)]
        public NodeRepointerTab NodeRepointer { get; set; }

        [ContentTab(@"Repoint", 0)]
        public RepointTab Repoint { get; set; }
    }
}