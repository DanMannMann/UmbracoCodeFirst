using System.Collections.Generic;

namespace Felinesoft.UmbracoCodeFirst.Core.ClassFileGeneration
{
    public sealed class TabDescription
    {
        public List<PropertyDescription> Properties { get; set; }
        public string TabName { get; set; }
        public string SortOrder { get; set; }
        public string TabClassName { get; set; }
        public string TabPropertyName { get; set; }
    }
}