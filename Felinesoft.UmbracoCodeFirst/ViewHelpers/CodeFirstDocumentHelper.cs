using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Marsman.UmbracoCodeFirst.ViewHelpers
{
    /// <summary>
    /// A specialised helper for use with the document model in code-first strongly-typed views
    /// </summary>
    /// <typeparam name="Tdocument">The document type</typeparam>
    class CodeFirstDocumentHelper<Tdocument>
    {
        Tdocument _doc;
        HtmlHelper<Tdocument> _docHelper;
        HtmlHelper _underlyingHelper;

        public CodeFirstDocumentHelper(HtmlHelper underlyingHelper, Tdocument doc)
        {
            _underlyingHelper = underlyingHelper;
            _doc = doc;
        }

        /// <summary>
        /// The document model
        /// </summary>
        public Tdocument Document
        {
            get
            {
                return _doc;
            }
        }

        /// <summary>
        /// A HTML helper for the document model
        /// </summary>
        public HtmlHelper<Tdocument> DocumentHelper
        {
            get
            {
                if (_docHelper == null)
                {
                    _docHelper = HtmlHelperFor<Tdocument>(_doc);
                }
                return _docHelper;
            }
        }

        protected HtmlHelper<TModel> HtmlHelperFor<TModel>()
        {
            return HtmlHelperFor(default(TModel));
        }

        protected HtmlHelper<TModel> HtmlHelperFor<TModel>(TModel model)
        {
            return HtmlHelperFor(model, null);
        }

        protected HtmlHelper<TModel> HtmlHelperFor<TModel>(TModel model, string htmlFieldPrefix)
        {
            var viewDataContainer = CreateViewDataContainer(_underlyingHelper.ViewData, model);

            TemplateInfo templateInfo = viewDataContainer.ViewData.TemplateInfo;

            if (!String.IsNullOrEmpty(htmlFieldPrefix))
                templateInfo.HtmlFieldPrefix = templateInfo.GetFullHtmlFieldName(htmlFieldPrefix);

            ViewContext viewContext = _underlyingHelper.ViewContext;
            ViewContext newViewContext = new ViewContext(viewContext.Controller.ControllerContext, viewContext.View, viewDataContainer.ViewData, viewContext.TempData, viewContext.Writer);

            return new HtmlHelper<TModel>(newViewContext, viewDataContainer, _underlyingHelper.RouteCollection);
        }

        protected IViewDataContainer CreateViewDataContainer(ViewDataDictionary viewData, object model)
        {
            var newViewData = new ViewDataDictionary(viewData)
            {
                Model = model
            };

            newViewData.TemplateInfo = new TemplateInfo
            {
                HtmlFieldPrefix = newViewData.TemplateInfo.HtmlFieldPrefix
            };

            return new ViewDataContainer
            {
                ViewData = newViewData
            };
        }

        class ViewDataContainer : IViewDataContainer
        {
            public ViewDataDictionary ViewData { get; set; }
        }
    }
}
