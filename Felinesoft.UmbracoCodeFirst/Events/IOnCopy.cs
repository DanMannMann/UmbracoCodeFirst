using Umbraco.Core.Models;
using System.Web;
using Umbraco.Web;
using Umbraco.Core;
using Felinesoft.UmbracoCodeFirst.ContentTypes;

namespace Felinesoft.UmbracoCodeFirst.Events
{
	public interface IOnCopyBase { }

	public interface IOnCopy<in T> : IOnCopyBase where T : CodeFirstContentBase
	{
		bool OnCopy(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext);
	}
}