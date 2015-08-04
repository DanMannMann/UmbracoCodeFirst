using Felinesoft.UmbracoCodeFirst.Attributes;
using System.Reflection;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public class PropertyRegistration : CodeFirstRegistration
    {
        internal PropertyRegistration() { }

        public PropertyRegistration(ContentPropertyAttribute propertyAttribute, string name, string alias, DataTypeRegistration dataType, PropertyInfo metadata, string cssClasses)
        {
            PropertyAttribute = propertyAttribute;
            Name = name;
            Alias = alias;
            DataType = dataType;
            Metadata = metadata;
            CssClasses = cssClasses;
        }

        public ContentPropertyAttribute PropertyAttribute { get; internal set; }
        public string Name { get; internal set; }
        public string Alias { get; internal set; }
        public DataTypeRegistration DataType { get; internal set; }
        public PropertyInfo Metadata { get; internal set; }
        public string CssClasses { get; internal set; }
    }
}