using Marsman.UmbracoCodeFirst.Core.Modules;
using Marsman.UmbracoCodeFirst.ContentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Umbraco.Core.Models;
using Castle.DynamicProxy;
using System.Reflection;

namespace Marsman.UmbracoCodeFirst.Core
{
	public class ProxyBase : IHtmlString
	{
		public string ToHtmlString()
		{
			var proxy = this as IProxyTargetAccessor;
			if (proxy == null)
			{
				return (this as IHtmlString)?.ToString();
			}
			var target = proxy.DynProxyGetTarget();
			if (target == null)
			{
				return (this as IHtmlString)?.ToString();
			}
			return (target as IHtmlString)?.ToString();
		}

		public override string ToString()
		{
			var proxy = this as IProxyTargetAccessor;
			if (proxy == null)
			{
				return base.ToString();
			}
			var target = proxy.DynProxyGetTarget();
			if (target == null)
			{
				return base.ToString();
			}
			return target.ToString();
		}

		public override bool Equals(object obj)
		{
			var proxy = this as IProxyTargetAccessor;
			if (proxy == null)
			{
				return base.Equals(obj);
			}
			var target = proxy.DynProxyGetTarget();
			if (target == null)
			{
				return base.Equals(obj);
			}
			return target.Equals(obj);
		}

		public override int GetHashCode()
		{
			var proxy = this as IProxyTargetAccessor;
			if (proxy == null)
			{
				return base.GetHashCode();
			}
			var target = proxy.DynProxyGetTarget();
			if (target == null)
			{
				return base.GetHashCode();
			}
			return target.GetHashCode();
		}
	}

	public sealed class CodeFirstModelContext
    {
        private class ContextContainer
        {
            private static readonly string Key = Guid.NewGuid().ToString();

            private Dictionary<object, CodeFirstModelContext> _dictionary;
            private CodeFirstModelContext _context;

            internal static ContextContainer Current
            {
                get
                {
                    if (HttpContext.Current == null || HttpContext.Current.Items == null)
                    {
                        return null;
                    }
                    if (!HttpContext.Current.Items.Contains(Key))
                    {
                        HttpContext.Current.Items.Add(Key, new ContextContainer());
                    }
                    return (ContextContainer)HttpContext.Current.Items[Key];
                }
            }

            internal Dictionary<object, CodeFirstModelContext> Dictionary
            {
                get
                {
                    if (_dictionary == null)
                    {
                        _dictionary = new Dictionary<object, CodeFirstModelContext>();
                    }
                    return _dictionary;
                }
            }
        }

        #region Context Instance
        public ContentTypeRegistration ContentType { get; internal set; }
        public ContentTypeCompositionRegistration CurrentComposition { get; internal set; }
        public TabRegistration CurrentTab { get; internal set; }
        public PropertyRegistration CurrentProperty { get; internal set; }
        public DataTypeRegistration CurrentDataType
        {
            get
            {
                return CurrentProperty == null ? null : CurrentProperty.DataType;
            }
        }
        public IEnumerable<PreValue> CurrentPreValues
        {
            get
            {
                return CurrentDataType == null ? null : CodeFirstManager.Current.Modules.PreValueCacheModule.Get(CurrentDataType);
            }
        }
        public CodeFirstModelContext ParentContext { get; internal set; }

        private CodeFirstModelContext Clone()
        {
            var result = new CodeFirstModelContext();
            result.ContentType = ContentType;
            result.CurrentTab = CurrentTab;
            result.CurrentProperty = CurrentProperty;
            result.CurrentComposition = CurrentComposition;
            result.ParentContext = ParentContext;
            return result;
        }
        #endregion

        #region Context Management
        private static Stack<CodeFirstModelContext> _stack = new Stack<CodeFirstModelContext>();
        private static CodeFirstModelContext _currentFrame;
        private static ProxyGenerator _generator = new ProxyGenerator();
		private static ProxyGenerationOptions _opts = new ProxyGenerationOptions()
		{
			BaseTypeForInterfaceProxy = typeof(ProxyBase)
		};

        private static CodeFirstModelContext RegisterFrozenContext(object key, CodeFirstModelContext context)
        {
            if (ContextContainer.Current == null || ContextContainer.Current.Dictionary == null || context == null)
            {
                return null;
            }
            if (!ContextContainer.Current.Dictionary.ContainsKey(key))
            {
                ContextContainer.Current.Dictionary.Add(key, context);
            }
            _currentFrame = context;
            return ContextContainer.Current.Dictionary[key];
        }

