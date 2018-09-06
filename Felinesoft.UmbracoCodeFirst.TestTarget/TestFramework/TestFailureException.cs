using System;

namespace Marsman.UmbracoCodeFirst.TestTarget.TestFramework
{
    public class TestFailureException : Exception
    {
        public TestFailureException(string message)
            : base(message) { }
    }
}