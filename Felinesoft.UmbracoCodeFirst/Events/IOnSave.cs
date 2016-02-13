using System;
using System.Linq;
using System.Collections.Generic;
using Umbraco.Core.Models;
using System.Web;
using Umbraco.Core;
using Umbraco.Web;
using Felinesoft.UmbracoCodeFirst.ContentTypes;

namespace Felinesoft.UmbracoCodeFirst.Events
{
	public interface IOnSaveBase { }

	public interface IOnSave<in T> : IOnSaveBase where T : CodeFirstContentBase
	{
		bool OnSave(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext);
	}
}