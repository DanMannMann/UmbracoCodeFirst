using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Umbraco.Web.Models;

namespace Felinesoft.UmbracoCodeFirst.Controllers
{
    /// <summary>
    /// Provides convenience methods and properties for working with code-first strongly-typed models and views.
    /// Allows a view model to be supplied by simply overriding an abstract method (GetViewModel) so that custom controllers
    /// need only compose the view model, with everything else handled automagically.
    /// This class is an ideal base for custom strongly-typed controllers, and can be inherited for any situation where you'd normally inherit RenderMvcController.
    /// </summary>
    /// <typeparam name="Tdocument">The document type</typeparam>
    /// <typeparam name="TviewModel">The view model type</typeparam>
    public abstract class CodeFirstController<Tdocument, TviewModel> : CodeFirstController<Tdocument> where Tdocument : DocumentTypeBase
    {
        /// <summary>
        /// Gets the view model
        /// </summary>
        /// <param name="render">The RenderModel for the request</param>
        protected abstract TviewModel GetViewModel(RenderModel render);

        public override ActionResult Index(RenderModel model)
        {
            return DocumentView(GetViewModel(model), model);
        }
    }
}
