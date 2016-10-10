using System;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace Felinesoft.UmbracoCodeFirst.Core
{
	/// <summary>
	///     Holds the current HttpContext, replaces it with a fake one, then returns the original upon dispose
	/// </summary>
	public class HttpContextFaker : IDisposable
	{
		private readonly HttpContext _httpContext;

		/// <summary>
		///     Initializes an instance of HttpContextFaker
		/// </summary>
		/// <param name="toInsert">An HTTPContext to insert</param>
		public HttpContextFaker(HttpContext toInsert = null)
		{
			_httpContext = HttpContext.Current;
			HttpContext.Current = toInsert ?? new HttpContext(new SimpleWorkerRequest("fake.aspx", "", new StringWriter()));
		}

		void IDisposable.Dispose()
		{
			HttpContext.Current = _httpContext;
		}
	} 
}