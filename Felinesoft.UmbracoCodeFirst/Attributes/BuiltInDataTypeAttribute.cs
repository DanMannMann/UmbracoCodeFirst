using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    /// <summary>
    /// Specifies that the class describes a wrapper for a built-in Umbraco data type
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    internal class BuiltInDataTypeAttribute : Attribute
    {
    }
}
