using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Felinesoft.UmbracoCodeFirst.Core
{
    public sealed class CodeFirstProxyInterceptor : IInterceptor
    {
        private Dictionary<PropertyInfo, CodeFirstLazyInitialiser> _initialisersByProperty;

        private Dictionary<string, CodeFirstLazyInitialiser> InitialisersByPropertyGetter
        {
            get
            {
                return _initialisersByProperty.ToDictionary(x => x.Key.GetGetMethod().Name, x => x.Value);
            }
        }
        private Dictionary<string, CodeFirstLazyInitialiser> InitialisersByPropertySetter
        {
            get
            {
                return _initialisersByProperty.ToDictionary(x => x.Key.GetSetMethod().Name, x => x.Value);
            }
        }

        public CodeFirstProxyInterceptor(Dictionary<PropertyInfo, CodeFirstLazyInitialiser> initialisersByProperty)
        {
            _initialisersByProperty = initialisersByProperty;
        }

        public void Intercept(IInvocation invocation)
        {
            if (InitialisersByPropertyGetter.ContainsKey(invocation.Method.Name))
            {
                InterceptGetter(invocation, InitialisersByPropertyGetter[invocation.Method.Name]);
            }
            else if (InitialisersByPropertySetter.ContainsKey(invocation.Method.Name))
            {
                InterceptSetter(invocation, InitialisersByPropertySetter[invocation.Method.Name]);
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

        /// <summary>
        /// http://blogs.msdn.com/b/kingces/archive/2005/08/17/452774.aspx
        /// </summary>
        private bool MemberInfoEquals(MemberInfo lhs, MemberInfo rhs)
        {
            if (lhs == rhs)
                return true;

            if (lhs.DeclaringType != rhs.DeclaringType)
                return false;

            // Methods on arrays do not have metadata tokens but their ReflectedType
            // always equals their DeclaringType
            if (lhs.DeclaringType != null && lhs.DeclaringType.IsArray)
                return false;

            if (lhs.MetadataToken != rhs.MetadataToken || lhs.Module != rhs.Module)
                return false;

            if (lhs is MethodInfo)
            {
                MethodInfo lhsMethod = lhs as MethodInfo;

                if (lhsMethod.IsGenericMethod)
                {
                    MethodInfo rhsMethod = rhs as MethodInfo;

                    Type[] lhsGenArgs = lhsMethod.GetGenericArguments();
                    Type[] rhsGenArgs = rhsMethod.GetGenericArguments();
                    for (int i = 0; i < rhsGenArgs.Length; i++)
                    {
                        if (lhsGenArgs[i] != rhsGenArgs[i])
                            return false;
                    }
                }
            }
            return true;
        }
    }
}