using System;
using System.Linq;
using System.Collections.Generic;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Marsman.UmbracoCodeFirst.Core;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Marsman.UmbracoCodeFirst.Events
{
	public abstract class EventHandlerBase<Tcontent> : IOnCreate<Tcontent>, IOnSave<Tcontent>, IOnDelete<Tcontent> where Tcontent : CodeFirstContentBase
	{
		public virtual bool OnCreate(Tcontent model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CancellableEventArgs e)
		{
			return true;
		}

		public virtual bool OnDelete(Tcontent model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CancellableEventArgs e)
		{
			return true;
		}

		public virtual bool OnSave(Tcontent model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CancellableEventArgs e)
		{
			return true;
		}
	}

	public abstract class MemberEventHandler<Tmember> : EventHandlerBase<Tmember> where Tmember : MemberTypeBase { }

	public abstract class MediaEventHandler<Tmedia> : EventHandlerBase<Tmedia>, IOnMove<Tmedia> where Tmedia : MediaTypeBase
	{
		public virtual bool OnMove(Tmedia model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CancellableEventArgs e)
		{
			return true;
		}
	}

	public abstract class DocumentEventHandler<Tdocument> : EventHandlerBase<Tdocument>, IOnPublish<Tdocument>, IOnUnpublish<Tdocument>, IOnCopy<Tdocument>, IOnMove<Tdocument>, IOnLoad<Tdocument> where Tdocument : DocumentTypeBase
	{
		public virtual bool OnMove(Tdocument model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CancellableEventArgs e)
		{
			return true;
		}

		public virtual bool OnPublish(Tdocument model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CancellableEventArgs e)
		{
			return true;
		}

		public virtual bool OnUnpublish(Tdocument model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CancellableEventArgs e)
		{
			return true;
		}

		public virtual bool OnCopy(Tdocument model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CancellableEventArgs e)
		{
			return true;
		}

		public virtual void OnLoad(Tdocument model, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext, IPublishedContent currentPage)
		{

		}
	}

	public abstract class DocumentEventHandler<Tdocument, Tviewmodel> : DocumentEventHandler<Tdocument>, IOnLoad<Tdocument, Tviewmodel> where Tdocument : DocumentTypeBase
	{
		public virtual void OnLoad(Tdocument model, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext, IPublishedContent currentPage, out Tviewmodel viewModel)
		{
			viewModel = default(Tviewmodel);
		}
	}
}