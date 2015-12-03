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
    [DocumentType(@"MODULE: Solutions", @"MODULESolutions", new Type[] { typeof(Submodulebulletpoint) }, @".sprTreeFolder", false, false, @"")]
    [Template(true, "MODULE Solutions", "MODULESolutions")]
    public class Modulesolutions : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Header", @"header", true, @"Max Character limit: 49", 0, false)]
            public Textstring Header { get; set; }

            [ContentProperty(@"Subheader", @"subheader", true, @"Max Character limit: 240", 1, false)]
            public Textstring Subheader { get; set; }

            [ContentProperty(@"CTA Button", @"cTAButton", false, @"Max Character limit: 28", 3, false)]
            public Textstring Ctabutton { get; set; }

            [ContentProperty(@"Background Image", @"backgroundImage", false, @"", 6, false)]
            public LegacyMediaPicker Backgroundimage { get; set; }

            [ContentProperty(@"Text area heading", @"textAreaHeading", true, @"Max Character limit: 59", 7, false)]
            public Textstring Textareaheading { get; set; }

            [ContentProperty(@"Text area subheading", @"textAreaSubheading", true, @"", 8, false)]
            public Textstring Textareasubheading { get; set; }

            [ContentProperty(@"CTA", @"cTA", false, @"Text area subheading 60", 9, false)]
            public Textstring Cta { get; set; }

            [ContentProperty(@"CTA Url", @"cTAUrl", false, @"Please enter the URL with http:// for external or without for internal.", 11, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Ctaurl { get; set; }

            [ContentProperty(@"Get Les Mills header", @"getLesMillsHeader", false, @"Max Character limit: 52", 12, false)]
            public Textstring Getlesmillsheader { get; set; }

            [ContentProperty(@"Get Les Mills button Text", @"getLesMillsButtonText", false, @"Max Character limit: 22", 13, false)]
            public Textstring Getlesmillsbuttontext { get; set; }

            [ContentProperty(@"Get Les Mills button Url", @"getLesMillsButtonUrl", false, @"Please enter the URL with http:// for external or without for internal.", 14, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Getlesmillsbuttonurl { get; set; }

            [ContentProperty(@"CTA Button Link", @"ctaButtonLink", false, @"", 4, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Ctabuttonlink { get; set; }

            [ContentProperty(@"CTA Button Video", @"ctaButtonVideo", false, @"", 5, false)]
            public LegacyMediaPicker Ctabuttonvideo { get; set; }

            [ContentProperty(@"SubheaderText", @"subheadertext", false, @"", 2, false)]
            public TextboxMultiple Subheadertext { get; set; }

            [ContentProperty(@"CTA Link Text", @"ctaLinkText", false, @"", 10, false)]
            public Textstring Ctalinktext { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}