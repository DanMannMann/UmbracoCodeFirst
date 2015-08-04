using Felinesoft.UmbracoCodeFirst.Attributes;
using System;
using System.Collections.Generic;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public class DocumentTypeRegistration : ContentTypeRegistration
    {
        public DocumentTypeRegistration(IEnumerable<PropertyRegistration> properties, IEnumerable<TabRegistration> tabs, IEnumerable<ContentTypeCompositionRegistration> compositions, string alias, string name, Type clrType, DocumentTypeAttribute documentTypeAttribute, string cssClasses)
            : base(properties, tabs, compositions, alias, name, clrType, documentTypeAttribute, cssClasses) { }

        public DocumentTypeAttribute DocumentTypeAttribute
        {
            get
            {
                return ContentTypeAttribute as DocumentTypeAttribute;
            }
            set
            {
                ContentTypeAttribute = value;
            }
        }

    }

}