using Newtonsoft.Json;

namespace Felinesoft.UmbracoCodeFirst.DataTypes
{
    public class ImageCropDefinition
    {
        [JsonProperty(propertyName: "height")]
        public int Height { get; set; }

        [JsonProperty(propertyName: "width")]
        public int Width { get; set; }

        [JsonProperty(propertyName: "alias")]
        public string Alias { get; set; }
    }
}