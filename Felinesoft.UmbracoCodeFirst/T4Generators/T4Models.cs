using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst
{
    internal class DocumentTypeDescription
    {
        internal string ClassName { get; set; }
        internal string Name { get; set; }
        internal string Alias { get; set; }
        internal string Icon { get; set; }
        internal string TemplateLocation { get; set; }
        internal string AllowedChildren { get; set; }
        internal string AllowAtRoot { get; set; }
        internal string EnableListView { get; set; }
        internal string RegisterTemplate { get; set; }
        internal string Description { get; set; }
        internal List<TabDescription> Tabs { get; set; }
        internal List<PropertyDescription> Properties { get; set; }
    }

    internal class PropertyDescription
    {
        internal string DataTypeClassName { get; set; }
        internal string PropertyName { get; set; }
        internal string Name { get; set; }
        internal string Alias { get; set; }
        internal string DataTypeInstanceName { get; set; }
        internal string PropertyEditorAlias { get; set; }
        internal string Mandatory { get; set; }
        internal string Description { get; set; }
        internal string SortOrder { get; set; }
    }

    internal class TabDescription
    {
        internal List<PropertyDescription> Properties { get; set; }
        internal string TabName { get; set; }
        internal string SortOrder { get; set; }
        internal string TabClassName { get; set; }
        internal string TabPropertyName { get; set; }
    }

    internal class DataTypeDescription
    {
        internal string DataTypeClassName { get; set; }
        internal string DataTypeInstanceName { get; set; }
        internal string InheritanceBase { get; set; }
        internal string PropertyEditorAlias { get; set; }
        internal string DbType { get; set; }
        internal List<string> PreValues { get; set; }
        internal string SerializedTypeName { get; set; }

        /// <summary>
        /// true to add serialise/initialise stubs for an unknown property editor
        /// </summary>
        internal bool CustomType { get; set; } 
    }
}
