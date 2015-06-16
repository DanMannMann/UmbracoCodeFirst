using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using Umbraco.Web.Mvc;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System.Reflection;
using Umbraco.Core.Models;
using System.Collections.Concurrent;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Umbraco.Web.Models;

namespace Felinesoft.UmbracoCodeFirst.Controllers
{
    /// <summary>
    /// <para>Converts IPublishedContent instances to code-first strongly-typed models and returns them as the model to a view. 
    /// If no suitable model exists the Umbraco RenderModel is passed through to the view, making it safe to mix code-first and manual
    /// document types.</para>
    /// <para>This controller is suitable as a replacement for the default controller in the controller resolver, allowing strongly-typed
    /// views to be created without creating a custom controller.</para>
    /// <para>Views which inherit <see cref="UmbracoViewPage{Tdocument}"/> or specify @model Tdocument will work with this controller</para>
    /// <para>This controller is the best base to derive from if you want to customise a code-first controller without a separate view model.
    /// See <see cref="CodeFirstController{Tdocument}"/> or <see cref="CodeFirstController{Tdocument, TviewModel}"/> for code-first controllers
    /// which support both a document model and a custom view model.</para>
    /// </summary>
    public class CodeFirstController : RenderMvcController
    {
        /// <summary>
        /// <para>Converts IPublishedContent instances to code-first strongly-typed models and returns them as the model to a view. 
        /// If no suitable model exists the Umbraco RenderModel is passed through to the view, making it safe to mix code-first and manual
        /// document types.</para>
        /// </summary>
        public override ActionResult Index(Umbraco.Web.Models.RenderModel model)
        {
            if (model.Content == null)
            {
                return base.Index(model);
            }

            Type docType;
            if (DocumentTypeRegister.DocumentTypeCache.TryGetValue(model.Content.DocumentTypeAlias, out docType))
            {
                MethodInfo convertToModel = DocumentTypeRegister.EnsureRegisterConvertMethod(docType);
                MethodInfo currentTemplate = DocumentTypeRegister.EnsureRegisterCurrentTemplateMethod(docType);

                var stronglyTypedModel = convertToModel.Invoke(null, new object[] { model.Content });
                return (ActionResult)currentTemplate.Invoke(this, new object[] { stronglyTypedModel });
            }
            else
            {
                //No code-first type found, just call base
                return base.Index(model);
            }
        }
    }
}
