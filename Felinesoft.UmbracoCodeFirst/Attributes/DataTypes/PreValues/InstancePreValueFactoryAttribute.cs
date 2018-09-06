using System;

namespace Marsman.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class InstancePreValueFactoryAttribute : PreValueFactoryAttribute, IDataTypeInstance
    {
        public InstancePreValueFactoryAttribute(Type factoryType)
            : base(factoryType) { }
    }

    /// <summary>
    /// Causes code-first to create a new data type instance whenever an attribute with this interface
    /// is added to a content property
    /// </summary>
    public interface IDataTypeInstance { }
}