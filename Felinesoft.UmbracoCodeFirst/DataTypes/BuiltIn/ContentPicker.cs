using Felinesoft.UmbracoCodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using Umbraco.Web;
using Felinesoft.UmbracoCodeFirst.Extensions;

namespace Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn
{
    /// <summary>
    /// Represents Umbraco's built-in content picker data type
    /// </summary>
    [DataType(name: BuiltInDataTypes.ContentPicker, propertyEditorAlias: BuiltInPropertyEditorAliases.ContentPickerAlias)]
    [BuiltInDataType]
    public class ContentPicker : IUmbracoIntDataType
    {
        /// <summary>
        /// The URL of the content
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// The ID of the content
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the content node
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// A description of any errors which occurred whilst loading the content
        /// </summary>
        public string ErrorMessage { get; protected set; }

        /// <summary>
        /// True if any errors occurred whilst loading the content
        /// </summary>
        public virtual bool HasContentError { get { return ErrorMessage != null; } }

        /// <summary>
        /// Initialises the instance from an Umbraco node ID
        /// </summary>
        public void Initialise(int dbValue)
        {
            var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            var id = dbValue;
            IPublishedContent content = umbracoHelper.TypedContent(id);
            if (content != null)
            {
                Url = content.Url;
                Name = content.Name;
                Id = id;
            }
            else
            {
                ErrorMessage = "Selected ID " + id.ToString() + " returned null content (maybe content was deleted)";
            }
        }

        public virtual IPublishedContent PublishedContent { get; private set; }

        /// <summary>
        /// Creates a new instance using the given ID.
        /// If published content with the specified ID exists then the URL and name are loaded.
        /// </summary>
        public static ContentPicker Create(int nodeId)
        {
            var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
            var result = new ContentPicker();
            IPublishedContent content = umbracoHelper.TypedContent(nodeId);
            if (content != null)
            {
                result.Url = content.Url;
                result.Name = content.Name;
                result.Id = nodeId;
            }
            else
            {
                result.ErrorMessage = "Selected ID " + nodeId.ToString() + " returned null content (maybe content was deleted)";
            }
            return result;
        }

        /// <summary>
        /// Returns the selected ID as a string
        /// </summary>
        public int Serialise()
        {
            return Id;
        }

        public override string ToString()
        {
            return Url;
        }
    }
}