using Marsman.UmbracoCodeFirst.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Marsman.UmbracoCodeFirst.Core.Modules
{
    public class TabRegistration : CodeFirstRegistration
    {
        internal TabRegistration() { }

        public TabRegistration(IEnumerable<PropertyRegistration> properties, string name, string originalName, ContentTabAttribute tabAttribute, Type clrType, PropertyInfo propertyOfParent)
        {
            _properties = properties;
            OriginalName = originalName;
            Name = name;
            TabAttribute = tabAttribute;
            ClrType = clrType;
            PropertyOfParent = propertyOfParent;
        }

        internal IEnumerable<PropertyRegistration> _properties;

        public IEnumerable<PropertyRegistration> Properties { get { return _properties.ToList().AsReadOnly(); } }
        public string Name { get; internal set; }
        public string OriginalName { get; internal set; }
        public ContentTabAttribute TabAttribute { get; internal set; }
        public Type ClrType { get; internal set; }
        public PropertyInfo PropertyOfParent { get; internal set; }
    }
}