using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    /// <summary>
    /// Specifies that a class should be used as a document type by UmbracoCodeFirst.
    /// Additionally causes all descendent types of the decorated type to be allowed as children of that type in the content tree.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DocumentTypeWithDescendantsAsChildrenAttribute : DocumentTypeAttribute
    {
        public override void Initialise(Type decoratedType)
        {
            base.Initialise(decoratedType);
            List<Type> children = new List<Type>(base.AllowedChildren);
            var currentType = decoratedType;

            while (currentType.BaseType != null && !CodeFirstManager.Current.DocumentTypeBases.Contains(currentType.BaseType)) //If there is a base type which is not a registered document type base
            {
                currentType = currentType.BaseType;
                if (currentType.Inherits<DocumentTypeBase>() && !children.Contains(currentType))
                {
                    children.Add(currentType);
                }
            }

            base.AllowedChildren = children.ToArray();
        }
    }
}
