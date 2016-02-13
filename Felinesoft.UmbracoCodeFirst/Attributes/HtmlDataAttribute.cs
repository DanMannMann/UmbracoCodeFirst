using System;
using System.Linq;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class HtmlDataAttribute : HtmlTagContextualAttribute
    {
        public HtmlDataAttribute(string attributeName, string attributeValue)
        {
            AttributeName = attributeName;
            AttributeValue = attributeValue;
        }

        public string AttributeName { get; set; }

        public string AttributeValue { get; set; }

        public string Value
        {
            get { return string.Format("data-{0}='{1}'", AttributeName, AttributeValue); }
        }

        public override string CombineToOutputString(System.Collections.Generic.IEnumerable<CodeFirstContextualAttribute> input)
        {
            var items = string.Join(" ", input.Where(x => x is HtmlDataAttribute).Cast<HtmlDataAttribute>().Select(x => x.Value));
            return items;
        }
    }
}