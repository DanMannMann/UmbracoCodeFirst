
using Felinesoft.UmbracoCodeFirst;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Umbraco.Core.Models;
using System;
using Umbraco.Web;
using Newtonsoft.Json;

namespace Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn
{
    [DataType("Umbraco.RelatedLinks", "Related Links", null, DataTypeDatabaseType.Ntext)]
    [BuiltInDataType]
    public class RelatedLinks : UmbracoJsonDataType, IEnumerable<RelatedLink>, ICollection<RelatedLink>
    {
        public RelatedLinks()
            : base(NullValueHandling.Ignore)
        {
            Links = new List<RelatedLink>();
        }

        public List<RelatedLink> Links { get; set; }

        [JsonIgnore]
        public RelatedLink this[int index]
        {
            get
            {
                return Links[index];
            }
            set
            {
                Links[index] = value;
            }
        }

        public void Add(RelatedLink item)
        {
            Links.Add(item);
        }

        public void Clear()
        {
            Links.Clear();
        }

        public bool Contains(RelatedLink item)
        {
            return Links.Contains(item);
        }

        public void CopyTo(RelatedLink[] array, int arrayIndex)
        {
            Links.CopyTo(array, arrayIndex);
        }

        [JsonIgnore]
        public int Count
        {
            get { return Links.Count; }
        }

        [JsonIgnore]
        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(RelatedLink item)
        {
            return Links.Remove(item);
        }

        public IEnumerator<RelatedLink> GetEnumerator()
        {
            return Links.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Links.GetEnumerator();
        }
    }

    public class RelatedLink
    {
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
        /// A read-only property which is computed depending on the value of IsInternal.
        /// </summary>
        [JsonProperty("internal")]
        public int? SelectedNodeId { get; set; }

        /// <summary>
        /// Describes any error encountered when computing the read-only properties (SelectedNodeId and Url)
        /// </summary>
        [JsonIgnore]
        public string ErrorMessage { get; private set; }

        public override string ToString()
        {
            return Url;
        }
    }
}