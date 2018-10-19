using Umbraco.Core.Models;
using System.Web;
using Umbraco.Web;
using Umbraco.Core;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Umbraco.Core.Events;

namespace Marsman.UmbracoCodeFirst.Events
{
	public interface IOnUnpublishBase { }

	public interface IOnUnpublish<in T> : IOnUnpublishBase where T : CodeFirstContentBase
	{
		bool OnUnpublish(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CancellableEventArgs e);
	}
}