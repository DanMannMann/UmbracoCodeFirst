using Umbraco.Core.Models;
using System.Web;
using Umbraco.Web;
using Umbraco.Core;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Umbraco.Core.Events;

namespace Marsman.UmbracoCodeFirst.Events
{
	public interface IOnMoveBase { }

	public interface IOnMove<in T> : IOnMoveBase where T : CodeFirstContentBase
	{
		bool OnMove(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CancellableEventArgs e);
	}
}