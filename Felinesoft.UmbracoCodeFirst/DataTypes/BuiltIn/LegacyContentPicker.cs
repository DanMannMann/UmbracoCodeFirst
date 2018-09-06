using Marsman.UmbracoCodeFirst;
using System;
using Marsman.UmbracoCodeFirst.Core;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Umbraco.Web;
using Marsman.UmbracoCodeFirst.Extensions;

namespace Marsman.UmbracoCodeFirst.DataTypes.BuiltIn
{
    /// <summary>
    /// Represents Umbraco's built-in content picker data type
    /// </summary>
    [DataType(name: BuiltInDataTypes.ContentPicker, propertyEditorAlias: BuiltInPropertyEditorAliases.ContentPickerAlias)]
    [DoNotSyncDataType][BuiltInDataType]
    public class LegacyContentPicker : IUmbracoIntegerDataType, IHtmlString
    {
		public static implicit operator LegacyContentPicker(int value)
		{
			return new LegacyContentPicker() { Id = value };
		}

		public static implicit operator LegacyContentPicker(CodeFirstContentBase value)
		{
			try
			{
				return new LegacyContentPicker() { Id = value.NodeDetails.UmbracoId };
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("Node Id (CodeFirstContentBase.NodeDetails.UmbracoId) must be set", ex);
			}
		}

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

        public string ToHtmlString()
        {
            return Url;
        }
    }
}