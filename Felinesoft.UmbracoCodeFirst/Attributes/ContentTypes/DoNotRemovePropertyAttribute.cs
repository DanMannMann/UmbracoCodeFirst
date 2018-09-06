using System;

namespace Marsman.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DoNotRemovePropertyAttribute : MultipleCodeFirstAttribute
    {
        public string PropertyAlias { get; private set; }

        public DoNotRemovePropertyAttribute(string propertyAlias)
        {
            PropertyAlias = propertyAlias;
        }
    }

}