using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Umbraco.Core.Models;
using System.Configuration;
using Felinesoft.UmbracoCodeFirst.Attributes;
using System.Reflection;
using Felinesoft.InitialisableAttributes;
using Felinesoft.UmbracoCodeFirst;

namespace Felinesoft.UmbracoCodeFirst.T4Generators
{
    internal static class DocumentTypeClassGenerator
    {
        internal static void GenerateDocTypes()
        {
            string nspace;
            List<DocumentTypeDescription> types;
            BuildDocumentTypeModels(out nspace, out types);
            GenerateClassFiles(nspace, types);
        }

        private static void GenerateClassFiles(string nspace, List<DocumentTypeDescription> types)
        {
            string dataDirectory = EnsureDataDirectory();

            foreach (var type in types)
            {
                UmbracoCodeFirstDocType dt = new UmbracoCodeFirstDocType();
                dt.Namespace = nspace;
                dt.Model = type;
                var output = dt.TransformText();
                System.IO.File.WriteAllText(System.IO.Path.Combine(dataDirectory, type.ClassName + ".cs"), output);
            }
        }

        private static void BuildDocumentTypeModels(out string nspace, out List<DocumentTypeDescription> types)
        {
            nspace = GetNamespace();
            types = new List<DocumentTypeDescription>();
            foreach (var node in ApplicationContext.Current.Services.ContentTypeService.GetAllContentTypes())
            {
                var type = BuildDocumentTypeModel(nspace, node);
                types.Add(type);
            }
        }

        private static DocumentTypeDescription BuildDocumentTypeModel(string nspace, IContentType node)
        {
            var type = CreateTypeDescription(node);
            AddAllowedChildTypes(node, type);
            ConfigureTemplate(node, type);
            AddTabs(nspace, node, type);
            type.Properties = new List<PropertyDescription>();
            AddProperties(type.Properties, node.PropertyTypes.Except(node.PropertyGroups.SelectMany(x => x.PropertyTypes)), nspace);
            return type;
        }

        internal static string GetNamespace()
        {
            string nspace;
            nspace = "UmbracoCodeFirst.GeneratedTypes";
            if (ConfigurationManager.AppSettings.AllKeys.Contains("CodeFirstNamespace"))
            {
                nspace = ConfigurationManager.AppSettings["CodeFirstNamespace"];
            }
            return nspace;
        }

        internal static string EnsureDataDirectory()
        {
            string dataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
            dataDirectory = System.IO.Path.Combine(dataDirectory, "UmbracoCodeFirstTypes");
            if (!System.IO.Directory.Exists(dataDirectory))
            {
                System.IO.Directory.CreateDirectory(dataDirectory);
            }
            return dataDirectory;
        }

        private static void AddTabs(string nspace, IContentType node, DocumentTypeDescription type)
        {
            type.Tabs = new List<TabDescription>();
            foreach (var group in node.PropertyGroups)
            {
                var tab = BuildTabModel(nspace, group);
                type.Tabs.Add(tab);
            }
        }

        private static TabDescription BuildTabModel(string nspace, PropertyGroup group)
        {
            var tab = new TabDescription();
            tab.TabName = group.Name;
            tab.TabClassName = group.Name.Replace('.', '_').Replace('-', '_').ToPascalCase() + "Tab";
            tab.TabPropertyName = group.Name.Replace('.', '_').Replace('-', '_').ToPascalCase();
            tab.SortOrder = group.SortOrder.ToString();
            tab.Properties = new List<PropertyDescription>();
            AddProperties(tab.Properties, group.PropertyTypes, nspace, "_" + group.Name.Replace(" ", "_"));
            return tab;
        }

        private static void ConfigureTemplate(IContentType node, DocumentTypeDescription type)
        {
            if (node.DefaultTemplate == null)
            {
                type.TemplateLocation = null;
                type.RegisterTemplate = "false";
            }
            else
            {
                type.TemplateLocation = node.DefaultTemplate.Path;
                type.RegisterTemplate = "true";
            }
        }

        private static void AddAllowedChildTypes(IContentType node, DocumentTypeDescription type)
        {
            if (node.AllowedContentTypes.Count() > 0)
            {
                var typeArray = "";
                foreach (var allowedType in node.AllowedContentTypes)
                {
                    if (typeArray != "")
                    {
                        typeArray += ", ";
                    }
                    typeArray += "typeof(" + allowedType.Alias.Replace('.', '_').Replace('-', '_').ToPascalCase() + ")";
                }
                type.AllowedChildren = "new Type[] { " + typeArray + " }";
            }
            else
            {
                type.AllowedChildren = "null";
            }
        }

        private static DocumentTypeDescription CreateTypeDescription(IContentType node)
        {
            var type = new DocumentTypeDescription();
            type.Alias = node.Alias;
            type.Name = node.Name;
            type.AllowAtRoot = node.AllowedAsRoot.ToString().ToLower();
            type.ClassName = node.Alias.Replace('.', '_').Replace('-', '_').ToPascalCase();
            type.EnableListView = node.IsContainer.ToString().ToLower();
            type.Icon = node.Icon;
            type.Description = node.Description;
            return type;
        }

        private static void AddProperties(List<PropertyDescription> list, IEnumerable<PropertyType> propertyTypeCollection, string nspace, string tabName = null)
        {
            foreach (var propType in propertyTypeCollection)
            {
                PropertyDescription prop = BuildPropertyModel(nspace, tabName, propType);
                list.Add(prop);
            }
        }

