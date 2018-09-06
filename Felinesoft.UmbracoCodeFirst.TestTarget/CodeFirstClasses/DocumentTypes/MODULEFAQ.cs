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
    [DocumentType(@"MODULE: FAQ", @"MODULEFAQ", new Type[] { typeof(Submodulefaqitem) }, @".sprTreeFolder", false, false, @"")]
    public class MODULEFAQ : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Heading", @"heading", false, @"Max Character limit: 58", 0, false)]
            public Textstring Heading { get; set; }

            [ContentProperty(@"Subheading", @"subheading", false, @"Max Character limit: 79", 1, false)]
            public Textstring Subheading { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}