using Felinesoft.UmbracoCodeFirst.Core.ClassFileGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules.ContentTypeBase.T4
{
    public partial class UmbracoCodeFirstContentType
    {
        internal string Namespace { get; set; }
        internal ContentTypeDescription Model { get; set; }
    }
}
