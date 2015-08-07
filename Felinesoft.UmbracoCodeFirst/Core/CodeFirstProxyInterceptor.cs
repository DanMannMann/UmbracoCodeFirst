using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Felinesoft.UmbracoCodeFirst.Core
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
}