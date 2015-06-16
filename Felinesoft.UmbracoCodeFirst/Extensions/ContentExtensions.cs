using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using System.Reflection;
using System.ComponentModel;
using Umbraco.Web;
using Umbraco.Core;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using System.Text.RegularExpressions;
using System.Globalization;
using Felinesoft.InitialisableAttributes;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Content;
using Felinesoft.UmbracoCodeFirst.Converters;

namespace Felinesoft.UmbracoCodeFirst.Extensions
{
    /// <summary>
    /// Extensions used to allow conversion between Umbraco interfaces
    /// and strongly typed models
    /// </summary>
    public static class ContentExtensions
    {
        #region Get Model from IContent
        /// <summary>
        /// Extension used to convert an IPublishedContent back to a Typed model instance.
        /// Your model does need to inherit from UmbracoGeneratedBase and contain the correct attributes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T ConvertToModel<T>(this IContent content)
        {
            T instance = Activator.CreateInstance<T>();
            var propertiesWithTabsAttribute = typeof(T).GetProperties().Where(x => x.GetCustomAttribute<DocumentTabAttribute>() != null);
            foreach (var property in propertiesWithTabsAttribute)
            {
                //tab instance
                DocumentTabAttribute tabAttribute = property.GetCodeFirstAttribute<DocumentTabAttribute>();
                var propertyTabInstance = Activator.CreateInstance(property.PropertyType);
                var propertiesOnTab = property.PropertyType.GetProperties();

                foreach (var propertyOnTab in propertiesOnTab.Where(x => x.GetCustomAttribute<DocumentPropertyAttribute>() != null))
                {
                    DocumentPropertyAttribute propertyAttribute = propertyOnTab.GetCodeFirstAttribute<DocumentPropertyAttribute>();
                    string alias = StringHelperExtensions.HyphenToUnderscore(StringHelperExtensions.ParseUrl(propertyAttribute.Alias + "_" + tabAttribute.Name, false));
                    GetPropertyValueOnInstance(content, propertyTabInstance, propertyOnTab, propertyAttribute, alias);
                }

                property.SetValue(instance, propertyTabInstance);

            }

            //properties on Generic Tab
            var propertiesOnGenericTab = typeof(T).GetProperties().Where(x => x.GetCustomAttribute<DocumentPropertyAttribute>() != null);
            foreach (var item in propertiesOnGenericTab)
            {
                DocumentPropertyAttribute umbracoPropertyAttribute = item.GetCodeFirstAttribute<DocumentPropertyAttribute>();
                GetPropertyValueOnInstance(content, instance, item, umbracoPropertyAttribute);
            }

            (instance as DocumentTypeBase).NodeDetails = new UmbracoNodeDetails(content);
            return instance;
        }

        private static void GetPropertyValueOnInstance(IContent content, object objectInstance, PropertyInfo propertyOnTab, DocumentPropertyAttribute propertyAttribute, string alias = null)
        {
            if (alias == null) alias = propertyAttribute.Alias;
            string umbracoStoredValue = content.GetValue<string>(alias);

            if (propertyAttribute.ConverterType != null)
            {
                IDataTypeConverter converter = (IDataTypeConverter)Activator.CreateInstance(propertyAttribute.ConverterType);
                propertyOnTab.SetValue(objectInstance, converter.Create(umbracoStoredValue)); 
            }
            else
            {
                propertyOnTab.SetValue(objectInstance, umbracoStoredValue);
            }
        }
        #endregion

        #region Get Model from IPublishedContent
        /// <summary>
        /// Extension used to convert an IPublishedContent back to a Typed model instance.
        /// Your model does need to inherit from UmbracoGeneratedBase and contain the correct attributes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T ConvertToModel<T>(this IPublishedContent content)
        {
            T instance = Activator.CreateInstance<T>();
            var propertiesWithTabsAttribute = typeof(T).GetProperties().Where(x => x.GetCodeFirstAttribute<DocumentTabAttribute>() != null);
            foreach (var property in propertiesWithTabsAttribute)
            {
                //tab instance
                DocumentTabAttribute tabAttribute = property.GetCodeFirstAttribute<DocumentTabAttribute>();
                var propertyTabInstance = Activator.CreateInstance(property.PropertyType);
                var propertiesOnTab = property.PropertyType.GetProperties();

                foreach (var propertyOnTab in propertiesOnTab.Where(x => x.GetCodeFirstAttribute<DocumentPropertyAttribute>() != null))
                {
                    DocumentPropertyAttribute propertyAttribute = propertyOnTab.GetCodeFirstAttribute<DocumentPropertyAttribute>();
                    string alias = StringHelperExtensions.HyphenToUnderscore(StringHelperExtensions.ParseUrl(propertyAttribute.Alias + (propertyAttribute.AddTabAliasToPropertyAlias ? "_" + tabAttribute.Name : ""), false));
                    GetPropertyValueOnInstance(content, propertyTabInstance, propertyOnTab, propertyAttribute, alias);
                }

                property.SetValue(instance, propertyTabInstance);

            }

            //properties on Generic Tab
            var propertiesOnGenericTab = typeof(T).GetProperties().Where(x => x.GetCodeFirstAttribute<DocumentPropertyAttribute>() != null);
            foreach (var item in propertiesOnGenericTab)
            {
                DocumentPropertyAttribute umbracoPropertyAttribute = item.GetCodeFirstAttribute<DocumentPropertyAttribute>();
                GetPropertyValueOnInstance(content, instance, item, umbracoPropertyAttribute);
            }

            (instance as DocumentTypeBase).NodeDetails = new UmbracoNodeDetails(content);
            return instance;
        }

