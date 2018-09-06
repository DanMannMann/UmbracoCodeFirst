using Marsman.UmbracoCodeFirst.Attributes;
using System;
using System.Collections.Generic;

namespace Marsman.UmbracoCodeFirst.Core.Modules
{
    public class DocumentTypeRegistration : ContentTypeRegistration
    {
        public DocumentTypeRegistration(IEnumerable<PropertyRegistration> properties, IEnumerable<TabRegistration> tabs, IEnumerable<ContentTypeCompositionRegistration> compositions, string alias, string name, Type clrType, DocumentTypeAttribute documentTypeAttribute)
            : base(properties, tabs, compositions, alias, name, clrType, documentTypeAttribute) { }

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