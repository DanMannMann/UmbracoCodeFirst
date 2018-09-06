using System;

namespace Marsman.UmbracoCodeFirst.Exceptions
{
    [Serializable]
    public class CodeFirstPassiveInitialisationException : CodeFirstException
    {
        public CodeFirstPassiveInitialisationException(string message) : base(message) { }

        public CodeFirstPassiveInitialisationException(string message, Exception inner) : base(message, inner) { }
    }
}