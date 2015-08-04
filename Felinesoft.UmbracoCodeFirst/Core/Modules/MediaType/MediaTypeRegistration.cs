using Felinesoft.UmbracoCodeFirst.Attributes;
using System;
using System.Collections.Generic;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public class MediaTypeRegistration : ContentTypeRegistration
    {
        public MediaTypeRegistration(IEnumerable<PropertyRegistration> properties, IEnumerable<TabRegistration> tabs, IEnumerable<ContentTypeCompositionRegistration> compositions, string alias, string name, Type clrType, MediaTypeAttribute mediaTypeAttribute, string cssClasses)
            : base(properties, tabs, compositions, alias, name, clrType, mediaTypeAttribute, cssClasses) { }

        public MediaTypeAttribute MediaTypeAttribute
        {
            get
            {
                return ContentTypeAttribute as MediaTypeAttribute;
            }
            set
            {
                ContentTypeAttribute = value;
            }
        }
    }
}