using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using Umbraco.Core;
using Umbraco.Core.Configuration;
using Umbraco.Web;
using Umbraco.Web.Routing;
using Umbraco.Web.Security;

namespace Felinesoft.UmbracoCodeFirst.Core
{
	/// <summary>
	///     Holds the current HttpContext, replaces it with a fake one, then returns the original upon dispose
	/// </summary>
	public class ContextHelper : IDisposable
	{
		private readonly HttpContext _httpContext;
		private readonly Version _umbracoVersion;

		/// <summary>
		///     Initializes an instance of ContextHelper
		/// </summary>
		/// <param name="toInsert">An HTTPContext to insert</param>
		public ContextHelper(HttpContext toInsert = null)
		{
			_umbracoVersion = new Version(ConfigurationManager.AppSettings["umbracoConfigurationStatus"]);
			_httpContext = HttpContext.Current;
			HttpContext.Current = toInsert ?? GetFakeHttpConext();
		}

		void IDisposable.Dispose()
		{
			HttpContext.Current = _httpContext;
		}

		/// <summary>
		///     Returns a fake UmbracoContext
		/// </summary>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		public UmbracoContext EnsureUmbracoContext()
		{
			if (_umbracoVersion <= new Version(6, 1, 3))
			{
				return GetFakeUmbracoContextForBefore_6_1_3();
			}
			if ((_umbracoVersion >= new Version(6, 1, 4)) && (_umbracoVersion <= new Version(7, 2, 8)))
			{
				return GetFakeUmbracoContextFor6_1_4_TO_7_2_8();
			}
			if (_umbracoVersion >= new Version(7, 3, 0))
			{
				return GetFakeUmbracoContextForAfter_7_3_0();
			}
			throw new Exception($"Umbraco Version `{_umbracoVersion}` not supported or not found.");
		}

		private static HttpContext GetFakeHttpConext()
		{
			return new HttpContext(new SimpleWorkerRequest("fake.aspx", "", new StringWriter()));
		}

		private static HttpContextWrapper GetFakeHttpContextWrapper()
		{
			return new HttpContextWrapper(new HttpContext(new SimpleWorkerRequest("fake.aspx", "", new StringWriter())));
		}

		private UmbracoContext GetFakeUmbracoContextForAfter_7_3_0()
		{
			var fakeHttpContextWrapper = GetFakeHttpContextWrapper();
			return UmbracoContext.EnsureContext(
				fakeHttpContextWrapper,
				ApplicationContext.Current,
				new WebSecurity(fakeHttpContextWrapper, ApplicationContext.Current),
				UmbracoConfig.For.UmbracoSettings(),
				UrlProviderResolver.Current.Providers,
				false);
		}

		private UmbracoContext GetFakeUmbracoContextFor6_1_4_TO_7_2_8()
		{
			var fakeHttpContextWrapper = GetFakeHttpContextWrapper();
			return UmbracoContext.EnsureContext(
				fakeHttpContextWrapper,
				ApplicationContext.Current,
				new WebSecurity(fakeHttpContextWrapper, ApplicationContext.Current),
				false);
		}

		private UmbracoContext GetFakeUmbracoContextForBefore_6_1_3()
		{
			var fakeHttpContextWrapper = GetFakeHttpContextWrapper();
			return typeof(UmbracoContext)
				.GetMethods(BindingFlags.Static | BindingFlags.NonPublic)
				.First(x => x.GetParameters().Count() == 3)
				.Invoke(null, new object[] {fakeHttpContextWrapper, ApplicationContext.Current, false}) as UmbracoContext;
		}
	}
}