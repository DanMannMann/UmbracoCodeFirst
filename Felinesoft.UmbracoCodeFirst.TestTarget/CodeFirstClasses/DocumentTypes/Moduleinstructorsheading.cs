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
    [DocumentType(@"MODULE: Instructors - Heading", @"MODULEInstructorsHeading", null, @".sprTreeFolder", false, false, @"")]
    public class Moduleinstructorsheading : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Hero header", @"heroHeader", true, @"Max Character limit: 42", 0, false)]
            public Textstring Heroheader { get; set; }

            [ContentProperty(@"Hero subheader", @"heroSubheader", false, @"Max Character limit: 120", 1, false)]
            public Textstring Herosubheader { get; set; }

            [ContentProperty(@"CTA Button", @"cTAButton", false, @"Max Character limit: 22", 2, false)]
            public Textstring Ctabutton { get; set; }

            [ContentProperty(@"Background Image", @"backgroundImage", false, @"[ THIS IS A PLACEHOLDER FOR THE CUSTOM MEDIA PICKER ]", 3, false)]
            public Label Backgroundimage { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}