using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.InitialisableAttributes;
using Felinesoft.UmbracoCodeFirst.Attributes;

namespace Felinesoft.UmbracoCodeFirst.DocumentTypes
{
    /// <summary>
    /// A base class for code-first document types
    /// </summary>
    public abstract class DocumentTypeBase
    {
        /// <summary>
        /// A base class for code-first document types.
        /// This constructor initialises the NodeDetails property with an empty instance of <see cref="UmbracoNodeDetails"/>
        /// </summary>
        public DocumentTypeBase()
        {
            NodeDetails = new UmbracoNodeDetails();
        }

        /// <summary>
        /// Gets the details of the represented node
        /// </summary>
        public UmbracoNodeDetails NodeDetails { get; internal set; }

        /// <summary>
        /// Persists the current values of the instance back to the database
        /// </summary>
        /// <param name="contentId">Id of the Umbraco Document</param>
        /// <param name="userId">The user ID for the audit trail</param>
        /// <param name="raiseEvents">True to raise Umbraco content service events</param>
        public virtual void Persist(int contentId, int userId = 0, bool raiseEvents = false)
        {
            IContentService contentSerivce = ApplicationContext.Current.Services.ContentService;
            IContent content = contentSerivce.GetById(contentId);

            //search for propertys with the UmbracoTab on
            Type currentType = this.GetType();
            var propertiesWithTabAttribute = currentType.GetProperties().Where(x => x.GetCodeFirstAttribute<DocumentTabAttribute>() != null).ToArray();
            int length = propertiesWithTabAttribute.Count();
            for (int i = 0; i < length; i++)
            {
                PropertyInfo tabProperty = propertiesWithTabAttribute[i];
                Type tabType = tabProperty.PropertyType;
                object instanceOfTab = tabProperty.GetValue(this);
                DocumentTabAttribute tabAttribute = tabProperty.GetCodeFirstAttribute<DocumentTabAttribute>();

                //persist the fields foreach tab
                var propertiesInsideTab = tabType.GetProperties().Where(x => x.GetCodeFirstAttribute<DocumentPropertyAttribute>() != null).ToArray();
                int propertyLength = propertiesInsideTab.Length;
                for (int j = 0; j < propertyLength; j++)
                {
                    PropertyInfo property = propertiesInsideTab[j];
                    DocumentPropertyAttribute umbracoPropertyAttribute = property.GetCodeFirstAttribute<DocumentPropertyAttribute>();
                    object propertyValue = property.GetValue(instanceOfTab);
                    string alias = StringHelperExtensions.HyphenToUnderscore(StringHelperExtensions.ParseUrl(umbracoPropertyAttribute.Alias + "_" + tabAttribute.Name, false));
                    SetPropertyOnIContent(content, umbracoPropertyAttribute, propertyValue, alias);
                }
            }

            //properties on generic tab
            var propertiesOnGenericTab = currentType.GetProperties().Where(x => x.GetCodeFirstAttribute<DocumentPropertyAttribute>() != null);
            foreach (var item in propertiesOnGenericTab)
            {
                DocumentPropertyAttribute umbracoPropertyAttribute = item.GetCodeFirstAttribute<DocumentPropertyAttribute>();
                object propertyValue = item.GetValue(this);
                SetPropertyOnIContent(content, umbracoPropertyAttribute, propertyValue);
            }

            if (NodeDetails != null)
            {
                content.Name = NodeDetails.Name;
            }

            //persist object into umbraco database
            contentSerivce.SaveAndPublishWithStatus(content, userId, raiseEvents);

            //update the node details
            NodeDetails = new UmbracoNodeDetails(content);
        }

        private void SetPropertyOnIContent(IContent content, DocumentPropertyAttribute umbracoPropertyAttribute, object propertyValue, string alias = null)
        {
            object convertedValue;
            if (umbracoPropertyAttribute.ConverterType != null)
            {

                TypeConverter converter = (TypeConverter)Activator.CreateInstance(umbracoPropertyAttribute.ConverterType);
                convertedValue = converter.ConvertTo(null, CultureInfo.InvariantCulture, propertyValue, typeof(string));
            }
            else
            {
                //No converter is given so basically we push the string back into umbraco
                convertedValue = (propertyValue != null ? propertyValue.ToString() : string.Empty);
            }

            if (alias == null) alias = umbracoPropertyAttribute.Alias;

            content.SetValue(alias, convertedValue);
        }
    }
}
