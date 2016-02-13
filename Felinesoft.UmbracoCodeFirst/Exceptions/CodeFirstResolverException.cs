using System;

namespace Felinesoft.UmbracoCodeFirst.Exceptions
{
    [Serializable]
    public class CodeFirstResolverException : Exception
    {
        public CodeFirstResolverException(string message) : base(message) { }
    }
}