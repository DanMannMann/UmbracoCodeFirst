using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Core.Modules;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public class PropertyModule : IPropertyModule 
    {
        private IDataTypeModule _dataTypeModule;
        private IDataTypeService _dataTypeService;

        public PropertyModule(IDataTypeModule dataTypeModule, IDataTypeService dataTypeService)
        {
            _dataTypeModule = dataTypeModule;
            _dataTypeService = dataTypeService;
        }

        public void Initialise(IEnumerable<Type> classes)
        {
            
        }

        /// <summary>
        /// Creates a new property on the ContentType under the correct tab
        /// </summary>
        /// <param name="newContentType"></param>
        /// <param name="tabName"></param>
        /// <param name="dataTypeService"></param>
        /// <param name="atTabGeneric"></param>
        /// <param name="item"></param>
        public PropertyRegistration CreateProperty(IContentTypeBase newContentType, TabRegistration tab, PropertyInfo item, Type documentClrType)
        {
            ContentPropertyAttribute attribute = item.GetCodeFirstAttribute<ContentPropertyAttribute>();
            var tabPostfix = tab == null || !attribute.AddTabAliasToPropertyAlias ? null : tab.OriginalName == null ? tab.Name : tab.OriginalName;
            var dataType = _dataTypeModule.GetDataType(item);
            var property = new PropertyRegistration();
            property.Name = attribute.Name;
            property.Alias = tabPostfix == null ? attribute.Alias : StringHelperExtensions.HyphenToUnderscore(StringHelperExtensions.ParseUrl(attribute.Alias + "_" + tabPostfix, false));
            property.DataType = dataType;
            property.PropertyAttribute = attribute;
            property.Metadata = item;

            PropertyType propertyType = new PropertyType(dataType.Definition);
            propertyType.Name = property.Name;
            propertyType.Alias = property.Alias;
            propertyType.Description = attribute.Description;
            propertyType.Mandatory = attribute.Mandatory;
            propertyType.SortOrder = attribute.SortOrder;
            propertyType.ValidationRegExp = attribute.ValidationRegularExpression;


            var propertyDeclaredOnThisDocType = property.Metadata.DeclaringType == documentClrType || property.Metadata.DeclaringType.GetCodeFirstAttribute<CodeFirstCommonBaseAttribute>(false) != null;
            var propertyDeclaredOnThisTab = tab == null ? false : property.Metadata.DeclaringType == tab.ClrType;
            var tabDeclaredOnThisDocType = tab == null ? false : tab.PropertyOfParent.DeclaringType == documentClrType || tab.PropertyOfParent.DeclaringType.GetCodeFirstAttribute<CodeFirstCommonBaseAttribute>() != null;
            var declaringTypeIsDocType = property.Metadata.DeclaringType.GetCodeFirstAttribute<ContentTypeAttribute>(false) != null;
            var propertyIsFromCommonBase = tab == null ?
                property.Metadata.DeclaringType.GetCodeFirstAttribute<CodeFirstCommonBaseAttribute>() != null && property.Metadata.DeclaringType == documentClrType.BaseType 
                : 
                property.Metadata.DeclaringType.GetCodeFirstAttribute<CodeFirstCommonBaseAttribute>() != null && property.Metadata.DeclaringType == tab.ClrType.BaseType;

            if (tab == null)
            {
                if (propertyDeclaredOnThisDocType || propertyIsFromCommonBase) //only if property declared at this level (or inherited from a non-doctype class)
                {
                    if (!propertyIsFromCommonBase || !newContentType.PropertyTypeExists(propertyType.Alias)) //check if common base properties already exist
                    {
                        CodeFirstManager.Current.Log("Adding property " + property.Name + " on content type " + newContentType.Name, this);
                        newContentType.AddPropertyType(propertyType);
                    }
                }
            }
            else if (tabDeclaredOnThisDocType || propertyIsFromCommonBase) //only if tab declared at this level
            {
                if (propertyDeclaredOnThisTab || propertyIsFromCommonBase) //only if property declared at this level
                {
                    if (!propertyIsFromCommonBase || !newContentType.PropertyTypeExists(propertyType.Alias)) //check if common base properties already exist
                    {
                        CodeFirstManager.Current.Log("Adding property " + property.Name + " on tab " + tab.Name + " of content type " + newContentType.Name, this);
                        newContentType.AddPropertyType(propertyType, tab.Name);
                    }
                }
            }

            return property;
        }

        /// <summary>
        /// Checks whether a property exists and adds if if it does not. The data type, alias, description and mandatory flag are update for existing properties, but not persisted.
        /// Callers should persist the value.
        /// </summary>
        public PropertyRegistration VerifyExistingProperty(IContentTypeBase contentType, TabRegistration tab, PropertyInfo item, Type documentClrType, ref bool modified)
        {
            ContentPropertyAttribute attribute = item.GetCodeFirstAttribute<ContentPropertyAttribute>();
            var tabPostfix = tab == null || !attribute.AddTabAliasToPropertyAlias ? null : tab.OriginalName == null ? tab.Name : tab.OriginalName;
            var property = new PropertyRegistration();
            var alias = property.Alias = tabPostfix == null ? attribute.Alias : StringHelperExtensions.HyphenToUnderscore(StringHelperExtensions.ParseUrl(attribute.Alias + "_" + tabPostfix, false));
            var dataType = property.DataType = _dataTypeModule.GetDataType(item);
            property.Name = attribute.Name;
            property.PropertyAttribute = attribute;
            property.Metadata = item;

            if (tab == null)
            {
                CodeFirstManager.Current.Log("Syncing property " + property.Name + " on content type " + contentType.Name, this);
            }
            else
            {
                CodeFirstManager.Current.Log("Syncing property " + property.Name + " on tab " + tab.Name + " of content type " + contentType.Name, this);
            }

            bool alreadyExisted = contentType.PropertyTypeExists(alias);
            PropertyType umbracoProperty = contentType.PropertyTypes.FirstOrDefault(x => x.Alias == alias);

            if (umbracoProperty == null && alreadyExisted)
            {
                //This is a property from an underlying tab. Leave it alone. Log a warning in case this is an orphaned property.
                CodeFirstManager.Current.Warn("Property ignored as it appears to exist on an ancestor type. Warning: could be orphaned. Property alias: " + alias + ", type alias: " + contentType.Alias, this);
                return property;
            }

            if(alreadyExisted)
            {
                modified = modified ||
                               !umbracoProperty.Name.Equals(attribute.Name, StringComparison.InvariantCultureIgnoreCase) || 
                               umbracoProperty.Mandatory != attribute.Mandatory ||
                               (umbracoProperty.SortOrder != attribute.SortOrder && attribute.SortOrder != 0) || //don't count sort order changes if no sort order is specified, as Umbraco will have assigned an automatic one
                               umbracoProperty.ValidationRegExp != attribute.ValidationRegularExpression;

                if (contentType.Description != attribute.Description)
                {
                    //If not both null/empty
                    if (!(string.IsNullOrEmpty(contentType.Description) && string.IsNullOrEmpty(attribute.Description)))
                    {
                        modified = true;
                    }
                }
            }

            if (umbracoProperty == null)
            {
                modified = true;
                umbracoProperty = new PropertyType(dataType.Definition);
            }
            else if (umbracoProperty.DataTypeDefinitionId != dataType.Definition.Id)
            {
                modified = true;
                umbracoProperty.DataTypeDefinitionId = dataType.Definition.Id;
            }

            umbracoProperty.Name = attribute.Name;
            umbracoProperty.Alias = alias;
            umbracoProperty.Description = attribute.Description;
            umbracoProperty.Mandatory = attribute.Mandatory;
            umbracoProperty.SortOrder = attribute.SortOrder;
            umbracoProperty.ValidationRegExp = attribute.ValidationRegularExpression;

            var propertyDeclaredOnThisDocType = property.Metadata.DeclaringType == documentClrType;
            var propertyDeclaredOnThisTab = tab == null ? false : property.Metadata.DeclaringType == tab.ClrType;
            var tabDeclaredOnThisDocType = tab == null ? false : tab.PropertyOfParent.DeclaringType == documentClrType || tab.PropertyOfParent.DeclaringType.GetCustomAttribute<ContentTypeAttribute>() == null;
            var declaringTypeIsDocType = property.Metadata.DeclaringType.GetCodeFirstAttribute<ContentTypeAttribute>(false) != null;
            var propertyIsFromCommonBase = tab == null ?
                property.Metadata.DeclaringType.GetCodeFirstAttribute<CodeFirstCommonBaseAttribute>() != null && property.Metadata.DeclaringType == documentClrType.BaseType
                :
                property.Metadata.DeclaringType.GetCodeFirstAttribute<CodeFirstCommonBaseAttribute>() != null && property.Metadata.DeclaringType == tab.ClrType.BaseType;

            if (alreadyExisted)
            {
                if (propertyIsFromCommonBase || (tabDeclaredOnThisDocType && propertyDeclaredOnThisTab))
                {
                    var currentTab = contentType.PropertyGroups.Where(x => x.PropertyTypes.Any(y => y.Alias == alias)).FirstOrDefault();
                    if (currentTab == null || !currentTab.Name.Equals(tab.Name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        modified = true;
                        contentType.MovePropertyType(alias, tab.Name);
                    }
                }
            }
            else
            {
                if (tab == null)
                {
                    if (propertyDeclaredOnThisDocType || !declaringTypeIsDocType) //only if property declared at this level (or inherited from common base)
                    {
                        if (!propertyIsFromCommonBase || !contentType.PropertyTypeExists(umbracoProperty.Alias)) //check if common base properties already exist
                        {
                            CodeFirstManager.Current.Log("Adding property " + property.Name + " on content type " + contentType.Name, this);
                            contentType.AddPropertyType(umbracoProperty);
                        }
                    }
                }
                else if (tabDeclaredOnThisDocType || propertyIsFromCommonBase) //only if tab declared at this level
                {
                    if (propertyDeclaredOnThisTab || propertyIsFromCommonBase) //only if property declared at this level
                    {
                        if (!propertyIsFromCommonBase || (tabDeclaredOnThisDocType && !contentType.PropertyTypeExists(umbracoProperty.Alias))) //check if common base properties already exist
                        {
                            CodeFirstManager.Current.Log("Adding property " + property.Name + " on tab " + tab.Name + " of content type " + contentType.Name, this);
                            contentType.AddPropertyType(umbracoProperty, tab.Name);
                        }
                    }
                }
            }

            return property;
        }

        private bool IsInheritedProperty(TabRegistration tab, Type documentClrType, PropertyRegistration property)
        {
            //If there is no current tab
            if (tab == null)
            {
                //and the declaring type is not the current type
                if (property.Metadata.DeclaringType != documentClrType)
                {
                    //and the declaring type is also an Umbraco content type
                    if (property.Metadata.DeclaringType.GetCodeFirstAttribute<ContentTypeAttribute>(false) != null)
                    {
                        //Inherited property. Don't persist at this level.
                        return true;
                    }
                }
            }
            //else if the current tab is not directly declared on the current type
            else if (tab.PropertyOfParent.DeclaringType != documentClrType)
            {
                //and the declaring type of the tab is also an Umbraco content type
                if (tab.PropertyOfParent.DeclaringType.GetCodeFirstAttribute<ContentTypeAttribute>(false) != null)
                {
                    //Inherited property. Don't persist at this level.
                    return true;
                }
            }
            //else if the current property is not directly declared on the current tab type
            else if (property.Metadata.DeclaringType != tab.ClrType)
            {
                var parent = documentClrType.BaseType.GetProperties().Where(x => x.IsParentTabFor(tab)).FirstOrDefault();

                //If any property (inherited or otherwise) of the document type's parent declares a parent tab for the current tab
                if (parent != null)
                {
                    //and that parent tab has this property on it
                    if (parent.PropertyType.GetProperties().Any(x => x.MetadataToken == property.Metadata.MetadataToken))
                    {
                        //Inherited property. Don't persist at this level.
                        return true;
                    }
                }
            }
            return false;
        }
    }
}

namespace Felinesoft.UmbracoCodeFirst.Extensions
{
    public static class PropertyModuleExtensions
    {
        public static void AddDefaultPropertyModule(this CodeFirstModuleResolver resolver)
        {
            resolver.RegisterModule<IPropertyModule>(new PropertyModuleFactory());
        }
    }
}