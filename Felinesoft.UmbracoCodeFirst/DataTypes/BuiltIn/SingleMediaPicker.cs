using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.ContentTypes;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;

namespace Marsman.UmbracoCodeFirst.DataTypes.BuiltIn
{
    [DataType(propertyEditorAlias: BuiltInPropertyEditorAliases.MultiNodeTreePicker)]
    public class SingleMediaPicker<T> : MediaPicker<T>, IPickedItem<T>, IHtmlString where T : MediaTypeBase, new()
    {
		public static implicit operator SingleMediaPicker<T>(T value)
		{
			var val = new BuiltIn.SingleMediaPicker<T>();
			val.SetCollection(new T[] { value });
			return val;
		}

		public string Url
        {
            get
            {
                var item = this.FirstOrDefault();
                return item == null || item.NodeDetails == null ? string.Empty : item.NodeDetails.Url;
            }
        }

        public string Name
        {
            get
            {
                var item = this.FirstOrDefault();
                return item == null || item.NodeDetails == null ? string.Empty : item.NodeDetails.Name;
            }
        }

        public T PickedItem
        {
            get
            {
                var item = this.FirstOrDefault();
                return item;
            }
        }

        public override string ToHtmlString()
        {
            var item = Items.FirstOrDefault();
            if (item == null)
            {
                return string.Empty;
            }
            else if (item is IHtmlString)
            {
                return (item as IHtmlString).ToHtmlString();
            }
            else
            {
                return HttpUtility.HtmlEncode(item.ToString());
            }
        }

        public override string ToString()
        {
            var item = Items.FirstOrDefault();
            if (item == null)
            {
                return string.Empty;
            }
            else
            {
                return item.ToString();
            }
        }

        public override IDictionary<string, PreValue> GetPreValues(PreValueContext context)
        {
            return base.GetPreValuesInternal(context, 1);
        }
    }

    public interface IPickedItem<T>
    {
        T PickedItem { get; }
    }

    public interface IPickedItems<T>
    {
        IEnumerable<T> Items { get; }
    }
}