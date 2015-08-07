using Newtonsoft.Json;
using System;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ImageCropPropertyAttribute : CodeFirstAttribute
    {
        public ImageCropPropertyAttribute(int height, int width)
        {
            Height = height;
            Width = width;
        }

        [JsonProperty(propertyName: "height")]
        public int Height { get; private set; }

        [JsonProperty(propertyName: "width")]
        public int Width { get; private set; }
    }
}