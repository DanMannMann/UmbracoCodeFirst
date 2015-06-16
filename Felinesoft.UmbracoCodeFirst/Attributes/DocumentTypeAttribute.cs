using Felinesoft.InitialisableAttributes;
using System;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using System.Linq;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    /// <summary>
    /// Specifies that a class should be used as a document type by UmbracoCodeFirst
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DocumentTypeAttribute : CodeFirstAttribute, IInitialisableAttribute
    {
        private bool _registerTemplate = false;
        private string _templatePath = null, _templateName = null, _templateAlias = null, _typeName = null;

        private DocumentTypeAttribute(string documentTypeName, string documentTypeAlias,
            Type[] allowedChildren, string icon,
            bool allowAtRoot, bool enableListView,
            string description)
        {
            DocumentTypeName = documentTypeName;
            DocumentTypeAlias = documentTypeAlias;
            EnableListView = enableListView;
            AllowedAtRoot = allowAtRoot;
            AllowedChildren = allowedChildren;
            Icon = icon;
            Description = description;
        }

        /// <summary>
        /// Specifies that a class should be used as a document type by UmbracoCodeFirst.
        /// Use this constructor when you want the template and document type names/aliases to be the same, or you don't want to register a template.
        /// </summary>
        /// <param name="templateLocation">The path to the view file in the form ~/[view path from project root]/[view name].cshtml</param>
        /// <param name="name">The name of the document type and template</param>
        /// <param name="alias">The alias of the document type and template</param>
        /// <param name="allowedChildren">An array of code-first document types which are valid children of this document type</param>
        /// <param name="icon">The icon to display in the content tree (see UmbracoCodeFirst.BuiltInIcons for constants representing Umbraco's default icon set)</param>
        /// <param name="allowAtRoot">True to allow this type of document to be created at the root of the content tree</param>
        /// <param name="enableListView">True to enable list view for this document type</param>
        /// <param name="description">The Description of the document type</param>
        /// <param name="registerTemplate">True to associate a template with the document type</param>
        public DocumentTypeAttribute(string templateLocation = null, string name = null, string alias = null,
            Type[] allowedChildren = null, string icon = BuiltInIcons.Folder,
            bool allowAtRoot = false, bool enableListView = false, bool registerTemplate = true, string description = "")
            : this(name, alias, allowedChildren, icon, allowAtRoot, enableListView, description)
        {
            _registerTemplate = registerTemplate;
            if (_registerTemplate)
            {
                TemplateAlias = alias;
                TemplatePath = templateLocation;
                TemplateName = name;
            }
        }

        /// <summary>
        /// Specifies that a class should be used as a document type by UmbracoCodeFirst.
        /// Use this constructor when you want the template and document type names/aliases to be different.
        /// </summary>
        /// <param name="documentTypeName">The name of the document type</param>
        /// <param name="documentTypeAlias">The alias of the document type</param>
        /// <param name="templateLocation">The path to the view file in the form ~/[view path from project root]/[view name].cshtml</param>
        /// <param name="templateName">The name of the template</param>
        /// <param name="templateAlias">The alias of the template</param>
        /// <param name="allowedChildren">An array of code-first document types which are valid children of this document type</param>
        /// <param name="icon">The icon to display in the content tree (see UmbracoCodeFirst.BuiltInIcons for constants representing Umbraco's default icon set)</param>
        /// <param name="allowAtRoot">True to allow this type of document to be created at the root of the content tree</param>
        /// <param name="enableListView">True to enable list view for this document type</param>
        /// <param name="description">The Description of the document type</param>
        public DocumentTypeAttribute(string documentTypeName, string documentTypeAlias, 
            string templateLocation, string templateName, string templateAlias,
            Type[] allowedChildren = null, string icon = BuiltInIcons.Folder,
            bool allowAtRoot = false, bool enableListView = false, string description = "")
            : this(documentTypeName, documentTypeAlias, allowedChildren, icon, allowAtRoot, enableListView, description)
        {
            TemplateAlias = templateAlias;
            TemplatePath = templateLocation;
            TemplateName = templateName;
            RegisterTemplate = true;
        }

        /// <summary>
        /// The name of the document type
        /// </summary>
        public string DocumentTypeName { get; set; }

        /// <summary>
        /// The alias of the document type
        /// </summary>
        public string DocumentTypeAlias { get; set; }

        /// <summary>
        /// An array of code-first document types which are valid children of this document type
        /// </summary>
        public Type[] AllowedChildren { get; set; }

        /// <summary>
        /// True to allow this type of document to be created at the root of the content tree
        /// </summary>
        public bool AllowedAtRoot { get; set; }

        /// <summary>
        /// True to enable list view for this document type
        /// </summary>
        public bool EnableListView { get; set; }

        /// <summary>
        /// The icon to display in the content tree (see UmbracoCodeFirst.BuiltInIcons for constants
        /// representing Umbraco's default icon set)
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// The Description of the document type
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// True to associate a template with the document type
        /// </summary>
        public bool RegisterTemplate { get { CheckInit(); return _registerTemplate; } set { _registerTemplate = value; } }

        /// <summary>
        /// The path to the view file in the form ~/[view path from project root]/[view name].cshtml
        /// </summary>
        public string TemplatePath { get { CheckInit(); return _templatePath; } set { _templatePath = value; } }

        /// <summary>
        /// The name of the template
        /// </summary>
        public string TemplateName { get { CheckInit(); return _templateName; } set { _templateName = value; } }

        /// <summary>
        /// The alias of the template
        /// </summary>
        public string TemplateAlias { get { CheckInit(); return _templateAlias; } set { _templateAlias = value; } }

        /// <summary>
        /// The fully qualified name of the type to which the attribute is applied
        /// </summary>
        public string DecoratedTypeFullName { get { CheckInit(); return _typeName; } }

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
            _typeName = decoratedType.FullName;
            if (DocumentTypeName == null)
            {
                DocumentTypeName = name;
            }
            if (DocumentTypeAlias == null)
            {
                DocumentTypeAlias = alias;
            }
            if (_templateName == null)
            {
                _templateName = name;
            }
            if (_templateAlias == null)
            {
                _templateAlias = alias;
            }
            if (_templatePath == null)
            {
                _templatePath = string.Format("~/Views/{0}.cshtml", alias);
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