using System.Collections.Generic;
using System.Linq;

namespace Felinesoft.UmbracoCodeFirst.DataTypes
{
    public class ThumbnailSizesPreValueFactory : IPreValueFactory
    {
        private string _value;

        public ThumbnailSizesPreValueFactory(params int[] sizes)
        {
            _value = string.Join(";", sizes.Select(x => x.ToString()));
        }

        public IDictionary<string, Umbraco.Core.Models.PreValue> GetPreValues(PreValueContext context)
        {
            var result = new Dictionary<string, Umbraco.Core.Models.PreValue>();
            result.Add("thumbs", new Umbraco.Core.Models.PreValue(_value));
            return result;
        }
    }
}