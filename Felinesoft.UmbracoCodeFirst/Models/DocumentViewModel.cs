using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Models;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Views;
using Felinesoft.UmbracoCodeFirst.ContentTypes;

namespace Felinesoft.UmbracoCodeFirst.Models
{
    /// <summary>
    /// A model which combines a strongly-typed document with a custom view model, suitable for use with a view which inherits <see cref="CodeFirstDocumentView`2"/>
    /// </summary>
    /// <typeparam name="Tdocument"></typeparam>
    /// <typeparam name="Tviewmodel"></typeparam>
    public class DocumentViewModel<Tdocument, Tviewmodel> where Tdocument : DocumentTypeBase
    {
        /// <summary>
        /// Constructs a new instance
        /// </summary>
        /// <param name="renderModel">The Umbraco render model</param>
        /// <param name="document">The strongly-typed document model</param>
        /// <param name="viewModel">A custom view model</param>
        public DocumentViewModel(RenderModel renderModel, Tdocument document, Tviewmodel viewModel)
        {
            Document = document;
            ViewModel = viewModel;
            RenderModel = renderModel;
        }

        /// <summary>
        /// Constructs a new instance, constructing the document
        /// model automatically from the render model
        /// </summary>
        /// <param name="renderModel">The Umbraco render model</param>
        /// <param name="viewModel">A custom view model</param>
        public DocumentViewModel(RenderModel renderModel, Tviewmodel viewModel)
        {
            Document = renderModel.Content.ConvertDocumentToModel<Tdocument>();
            ViewModel = viewModel;
            RenderModel = renderModel;
        }

        /// <summary>
        /// The strongly-typed document model
        /// </summary>
        public Tdocument Document { get; set; }

        /// <summary>
        /// The custom view model
        /// </summary>
        public Tviewmodel ViewModel { get; set; }

        /// <summary>
        /// The Umbraco render model
        /// </summary>
        public RenderModel RenderModel { get; set; }
    }
}