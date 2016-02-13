using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Felinesoft.UmbracoCodeFirst.ViewHelpers
{
    /// <summary>
    /// A set of specialised helpers for use with the document and view models in code-first strongly-typed views
    /// </summary>
    /// <typeparam name="Tdocument">The document type</typeparam>
    /// <typeparam name="Tviewmodel">The view model type</typeparam>
    public interface ICodeFirstViewHelper<Tdocument, Tviewmodel>
    {
        /// <summary>
        /// A HTML helper for the document model
        /// </summary>
        HtmlHelper<Tdocument> DocumentHelper { get; }

        /// <summary>
        /// A HTML helper for the view model
        /// </summary>
        HtmlHelper<Tviewmodel> ViewModelHelper { get; }

        /// <summary>
        /// The document model
        /// </summary>
        Tdocument Document { get; }

        /// <summary>
        /// The view model
        /// </summary>
        Tviewmodel ViewModel { get; }
    }
}
