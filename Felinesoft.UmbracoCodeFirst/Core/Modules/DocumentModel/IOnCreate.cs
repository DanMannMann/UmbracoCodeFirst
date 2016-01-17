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

	public interface IOnSaveBase { }

	public interface IOnSave : IOnSaveBase
	{
		bool OnSave(IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext);
	}

	public interface IOnSave<in T> : IOnSaveBase where T : CodeFirstContentBase
	{
		bool OnSave(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext);
	}

	public interface IOnDeleteBase { }

	public interface IOnDelete : IOnDeleteBase
	{
		bool OnDelete(IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext);
	}

	public interface IOnDelete<in T> : IOnDeleteBase where T : CodeFirstContentBase
	{
		bool OnDelete(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext);
	}

	public interface IOnRender
	{
		void OnRender(HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext, IPublishedContent currentPage);
	}

	public interface IOnRender<in T> where T : CodeFirstContentBase
	{
		void OnRender(T model, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext, IPublishedContent currentPage);
	}

	public interface IOnRender<in T, Tviewmodel> where T : CodeFirstContentBase
	{
		void OnRender(T model, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext, IPublishedContent currentPage, out Tviewmodel viewModel);
	}

	internal static class ModelEventDispatcher
	{
		internal static bool HasEvent<Tinterface>(Type type)
		{
			return type.Implements<Tinterface>() || type.GetCodeFirstAttribute<EventHandlerAttribute>()?.EventHandlerType?.Implements<Tinterface>() == true;
        }

		internal static bool OnCreateObject(object model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			if (model.GetType().Inherits<CodeFirstContentBase>())
			{
				return (bool)typeof(ModelEventDispatcher<>)
					.MakeGenericType(model.GetType())
					.GetTypeInfo()
					.InvokeMember("OnCreate", 
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

		internal static bool OnSaveObject(object model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			if (model.GetType().Inherits<CodeFirstContentBase>())
			{
				return (bool)typeof(ModelEventDispatcher<>)
					.MakeGenericType(model.GetType())
					.GetTypeInfo()
					.InvokeMember("OnSave",
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

		internal static bool OnDeleteObject(object model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			if (model.GetType().Inherits<CodeFirstContentBase>())
			{
				return (bool)typeof(ModelEventDispatcher<>)
					.MakeGenericType(model.GetType())
					.GetTypeInfo()
					.InvokeMember("OnDelete",
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

		internal static void OnRenderObject(object model, IPublishedContent contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext)
		{
			if (model.GetType().Inherits<CodeFirstContentBase>())
			{
				typeof(ModelEventDispatcher<>)
					.MakeGenericType(model.GetType())
					.GetTypeInfo()
					.InvokeMember("OnRender",
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
	}

	internal static class ModelEventDispatcher<T> where T : CodeFirstContentBase
	{
		internal static bool OnCreate(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				if (typeof(T).Implements<IOnCreate>())
				{
					return (model as IOnCreate)?.OnCreate(contentInstance, httpContext, umbContext, appContext) ?? true;
				}
				else if (typeof(T).Implements<IOnCreate<T>>())
				{
					return (model as IOnCreate<T>)?.OnCreate(model, contentInstance, httpContext, umbContext, appContext) ?? true;
				}
				else if (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>()?.EventHandlerType?.Implements<IOnCreate<T>>() == true)
				{
					return (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().EventHandler as IOnCreate<T>).OnCreate(model, contentInstance, httpContext, umbContext, appContext);
				}
			}
			return true;
		}

		internal static bool OnSave(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				if (typeof(T).Implements<IOnSave>())
				{
					return (model as IOnSave)?.OnSave(contentInstance, httpContext, umbContext, appContext) ?? true;
				}
				else if (typeof(T).Implements<IOnCreate<T>>())
				{
					return (model as IOnSave<T>)?.OnSave(model, contentInstance, httpContext, umbContext, appContext) ?? true;
				}
				else if (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>()?.EventHandlerType?.Implements<IOnSave<T>>() == true)
				{
					return (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().EventHandler as IOnSave<T>).OnSave(model, contentInstance, httpContext, umbContext, appContext);
				}
			}
			return true;
		}

		internal static bool OnDelete(T model, IContentBase contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				if (typeof(T).Implements<IOnDelete>())
				{
					return (model as IOnDelete)?.OnDelete(contentInstance, httpContext, umbContext, appContext) ?? true;
				}
				else if (typeof(T).Implements<IOnDelete<T>>())
				{
					return (model as IOnDelete<T>)?.OnDelete(model, contentInstance, httpContext, umbContext, appContext) ?? true;
				}
				else if (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>()?.EventHandlerType?.Implements<IOnDelete<T>>() == true)
				{
					return (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().EventHandler as IOnDelete<T>).OnDelete(model, contentInstance, httpContext, umbContext, appContext);
				}
			}
			return true;
		}

		internal static void OnRender(T model, IPublishedContent contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				if (typeof(T).Implements<IOnRender>())
				{
					(model as IOnRender)?.OnRender(httpContext, umbContext, appContext, modelContext, contentInstance);
				}
				else if (typeof(T).Implements<IOnRender<T>>())
				{
					(model as IOnRender<T>)?.OnRender(model, httpContext, umbContext, appContext, modelContext, contentInstance);
				}
				else if (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>()?.EventHandlerType?.Implements<IOnRender<T>>() == true)
				{
					(typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().EventHandler as IOnRender<T>).OnRender(model, httpContext, umbContext, appContext, modelContext, contentInstance);
				}
			}
		}

		internal static void OnRender<Tviewmodel>(T model, out Tviewmodel viewModel, IPublishedContent contentInstance, HttpContextBase httpContext, UmbracoContext umbContext, ApplicationContext appContext, CodeFirstModelContext modelContext)
		{
			viewModel = default(Tviewmodel);
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				if (typeof(T).Implements<IOnRender<T, Tviewmodel>>())
				{
					(model as IOnRender<T, Tviewmodel>)?.OnRender(model, httpContext, umbContext, appContext, modelContext, contentInstance, out viewModel);
				}
				else if (typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>()?.EventHandlerType?.Implements<IOnRender<T, Tviewmodel>>() == true)
				{
					(typeof(T).GetCodeFirstAttribute<EventHandlerAttribute>().EventHandler as IOnRender<T, Tviewmodel>).OnRender(model, httpContext, umbContext, appContext, modelContext, contentInstance, out viewModel);
				}
				else
				{
					OnRender(model, contentInstance, httpContext, umbContext, appContext, modelContext);
				}
			}
		}
	}
}