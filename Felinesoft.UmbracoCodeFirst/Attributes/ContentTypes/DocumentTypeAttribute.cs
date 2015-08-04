
using System;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using System.Linq;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Felinesoft.UmbracoCodeFirst.Core;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    /// <summary>
    /// Specifies that a class should be used as a document type by UmbracoCodeFirst
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DocumentTypeAttribute : ContentTypeAttribute, IInitialisableAttribute
    {
        public DocumentTypeAttribute(string name = null, string alias = null,
            Type[] allowedChildren = null, string icon = BuiltInIcons.IconDocument,
            bool allowAtRoot = false, bool enableListView = false,
            string description = "", string cssClasses = "")
        {
            Name = name;
            Alias = alias;
            EnableListView = enableListView;
            AllowedAtRoot = allowAtRoot;
            AllowedChildren = allowedChildren;
            Icon = icon;
            Description = description;
            CssClasses = cssClasses;
        }

        /// <summary>
        /// Returns true if the attribute instance has been initialised.
        /// Until the attribute is initialised any inferred properties (i.e. any not explicitly set by the caller) will still be null.
        /// </summary>
        public bool Initialised
        {
            get;
            private set;
        }

        /// <summary>
        /// Initialises the Factory property based on the type to which the attribute is applied.
        /// </summary>
        /// <param name="decoratedType">The type to which the attribute is applied</param>
        public virtual void Initialise(Type decoratedType)
        {
            if (Initialised)
            {
                throw new InvalidOperationException("Already initialised!");
            }

            var name = decoratedType.Name.ToProperCase();
            var alias = decoratedType.Name.ToCamelCase();
            if (Name == null)
            {
                Name = name;
            }
            if (Alias == null)
            {
                Alias = alias;
            }
            if (AllowedChildren == null && decoratedType.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IListViewDocumentType<>)))
            {
                var type = decoratedType.GetInterfaces().First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IListViewDocumentType<>)).GetGenericArguments().First();

                if (type.GetCodeFirstAttribute<DocumentTypeAttribute>(false) != null)
                {
                    AllowedChildren = new Type[] { type };
                }
            }
            Initialised = true;
        }

        private void CheckInit()
        {
            if (!Initialised)
            {
                throw new AttributeInitialisationException();
            }
        }  
    }

}