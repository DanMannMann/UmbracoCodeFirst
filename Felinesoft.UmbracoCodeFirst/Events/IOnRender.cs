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
	public interface IOnRenderBase { }

    public interface IOnRender : IOnRenderBase
	{
		void OnRender(HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext, IPublishedContent currentPage);
	}

	public interface IOnRender<in T> : IOnRenderBase where T : CodeFirstContentBase
	{
		void OnRender(T model, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext, IPublishedContent currentPage);
	}

	public interface IOnRender<in T, Tviewmodel> : IOnRenderBase where T : CodeFirstContentBase
	{
		void OnRender(T model, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext, IPublishedContent currentPage, out Tviewmodel viewModel);
	}
}