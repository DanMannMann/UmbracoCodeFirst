using System;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class InstancePreValueFileAttribute : PreValueFileAttribute, IDataTypeInstance
    {
        public InstancePreValueFileAttribute(string relativePath)
            : base(relativePath) { }
    }
}