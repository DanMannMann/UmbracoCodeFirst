using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using System.Reflection;
using Felinesoft.UmbracoCodeFirst.Content;
using Felinesoft.UmbracoCodeFirst.Content.Interfaces;


namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    /// <summary>
    /// Registers a content factory, which must implement IContentFactory, to be used to create a default document for a document type
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ContentFactoryAttribute : Attribute
    {
        private IContentFactory _factory;

        /// <summary>
        /// <para>Registers a content factory to be used to create default content for a document type</para>
        /// </summary>
        /// <param name="factory">A type with a parameterless constructor which implements IContentFactory</param>
        /// <param name="sortOrder">The sort order for the document amongst its' siblings</param>
        public ContentFactoryAttribute(Type factory, int sortOrder = 10)
        {
            _factory = Activator.CreateInstance(factory) as IContentFactory;
            SortOrder = sortOrder;
        }

        /// <summary>
        /// Inheritors can call this if they want to construct their own factory (useful if your factory type has constructor parameters or other initialisation requirements).
        /// The inheritor will need to assign the constructed factory to the ContentFactoryAttribute.Factory property, which has a protected setter for this purpose, within its constructor.
        /// </summary>
        /// <param name="sortOrder">The sort order for the document amongst its' siblings</param>
        protected ContentFactoryAttribute(int sortOrder = 10)
        {
            SortOrder = sortOrder;
        }

        /// <summary>
        /// An instance of the type specified in the FactoryType property, which
        /// can be used to create a default document instance for the target document type
        /// </summary>
        public virtual IContentFactory Factory { get { return _factory; } protected set { _factory = value; } }

        /// <summary>
        /// The type, that implements IContentFactory, which can be used to create a 
        /// default document instance for the target document type
        /// </summary>
        public virtual Type FactoryType { get { return Factory == null ? null : Factory.GetType(); } }

        /// <summary>
        /// Returns true if this factory does not specify a parent content item (i.e. it creates root content)
        /// </summary>
        public virtual bool IsRootContent
        {
            get
            {
                return ParentContentType == null;
            }
        }

        /// <summary>
        /// The type whose default document will be the parent of this document
        /// </summary>
        public virtual Type ParentContentType
        {
            get
            {
                var contentInterface = FactoryType.GetInterfaces().FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IContentFactory<>));
                if(contentInterface == null)
                {
                    return null;
                }
                else
                {
                    return contentInterface.GetGenericArguments().First();
                }
            }
        }

        /// <summary>
        /// The sort order for the document amongst its' siblings
        /// </summary>
        public int SortOrder { get; private set; }
    }
}