        internal static T CreateContextualInstance<T>(CodeFirstRegistration registration, out Dictionary<PropertyInfo, CodeFirstLazyInitialiser> dict, CodeFirstModelContext parentContext = null, bool useProxy = true) where T : ContentTypeBase
        {
            if (CodeFirstManager.Current.Features.UseLazyLoadingProxies && useProxy)
            {
                dict = new Dictionary<PropertyInfo, CodeFirstLazyInitialiser>();
                T proxy = _generator.CreateClassProxy<T>(_opts, new CodeFirstProxyInterceptor(dict));
                MoveNextContext(proxy, registration, parentContext);
                return proxy;
            }
            else
            {
                dict = null;
                var instance = Activator.CreateInstance<T>();
                MoveNextContext(instance, registration, parentContext);
                return instance;
            }
        }

        internal static object CreateContextualInstance(Type type, CodeFirstRegistration registration, out Dictionary<PropertyInfo, CodeFirstLazyInitialiser> dict, CodeFirstModelContext parentContext = null, bool useProxy = true)
        {
            if (CodeFirstManager.Current.Features.UseLazyLoadingProxies && useProxy)
            {
                dict = new Dictionary<PropertyInfo, CodeFirstLazyInitialiser>();
                var proxy = _generator.CreateClassProxy(type, _opts, new CodeFirstProxyInterceptor(dict));
                MoveNextContext(proxy, registration, parentContext);
                return proxy;
            }
            else
            {
                dict = null;
                var instance = Activator.CreateInstance(type);
                MoveNextContext(instance, registration, parentContext);
                return instance;
            }
        }

        internal static CodeFirstModelContext GetCompositionParentContext(ContentTypeCompositionRegistration registration)
        {
            var newContext = new CodeFirstModelContext()
            {
                ContentType = _currentFrame.ContentType, //should never be null - compositions only exist on documents, which would have a context
                CurrentComposition = registration
            };
            //TODO needed?
            _currentFrame = newContext;
            return newContext;
        }


        internal static void MoveNextContext(object instance, CodeFirstRegistration registration, CodeFirstModelContext parentContext = null)
        {
            if (ContextContainer.Current == null || ContextContainer.Current.Dictionary == null)
            {
                return;
            }
            else if (!ContextContainer.Current.Dictionary.ContainsKey(instance))
            {
                if (registration is ContentTypeRegistration)
                {
                    var newContext = new CodeFirstModelContext()
                    {
                        ContentType = (registration as ContentTypeRegistration),
                        ParentContext = parentContext == null ? parentContext : _currentFrame
                    };
                    if (_currentFrame != null)
                    {
                        _stack.Push(_currentFrame);
                    }
                    _currentFrame = newContext;
                    ContextContainer.Current.Dictionary.Add(instance, _currentFrame);
                }
                else if (registration is TabRegistration)
                {
                    var newContext = _currentFrame.Clone();
                    newContext.CurrentTab = registration as TabRegistration;
                    _currentFrame = newContext;
                    ContextContainer.Current.Dictionary.Add(instance, _currentFrame);
                }
                else if (registration is PropertyRegistration)
                {
                    var newContext = _currentFrame.Clone();
                    newContext.CurrentProperty = registration as PropertyRegistration;
                    _currentFrame = newContext;
                    ContextContainer.Current.Dictionary.Add(instance, _currentFrame);
                }
                else
                {
                    throw new InvalidOperationException("Unknown context registration type");
                }
            }
        }

        internal static void ReinstateContext(object key)
        {
            if (ContextContainer.Current == null || ContextContainer.Current.Dictionary == null)
            {
                return;
            }
            var target = GetContext(key);
            if (_currentFrame != null)
            {
                _stack.Push(_currentFrame);
            }
            _currentFrame = target;
        }

        internal static void ResetContext()
        {
            if (ContextContainer.Current == null || ContextContainer.Current.Dictionary == null)
            {
                return;
            }

            if (_stack.Count > 0)
            {
                _currentFrame = _stack.Pop();
            }
            else
            {
                _currentFrame = null;
            }
        }

        public static CodeFirstModelContext GetContext(object key)
        {
            if (ContextContainer.Current == null || ContextContainer.Current.Dictionary == null || !ContextContainer.Current.Dictionary.ContainsKey(key))
            {
                return null;
            }
            return ContextContainer.Current.Dictionary[key];
        }
        #endregion
    }


}