        private static PropertyDescription BuildPropertyModel(string nspace, string tabName, PropertyType propType)
        {
            PropertyDescription prop = new PropertyDescription();
            prop.Alias = propType.Alias;
            if (tabName != null)
            {
                prop.Alias = prop.Alias.Replace(tabName, "");
            }
            prop.Name = propType.Name;
            prop.PropertyEditorAlias = propType.PropertyEditorAlias;
            prop.PropertyName = propType.Name.Replace('.', '_').Replace('-', '_').ToPascalCase();
            prop.DataTypeClassName = DataTypeClassGenerator.GetDataTypeClassName(propType.DataTypeDefinitionId, nspace);
            prop.DataTypeInstanceName = ApplicationContext.Current.Services.DataTypeService.GetDataTypeDefinitionById(propType.DataTypeDefinitionId).Name;
            prop.Mandatory = propType.Mandatory.ToString().ToLower();
            prop.Description = propType.Description;
            prop.SortOrder = propType.SortOrder.ToString();
            return prop;
        }
    }

    internal static class DataTypeClassGenerator
    {
        private static Dictionary<DataTypeAttribute, string> _typeDefs;

        private static void GenerateClassFiles(string nspace, List<DataTypeDescription> types)
        {
            string dataDirectory = DocumentTypeClassGenerator.EnsureDataDirectory();

            foreach (var type in types)
            {
                UmbracoCodeFirstDataType dt = new UmbracoCodeFirstDataType();
                dt.Namespace = nspace;
                dt.Model = type;
                var output = dt.TransformText();
                System.IO.File.WriteAllText(System.IO.Path.Combine(dataDirectory, type.DataTypeClassName + ".cs"), output);
            }
        }

        internal static void GenerateDataTypes()
        {
            var defs = ApplicationContext.Current.Services.DataTypeService.GetAllDataTypeDefinitions();
            if (_typeDefs == null)
            {
                _typeDefs = typeof(DocumentTypeClassGenerator).Assembly.GetTypes().Where(x => x.GetCustomAttribute<BuiltInDataTypeAttribute>() != null && !x.IsGenericTypeDefinition).ToDictionary(x => x.GetInitialisedAttribute<DataTypeAttribute>(), x => x.Name);
            }
            List<DataTypeDescription> types = new List<DataTypeDescription>();
            foreach (var def in defs)
            {
                if (!_typeDefs.Any(x => x.Key.Name == def.Name)) //not a known built-in type
                {
                    var dataType = new DataTypeDescription();
                    dataType.PreValues = ApplicationContext.Current.Services.DataTypeService.GetPreValuesByDataTypeId(def.Id).Select(x => x.Replace("\"", "\"\"")).ToList();
                    if (_typeDefs.Any(x => x.Key.PropertyEditorAlias == def.PropertyEditorAlias)) //can base on a known built-in type
                    {
                        var builtIn = _typeDefs.First(x => x.Key.PropertyEditorAlias == def.PropertyEditorAlias);
                        dataType.CustomType = false;
                        dataType.InheritanceBase = builtIn.Key.DecoratedType.Name;
                        dataType.DataTypeClassName = GetDataTypeClassName(def.Id, null);
                        dataType.DataTypeInstanceName = def.Name;
                        dataType.PropertyEditorAlias = def.PropertyEditorAlias;
                        dataType.DbType = Enum.GetName(typeof(DataTypeDatabaseType), def.DatabaseType);
                    }
                    else
                    {
                        dataType.CustomType = true;
                        switch (def.DatabaseType)
                        {
                            case DataTypeDatabaseType.Date:
                                dataType.InheritanceBase = "IUmbracoDateDataType";
                                dataType.SerializedTypeName = "DateTime";
                                break;
                            case DataTypeDatabaseType.Integer:
                                dataType.InheritanceBase = "IUmbracoIntDataType";
                                dataType.SerializedTypeName = "int";
                                break;
                            case DataTypeDatabaseType.Ntext:
                            case DataTypeDatabaseType.Nvarchar:
                                dataType.InheritanceBase = "IUmbracoStringDataType";
                                dataType.SerializedTypeName = "string";
                                break;
                        }
                        dataType.DataTypeClassName = GetDataTypeClassName(def.Id, null);
                        dataType.DataTypeInstanceName = def.Name;
                        dataType.PropertyEditorAlias = def.PropertyEditorAlias;
                        dataType.DbType = Enum.GetName(typeof(DataTypeDatabaseType), def.DatabaseType);
                    }
                    types.Add(dataType);
                }
            }
            GenerateClassFiles(DocumentTypeClassGenerator.GetNamespace(), types);
        }

        internal static string GetDataTypeClassName(int dataTypeDefinitionId, string nameSpace)
        {
            if (_typeDefs == null)
            {
                _typeDefs = typeof(DocumentTypeClassGenerator).Assembly.GetTypes().Where(x => x.GetCustomAttribute<BuiltInDataTypeAttribute>() != null && !x.IsGenericTypeDefinition).ToDictionary(x => x.GetCustomAttribute<DataTypeAttribute>(), x => x.Name);
            }
            if (!string.IsNullOrEmpty(nameSpace))
            {
                nameSpace += ".";
            }

            var dtd = ApplicationContext.Current.Services.DataTypeService.GetDataTypeDefinitionById(dataTypeDefinitionId);

            if (dtd != null)
            {
                var builtIn = _typeDefs.Where(x => x.Key.Name == dtd.Name).FirstOrDefault().Value;
                if (builtIn == null)
                {
                    return nameSpace + dtd.Name.Replace('.', '_').Replace('-', '_').Replace("/", "").ToPascalCase();
                }
                return builtIn;
            }
            else
            {
                return null;
            }
        }
    }
}
