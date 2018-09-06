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
    [DocumentType(@"MODULE: Related Classes", @"MODULERelatedClasses", new Type[] { typeof(Submoduleproxyclass) }, @".sprTreeFolder", false, false, @"")]
    public class Modulerelatedclasses : Modules
    {
        public class ContentTab : TabBase
        {
            [ContentProperty(@"Module heading", @"relatedClassesmoduleHeading", false, @"Max Character limit: 58", 0, false)]
            public Textstring Relatedclassesmoduleheading { get; set; }

            [ContentProperty(@"Module subheading", @"relatedClassesModuleSubheading", false, @"Max Character limit: 58", 1, false)]
            public Textstring Relatedclassesmodulesubheading { get; set; }

            [ContentProperty(@"Subheading", @"relatedClassesSubheading", false, @"Max Character limit: 54", 3, false)]
            public Textstring Relatedclassessubheading { get; set; }

            [ContentProperty(@"CTA Text", @"relatedClassesCTAText", false, @"Max Character limit: 27", 4, false)]
            public Textstring Relatedclassesctatext { get; set; }

            [ContentProperty(@"CTA Url", @"relatedClassesCTAUrl", false, @"", 5, false)]
            public LMI.BusinessLogic.CodeFirst.UrlPicker Relatedclassesctaurl { get; set; }

            [ContentProperty(@"Related Classes", @"relatedClasses", false, @"", 2, false)]
            public LMI.BusinessLogic.CodeFirst.ClassPicker Relatedclasses { get; set; }

            [ContentProperty(@"Module heading colour", @"relatedClassesModuleHeadingColour", false, @"", 6, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Relatedclassesmoduleheadingcolour { get; set; }

            [ContentProperty(@"Module subheading colour", @"relatedClassesModuleSubheadingColour", false, @"", 7, false)]
            public LMI.BusinessLogic.CodeFirst.SpectrumColorPicker Relatedclassesmodulesubheadingcolour { get; set; }

        }

        [ContentTab(@"Content", 0)]
        public ContentTab Content { get; set; }
    }
}