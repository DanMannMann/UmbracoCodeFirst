using Felinesoft.UmbracoCodeFirst.Exceptions;
using System;
using System.Linq;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class AltTextAttribute : HtmlTagContextualAttribute
    {
        public string Value { get; private set; }

        public AltTextAttribute(string altText, bool useAsTitle = true)
        {
            Value = string.Format("alt=\"{0}\"", altText);
            if (useAsTitle)
            {
                Value = string.Format("{0} title=\"{1}\"", Value, altText);
            }
        }

        public override string CombineToOutputString(System.Collections.Generic.IEnumerable<CodeFirstContextualAttribute> input)
        {
            var val = input.Where(x => x is AltTextAttribute).Cast<AltTextAttribute>().FirstOrDefault();
            return val == null ? string.Empty : val.Value;
        }
    }

}