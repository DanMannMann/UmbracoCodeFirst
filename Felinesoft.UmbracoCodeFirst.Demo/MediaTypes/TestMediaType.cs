using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Felinesoft.UmbracoCodeFirst.Demo.MediaTypes
{
    [MediaType(icon: BuiltInIcons.IconVideo)]
    public class SEOVideo : MediaImageBase
    {
        [ContentTab]
        public SEOVideoTab SEO { get; set; }
    }

    public class SEOVideoTab : TabBase
    {
        [ContentProperty]
        public Textstring Test { get; set; }
    }

    [MediaType]
    public class TestMediaType : MediaTypeBase
    {
        [FileUploadProperty]
        public Upload File { get; set; }

        [FileSizeProperty]
        public Label FileSize { get; set; }

        [ContentProperty(alias:"borderColor")] //Property renamed 23/07/2015 - Dan M
        [DataTypeInstance]
        [InstancePreValue("0", "CC52A3")]
        [InstancePreValue("1", "24248F")]
        [InstancePreValue("2", "6BB247")]
        [InstancePreValue("3", "CCFFCC")]
        public ColorPicker FileIconColor { get; set; }

        [ContentComposition]
        public TestMediaTypeTwo Image { get; set; }
    }

    [MediaType]
    public class TestMediaTypeTwo : MediaTypeBase
    {
        [ImageWidthProperty]
        public Label Width { get; set; }

        [ImageHeightProperty]
        public Label Height { get; set; }

        [ContentProperty]
        [DataTypeInstance]
        [InstancePreValue("0", "470024")]
        [InstancePreValue("1", "990000")]
        [InstancePreValue("2", "000066")]
        [InstancePreValue("3", "002500")]
        public ColorPicker ImageBorderColor { get; set; }
    }

    [MediaType]
    public class Helicropter : MediaTypeBase
    {
        [FileUploadProperty]
        public CropShop RoyCropper { get; set; }
    }

    [DataType]
    public class CropShop : ImageCropper
    {
        [ImageCropProperty(200, 200)]
        public ImageCrop BigThumb { get; set; }

        [ImageCropProperty(100, 100)]
        public ImageCrop SmallThumb { get; set; }
    }
}