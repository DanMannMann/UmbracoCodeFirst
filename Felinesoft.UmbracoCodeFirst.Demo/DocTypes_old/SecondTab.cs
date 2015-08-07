using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Core;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Demo.MediaTypes;

namespace Felinesoft.UmbracoCodeFirst.Demo.DocTypes
{
    public class SecondTab : TabBase
    {
        [ContentProperty]
        public virtual LegacyMemberPicker MemberPicker { get; set; }

        [ContentProperty]
        public virtual Numeric Numeric { get; set; }

        [ContentProperty]
        public virtual RadioButtonList RadioButtonList { get; set; }

        [ContentProperty]
        [CssClass("related-link")]
        public virtual RelatedLinks RelatedLinks { get; set; }

        [ContentProperty]
        public virtual Textstring Stringy { get; set; }

        [ContentProperty]
        [NodePickerConfig(maximumItems: 1)]
        public virtual MediaPicker<Image> SingleImage { get; set; }

        [ContentProperty]
        [DataTypeInstance]
        public virtual MediaPicker<Image> MultipleImage { get; set; }

        [ContentProperty]
        [NodePickerConfig(maximumItems: 1)]
        public virtual SingleMediaPicker<Helicropter> Cropped { get; set; }
    }
}