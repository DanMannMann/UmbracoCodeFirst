using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using System.Reflection;
using Felinesoft.UmbracoCodeFirst.Content;
using Felinesoft.UmbracoCodeFirst;
using Felinesoft.InitialisableAttributes;
using Felinesoft.UmbracoCodeFirst.Content.Factories;
using Felinesoft.UmbracoCodeFirst.Content.Interfaces;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    /// <summary>
    /// <para>
    /// When applied to a code-first document type class this attribute instructs the code-first initialiser
    /// to create a document instance of that type, if it does not exist. This attribute should only be applied
    /// to classes which inherit DocumentTypeBase and which also have a [DocumentType] attribute.
    /// </para>
    /// <para>
    /// The values for the document instance can either be configured in a parameterless constructor of the document type
    /// class or in the InitialiseDefaults method if the class implements IAutoContent
    /// </para>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class AutoContentAttribute : ContentFactoryAttribute, IInitialisableAttribute
    {
        private Type _parentType;
        private string _name;

        /// <summary>
        /// Returns true if the attribute instance has been initialised.
        /// Until the attribute is initialised accessing the Factory or FactoryType properties will cause an exception.
        /// </summary>
        public bool Initialised { get; private set; }

        /// <summary>
        /// The type whose default document will be the parent of this document
        /// </summary>
        public override Type ParentContentType
        {
            get
            {
                return _parentType;
            }
        }

        /// <summary>
        /// <para>
        /// When applied to a code-first document type class this attribute instructs the code-first initialiser
        /// to create a document instance of that type, if it does not exist. This attribute should only be applied
        /// to classes which inherit DocumentTypeBase and which also have a [DocumentType] attribute.
        /// </para>
        /// <para>
        /// The values for the document instance can either be configured in a parameterless constructor of the document type
        /// class or in the InitialiseDefaults method if the class implements IAutoContent
        /// </para>
        /// </summary>
        /// <param name="name">The name of the document node</param>
        /// <param name="sortOrder">The sort order for the document amongst its' siblings</param>
        /// <param name="parentType">
        /// <para>The type whose default document will be the parent of this document.</para>
        /// <para>The supplied type must inherit DocumentTypeBase and must specify a default content provider via an [AutoContent] or [ContentFactory] attribute</para>
        /// </param>
        public AutoContentAttribute(string name, Type parentType = null, int sortOrder = 10)
            : base(sortOrder)
        {
            _parentType = parentType;
            _name = name;
        }

        /// <summary>
        /// The type, that implements IContentFactory, which can be used to create a 
        /// default document instance for the target document type
        /// </summary>
        public override Type FactoryType
        {
            get
            {
                if (!Initialised)
                {
                    throw new InvalidOperationException("Not initialised!");
                }
                return base.FactoryType;
            }
        }

        /// <summary>
        /// An instance of the type specified in the FactoryType property, which
        /// can be used to create a default document instance for the target document type
        /// </summary>
        public override IContentFactory Factory
        {
            get
            {
                if (!Initialised)
                {
                    throw new InvalidOperationException("Not initialised!");
                }
                return base.Factory;
            }
            protected set
            {
                base.Factory = value;
            }
        }

        /// <summary>
        /// Initialises the Factory property based on the type to which the attribute is applied.
        /// </summary>
        /// <param name="targetType">The type to which the attribute is applied</param>
        public void Initialise(Type targetType)
        {
            if (Initialised)
            {
                throw new InvalidOperationException("Already initialised!");
            }

            var values = GetValues(targetType);

            Factory = ConstructFactory(values);

            Initialised = true;
        }

        /// <summary>
        /// Automatically constructs a factory for any inheritor of DocumentTypeBase
        /// </summary>
        /// <param name="values">The document type to create</param>
        /// <returns>a factory for any inheritor of DocumentTypeBase</returns>
        protected virtual IContentFactory ConstructFactory(DocumentTypeBase values)
        {
            values.NodeDetails.Name = _name;
            if (_parentType == null) //Root node
            {
                return new RootContentFactory(values);
            }
            else //Child node
            {
                return Activator.CreateInstance(typeof(ChildContentFactory<>).MakeGenericType(_parentType), values) as IContentFactory;
            }
        }

        /// <summary>
        /// Gets the document instance values for use in creating the default content
        /// </summary>
        /// <param name="targetType">The document type</param>
        /// <returns>
        /// The document instance, populated by calling 
        /// InitialiseDefaults if targetType implements IAutoContent
        /// </returns>
        protected virtual DocumentTypeBase GetValues(Type targetType)
        {
            var values = Activator.CreateInstance(targetType) as DocumentTypeBase;
            if (values is IAutoContent)
            {
                (values as IAutoContent).InitialiseDefaults();
            }
            return values;
        }
    }
}
