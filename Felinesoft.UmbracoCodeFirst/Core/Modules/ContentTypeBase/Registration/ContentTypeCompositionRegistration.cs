using Felinesoft.UmbracoCodeFirst.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public class ContentTypeCompositionRegistration : ContentTypeRegistration
    {
        private ContentTypeRegistration _basis;

        public ContentTypeCompositionRegistration(ContentTypeRegistration basis, PropertyInfo propertyOfContainer)
        {
            _basis = basis;
            PropertyOfContainer = propertyOfContainer;
        }

        public PropertyInfo PropertyOfContainer { get; internal set; }

        public override string Alias
        {
            get
            {
                return _basis.Alias;
            }
        }

        public override Type ClrType
        {
            get
            {
                return _basis.ClrType;
            }
        }

        public override IReadOnlyList<ContentTypeCompositionRegistration> Compositions
        {
            get
            {
                return _basis.Compositions;
            }
        }

        public override ContentTypeAttribute ContentTypeAttribute
        {
            get
            {
                return _basis.ContentTypeAttribute;
            }
        }

        public override string Name
        {
            get
            {
                return _basis.Name;
            }
        }

        public override IReadOnlyList<PropertyRegistration> Properties
        {
            get
            {
                return _basis.Properties;
            }
        }

        public override IReadOnlyList<TabRegistration> Tabs
        {
            get
            {
                return _basis.Tabs;
            }
        }
    }
}