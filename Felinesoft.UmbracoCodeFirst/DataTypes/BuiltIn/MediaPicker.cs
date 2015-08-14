
using Felinesoft.UmbracoCodeFirst;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Umbraco.Core.Models;
using System;
using Felinesoft.UmbracoCodeFirst.Core;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using System.Web;
using Umbraco.Web;

namespace Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn
{
    [DataType(propertyEditorAlias: BuiltInPropertyEditorAliases.MultiNodeTreePicker)]
    public class MediaPicker<T> : NodePicker<T, MediaNodeDetails>, IMediaPicker where T : MediaTypeBase, new()
    {
        public MediaPicker() : base(NodeType.media) { }

        public override string ToHtmlString()
        {
            if (Items.Count == 1)
            {
                var item = Items.First();
                if (item is IHtmlString)
                {
                    return (item as IHtmlString).ToHtmlString();
                }
                else
                {
                    return HttpUtility.HtmlEncode(item.ToString());
                }
            }
            else if (Items.Count > 1)
            {
                return Items.Count + " media items selected";
            }
            else
            {
                return "No media items selected";
            }
        }

        public override string ToString()
        {
            if (Items.Count == 1)
            {
                var item = Items.First();
                return item.ToString();
            }
            else if (Items.Count > 1)
            {
                return Items.Count + " media items selected";
            }
            else
            {
                return "No media items selected";
            }
        }

        protected override T GetModelFromId(int id)
        {
            var node = new Umbraco.Web.UmbracoHelper(Umbraco.Web.UmbracoContext.Current).TypedMedia(id);
            return node.ConvertMediaToModel<T>(CodeFirstModelContext.GetContext(this));
        }
    }

    internal interface IMediaPicker { }
}