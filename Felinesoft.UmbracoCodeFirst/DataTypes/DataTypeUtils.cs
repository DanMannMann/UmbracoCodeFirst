using Felinesoft.UmbracoCodeFirst.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Attributes;

namespace Felinesoft.UmbracoCodeFirst.DataTypes
{
    public static class DataTypeUtils
    {
        public static string GetHtmlTagContentFromContextualAttributes(object key)
        {
            var allAttrs = key.GetAttributesFromContextTree<HtmlTagContextualAttribute>();
            var sortedAttrs = new Dictionary<Type, List<HtmlTagContextualAttribute>>();
            List<string> result = new List<string>();

            foreach (var attr in allAttrs)
            {
                var type = attr.GetType();
                if (!sortedAttrs.ContainsKey(type))
                {
                    sortedAttrs.Add(type, new List<HtmlTagContextualAttribute>());
                }
                sortedAttrs[type].Add(attr);
            }

            foreach (var attrType in sortedAttrs)
            {
                if (attrType.Value.Count() > 0)
                {
                    result.Add(attrType.Value.First().CombineToOutputString(attrType.Value));
                }
            }

            var resultString = string.Join(" ", result);
            return string.IsNullOrWhiteSpace(resultString) ? string.Empty : " " + resultString;
        }
    }
}
