using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Demo.MediaTypes;

namespace Felinesoft.UmbracoCodeFirst.Demo.DocTypes
{
    [DocumentType]
    public class TestComp1 : DocumentTypeBase
    {
        [ContentProperty]
        public virtual Textstring CompositeTextstring { get; set; }
    }

    [DocumentType]
    public class TestComp2 : DocumentTypeBase
    {
        [ContentProperty]
        public virtual Textstring CompositeTextstring2 { get; set; }

        [ContentComposition]
        public virtual Home HomePage { get; set; }
    }

    [DocumentType]
    [Template(isDefault:true)]
    public class TestPage : Master
    {
        public new class SEOTab : Master.SEOTab
        {
            [ContentProperty]
            public virtual Textstring ExtraSeoProperty { get; set; }
        }

        public class ImageTab : TabBase
        {
            [MediaPickerProperty(cssClasses: "test test-cropper test-attr")]
            public virtual Helicropter One { get; set; }

            [ContentProperty(cssClasses: "test test-cropper test-property")]
            public virtual SingleMediaPicker<Helicropter> Two { get; set; }

            [ContentProperty(cssClasses: "test test-file test-property")]
            public virtual SingleMediaPicker<MediaFile> Three { get; set; }

            [MediaPickerProperty(cssClasses: "test test-image test-attr")]
            public virtual MediaImage Four { get; set; }
        }

        [ContentTab]
        public ImageTab Images { get; set; }

        [ContentProperty]
        public virtual int Counter { get; set; }

        [ContentProperty]
        public virtual DateTime Dater { get; set; }

        [ContentTab]
        public new SEOTab SEO { get; set; }

        [ContentTab(originalName:"FirstTab")]
        public FirstTab FirstTabNewName { get; set; }

        [ContentTab]
        public SecondTab SecondTab { get; set; }

        [ContentProperty]
        public virtual RichtextEditor RichtextEditor { get; set; }

        [ContentProperty]
        public virtual Tags Tags { get; set; }

        [ContentProperty]
        public virtual TextboxMultiple TextboxMultiple { get; set; }

        [ContentProperty]
        public virtual Textstring Textstring { get; set; }

        [ContentProperty]
        public virtual TrueFalse TrueFalse { get; set; }

        [ContentProperty]
        public virtual SingleDocumentPicker<Genericpage> TypedContentPicker { get; set; }

        [ContentProperty]
        public virtual Upload Upload { get; set; }

        [ContentComposition]
        public virtual TestComp1 Comp { get; set; }
    }
}