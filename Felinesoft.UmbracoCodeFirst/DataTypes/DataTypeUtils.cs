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
        public static string GetHtmlClassAttribute(object contextKey)
        {
            var context = CodeFirstModelContext.GetContext(contextKey);
            var csses = new List<string>();

            GetClasses(context, csses);

            var current = context.ParentContext;
            while (current != null)
            {
                GetClasses(current, csses);
                current = current.ParentContext;
            }

            var result = string.Join(" ", csses.Where(x => !string.IsNullOrWhiteSpace(x)));
            if (!string.IsNullOrWhiteSpace(result))
            {
                result = " class='" + result + "'";
            }
            return result;
        }

        private static void GetClasses(CodeFirstModelContext context, List<string> csses)
        {
            if (context.CurrentDataType != null)
            {
                //Add classes from the current data type
                csses.Add(context.CurrentDataType.CssClasses);
            }

            if (context.CurrentProperty != null)
            {
                //Add classes from property
                csses.Add(context.CurrentProperty.CssClasses);
            }

            if (context.ContentType != null)
            {
                //Add classes from current content type
                csses.Add(context.ContentType.CssClasses);
            }
        }
    }
}
