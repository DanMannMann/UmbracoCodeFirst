using System;

namespace Marsman.UmbracoCodeFirst.Exceptions
{
    [Serializable]
    public class CodeFirstResolverException : Exception
    {
        public CodeFirstResolverException(string message) : base(message) { }
    }
}