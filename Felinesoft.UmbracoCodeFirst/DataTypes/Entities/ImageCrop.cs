using Felinesoft.UmbracoCodeFirst.Core;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Newtonsoft.Json;
using System.Web;
using Umbraco.Web.Models;

namespace Felinesoft.UmbracoCodeFirst.DataTypes
{
    public class ImageCrop : ImageCropData, IHtmlString
    {
        [JsonIgnore]
        private ImageCropDataSet _underlying;

        [JsonIgnore]
        private ImageCropper _parent;

        public ImageCrop(ImageCropper parent)
        {
            Effects = new CropEffects();
            _parent = parent;
        }

        public void SetUnderlyingDataset(ImageCropDataSet underlying)
        {
            _underlying = underlying;
            Effects.SetUnderlyingDataset(_underlying, Alias);
        }

        [JsonIgnore]
        public CropEffects Effects { get; set; }

        [JsonIgnore]
        public string Url
        {
            get
            {
                return _underlying.GetCropUrl(Alias, true, _underlying.HasFocalPoint());
            }
        }

        public string ToHtmlString()
        {
            var css = DataTypeUtils.GetHtmlClassAttribute(_parent);
            css = css.Replace("codefirst-cropper", "codefirst-cropper codefirst-cropper-" + Alias);
            return "<img" + css + " src='" + Url + "' alt='" + CodeFirstModelContext.GetContext(this).ContentType.Name + " - " + Alias + "' />";
        }

        public override string ToString()
        {
            return Url;
        }
    }
}