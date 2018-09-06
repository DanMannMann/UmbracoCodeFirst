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
    [DocumentType(@"Forms Folder", @"FormsFolder", new Type[] { typeof(Formsfolder), typeof(Emailsignupform) }, @"icon-documents", false, false, @"")]
    public class Formsfolder : Repositoryroot
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Form", @"form", false, @"", 0, false)]
            public RichtextEditor Form { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}