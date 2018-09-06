using Marsman.UmbracoCodeFirst.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Marsman.UmbracoCodeFirst.Core.Modules
{
    public class ContentTypeRegistration : CodeFirstRegistration
    {
        internal List<PropertyRegistration> _properties;
        internal List<TabRegistration> _tabs;
        internal List<ContentTypeCompositionRegistration> _compositions;
        private string _alias;
        private string _name;
        private Type _clrType;
        private ContentTypeAttribute _contentTypeAttribute;

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is ContentTypeRegistration))
                return false;

            return (obj as ContentTypeRegistration).Alias == Alias;
        }

        public override int GetHashCode()
        {
            return Alias.GetHashCode();
        }

        protected ContentTypeRegistration() { }

        public ContentTypeRegistration(IEnumerable<PropertyRegistration> properties, IEnumerable<TabRegistration> tabs, IEnumerable<ContentTypeCompositionRegistration> compositions, string alias, string name, Type clrType, ContentTypeAttribute contentTypeAttribute)
        {
            _properties = properties.ToList();
            _tabs = tabs.ToList();
            _compositions = compositions.ToList();
            _alias = alias;
            _name = name;
            _clrType = clrType;
            _contentTypeAttribute = contentTypeAttribute;
        }

        public virtual IReadOnlyList<PropertyRegistration> Properties { get { return _properties.ToList().AsReadOnly(); } }
        public virtual IReadOnlyList<TabRegistration> Tabs { get { return _tabs.ToList().AsReadOnly(); } }
        public virtual IReadOnlyList<ContentTypeCompositionRegistration> Compositions { get { return _compositions.ToList().AsReadOnly(); } }

        public virtual string Alias
        {
            get { return _alias; }
            private set { _alias = value; }
        }

        public virtual string Name
        {
            get { return _name; }
            private set { _name = value; }
        }

        public virtual Type ClrType
        {
            get { return _clrType; }
            private set { _clrType = value; }
        }

        public virtual ContentTypeAttribute ContentTypeAttribute
        {
            get { return _contentTypeAttribute; }
            protected set { _contentTypeAttribute = value; }
        }
    }
}