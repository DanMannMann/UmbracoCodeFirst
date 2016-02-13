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
	public interface IOnLoadBase { }

	public interface IOnLoad<in T> : IOnLoadBase where T : CodeFirstContentBase
	{
		void OnLoad(T model, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext, IPublishedContent currentPage);
	}

	public interface IOnLoad<in T, Tviewmodel> : IOnLoadBase where T : CodeFirstContentBase
	{
		void OnLoad(T model, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext, IPublishedContent currentPage, out Tviewmodel viewModel);
	}
}