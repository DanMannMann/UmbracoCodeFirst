using Felinesoft.UmbracoCodeFirst.TestTarget.TestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace Felinesoft.UmbracoCodeFirst.TestTarget.TestFramework
{
    public class CompareByProperty<T> : IEqualityComparer<T> where T : class
    {
        private Func<T, object> _selector;

        public CompareByProperty(Func<T, object> selector)
        {
            _selector = selector;
        }

        public bool Equals(T x, T y)
        {
            if (x == null && y == null)
            {
                return false;
            }
            else if (x == null || y == null)
            {
                return true;
            }
            var X = _selector.Invoke(x);
            var Y = _selector.Invoke(y);
            if (X == null && Y == null)
            {
                return false;
            }
            else if (X == null || Y == null)
            {
                return true;
            }
            return X.Equals(Y);
        }

        public int GetHashCode(T obj)
        {
            if (obj == null)
            {
                return 0;
            }
            var result = _selector.Invoke(obj);
            return result == null ? 0 : result.GetHashCode();
        }
    }

    public abstract class TestBase
    {
        protected const string _namespacePrefix = "Felinesoft.UmbracoCodeFirst.TestTarget.";
        protected IContentTypeService ContentTypeService;
        protected IDataTypeService DataTypeService;

        protected void Initialise(string targetNamespace)
        {
            ContentTypeService = Umbraco.Core.ApplicationContext.Current.Services.ContentTypeService;
            DataTypeService = Umbraco.Core.ApplicationContext.Current.Services.DataTypeService;
            var types = typeof(startup).Assembly.GetTypes().Where(x => x.Namespace.StartsWith(_namespacePrefix + targetNamespace)).ToList();
            CodeFirstManager.Current.Initialise(types);
        }

        protected void AssertContentTypes(IEnumerable<IContentTypeBase> types, List<ExpectedType> expectedTypes)
        {
            foreach (var type in expectedTypes)
            {
                if (!types.Any(x => x.Alias == type.Alias))
                {
                    throw new TestFailureException("Missing type: " + type.Alias);
                }
                else
                {
                    var umbType = types.Single(x => x.Alias == type.Alias);
                    AssertContentType(umbType, type);
                }
            }

        }

        protected void AssertContentType(IContentTypeBase umbType, ExpectedType type)
        {
            AssertBasicContentTypeInfo(umbType, type);
            var propGroups = umbType.PropertyGroups.Where(x => x.PropertyTypes.Count > 0);
            AssertTabs(type, propGroups);
            AssertProperties(umbType.PropertyTypes.Except(umbType.PropertyGroups.SelectMany(x => x.PropertyTypes), new CompareByProperty<PropertyType>(x => x.Alias)), type.Properties, type.Alias);
        }

        protected void AssertBasicContentTypeInfo(IContentTypeBase umbType, ExpectedType type)
        {
            Assert(umbType, x => x.Name == type.Name, "Name doesn't match. Type: " + type.Name);
            Assert(umbType, x => x.SortOrder == type.SortOrder || type.SortOrder == 0, "Sort Order doesn't match. Type: " + type.Name);
            Assert(umbType, x => x.IsContainer == type.ListView, "ListView doesn't match. Type: " + type.Name);
            Assert(umbType, x => x.AllowedAsRoot == type.AllowAtRoot, "AllowAtRoot doesn't match. Type: " + type.Name);
            Assert(umbType, x => MatchOrBothEmpty(x.Description, type.Description), "Description doesn't match. Type: " + type.Name);
            Assert(umbType, x => x.Icon == type.IconWithColor, "Icon or colour doesn't match. Type: " + type.Name);
            AssertParent(umbType, type);
            AssertAllowedChildren(umbType, type);
            AssertCompositions(umbType, type);
        }

        protected void AssertParent(IContentTypeBase umbType, ExpectedType type)
        {
            if (string.IsNullOrWhiteSpace(type.ParentAlias))
            {
                if (umbType.ParentId > 0)
                {
                    throw new TestFailureException("Parent is set when there should be none. Type: " + type.Name);
                }
            }
            else
            {
                if (umbType.ParentId <= 0)
                {
                    throw new TestFailureException("Parent is not set when it should be. Type: " + type.Name);
                }
                var parent = GetParentType(umbType);
                Assert(parent, x => x != null && MatchOrBothEmpty(x.Alias, type.ParentAlias), "Parent alias doesn't match. Type: " + type.Name);
            }
        }

        protected void AssertAllowedChildren(IContentTypeBase umbType, ExpectedType type)
        {
            foreach (var child in type.AllowedChildrenAliases)
            {
                if (!umbType.AllowedContentTypes.Any(x => x.Alias == child))
                {
                    throw new TestFailureException("Allowed child " + child + " missing. Type: " + type.Name);
                }
            }
            foreach (var child in umbType.AllowedContentTypes)
            {
                if (!type.AllowedChildrenAliases.Contains(child.Alias))
                {
                    throw new TestFailureException("Allowed child " + child + " present on type " + type.Name + " when it should not be");
                }
            }
        }

        protected void AssertCompositions(IContentTypeBase umbType, ExpectedType type)
        {
            if (umbType is IContentTypeComposition)
            {
                AssertCompositions(umbType as IContentTypeComposition, type);
            }
            else if (type.CompositionAliases != null && type.CompositionAliases.Count() > 0)
            {
                throw new TestFailureException("Compositions expected for a type which is not IContentTypeComposition. Type: " + type.Name);
            }
        }

        private void AssertCompositions(IContentTypeComposition umbType, ExpectedType type)
        {
            foreach (var comp in type.CompositionAliases)
            {
                if (!umbType.ContentTypeComposition.Any(x => x.Alias == comp))
                {
                    throw new TestFailureException("Composition " + comp + " missing. Type: " + type.Name);
                }
            }
            foreach (var comp in umbType.ContentTypeComposition)
            {
                if (comp.Alias != type.ParentAlias && !type.CompositionAliases.Contains(comp.Alias))
                {
                    throw new TestFailureException("Composition " + comp.Alias + " present on type " + type.Name + " when it should not be");
                }
            }
        }

        private void AssertTabs(ExpectedType type, IEnumerable<PropertyGroup> propGroups)
        {
            foreach (var tab in type.Tabs)
            {
                if (!propGroups.Any(x => x.Name == tab.Name))
                {
                    throw new TestFailureException("Missing tab " + tab.Name + " on type " + type.Alias);
                }
                else
                {
                    AssertTab(propGroups.First(x => x.Name == tab.Name), tab, type.Alias);
                }
            }

            foreach (var group in propGroups)
            {
                if (!type.Tabs.Any(x => x.Name == group.Name))
                {
                    throw new TestFailureException("Unexpected tab " + group.Name + " on type " + type.Alias);
                }
            }
        }

        protected void AssertTab(PropertyGroup propertyGroup, ExpectedTab tab, string typeAlias)
        {
            Assert(propertyGroup, x => x.Name == tab.Name, "Tab name doesn't match. Tab: " + tab.Name + ", type: " + typeAlias);
            Assert(propertyGroup, x => x.SortOrder == tab.SortOrder || tab.SortOrder == 0, "Tab sort order doesn't match. Tab: " + tab.Name + ", type: " + typeAlias);
            AssertProperties(propertyGroup.PropertyTypes, tab.Properties, typeAlias, tab.Name);
        }

        protected void AssertProperties(IEnumerable<PropertyType> propertyTypeCollection, List<ExpectedProperty> expectedProperties, string typeAlias, string tabName = null)
        {
            var msgPostfix = " on type " + typeAlias + (tabName == null ? "" : ", tab: " + tabName);

            foreach (var property in expectedProperties)
            {
                if (!propertyTypeCollection.Any(x => x.Alias == property.Alias))
                {
                    throw new TestFailureException("Missing property " + property.Alias + msgPostfix);
                }
                else
                {
                    var umbProp = propertyTypeCollection.First(x => x.Alias == property.Alias);
                    AssertProperty(msgPostfix, property, umbProp);
                }
            }

            foreach (var property in propertyTypeCollection)
            {
                if (!expectedProperties.Any(x => x.Alias == property.Alias))
                {
                    throw new TestFailureException("Unexpected property " + property.Alias + msgPostfix);
                }
            }
        }

        private void AssertProperty(string msgPostfix, ExpectedProperty property, PropertyType umbProp)
        {
            Assert(umbProp, x => x.Name == property.Name, "Property name doesn't match. Property: " + property.Alias + msgPostfix);
            Assert(umbProp, x => MatchOrBothEmpty(x.Description, property.Description), "Property description doesn't match. Property: " + property.Alias + msgPostfix);
            Assert(umbProp, x => x.Mandatory == property.Mandatory, "Property mandatoryness doesn't match. Property: " + property.Alias + msgPostfix);
            Assert(umbProp, x => x.SortOrder == property.SortOrder || property.SortOrder == 0, "Property Sort Order doesn't match. Property: " + property.Alias + msgPostfix);
            Assert(umbProp, x => MatchOrBothEmpty(x.ValidationRegExp, property.Regex), "Property validation regex doesn't match. Property: " + property.Alias + msgPostfix);
            AssertDataType(DataTypeService.GetDataTypeDefinitionById(umbProp.DataTypeDefinitionId), property.DataType, msgPostfix + ", property: " + property.Alias);
        }

        protected void AssertDataTypes(IEnumerable<IDataTypeDefinition> types, IEnumerable<ExpectedDataType> expectedTypes)
        {
            foreach (var exp in expectedTypes)
            {
                var type = types.FirstOrDefault(x => x.Name == exp.DataTypeName);
                if (type == null)
                {
                    throw new TestFailureException("Expected Data Type '" + exp.DataTypeName + "' not found");
                }
                else
                {
                    AssertDataType(type, exp, string.Empty);
                }
            }
        }

        protected void AssertDataType(IDataTypeDefinition dataTypeDefinition, ExpectedDataType expectedDataType, string msgPostfix)
        {
            Assert(dataTypeDefinition, x => x.Name == expectedDataType.DataTypeName, "Data type name doesn't match. Data type: " + expectedDataType.DataTypeName + msgPostfix);
            Assert(dataTypeDefinition, x => x.PropertyEditorAlias == expectedDataType.PropertyEditorAlias, "Data type property editor alias doesn't match. Data type: " + expectedDataType.DataTypeName + msgPostfix);
            Assert(dataTypeDefinition, x => x.DatabaseType == expectedDataType.DbType, "Data type database type doesn't match. Data type: " + expectedDataType.DataTypeName + msgPostfix);
        }

        private IContentTypeBase GetParentType(IContentTypeBase type)
        {
            if (type.ParentId <= 0) return null;

            if (type is IContentType)
            {
                return ContentTypeService.GetContentType(type.ParentId);
            }
            else if (type is IMediaType)
            {
                return ContentTypeService.GetMediaType(type.ParentId);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        protected void Assert<T>(T input, Func<T, bool> test, string message)
        {
            if (!test.Invoke(input))
            {
                throw new TestFailureException(message);
            }
        }

        private bool MatchOrBothEmpty(string p1, string p2)
        {
            if (string.IsNullOrWhiteSpace(p1) && string.IsNullOrWhiteSpace(p2))
            {
                return true;
            }
            else
            {
                return p1 == p2;
            }
        }
    }
}