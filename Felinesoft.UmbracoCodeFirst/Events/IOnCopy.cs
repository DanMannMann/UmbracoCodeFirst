using Umbraco.Core.Models;
using System.Web;
using Umbraco.Web;
using Umbraco.Core;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Umbraco.Core.Events;

namespace Marsman.UmbracoCodeFirst.Events
{
	public interface IOnCopyBase { }

	public interface IOnCopy<in T> : IOnCopyBase where T : CodeFirstContentBase
	{
		bool OnCopy(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CancellableEventArgs e);
	}

	public interface IOnCopiedBase { }

	public interface IOnCopied<in T> : IOnCopyBase where T : CodeFirstContentBase
	{
		bool OnCopied(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CancellableEventArgs e);
	}
}