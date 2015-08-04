using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Models;
using Felinesoft.UmbracoCodeFirst.Views;

namespace Felinesoft.UmbracoCodeFirst.Controllers
{
    /// <summary>
    /// Provides convenience methods and properties for working with code-first strongly-typed models and views.
    /// This class is an ideal base for custom strongly-typed controllers, and can be inherited for any situation where you'd normally inherit RenderMvcController.
    /// <para>Views which inherit <see cref="CodeFirstDocumentView{Tdocument, Tviewmodel}"/> will work with this controller</para>
    /// </summary>
    /// <typeparam name="Tdocument">The document type</typeparam>
    public abstract class CodeFirstController<Tdocument> : CodeFirstController where Tdocument : DocumentTypeBase
    {
        private Tdocument _document = null;

        /// <summary>
        /// Returns the converted document model
        /// </summary>
        protected Tdocument DocumentModel
        {
            get
            {
                if (_document == null)
                {
                    _document = CurrentPage.ConvertToModel<Tdocument>();
                }
                return _document;
            }
        }

        /// <summary>
        /// Composes a document view model combining the render model, document model and view model.
        /// The model returned is compatible with CodeFirstDocumentView[Tdocument, Tviewmodel]
        /// </summary>
        /// <typeparam name="T">The view model type</typeparam>
        /// <param name="renderModel">The render model</param>
        /// <param name="viewModel">The view model</param>
        /// <returns>a DocumentViewModel combining the render model, document model and view model.</returns>
        protected DocumentViewModel<Tdocument, T> GetDocumentViewModel<T>(RenderModel renderModel, T viewModel)
        {
            return new DocumentViewModel<Tdocument, T>(renderModel, viewModel);
        }

        /// <summary>
        /// Returns a strongly-typed code-first view.
        /// The view should @inherit CodeFirstDocumentView[Tdocument, Tviewmodel]
        /// </summary>
        /// <typeparam name="T">The view model type</typeparam>
        /// <param name="renderModel">The render model</param>
        /// <param name="viewModel">The view model</param>
        /// <returns>A strongly-typed code-first view.</returns>
        protected ActionResult DocumentView<T>(T viewModel, RenderModel renderModel)
        {
            return CodeFirstTemplate(GetDocumentViewModel(renderModel, viewModel));
        }

        /// <summary>
        /// Returns a strongly-typed code-first view.
        /// The view should @inherit CodeFirstDocumentView[Tdocument, Tviewmodel]
        /// </summary>
        /// <typeparam name="T">The view model type</typeparam>
        /// <param name="renderModel">The render model</param>
        /// <param name="viewModel">The view model</param>
        /// <param name="view">The name of the view to use</param>
        /// <returns>A strongly-typed code-first view.</returns>
        protected ActionResult DocumentView<T>(string view, T viewModel, RenderModel renderModel)
        {
            return View(view, GetDocumentViewModel(renderModel, viewModel));
        }
    }
}
