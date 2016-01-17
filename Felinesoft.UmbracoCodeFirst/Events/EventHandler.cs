using System;
using System.Linq;
using System.Collections.Generic;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Core;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Felinesoft.UmbracoCodeFirst.Events
{
	public abstract class EventHandlerBase<Tdocument> : IOnCreate<Tdocument>, IOnSave<Tdocument>, IOnDelete<Tdocument> where Tdocument : CodeFirstContentBase
	{
		public virtual bool OnCreate(Tdocument model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			return true;
		}

		public virtual bool OnDelete(Tdocument model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			return true;
		}

		public virtual bool OnSave(Tdocument model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			return true;
		}
	}

	public class EventHandler<Tdocument> : EventHandlerBase<Tdocument>, IOnRender<Tdocument> where Tdocument : CodeFirstContentBase
	{
		public virtual void OnRender(Tdocument model, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext, IPublishedContent currentPage)
		{

		}
	}

	public class EventHandler<Tdocument, Tviewmodel> : EventHandlerBase<Tdocument>, IOnRender<Tdocument, Tviewmodel> where Tdocument : CodeFirstContentBase
	{
		public virtual void OnRender(Tdocument model, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext, IPublishedContent currentPage, out Tviewmodel viewModel)
		{
			viewModel = default(Tviewmodel);
		}
	}
}