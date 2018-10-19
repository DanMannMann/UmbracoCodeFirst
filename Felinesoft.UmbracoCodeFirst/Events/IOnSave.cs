using System;
using System.Linq;
using System.Collections.Generic;
using Umbraco.Core.Models;
using System.Web;
using Umbraco.Core;
using Umbraco.Web;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Umbraco.Core.Events;

namespace Marsman.UmbracoCodeFirst.Events
{
	public interface IOnSaveBase { }

	public interface IOnSave<in T> : IOnSaveBase where T : CodeFirstContentBase
	{
		bool OnSave(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CancellableEventArgs e);
	}

	public interface IOnSavedBase { }

	public interface IOnSaved<in T> : IOnSaveBase where T : CodeFirstContentBase
	{
		bool OnSaved(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CancellableEventArgs e);
	}
}