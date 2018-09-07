using Marsman.UmbracoCodeFirst.Core;
using Marsman.UmbracoCodeFirst.DataTypes.BuiltIn;
using Newtonsoft.Json;
using System.Web;
using Umbraco.Web.Models;

namespace Marsman.UmbracoCodeFirst.DataTypes
{
    public class ImageCrop : ImageCropData, IHtmlString
    {
        [JsonIgnore]
        private ImageCropDataSet _underlying;

        public ImageCrop()
        {
            Effects = new CropEffects();
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
                return _underlying.Src + _underlying.GetCropUrl(Alias, true, _underlying.HasFocalPoint());
            }
        }

        public string ToHtmlString()
        {
            var toAdd = DataTypeUtils.GetHtmlTagContentFromContextualAttributes(this);
            return "<img" + toAdd + " src='" + Url + "' />";
        }

        public override string ToString()
        {
            return Url;
        }
    }
}