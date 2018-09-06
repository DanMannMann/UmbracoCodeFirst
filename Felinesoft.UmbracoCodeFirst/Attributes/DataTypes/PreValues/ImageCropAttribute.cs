using Marsman.UmbracoCodeFirst.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Marsman.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
    public class ImageCropAttribute : MultipleCodeFirstAttribute, IDataTypeInstance
    {
        public ImageCropAttribute(string alias, int height, int width)
        {
            if (string.IsNullOrWhiteSpace(alias))
            {
                throw new CodeFirstException("Alias must be set to an alphanumeric value in [ImageCropAttribute] constructor");
            }
            Alias = alias;
            Height = height;
            Width = width;
        }

        public override bool Equals(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            if(!(obj is ImageCropAttribute))
            {
                return false;
            }
            var tobj = obj as ImageCropAttribute;
            return Alias.Equals(tobj.Alias, StringComparison.InvariantCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return Alias.GetHashCode();
        }

        [JsonProperty(propertyName:"alias")]
        public string Alias { get; private set; }

        [JsonProperty(propertyName: "height")]
        public int Height { get; private set; }

        [JsonProperty(propertyName: "width")]
        public int Width { get; private set; }
    }

}
