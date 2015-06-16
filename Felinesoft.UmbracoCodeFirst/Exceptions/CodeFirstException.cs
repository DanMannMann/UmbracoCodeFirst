using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.Exceptions
{
    /// <summary>
    /// A base type for exceptions relating to the creation, persistence and mapping of code-first data and document types
    /// </summary>
    [Serializable]
    public class CodeFirstException : Exception
    {
        public CodeFirstException(string message) : base(message) { }

        public CodeFirstException(string message, Exception inner) : base(message, inner) { }
    }
}
