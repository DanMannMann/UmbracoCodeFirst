using System.Web;
using System.Web.Mvc;
using Marsman.UmbracoCodeFirst.Extensions;
using Umbraco.Web.Models;
using Marsman.UmbracoCodeFirst.ViewHelpers;
using System;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Marsman.UmbracoCodeFirst.Core.Modules;
using Umbraco.Core;
using Marsman.UmbracoCodeFirst.Events;
using Marsman.UmbracoCodeFirst.Dictionaries;

namespace Marsman.UmbracoCodeFirst.Views
{
    /// <summary>
    /// Extends the default Umbraco view, adding a strongly-typed document property and a DocumentHelper.
    /// This view can be used to leverage strongly-typed models without replacing the default controller or implementing a custom controller.
    /// An exception will be thrown during construction if the current page is not of the correct document type. 
    /// </summary>
    /// <typeparam name="Tdocument">The document type</typeparam>
    public abstract class UmbracoDocumentViewPage<Tdocument> : Umbraco.Web.Mvc.UmbracoViewPage<Umbraco.Web.Models.RenderModel>  where Tdocument : DocumentTypeBase
    {
        protected UmbracoDocumentViewPage() : base()
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents && ModelEventDispatcher.HasEvent<IOnLoadBase>(typeof(Tdocument)))
			{
				var doc = GetDocument();
                _helper = new Lazy<CodeFirstDocumentHelper<Tdocument>>(() => new CodeFirstDocumentHelper<Tdocument>(Html, doc));
			}
			else
			{
				_helper = new Lazy<CodeFirstDocumentHelper<Tdocument>>(() => new CodeFirstDocumentHelper<Tdocument>(Html, GetDocument()));
			}
		}

		public Tdict Dictionary<Tdict>() where Tdict : DictionaryBase
		{
			return CodeFirstExtensions.Dictionary<Tdict>(this);
		}

        private Tdocument GetDocument()
        {
			if (_converted == null)
			{
				_converted = base.Umbraco.AssignedContentItem.ConvertToModel() as Tdocument;
				//ModelEventDispatcher<Tdocument>.OnLoad(_converted, Umbraco.AssignedContentItem, Context, UmbracoContext, ApplicationContext, Core.CodeFirstModelContext.GetContext(_converted));
			}
            return _converted;
        }

        private Lazy<CodeFirstDocumentHelper<Tdocument>> _helper;
		private Tdocument _converted;

		/// <summary>
		/// A HTML helper for the document model
		/// </summary>
		public HtmlHelper<Tdocument> DocumentHelper { get { return _helper.Value.DocumentHelper; } }

        /// <summary>
        /// The document model
        /// </summary>
        public Tdocument Document { get { return _helper.Value.Document; } }

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
