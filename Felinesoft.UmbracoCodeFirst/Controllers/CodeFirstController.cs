using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Umbraco.Web.Mvc;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System.Reflection;
using Umbraco.Core.Models;
using System.Collections.Concurrent;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Umbraco.Web.Models;
using Felinesoft.UmbracoCodeFirst.Core.Modules;
using Felinesoft.UmbracoCodeFirst.Dictionaries;

namespace Felinesoft.UmbracoCodeFirst.Controllers
{
	public abstract class CodeFirstSurfaceController<Tdocument> : SurfaceController where Tdocument : DocumentTypeBase
	{
		private Tdocument _document;

		public Tdocument Document
		{
			get
			{
				_document = _document ?? CurrentPage.ConvertDocumentToModel<Tdocument>();
				return _document;
			}
		}

		protected Tdict Dictionary<Tdict>() where Tdict : DictionaryBase
		{
			return CodeFirstExtensions.Dictionary<Tdict>(this);
		}
	}

	/// <summary>
	/// <para>Converts IPublishedContent instances to code-first strongly-typed models and returns them as the model to a view. 
	/// If no suitable model exists the Umbraco RenderModel is passed through to the view, making it safe to mix code-first and manual
	/// document types.</para>
	/// <para>This controller is suitable as a replacement for the default controller in the controller resolver, allowing strongly-typed
	/// views to be created without creating a custom controller.</para>
	/// <para>Views which inherit <see cref="UmbracoViewPage&gt;Tdocument&lt;"/> or specify @model Tdocument will work with this controller</para>
	/// </summary>
	[Obsolete("Don't use this. It probably doesn't work and the whole general concept sucks.")]
    public class CodeFirstController : RenderMvcController
    {
        private static MethodInfo _currentTemplateGenericMethod = typeof(RenderMvcController).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Single(x => x.Name == "CurrentTemplate");
        private static ConcurrentDictionary<Type, MethodInfo> _runtimeCurrentTemplateMethods = new ConcurrentDictionary<Type, MethodInfo>();

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
            else
            {
                return CodeFirstTemplate(model.Content.ConvertToModel());
            }
        }

        protected ActionResult CodeFirstTemplate(object model)
        {
            MethodInfo currentTemplate = GetControllerCurrentTemplateMethod(model.GetType());
            return (ActionResult)currentTemplate.Invoke(this, new object[] { model });
        }

        private MethodInfo GetControllerCurrentTemplateMethod(Type docType)
        {
            if (!_runtimeCurrentTemplateMethods.ContainsKey(docType))
            {
                var currentTemplate = _currentTemplateGenericMethod.MakeGenericMethod(docType);
                _runtimeCurrentTemplateMethods.TryAdd(docType, currentTemplate);
            }
            return _runtimeCurrentTemplateMethods[docType];
        }

    }
}
