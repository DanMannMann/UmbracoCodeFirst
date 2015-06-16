using Felinesoft.UmbracoCodeFirst.Attributes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felinesoft.InitialisableAttributes;
using System.Reflection;

namespace Felinesoft.UmbracoCodeFirst.CodeFirst
{
    /// <summary>
    /// Caches attributes when they are first loaded then dereferences them by decorated type and attribute type so that they must
    /// only be discovered by reflection once per app lifetime.
    /// </summary>
    internal static class CodeFirstAttributeCache
    {
        private static ConcurrentDictionary<Type, ConcurrentDictionary<Type, CodeFirstAttribute>> _cache = new ConcurrentDictionary<Type, ConcurrentDictionary<Type, CodeFirstAttribute>>();
        private static ConcurrentDictionary<Type, ConcurrentDictionary<Type, IEnumerable<MultipleCodeFirstAttribute>>> _multipleCache = new ConcurrentDictionary<Type, ConcurrentDictionary<Type, IEnumerable<MultipleCodeFirstAttribute>>>();

        /// <summary>
        /// Gets an attribute of type T which is applied to the input type
        /// </summary>
        /// <typeparam name="T">The type of attribute to look for</typeparam>
        /// <param name="type">The type to look for attributes on</param>
        /// <param name="initialise">True to use GetInitialisedAttribute when first loading the attribute, false to use GetCustomAttribute</param>
        /// <returns>an attribute of type T which is applied to the input type, or null if none is found</returns>
        internal static T Get<T>(Type type, bool initialise = true) where T : CodeFirstAttribute
        {
            ConcurrentDictionary<Type, CodeFirstAttribute> innerCache;
            Type attributeType = typeof(T);
            CodeFirstAttribute attr = null;
            _cache.TryGetValue(type, out innerCache);
            if (innerCache == null)
            {
                innerCache = new ConcurrentDictionary<Type, CodeFirstAttribute>();
                innerCache = _cache.GetOrAdd(type, innerCache);
            }
            innerCache.TryGetValue(attributeType, out attr);
            if (attr == null)
            {
                if (initialise)
                {
                    attr = type.GetInitialisedAttribute<T>();
                }
                else
                {
                    attr = type.GetCustomAttribute<T>();
                }
            }
            return (T)innerCache.GetOrAdd(attributeType, attr);
        }

        /// <summary>
        /// Gets a collection of attributes of type T which are applied to the input type
        /// </summary>
        /// <typeparam name="T">The type of attribute to look for</typeparam>
        /// <param name="type">The type to look for attributes on</param>
        /// <param name="initialise">True to use GetInitialisedAttribute when first loading the attribute, false to use GetCustomAttribute</param>
        /// <returns>a collection of attributes of type T which are applied to the input type, or an empty collection if none is found</returns>
        internal static IEnumerable<MultipleCodeFirstAttribute> GetMany<T>(Type type, bool initialise = true) where T : MultipleCodeFirstAttribute
        {
            ConcurrentDictionary<Type, IEnumerable<MultipleCodeFirstAttribute>> innerCache;
            Type attributeType = typeof(T);
            IEnumerable<MultipleCodeFirstAttribute> attrs = null;
            _multipleCache.TryGetValue(attributeType, out innerCache);
            if (innerCache == null)
            {
                innerCache = new ConcurrentDictionary<Type, IEnumerable<MultipleCodeFirstAttribute>>();
                innerCache = _multipleCache.GetOrAdd(type, innerCache);
            }
            innerCache.TryGetValue(attributeType, out attrs);
            if (attrs == null)
            {
                if (initialise)
                {
                    attrs = type.GetInitialisedAttributes<T>();
                }
                else
                {
                    attrs = type.GetCustomAttributes<T>();
                }
            }
            return innerCache.GetOrAdd(attributeType, attrs);
        }
    }

}
