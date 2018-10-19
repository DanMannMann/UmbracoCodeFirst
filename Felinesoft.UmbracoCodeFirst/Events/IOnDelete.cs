using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Umbraco.Core.Events;

namespace Marsman.UmbracoCodeFirst.Events
{
	public interface IOnDeleteBase { }

	public interface IOnDelete<in T> : IOnDeleteBase where T : CodeFirstContentBase
	{
		bool OnDelete(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CancellableEventArgs e);
	}

	public interface IOnDeletedBase { }

	public interface IOnDeleted<in T> : IOnDeleteBase where T : CodeFirstContentBase
	{
		bool OnDeleted(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CancellableEventArgs e);
	}
}