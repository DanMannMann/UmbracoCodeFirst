using Felinesoft.UmbracoCodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using Umbraco.Web;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Exceptions;

namespace Felinesoft.UmbracoCodeFirst.DataTypes.BuiltIn
{
    /// <summary>
    /// A strongly-typed content picker which provides a strongly-typed model of the picked document.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataType(propertyEditorAlias: BuiltInPropertyEditorAliases.MultiNodeTreePicker)]
    public class TypedContentPicker<T> : ContentPicker, IPreValueFactory where T : DocumentTypeBase
    {
        private T _publishedContent = null;
        private bool _initFailed = false;

        public TypedContentPicker()
        {
            Id = -1; //set to -1 to indicate not yet configured
        }

        /// <summary>
        /// Returns true if there were any errors whilst loading the IPublishedContent
        /// or converting it to an instance of <c>T</c>
        /// </summary>
        public override bool HasContentError
        {
            get
            {
                EnsureInitialised();
                return _initFailed || base.HasContentError;
            }
        }

        /// <summary>
        /// The typed content instance.
        /// If the current document is not of the correct type then the TypedContent property will be null,
        /// but the PublishedContent property will still contain the IPublishedContent instance.
        /// </summary>
        public T TypedContent
        {
            get
            {
                EnsureInitialised();
                return _publishedContent;
            }
        }

        /// <summary>
        /// Initialises the typed content instance if it has not already been done
        /// </summary>
        private void EnsureInitialised()
        {
            if (_publishedContent == null && !_initFailed)
            {
                try
                {
                    if (Id == -1)
                    {
                        ErrorMessage = "No content ID set.";
                        _publishedContent = null;
                        _initFailed = true;
                        return;
                    }

                    var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);
                    var content = umbracoHelper.TypedContent(Id);
                    if (content == null)
                    {
                        ErrorMessage = "No published content with ID " + Id.ToString() + " found.";
                        _publishedContent = null;
                        _initFailed = true;
                        return;
                    }

                    if (typeof(T).GetDocumentTypeAlias() != content.DocumentTypeAlias)
                    {
                        ErrorMessage = "Picked content (Name: " + content.Name + ", ID: " + Id.ToString() + ") is not of type " + typeof(T).GetDocumentTypeAlias();
                        _publishedContent = null;
                        _initFailed = true;
                        return;
                    }

                    _publishedContent = content.ConvertToModel<T>();
                    _initFailed = false;
                    ErrorMessage = null;
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                }
            }
        }

        public IDictionary<string, PreValue> GetPreValues()
        {
            Dictionary<string, PreValue> result = new Dictionary<string, PreValue>();
            string typeAlias;
            try
            {
                typeAlias = this.GetType().GetGenericArguments().First().GetCodeFirstAttribute<DocumentTypeAttribute>().DocumentTypeAlias;
            }
            catch (Exception e)
            {
                throw new CodeFirstException("The type " + this.GetType().FullName + " does not specify a valid document type as its generic parameter", e);
            }

            result.Add("startNode", new PreValue(id: 0, value: "{\"type\": \"content\"}", sortOrder: 1));
            result.Add("filter", new PreValue(id: 0, value: typeAlias, sortOrder: 2));
            result.Add("minNumber", new PreValue(id: 0, value: "", sortOrder: 3));
            result.Add("maxNumber", new PreValue(id: 0, value: "1", sortOrder: 4));
            result.Add("showEditButton", new PreValue(id: 0, value: "0", sortOrder: 5));
            return result;
        }
    }
}
