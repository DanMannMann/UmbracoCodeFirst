using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;

namespace Felinesoft.UmbracoCodeFirst
{
    internal static class DocumentTypeRegister
    {
        private static MethodInfo _convertToModel;
        private static MethodInfo _currentTemplate;

        /// <summary>
        /// A cache of registered document types, keyed by alias, to assist in runtime resolution of content types for type-safe rendering
        /// </summary>
        internal static ConcurrentDictionary<string, Type> DocumentTypeCache = new ConcurrentDictionary<string, Type>();

        /// <summary>
        /// A cache of ConvertToModel{T} methods for registered document types, keyed by CLR type, to assist in runtime resolution of content for type-safe rendering
        /// </summary>
        internal static ConcurrentDictionary<Type, MethodInfo> RuntimeConvertToModelMethods = new ConcurrentDictionary<Type, MethodInfo>();

        /// <summary>
        /// A cache of RenderMvcController.CurrentTemplate{T} methods for registered document types, keyed by CLR type, to assist in runtime resolution of content for type-safe rendering
        /// </summary>
        internal static ConcurrentDictionary<Type, MethodInfo> RuntimeCurrentTemplateMethods = new ConcurrentDictionary<Type, MethodInfo>();

        internal static MethodInfo EnsureRegisterConvertMethod(Type docType)
        {
            if (!RuntimeConvertToModelMethods.ContainsKey(docType))
            {
                if (_convertToModel == null)
                {
                    _convertToModel = typeof(Felinesoft.UmbracoCodeFirst.Extensions.ContentExtensions).GetMethod("ConvertToModel", new Type[] { typeof(IPublishedContent) });
                }
                var convertToModel = _convertToModel.MakeGenericMethod(docType);
                RuntimeConvertToModelMethods.TryAdd(docType, convertToModel);
            }
            return RuntimeConvertToModelMethods[docType];
        }

        internal static MethodInfo EnsureRegisterCurrentTemplateMethod(Type docType)
        {
            if (!DocumentTypeRegister.RuntimeCurrentTemplateMethods.ContainsKey(docType))
            {
                if (_currentTemplate == null)
                {
                    _currentTemplate = typeof(RenderMvcController).GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Single(x => x.Name == "CurrentTemplate");
                }
                var currentTemplate = _currentTemplate.MakeGenericMethod(docType);
                RuntimeCurrentTemplateMethods.TryAdd(docType, currentTemplate);
            }
            return RuntimeCurrentTemplateMethods[docType];
        }
    }
}
