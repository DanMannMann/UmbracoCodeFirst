using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;
using Felinesoft.UmbracoCodeFirst.ContentTypes;

namespace Felinesoft.UmbracoCodeFirst.Events
{
	public interface IOnDeleteBase { }

	public interface IOnDelete<in T> : IOnDeleteBase where T : CodeFirstContentBase
	{
		bool OnDelete(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext);
	}

	public interface IOnDelete : IOnDeleteBase
	{
		bool OnDelete(IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext);
	}
}