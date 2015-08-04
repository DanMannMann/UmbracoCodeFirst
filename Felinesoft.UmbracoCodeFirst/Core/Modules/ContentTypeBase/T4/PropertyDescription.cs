
namespace Felinesoft.UmbracoCodeFirst.Core.ClassFileGeneration
{
    public sealed class PropertyDescription
    {
        public string DataTypeClassName { get; set; }
        public string PropertyName { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string DataTypeInstanceName { get; set; }
        public string PropertyEditorAlias { get; set; }
        public string Mandatory { get; set; }
        public string Description { get; set; }
        public string SortOrder { get; set; }
    }
}