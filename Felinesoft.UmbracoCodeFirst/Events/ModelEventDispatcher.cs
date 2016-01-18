using System;
using System.Linq;
using System.Collections.Generic;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Core;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System;
using System.Reflection;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Web;

namespace Felinesoft.UmbracoCodeFirst.Events
{
	internal static class ModelEventDispatcher
	{
		internal static bool HasEvent<Tinterface>(Type type)
		{
			return type.Implements<Tinterface>() || type.GetCodeFirstAttribute<EventHandlerAttribute>()?.EventHandlerType?.Implements<Tinterface>() == true;
		}

		private static bool OnEvent(string eventName, object model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			if (model.GetType().Inherits<CodeFirstContentBase>())
			{
				return (bool)typeof(ModelEventDispatcher<>)
				.MakeGenericType(model.GetType())
				.GetTypeInfo()
				.InvokeMember(eventName,
				BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
				null,
				null,
				new object[]
				{
					model,
					contentInstance,
					httpContext,
					umbContext,
					appContext
				});
			}
			else
			{
				throw new CodeFirstException("Not a valid model type");
			}
		}

		internal static bool OnCreateObject(object model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			return OnEvent("OnCreate", model, contentInstance, httpContext, umbContext, appContext);
        }

		internal static bool OnSaveObject(object model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			return OnEvent("OnSave", model, contentInstance, httpContext, umbContext, appContext);
		}

		internal static bool OnDeleteObject(object model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			return OnEvent("OnDelete", model, contentInstance, httpContext, umbContext, appContext);
		}

		internal static void OnLoadObject(object model, IPublishedContent contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext)
		{
			if (model.GetType().Inherits<CodeFirstContentBase>())
			{
				typeof(ModelEventDispatcher<>)
				.MakeGenericType(model.GetType())
				.GetTypeInfo()
				.InvokeMember("OnLoad",
				BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
				null,
				null,
				new object[]
				{
					model,
					contentInstance,
					httpContext,
					umbContext,
					appContext,
					modelContext
				});
			}
			else
			{
				throw new CodeFirstException("Not a valid model type");
			}
		}

		internal static bool OnUnpublishObject(object model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			return OnEvent("OnUnpublish", model, contentInstance, httpContext, umbContext, appContext);
		}

		internal static bool OnPublishObject(object model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			return OnEvent("OnPublish", model, contentInstance, httpContext, umbContext, appContext);
		}

		internal static bool OnCopyObject(object model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			return OnEvent("OnCopy", model, contentInstance, httpContext, umbContext, appContext);
		}

		internal static bool OnMoveObject(object model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			return OnEvent("OnMove", model, contentInstance, httpContext, umbContext, appContext);
		}
	}

	internal static class ModelEventDispatcher<T> where T : CodeFirstContentBase
	{
		internal static bool OnCreate(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				if (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>()?.EventHandlerType?.Implements<IOnCreate<T>>() == true)
				{
					return (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().ContentIsSelfHandling ? model as IOnCreate<T> : typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().EventHandler as IOnCreate<T>).OnCreate(model, contentInstance, httpContext, umbContext, appContext);
				}
			}
			return true;
		}

		internal static bool OnSave(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				if (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>()?.EventHandlerType?.Implements<IOnSave<T>>() == true)
				{
					return (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().ContentIsSelfHandling ? model as IOnSave<T> : typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().EventHandler as IOnSave<T>).OnSave(model, contentInstance, httpContext, umbContext, appContext);
				}
			}
			return true;
		}

		internal static bool OnDelete(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				if (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>()?.EventHandlerType?.Implements<IOnDelete<T>>() == true)
				{
					return (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().ContentIsSelfHandling ? model as IOnDelete<T> : typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().EventHandler as IOnDelete<T>).OnDelete(model, contentInstance, httpContext, umbContext, appContext);
				}
			}
			return true;
		}

		internal static bool OnCopy(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				if (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>()?.EventHandlerType?.Implements<IOnCopy<T>>() == true)
				{
					return (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().ContentIsSelfHandling ? model as IOnCopy<T> : typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().EventHandler as IOnCopy<T>).OnCopy(model, contentInstance, httpContext, umbContext, appContext);
				}
			}
			return true;
		}

		internal static bool OnMove(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				if (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>()?.EventHandlerType?.Implements<IOnMove<T>>() == true)
				{
					return (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().ContentIsSelfHandling ? model as IOnMove<T> : typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().EventHandler as IOnMove<T>).OnMove(model, contentInstance, httpContext, umbContext, appContext);
				}
			}
			return true;
		}

		internal static bool OnPublish(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				if (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>()?.EventHandlerType?.Implements<IOnPublish<T>>() == true)
				{
					return (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().ContentIsSelfHandling ? model as IOnPublish<T> : typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().EventHandler as IOnPublish<T>).OnPublish(model, contentInstance, httpContext, umbContext, appContext);
				}
			}
			return true;
		}

		internal static bool OnUnpublish(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				if (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>()?.EventHandlerType?.Implements<IOnUnpublish<T>>() == true)
				{
					return (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().ContentIsSelfHandling ? model as IOnUnpublish<T> : typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().EventHandler as IOnUnpublish<T>).OnUnpublish(model, contentInstance, httpContext, umbContext, appContext);
				}
			}
			return true;
		}

		internal static void OnLoad(T model, IPublishedContent contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				if (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>()?.EventHandlerType?.Implements<IOnLoad<T>>() == true)
				{
					(typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().ContentIsSelfHandling ? model as IOnLoad<T> : typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().EventHandler as IOnLoad<T>).OnLoad(model, httpContext, umbContext, appContext, modelContext, contentInstance);
				}
			}
		}

		internal static void OnLoad<Tviewmodel>(T model, out Tviewmodel viewModel, IPublishedContent contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext)
		{
			viewModel = default(Tviewmodel);
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				if (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>()?.EventHandlerType?.Implements<IOnLoad<T, Tviewmodel>>() == true)
				{
					(typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().ContentIsSelfHandling ? model as IOnLoad<T,Tviewmodel> : typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().EventHandler as IOnLoad<T, Tviewmodel>).OnLoad(model, httpContext, umbContext, appContext, modelContext, contentInstance, out viewModel);
				}
				else
				{
					OnLoad(model, contentInstance, httpContext, umbContext, appContext, modelContext);
				}
			}
		}
	}
}