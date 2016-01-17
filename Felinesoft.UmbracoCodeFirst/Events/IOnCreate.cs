using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using Umbraco.Web;
using Umbraco.Core.Models;
using Umbraco.Core;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Attributes;
using System.Reflection;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Felinesoft.UmbracoCodeFirst.Core;

namespace Felinesoft.UmbracoCodeFirst.Events
{
	public interface IOnCreateBase { }

	public interface IOnCreate : IOnCreateBase
	{
        bool OnCreate(IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext);
    }

	public interface IOnCreate<in T> : IOnCreateBase where T : CodeFirstContentBase
	{
		bool OnCreate(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext);
	}

}