        private static Type[] _supportedTypes = new Type[] { typeof(string), typeof(bool), typeof(DateTime), typeof(int) };

        private static void GetPropertyValueOnInstance(IPublishedContent content, object objectInstance, PropertyInfo propertyOnTab, DocumentPropertyAttribute propertyAttribute, string alias = null)
        {
            if (alias == null) alias = propertyAttribute.Alias;

            var umbracoStoredValue = content.GetPropertyValue(alias);

            if (propertyAttribute.ConverterType != null)
            {
                IDataTypeConverter converter = (IDataTypeConverter)Activator.CreateInstance(propertyAttribute.ConverterType);
                propertyOnTab.SetValue(objectInstance, converter.Create(umbracoStoredValue)); 
            }
            else
            {
                propertyOnTab.SetValue(objectInstance, umbracoStoredValue);
            }
        }
        #endregion

        #region Create and update IContent
        /// <summary>
        /// <para>Creates an IContent populated with the current values of the model</para>
        /// </summary>
        public static IContent CreateContent(this DocumentTypeBase input, int parentId)
        {
            if (input == null)
            {
                throw new ArgumentException("input cannot be null");
            }

            //Get the type alias and create the content
            var typeAlias = input.GetDocumentTypeAlias();
            var node = ApplicationContext.Current.Services.ContentService.CreateContent(input.NodeDetails.Name, parentId, typeAlias);
            MapModelToContent(input, node);
            return node;
        }

        /// <summary>
        /// Updates an existing IContent item with the current values of the model
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IContent UpdateContent(this DocumentTypeBase input)
        {
            if (input.NodeDetails == null || input.NodeDetails.UmbracoId == -1)
            {
                throw new ArgumentException("Can't update content for a model with no ID");
            }
            var node = ApplicationContext.Current.Services.ContentService.GetById(input.NodeDetails.UmbracoId);
            MapModelToContent(input, node);
            return node;
        }

        private static void MapModelToContent(DocumentTypeBase model, IContent node)
        {
            node.Name = model.NodeDetails.Name;

            //Get and then set all the first-level properties
            var props = model.GetType().GetPropertyTuples().ToList();
            foreach (var prop in props)
            {
                SetPropertyOnContent(node, prop.Item2, prop.Item1.GetValue(model));
            }

            //Get and then set all the properties from any tabs
            var tabs = model.GetType().GetProperties().Where(x => x.GetCustomAttribute<DocumentTabAttribute>() != null);
            foreach (var tab in tabs)
            {
                var attr = tab.GetCodeFirstAttribute<DocumentTabAttribute>();
                var tabInstance = tab.GetValue(model);

                if (tabInstance != null)
                {
                    var tabProps = tab.PropertyType.GetPropertyTuples();
                    foreach (var prop in tabProps)
                    {
                        SetPropertyOnContent(node, prop.Item2, prop.Item1.GetValue(tabInstance), "_" + attr.Name.Replace(" ", "_"));
                    }
                }
            }
        }

        private static IEnumerable<Tuple<PropertyInfo, DocumentPropertyAttribute>> GetPropertyTuples(this Type type)
        {
            return type.GetProperties().Where(x => x.GetCodeFirstAttribute<DocumentPropertyAttribute>() != null).Select(z => new Tuple<PropertyInfo, DocumentPropertyAttribute>(z, z.GetCodeFirstAttribute<DocumentPropertyAttribute>()));
        }

        private static void SetPropertyOnContent(IContent content, DocumentPropertyAttribute umbracoPropertyAttribute, object propertyValue, string tabPostfix = "")
        {
            object convertedValue;
            if (umbracoPropertyAttribute.ConverterType != null)
            {

                TypeConverter converter = (TypeConverter)Activator.CreateInstance(umbracoPropertyAttribute.ConverterType);
                convertedValue = converter.ConvertTo(null, CultureInfo.InvariantCulture, propertyValue, typeof(string));
            }
            else
            {
                //No converter is given so we push the string back into umbraco without conversion
                convertedValue = (propertyValue != null ? propertyValue.ToString() : string.Empty);
            }

            content.SetValue(umbracoPropertyAttribute.Alias + tabPostfix, convertedValue);
        }
        #endregion
    }
}
