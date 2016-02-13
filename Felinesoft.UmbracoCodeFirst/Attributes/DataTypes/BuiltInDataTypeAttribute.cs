using System;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    /// <summary>
    /// Specifies that the class describes one of the built-in Code-First data types
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    internal class BuiltInDataTypeAttribute : BuiltInTypeAttribute
    {
        public override string BuiltInTypeName
        {
            get { return "Data Type"; }
        }
    }
}