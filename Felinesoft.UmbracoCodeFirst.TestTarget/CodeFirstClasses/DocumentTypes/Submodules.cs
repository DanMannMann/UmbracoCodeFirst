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
    [DocumentType(@"Sub Modules", @"SubModules", null, @".sprTreeFolder", false, false, @"")]
    public class Submodules : Master
    {
        public class ContentTab : TabBase
        {
        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}