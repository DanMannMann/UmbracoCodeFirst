using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.Exceptions
{
    [Serializable]
    public class AttributeInitialisationException : InvalidOperationException
    {
        public AttributeInitialisationException() { }
        public AttributeInitialisationException(string message)
            : base(message) { }
    }
}
