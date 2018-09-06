using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Marsman.UmbracoCodeFirst.Extensions;
using Umbraco.Web.Models;
using Marsman.UmbracoCodeFirst.ViewHelpers;
using Marsman.UmbracoCodeFirst.Models;
using Marsman.UmbracoCodeFirst.Views;
using Marsman.UmbracoCodeFirst.Controllers;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Marsman.UmbracoCodeFirst.Exceptions;
using Marsman.UmbracoCodeFirst.Core.Modules;
using Umbraco.Core;
using Marsman.UmbracoCodeFirst.Events;
using Marsman.UmbracoCodeFirst.Dictionaries;

namespace Marsman.UmbracoCodeFirst.Views
{
    /// <summary>
    /// <para>
    /// A custom view which provides a strongly-typed document model and a custom view model with specialised HTML helpers for each model.
    /// </para>
    /// <para>
    /// The view has an @Model property containing the original Umbraco RenderModel and also has @Document and @ViewModel properties of types Tdocument and Tviewmodel respectively,
    /// as well as @DocumentHelper and @ViewModelHelper HTML helper properties.
    /// </para>
    /// </summary>
    /// <typeparam name="Tdocument">the document type</typeparam>
    /// <typeparam name="Tviewmodel">the view model type</typeparam>
    public abstract class CodeFirstDocumentView<Tdocument, Tviewmodel> : Umbraco.Web.Mvc.UmbracoViewPage<RenderModel>, ICodeFirstViewHelper<Tdocument, Tviewmodel> where Tdocument : DocumentTypeBase
    {
        private DocumentViewModel<Tdocument, Tviewmodel> _innerModel;

		public Tdict Dictionary<Tdict>() where Tdict : DictionaryBase
		{
			return CodeFirstExtensions.Dictionary<Tdict>(this);
		}

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

		public DocumentViewModel<Tdocument, Tviewmodel> UnderlyingViewModel
		{
			get
			{
				return _innerModel;
			}
		}

		/// <summary>
		/// Intercepts the view data to get a reference to the code-first model, then
		/// passes the render model down to the base method (thus allowing the @Model property
		/// to behave as it usually does in an Umbraco view)
		/// </summary>
		/// <param name="viewData">The view data for the request</param>
		protected override void SetViewData(ViewDataDictionary viewData)
        {	
			if (viewData.Model is RenderModel)
			{
				var doc = (viewData.Model as RenderModel).Content.ConvertToModel();
				if (doc.GetType() != typeof(Tdocument) && !doc.GetType().Inherits<Tdocument>())
				{
					throw new CodeFirstException("Wrong type of model. This model does not inherit " + typeof(Tdocument).FullName);
                }
				base.SetViewData(viewData);
				Tviewmodel vm;
				ModelEventDispatcher<Tdocument>.OnLoad((Tdocument)doc, out vm, Umbraco.AssignedContentItem, Context, UmbracoContext, ApplicationContext, Core.CodeFirstModelContext.GetContext(doc));
                _innerModel = new DocumentViewModel<Tdocument, Tviewmodel>((viewData.Model as RenderModel), (Tdocument)doc, vm);
			}
            else if (viewData.Model is DocumentViewModel<Tdocument, Tviewmodel>)
            {
				_innerModel = viewData.Model as DocumentViewModel<Tdocument, Tviewmodel>;
				viewData.Model = _innerModel.RenderModel;
				base.SetViewData(viewData);
				ModelEventDispatcher<Tdocument>.OnLoad(UnderlyingViewModel.Document, Umbraco.AssignedContentItem, Context, UmbracoContext, ApplicationContext, Core.CodeFirstModelContext.GetContext(UnderlyingViewModel.Document));
            }
            else
            {
                throw new CodeFirstException("Wrong type of model. This view requires either a RenderModel (if the document type or its specified event handler implements IOnRender<Tdocument,Tviewmodel>) or DocumentViewModel<Tdocument, Tviewmodel> (if you're using a custom RenderMvcController). Note that CodeFirstManager.Current.Features.EnableContentEvents must be true to use IOnRender.");
            }
        }

		public MvcHtmlString IgnoreNullRaw(Func<Tdocument, IHtmlString> selector, string defaultValue = "")
		{
			try
			{
				return new MvcHtmlString(selector.Invoke(Document).ToHtmlString());
			}
			catch (NullReferenceException ex)
			{
				return new MvcHtmlString(defaultValue);
			}
		}

		public string IgnoreNull(Func<Tdocument, object> selector, string defaultValue = "")
		{
			try
			{
				return selector.Invoke(Document).ToString();
			}
			catch (NullReferenceException ex)
			{
				return defaultValue;
			}
		}
	}
}