using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using Umbraco.Web;
using Umbraco.Core.Models;
using Umbraco.Core;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Marsman.UmbracoCodeFirst.Extensions;
using Marsman.UmbracoCodeFirst.Attributes;
using System.Reflection;
using Marsman.UmbracoCodeFirst.Exceptions;
using Marsman.UmbracoCodeFirst.Core;
using Umbraco.Core.Events;

namespace Marsman.UmbracoCodeFirst.Events
{
	public interface IOnCreateBase { }

	public interface IOnCreate<in T> : IOnCreateBase where T : CodeFirstContentBase
	{
		bool OnCreate(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CancellableEventArgs e);
	}

}