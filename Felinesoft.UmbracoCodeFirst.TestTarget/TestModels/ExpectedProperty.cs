
namespace Felinesoft.UmbracoCodeFirst.TestTarget.TestModels
{
    public class ExpectedProperty
    {
        public string Alias { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public bool Mandatory { get; set; }
        public string Description { get; set; }
        public ExpectedDataType DataType { get; set; }
        public string Regex { get; set; }
    }
}