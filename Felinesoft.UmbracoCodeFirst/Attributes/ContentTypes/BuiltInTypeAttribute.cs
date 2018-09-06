using System;

namespace Marsman.UmbracoCodeFirst.Attributes
{
    internal abstract class BuiltInTypeAttribute : Attribute { public abstract string BuiltInTypeName { get; } }
}