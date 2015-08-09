using System;

namespace Felinesoft.UmbracoCodeFirst.TestTarget.TestFramework
{
    public class TestFailureException : Exception
    {
        public TestFailureException(string message)
            : base(message) { }
    }
}