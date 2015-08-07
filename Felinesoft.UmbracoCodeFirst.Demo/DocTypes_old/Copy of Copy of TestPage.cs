using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Felinesoft.UmbracoCodeFirst.Extensions;

namespace Felinesoft.UmbracoCodeFirst.Demo.DocTypes
{
    [DocumentType]
    public class TestComp1CC : DocumentTypeBase
    {
        [ContentProperty]
        public Textstring CompositeTextstring { get; set; }
    }

    [DocumentType]
    public class TestComp2CC : DocumentTypeBase
    {
        [ContentProperty]
        public Textstring CompositeTextstring2 { get; set; }

        [ContentComposition]
        public Home HomePage { get; set; }
    }

    [DocumentType]
    public class TestPageCC : Master
    {
        private SEOTab _tabOverride;

        public class SEOTab : Master.SEOTab
        {
            [ContentProperty]
            public Textstring ExtraSeoProperty { get; set; }
        }

        [ContentProperty]
        public int Counter { get; set; }

        [ContentProperty]
        public DateTime Dater { get; set; }

        [ContentTab]
        public new SEOTab SEO
        {
            get
            {
                return _tabOverride;
            }
            set
            {
                _tabOverride = value;
                base.SEO = value;
            }
        }

        [ContentTab(originalName:"FirstTab")]
        public FirstTab FirstTabNewName { get; set; }

        [ContentTab]
        public SecondTab SecondTab { get; set; }

        [ContentProperty]
        public RichtextEditor RichtextEditor { get; set; }

        [ContentProperty]
        public Tags Tags { get; set; }

        [ContentProperty]
        public TextboxMultiple TextboxMultiple { get; set; }

        [ContentProperty]
        public Textstring Textstring { get; set; }

        [ContentProperty]
        public TrueFalse TrueFalse { get; set; }

        [ContentProperty]
        public DocumentPicker<Genericpage> TypedContentPicker { get; set; }

        [ContentProperty]
        public Upload Upload { get; set; }

        [ContentComposition]
        public TestComp1 Comp { get; set; }
    }
}