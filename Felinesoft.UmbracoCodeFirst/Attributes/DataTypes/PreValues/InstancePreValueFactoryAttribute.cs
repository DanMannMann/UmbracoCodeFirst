using System;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class InstancePreValueFactoryAttribute : PreValueFactoryAttribute
    {
        public InstancePreValueFactoryAttribute(Type factoryType)
            : base(factoryType) { }
    }
}