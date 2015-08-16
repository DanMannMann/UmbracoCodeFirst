using Felinesoft.UmbracoCodeFirst.Attributes;
using System;
using System.Collections.Generic;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public class MemberTypeRegistration : ContentTypeRegistration
    {
        public MemberTypeRegistration(IEnumerable<PropertyRegistration> properties, IEnumerable<TabRegistration> tabs, IEnumerable<ContentTypeCompositionRegistration> compositions, string alias, string name, Type clrType, MemberTypeAttribute mediaTypeAttribute)
            : base(properties, tabs, compositions, alias, name, clrType, mediaTypeAttribute) { }

        public MemberTypeAttribute MemberTypeAttribute
        {
            get
            {
                return ContentTypeAttribute as MemberTypeAttribute;
            }
            set
            {
                ContentTypeAttribute = value;
            }
        }
    }
}