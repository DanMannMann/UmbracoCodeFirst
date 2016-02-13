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