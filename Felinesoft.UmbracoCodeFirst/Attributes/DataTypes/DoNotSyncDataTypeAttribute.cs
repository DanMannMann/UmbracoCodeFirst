using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    /// <summary>
    /// Specifies that the class should not be persisted in the Umbraco database (used for models of the default data types included in Umbraco)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DoNotSyncDataTypeAttribute : Attribute
    {
    }
}
