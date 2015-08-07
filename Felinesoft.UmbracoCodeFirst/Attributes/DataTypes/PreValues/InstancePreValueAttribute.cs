using System;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class InstancePreValueAttribute : PreValueAttribute, IDataTypeInstance
    {
        public InstancePreValueAttribute(string alias, string value, int sortOrder, int id = 0)
            : base(alias, value, sortOrder, id) { }

        public InstancePreValueAttribute(string alias, string value)
            : base(alias, value) { }
    }
}