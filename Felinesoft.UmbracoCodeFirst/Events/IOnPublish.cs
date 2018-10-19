using Umbraco.Core.Models;
using System.Web;
using Umbraco.Web;
using Umbraco.Core;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Umbraco.Core.Events;

namespace Marsman.UmbracoCodeFirst.Events
{
	public interface IOnPublishBase { }

	public interface IOnPublish<in T> : IOnPublishBase where T : CodeFirstContentBase
	{
		bool OnPublish(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CancellableEventArgs e);
	}

	public interface IOnPublishedBase { }

	public interface IOnPublished<in T> : IOnPublishedBase where T : CodeFirstContentBase
	{
		bool OnPublished(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CancellableEventArgs e);
	}
}