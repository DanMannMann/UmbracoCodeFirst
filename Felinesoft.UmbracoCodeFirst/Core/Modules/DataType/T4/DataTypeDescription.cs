using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst
{
    internal class DataTypeDescription
    {
        internal string DataTypeClassName { get; set; }
        internal string DataTypeInstanceName { get; set; }
        internal string InheritanceBase { get; set; }
        internal string PropertyEditorAlias { get; set; }
        internal string DbType { get; set; }
        internal List<string> PreValues { get; set; }
        internal string SerializedTypeName { get; set; }

        /// <summary>
        /// true to add serialise/initialise stubs for an unknown property editor
        /// </summary>
        internal bool CustomType { get; set; } 
    }
}
