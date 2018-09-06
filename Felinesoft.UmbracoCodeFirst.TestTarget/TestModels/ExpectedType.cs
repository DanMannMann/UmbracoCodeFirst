
using System.Collections.Generic;
namespace Marsman.UmbracoCodeFirst.TestTarget.TestModels
{
    public class ExpectedType
    {
        public ExpectedType()
        {
            AllowedChildrenAliases = new string[] { };
            CompositionAliases = new string[] { };
            Properties = new List<ExpectedProperty>();
            Tabs = new List<ExpectedTab>();
        }

        public string Alias { get; set; }
        public string Name { get; set; }
        public string[] AllowedChildrenAliases { get; set; }
        public string[] CompositionAliases { get; set; }
        public string ParentAlias { get; set; }
        public int SortOrder { get; set; }
        public string IconWithColor { get; set; }
        public List<ExpectedProperty> Properties { get; set; }
        public List<ExpectedTab> Tabs { get; set; }
        public bool AllowAtRoot { get; set; }
        public bool ListView { get; set; }
        public string Description { get; set; }
    }

    public class ExpectedTab
    {
        public ExpectedTab()
        {
            Properties = new List<ExpectedProperty>();
        }

        public string Name { get; set; }
        public int SortOrder { get; set; }
        public List<ExpectedProperty> Properties { get; set; }
    }
}