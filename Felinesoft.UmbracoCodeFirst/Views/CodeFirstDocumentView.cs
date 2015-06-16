using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Umbraco.Web.Models;
using Felinesoft.UmbracoCodeFirst.ViewHelpers;
using Felinesoft.UmbracoCodeFirst.Models;
using Felinesoft.UmbracoCodeFirst.Views;
using Felinesoft.UmbracoCodeFirst.Controllers;

namespace Felinesoft.UmbracoCodeFirst.Views
{
    /// <summary>
    /// <para>
    /// A custom view which provides a strongly-typed document model and a custom view model with specialised HTML helpers for each model.
    /// </para>
    /// <para>
    /// This view expects an input model of type <see cref="DocumentViewModel{Tdocument, Tviewmodel}"/>.
    /// The controller <see cref="CodeFirstController{Tdocument}"/> provides a convenience 
    /// method <c>DocumentView{T}(T viewModel, RenderModel renderModel)</c> to generate this model
    /// The view has an @Model property containing the original Umbraco RenderModel and also has @Document and @ViewModel properties of types Tdocument and Tviewmodel respectively,
    /// as well as @DocumentHelper and @ViewModelHelper HTML helper properties.
    /// </para>
    /// </summary>
    /// <typeparam name="Tdocument">the document type</typeparam>
    /// <typeparam name="Tviewmodel">the view model type</typeparam>
    public abstract class CodeFirstDocumentView<Tdocument, Tviewmodel> : Umbraco.Web.Mvc.UmbracoViewPage<RenderModel>, ICodeFirstViewHelper<Tdocument, Tviewmodel>
    {
        private DocumentViewModel<Tdocument, Tviewmodel> _innerModel;

        protected CodeFirstDocumentView() : base() { _helper = new Lazy<CodeFirstViewHelper<Tdocument, Tviewmodel>>(() => new CodeFirstViewHelper<Tdocument, Tviewmodel>(Html, _innerModel.Document, _innerModel.ViewModel)); }

        private Lazy<CodeFirstViewHelper<Tdocument, Tviewmodel>> _helper;

        /// <summary>
        /// A HTML helper for the document model
        /// </summary>
        public HtmlHelper<Tdocument> DocumentHelper { get { return _helper.Value.DocumentHelper; } }

        /// <summary>
        /// A HTML helper for the view model
        /// </summary>
        public HtmlHelper<Tviewmodel> ViewModelHelper { get { return _helper.Value.ViewModelHelper; } }

        /// <summary>
        /// The document model
        /// </summary>
        public Tdocument Document { get { return _helper.Value.Document; } }

        /// <summary>
        /// The view model
        /// </summary>
        public Tviewmodel ViewModel { get { return _helper.Value.ViewModel; } }

        /// <summary>
        /// Intercepts the view data to get a reference to the code-first model, then
        /// passes the render model down to the base method (thus allowing the @Model property
        /// to behave as it usually does in an Umbraco view)
        /// </summary>
        /// <param name="viewData">The view data for the request</param>
        protected override void SetViewData(ViewDataDictionary viewData)
        {
            if (viewData.Model is DocumentViewModel<Tdocument, Tviewmodel>)
            {
                _innerModel = viewData.Model as DocumentViewModel<Tdocument, Tviewmodel>;
                viewData.Model = _innerModel.RenderModel;
                base.SetViewData(viewData);
            }
            else
            {
                //TODO throw exception for wrong type of view
            }
        }
    }
}