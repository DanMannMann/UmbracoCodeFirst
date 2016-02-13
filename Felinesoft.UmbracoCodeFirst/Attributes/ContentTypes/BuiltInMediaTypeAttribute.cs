using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class BuiltInMediaTypeAttribute : BuiltInTypeAttribute
    {
        public override string BuiltInTypeName
        {
            get { return "Media Type"; }
        }
    }

}
