using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marsman.UmbracoCodeFirst.Extensions;
using Marsman.UmbracoCodeFirst.Exceptions;

namespace Marsman.UmbracoCodeFirst.Attributes
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class TemplateAttribute : MultipleCodeFirstAttribute, IInitialisableAttribute
    {
        private string _typeName;

        public TemplateAttribute(bool isDefault = false, string name = null, string alias = null)
        {
            TemplateAlias = alias;
            TemplateName = name;
            IsDefault = isDefault;
        }

        public string TemplateName { get; set; }

        public string TemplateAlias { get; set; }

        public bool IsDefault { get; set; }

        public string DecoratedTypeFullName
        {
            get
            {
                return _typeName;
            }
            set
            {
                _typeName = value;
            }
        }

        public void Initialise(Type decoratedType)
        {
            var docAttr = decoratedType.GetCodeFirstAttribute<DocumentTypeAttribute>();
            if (docAttr == null)
            {
                throw new AttributeInitialisationException("[Template] can only be applied to classes which also have a [DocumentType] attribute. Affected type: " + decoratedType.FullName);
            }
            if (TemplateAlias == null)
            {
                TemplateAlias = docAttr.Alias;
            }
            if (TemplateName == null)
            {
                TemplateName = docAttr.Name;
            }
            _typeName = decoratedType.FullName;
            Initialised = true;
        }

        public bool Initialised { get; private set; }
    }
}
