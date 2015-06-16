using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Felinesoft.UmbracoCodeFirst.ViewHelpers
{
    /// <summary>
    /// A set of specialised helpers for use with the document and view models in code-first strongly-typed views
    /// </summary>
    /// <typeparam name="Tdocument">The document type</typeparam>
    /// <typeparam name="Tviewmodel">The view model type</typeparam>
    class CodeFirstViewHelper<Tdocument, Tviewmodel> : CodeFirstDocumentHelper<Tdocument>, ICodeFirstViewHelper<Tdocument, Tviewmodel>
    {
        HtmlHelper<Tviewmodel> _vmHelper;
        Tviewmodel _view;


        public CodeFirstViewHelper(HtmlHelper underlyingHelper, Tdocument doc, Tviewmodel view)
            : base(underlyingHelper, doc)
        {
            _view = view;
        }

        /// <summary>
        /// A HTML helper for the view model
        /// </summary>
        public HtmlHelper<Tviewmodel> ViewModelHelper
        {
            get
            {
                if (_vmHelper == null)
                {
                    _vmHelper = HtmlHelperFor<Tviewmodel>(_view);
                }
                return _vmHelper;
            }
        }

        /// <summary>
        /// The view model
        /// </summary>
        public Tviewmodel ViewModel
        {
            get
            {
                return _view;
            }
        }
    }
}