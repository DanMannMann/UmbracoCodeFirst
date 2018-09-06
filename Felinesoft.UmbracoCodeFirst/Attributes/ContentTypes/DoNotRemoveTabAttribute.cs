using System;

namespace Marsman.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DoNotRemoveTabAttribute : MultipleCodeFirstAttribute
    {
        public string TabName { get; private set; }

        public DoNotRemoveTabAttribute(string tabName)
        {
            TabName = tabName;
        }
    }
}