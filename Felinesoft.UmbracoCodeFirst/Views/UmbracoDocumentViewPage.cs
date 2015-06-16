using System.Web;
using System.Web.Mvc;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Umbraco.Web.Models;
using Felinesoft.UmbracoCodeFirst.ViewHelpers;
using System;

namespace Felinesoft.UmbracoCodeFirst.Views
{
    /// <summary>
    /// Extends the default Umbraco view, adding a strongly-typed document property and a DocumentHelper.
    /// This view can be used to leverage strongly-typed models without replacing the default controller or implementing a custom controller.
    /// An exception will be thrown during construction if the current page is not of the correct document type. 
    /// </summary>
    /// <typeparam name="Tdocument">The document type</typeparam>
    public abstract class UmbracoDocumentViewPage<Tdocument> : Umbraco.Web.Mvc.UmbracoViewPage<Umbraco.Web.Models.RenderModel>
    {
        protected UmbracoDocumentViewPage() : base() { _helper = new Lazy<CodeFirstDocumentHelper<Tdocument>>(() => new CodeFirstDocumentHelper<Tdocument>(Html, GetDocument())); }

        private Tdocument GetDocument()
        {
            return base.Umbraco.AssignedContentItem.ConvertToModel<Tdocument>();
        }

        private Lazy<CodeFirstDocumentHelper<Tdocument>> _helper;

        /// <summary>
        /// A HTML helper for the document model
        /// </summary>
        public HtmlHelper<Tdocument> DocumentHelper { get { return _helper.Value.DocumentHelper; } }

        /// <summary>
        /// The document model
        /// </summary>
        public Tdocument Document { get { return _helper.Value.Document; } }
    }
}
