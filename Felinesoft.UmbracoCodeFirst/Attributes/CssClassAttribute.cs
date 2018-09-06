using System;
using System.Linq;

namespace Marsman.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class CssClassAttribute : HtmlTagContextualAttribute
    {
        public string Value { get; private set; }

        public CssClassAttribute(string className)
        {
            Value = className;
        }

        public override string CombineToOutputString(System.Collections.Generic.IEnumerable<CodeFirstContextualAttribute> input)
        {
            var classes = string.Join(" ", input.Where(x => x is CssClassAttribute).Cast<CssClassAttribute>().Select(x => x.Value));
            var attr = string.IsNullOrWhiteSpace(classes) ? string.Empty : string.Format("Class = \"{0}\"", classes);
            return attr;
        }
    }

}