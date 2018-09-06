using Newtonsoft.Json;
using System.Web;

namespace Marsman.UmbracoCodeFirst.DataTypes
{
    public class RelatedLink : IHtmlString
    {
        private IUmbracoDataType _parent;

        internal void SetParent(IUmbracoDataType parent)
        {
            _parent = parent;
        }

        /// <summary>
        /// A free-text caption
        /// </summary>
        [JsonProperty("caption")]
        public string Caption { get; set; }

        /// <summary>
        /// An integer if IsInternal is true, an absolute URL if IsInternal is false
        /// </summary>
        [JsonProperty("link")]
        public string Url { get; set; }

        /// <summary>
        /// True to open link in _blank
        /// </summary>
        [JsonProperty("newWindow")]
        public bool NewWindow { get; set; }

        /// <summary>
        /// True if the link is to a document node in this site
        /// </summary>
        [JsonProperty("isInternal")]
        public bool IsInternal { get; set; }

        /// <summary>
        /// The name of the internal node, or null if IsInternal is false
        /// </summary>
        [JsonProperty("internalName")]
        public string InternalName { get; set; }

        /// <summary>
        /// A free-text title. When a link is specified via the Umb back-office this is always the same as the caption.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("edit")]
        public bool Edit { get; set; }

        [JsonProperty("type")]
        public string LinkType { get; set; }

        /// <summary>
        /// The ID of the selected internal node.
        /// </summary>
        [JsonProperty("internal")]
        public int? SelectedNodeId { get; set; }

        public override string ToString()
        {
            return Url;
        }

        public string ToHtmlString()
        {
            var toAdd = _parent == null ? string.Empty : DataTypeUtils.GetHtmlTagContentFromContextualAttributes(_parent);
            return string.Format("<a{0} target='{1}' href='{2}'>{3}</a>", toAdd, NewWindow ? "_blank" : "_self", Url, Caption);
        }
    }
}