using Felinesoft.UmbracoCodeFirst.Core.Modules;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Umbraco.Core.Models;
using Castle.DynamicProxy;
using System.Reflection;

namespace Felinesoft.UmbracoCodeFirst.Core
{
    public sealed class CodeFirstModelContext
    {
        public sealed class CodeFirstProxyInterceptor : IInterceptor
        {
            private Lazy<Dictionary<int, CodeFirstLazyInitialiser>> _initialisersByPropertyGetter;
            private Lazy<Dictionary<int, CodeFirstLazyInitialiser>> _initialisersByPropertySetter;

            public CodeFirstProxyInterceptor(Dictionary<PropertyInfo, CodeFirstLazyInitialiser> initialisersByProperty)
            {
                _initialisersByPropertyGetter = new Lazy<Dictionary<int, CodeFirstLazyInitialiser>>(() => initialisersByProperty.ToDictionary(x => x.Key.GetGetMethod().MetadataToken, x => x.Value));
                _initialisersByPropertySetter = new Lazy<Dictionary<int, CodeFirstLazyInitialiser>>(() => initialisersByProperty.ToDictionary(x => x.Key.GetSetMethod().MetadataToken, x => x.Value));
            }

            public void Intercept(IInvocation invocation)
            {
                if (_initialisersByPropertyGetter.Value.ContainsKey(invocation.Method.MetadataToken))
                {
                    InterceptGetter(invocation, _initialisersByPropertyGetter.Value[invocation.Method.MetadataToken]);
                }
                else if (_initialisersByPropertySetter.Value.ContainsKey(invocation.Method.MetadataToken))
                {
                    InterceptSetter(invocation, _initialisersByPropertySetter.Value[invocation.Method.MetadataToken]);
                }
                invocation.Proceed();
            }

            private void InterceptSetter(IInvocation invocation, CodeFirstLazyInitialiser codeFirstLazyInitialiser)
            {
                if (!codeFirstLazyInitialiser.IsDone)
                {
                    codeFirstLazyInitialiser.Complete();
                }
            }

            private void InterceptGetter(IInvocation invocation, CodeFirstLazyInitialiser codeFirstLazyInitialiser)
            {
                if (!codeFirstLazyInitialiser.IsDone)
                {
                    CodeFirstModelContext.ReinstateContext(invocation.InvocationTarget);
                    codeFirstLazyInitialiser.Execute();
                    CodeFirstModelContext.ResetContext();
                }
            }
        }

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

        internal static T CreateContextualInstance<T>(CodeFirstRegistration registration, out Dictionary<PropertyInfo,CodeFirstLazyInitialiser> dict, CodeFirstModelContext parentContext = null) where T : ContentTypeBase
        {
            dict = new Dictionary<PropertyInfo,CodeFirstLazyInitialiser>();
            T proxy = _generator.CreateClassProxy<T>(new CodeFirstProxyInterceptor(dict));
            NextContext(proxy, registration, parentContext);
            return proxy;
        }

        internal static object CreateContextualInstance(Type type, CodeFirstRegistration registration, out Dictionary<PropertyInfo, CodeFirstLazyInitialiser> dict, CodeFirstModelContext parentContext = null)
        {
            dict = new Dictionary<PropertyInfo, CodeFirstLazyInitialiser>();
            var proxy = _generator.CreateClassProxy(type, new CodeFirstProxyInterceptor(dict));
            NextContext(proxy, registration, parentContext);
            return proxy;
        }

        internal static void NextContext(object instance, CodeFirstRegistration registration, CodeFirstModelContext parentContext = null)
        {
            if (ContextContainer.Current == null || ContextContainer.Current.Dictionary == null)
            {
                return;
            }

            //For compositions we just make sure the current frame is in an appropriate state to be cloned as the parent context of the composition
            if (registration is ContentTypeCompositionRegistration)
            {
                var newContext = new CodeFirstModelContext()
                {
                    ContentType = _currentFrame.ContentType, //should never be null - compositions only exist on documents, which would have a context
                    CurrentComposition = (registration as ContentTypeCompositionRegistration)
                };
                _currentFrame = newContext;
                return;
            }

            if (!ContextContainer.Current.Dictionary.ContainsKey(instance))
            {
                if (registration is ContentTypeRegistration)
                {
                    var newContext = new CodeFirstModelContext()
                    {
                        ContentType = (registration as ContentTypeRegistration),
                        ParentContext = _currentFrame
                    };
                    if (_currentFrame != null)
                    {
                        _stack.Push(_currentFrame);
                    }
                    RegisterFrozenContext(instance, newContext);
                }
                else if (registration is TabRegistration)
                {
                    var newContext = _currentFrame.Clone();
                    newContext.CurrentTab = registration as TabRegistration;
                    RegisterFrozenContext(instance, newContext);
                }
                else if (registration is PropertyRegistration)
                {
                    var newContext = _currentFrame.Clone();
                    newContext.CurrentProperty = registration as PropertyRegistration;
                    RegisterFrozenContext(instance, newContext);
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
