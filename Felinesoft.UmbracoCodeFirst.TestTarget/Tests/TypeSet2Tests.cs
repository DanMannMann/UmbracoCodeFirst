using Marsman.UmbracoCodeFirst.DataTypes;
using Marsman.UmbracoCodeFirst.TestTarget.TestFramework;
using Marsman.UmbracoCodeFirst.TestTarget.TestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace Marsman.UmbracoCodeFirst.TestTarget.Tests
{
    public class TypeSet2Tests : TestBase, ICodeFirstTest
    {
        public void Run()
        {
            Initialise("TypeSet2");

            var dataTypes = DataTypeService.GetAllDataTypeDefinitions();
            var expectedDataTypes = GetDataTypes();
            AssertDataTypes(dataTypes, expectedDataTypes);

            var docTypes = ContentTypeService.GetAllContentTypes();
            var expectedDocTypes = GetDocTypes();
            AssertContentTypes(docTypes, expectedDocTypes);

            var mediaTypes = ContentTypeService.GetAllMediaTypes();
            var expectedMediaTypes = GetMediaTypes();
            AssertContentTypes(mediaTypes, expectedMediaTypes);
        }

        private List<ExpectedType> GetDocTypes()
        {
            var result = new List<ExpectedType>();
            result.Add(new ExpectedType()
            {
                Alias = "master",
                Name = "Master",
                AllowAtRoot = true,
                AllowedChildrenAliases = new string[] { },
                CompositionAliases = new string[] { },
                Description = string.Empty,
                IconWithColor = "icon-document",
                ListView = false,
                ParentAlias = string.Empty,
                SortOrder = 0
            });
            AddProperties(result.Last(), "master", "Master", 2);
            AddTab(result.Last(), "master", "Master", false, 3);

            result.Add(new ExpectedType()
            {
                Alias = "child1",
                Name = "Child 1",
                AllowAtRoot = false,
                AllowedChildrenAliases = new string[] { },
                CompositionAliases = new string[] { },
                Description = string.Empty,
                IconWithColor = "icon-activity color-green",
                ListView = false,
                ParentAlias = "master",
                SortOrder = 0
            });
            AddProperties(result.Last(), "child1", "Child 1", 4);
            AddTab(result.Last(), "child1", "Child 1", true, 1);

            result.Add(new ExpectedType()
            {
                Alias = "child2",
                Name = "Child 2",
                AllowAtRoot = false,
                AllowedChildrenAliases = new string[] { },
                CompositionAliases = new string[] { "child2Composition" },
                Description = string.Empty,
                IconWithColor = "icon-document",
                ListView = false,
                ParentAlias = "master",
                SortOrder = 0
            });
            AddProperties(result.Last(), "child2", "Child 2");
            AddTab(result.Last(), "child2", "Child 2");

            result.Add(new ExpectedType()
            {
                Alias = "grandChild1",
                Name = "Grand Child 1",
                AllowAtRoot = false,
                AllowedChildrenAliases = new string[] { },
                CompositionAliases = new string[] { },
                Description = string.Empty,
                IconWithColor = "icon-document",
                ListView = false,
                ParentAlias = "child1",
                SortOrder = 0
            });
            AddProperties(result.Last(), "grandchild1", "Grandchild 1");
            //grandchild has no tab


            result.Add(new ExpectedType()
            {
                Alias = "child2Composition",
                Name = "Child 2 Composition",
                AllowAtRoot = false,
                AllowedChildrenAliases = new string[] { },
                CompositionAliases = new string[] { },
                Description = string.Empty,
                IconWithColor = "icon-document",
                ListView = false,
                ParentAlias = string.Empty,
                SortOrder = 0
            });
            var props = result.Last().Properties = new List<ExpectedProperty>();
            props.Add(new ExpectedProperty()
            {
                Name = "Composition Numeric Root",
                Alias = "compositionNumericRoot",
                DataType = new ExpectedDataType()
                {
                    DataTypeName = "Numeric",
                    DbType = DataTypeDatabaseType.Integer,
                    PropertyEditorAlias = "Umbraco.Integer"
                },
                Description = "",
                Mandatory = false,
                SortOrder = 0,
                Regex = ""
            });

            return result;
        }

        private List<ExpectedType> GetMediaTypes()
        {
            var result = new List<ExpectedType>();
            result.Add(new ExpectedType()
            {
                Alias = "mediaMaster",
                Name = "Media Master",
                AllowAtRoot = true,
                AllowedChildrenAliases = new string[] { },
                CompositionAliases = new string[] { },
                Description = string.Empty,
                IconWithColor = "icon-document",
                ListView = false,
                ParentAlias = string.Empty,
                SortOrder = 0
            });
            AddProperties(result.Last(), "master", "Master", 2);
            AddTab(result.Last(), "master", "Master", false, 3);

            result.Add(new ExpectedType()
            {
                Alias = "mediaChild1",
                Name = "Media Child 1",
                AllowAtRoot = false,
                AllowedChildrenAliases = new string[] { },
                CompositionAliases = new string[] { },
                Description = string.Empty,
                IconWithColor = "icon-activity color-green",
                ListView = false,
                ParentAlias = "mediaMaster",
                SortOrder = 0
            });
            AddProperties(result.Last(), "child1", "Child 1", 4);
            AddTab(result.Last(), "child1", "Child 1", true, 1);

            result.Add(new ExpectedType()
            {
                Alias = "mediaChild2",
                Name = "Media Child 2",
                AllowAtRoot = false,
                AllowedChildrenAliases = new string[] { },
                CompositionAliases = new string[] { "mediaChild2Composition" },
                Description = string.Empty,
                IconWithColor = "icon-document",
                ListView = false,
                ParentAlias = "mediaMaster",
                SortOrder = 0
            });
            AddProperties(result.Last(), "child2", "Child 2");
            AddTab(result.Last(), "child2", "Child 2");

            result.Add(new ExpectedType()
            {
                Alias = "mediaGrandChild1",
                Name = "Media Grand Child 1",
                AllowAtRoot = false,
                AllowedChildrenAliases = new string[] { },
                CompositionAliases = new string[] { },
                Description = string.Empty,
                IconWithColor = "icon-document",
                ListView = false,
                ParentAlias = "mediaChild1",
                SortOrder = 0
            });
            AddProperties(result.Last(), "grandchild1", "Grandchild 1");
            //grandchild has no tab


            result.Add(new ExpectedType()
            {
                Alias = "mediaChild2Composition",
                Name = "Media Child 2 Composition",
                AllowAtRoot = false,
                AllowedChildrenAliases = new string[] { },
                CompositionAliases = new string[] { },
                Description = string.Empty,
                IconWithColor = "icon-document",
                ListView = false,
                ParentAlias = string.Empty,
                SortOrder = 0
            });
            var props = result.Last().Properties = new List<ExpectedProperty>();
            props.Add(new ExpectedProperty()
            {
                Name = "Composition Numeric Root",
                Alias = "compositionNumericRoot",
                DataType = new ExpectedDataType()
                {
                    DataTypeName = "Numeric",
                    DbType = DataTypeDatabaseType.Integer,
                    PropertyEditorAlias = "Umbraco.Integer"
                },
                Description = "",
                Mandatory = false,
                SortOrder = 0,
                Regex = ""
            });

            return result;
        }

        private void AddTab(ExpectedType expectedType, string alias, string name, bool useCommon = true, int count = 2)
        {
            expectedType.Tabs = new List<ExpectedTab>();
            expectedType.Tabs.Add(new ExpectedTab()
            {
                Name = name + " Tab",
                SortOrder = 0
            });
            expectedType.Tabs.Last().Properties = new List<ExpectedProperty>();

            if (useCommon)
                expectedType.Tabs.Last().Properties.Add(CommonTabProperty(name));

            count--;
            if (count < 0) return;
            expectedType.Tabs.Last().Properties.Add(new ExpectedProperty()
            {
                Alias = alias + "RichtextEditorTab_" + name.Replace(" ", "_") + "_Tab",
                Name = name + " Richtext Editor Tab",
                Description = "",
                DataType = new ExpectedDataType()
                {
                    DataTypeName = "Richtext editor",
                    DbType = DataTypeDatabaseType.Ntext,
                    PropertyEditorAlias = "Umbraco.TinyMCEv3"
                },
                Mandatory = false,
                SortOrder = 0,
                Regex = ""
            });

            count--;
            if (count < 0) return;
            expectedType.Tabs.Last().Properties.Add(new ExpectedProperty()
            {
                Alias = alias + "DatePickerTab_" + name.Replace(" ", "_") + "_Tab",
                Name = name + " Date Picker Tab",
                Description = "",
                DataType = new ExpectedDataType()
                {
                    DataTypeName = "Date Picker",
                    DbType = DataTypeDatabaseType.Date,
                    PropertyEditorAlias = "Umbraco.Date"
                },
                Mandatory = false,
                SortOrder = 0,
                Regex = ""
            });

            count--;
            if (count < 0) return;
            expectedType.Tabs.Last().Properties.Add(new ExpectedProperty()
            {
                Alias = alias + "TextstringTab_" + name.Replace(" ", "_") + "_Tab",
                Name = name + " Textstring Tab",
                Description = "",
                DataType = new ExpectedDataType()
                {
                    DataTypeName = "Textstring",
                    DbType = DataTypeDatabaseType.Nvarchar,
                    PropertyEditorAlias = "Umbraco.Textbox"
                },
                Mandatory = true,
                SortOrder = 0,
                Regex = ""
            });
        }

        private ExpectedProperty CommonTabProperty(string name)
        {
            return new ExpectedProperty()
            {
                Alias = "commonTabBaseTrueFalse_" + name.Replace(" ", "_") + "_Tab",
                Name = "Common Tab Base True False",
                Description = "",
                DataType = new ExpectedDataType()
                {
                    DataTypeName = "Checkbox",
                    DbType = DataTypeDatabaseType.Integer,
                    PropertyEditorAlias = "Umbraco.TrueFalse"
                },
                Mandatory = false,
                SortOrder = 0,
                Regex = ""
            };
        }

        private void AddProperties(ExpectedType expectedType, string alias, string name, int count = 3)
        {
            expectedType.Properties = new List<ExpectedProperty>();
            count--;
            if (count < 0) return;
            expectedType.Properties.Add(new ExpectedProperty()
            {
                Name = name + " Textstring Root",
                Alias = alias + "TextstringRoot",
                DataType = new ExpectedDataType()
                {
                    DataTypeName = "Textstring",
                    DbType = DataTypeDatabaseType.Nvarchar,
                    PropertyEditorAlias = "Umbraco.Textbox"
                },
                Description = "",
                Mandatory = false,
                SortOrder = 0,
                Regex = ""
            });
            count--;
            if (count < 0) return;
            expectedType.Properties.Add(new ExpectedProperty()
            {
                Name = name + " True False Root",
                Alias = alias + "TrueFalseRoot",
                DataType = new ExpectedDataType()
                {
                    DataTypeName = "Checkbox",
                    DbType = DataTypeDatabaseType.Integer,
                    PropertyEditorAlias = "Umbraco.TrueFalse"
                },
                Description = "",
                Mandatory = false,
                SortOrder = 0,
                Regex = ""
            });
            count--;
            if (count < 0) return;
            expectedType.Properties.Add(new ExpectedProperty()
            {
                Name = name + " Numeric Root",
                Alias = alias + "NumericRoot",
                DataType = new ExpectedDataType()
                {
                    DataTypeName = "Numeric",
                    DbType = DataTypeDatabaseType.Integer,
                    PropertyEditorAlias = "Umbraco.Integer"
                },
                Description = "",
                Mandatory = false,
                SortOrder = 0,
                Regex = ""
            });
            count--;
            if (count < 0) return;
            expectedType.Properties.Add(new ExpectedProperty()
            {
                Name = name + " Label Root",
                Alias = alias + "LabelRoot",
                DataType = new ExpectedDataType()
                {
                    DataTypeName = "Label",
                    DbType = DataTypeDatabaseType.Nvarchar,
                    PropertyEditorAlias = "Umbraco.NoEdit"
                },
                Description = "  This be a  lABel ",
                Mandatory = true,
                SortOrder = 0,
                Regex = ""
            });
        }

        private List<ExpectedDataType> GetDataTypes()
        {
            var result = new List<ExpectedDataType>();
            result.Add(GetDataType("Custom Data Type 1", "Umbraco.DateTime", Umbraco.Core.Models.DataTypeDatabaseType.Date));
            result.Add(GetDataType("Custom Data Type 2", "Umbraco.Textbox", Umbraco.Core.Models.DataTypeDatabaseType.Nvarchar));
            result.Add(GetDataType("Overidden name", "Umbraco.Grid", Umbraco.Core.Models.DataTypeDatabaseType.Ntext));
            result.Add(GetDataType("Inheriting Data Type 1", "Umbraco.Integer", Umbraco.Core.Models.DataTypeDatabaseType.Integer));
            result.Add(GetDataType("Inheriting Data Type 2", "Umbraco.TrueFalse", Umbraco.Core.Models.DataTypeDatabaseType.Integer));
            result.Add(GetDataType("Inheriting Data Type 3", "Umbraco.MultiNodeTreePicker", Umbraco.Core.Models.DataTypeDatabaseType.Ntext));
            return result;
        }

        private ExpectedDataType GetDataType(string name, string alias, Umbraco.Core.Models.DataTypeDatabaseType dbType)
        {
            return new ExpectedDataType()
            {
                DataTypeName = name,
                DbType = dbType,
                PropertyEditorAlias = alias
            };
        }
    } 
}