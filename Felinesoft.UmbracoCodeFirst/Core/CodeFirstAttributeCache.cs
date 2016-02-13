using Felinesoft.UmbracoCodeFirst.Attributes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felinesoft.UmbracoCodeFirst.Extensions;
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
        private static ConcurrentDictionary<Type, ConcurrentDictionary<Type, object>> _multipleCache = new ConcurrentDictionary<Type, ConcurrentDictionary<Type, object>>();

        private static ConcurrentDictionary<PropertyInfo, ConcurrentDictionary<Type, CodeFirstAttribute>> _propertyCache = new ConcurrentDictionary<PropertyInfo, ConcurrentDictionary<Type, CodeFirstAttribute>>();
        private static ConcurrentDictionary<PropertyInfo, ConcurrentDictionary<Type, object>> _multiplePropertyCache = new ConcurrentDictionary<PropertyInfo, ConcurrentDictionary<Type, object>>();

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

			if (initialise && attr is IInitialisableAttribute && !(attr as IInitialisableAttribute).Initialised)
			{
				(attr as IInitialisableAttribute).Initialise(type);
			}

			if (!initialise && attr is IInitialisableAttribute)
			{
				//Do not add uninitialised IInitialisableAttributes to the cache. They should be 
				return (T)attr;
			}
			else
			{
				return (T)innerCache.GetOrAdd(attributeType, attr);
			}
        }

        /// <summary>
        /// Gets a collection of attributes of type T which are applied to the input type
        /// </summary>
        /// <typeparam name="T">The type of attribute to look for</typeparam>
        /// <param name="type">The type to look for attributes on</param>
        /// <param name="initialise">True to use GetInitialisedAttribute when first loading the attribute, false to use GetCustomAttribute</param>
        /// <returns>a collection of attributes of type T which are applied to the input type, or an empty collection if none is found</returns>
        internal static IEnumerable<T> GetMany<T>(Type type, bool initialise = true) where T : MultipleCodeFirstAttribute
        {
            ConcurrentDictionary<Type, object> innerCache;

            Type attributeType = typeof(T);
            object temp;
            IEnumerable<T> attrs = null;
            _multipleCache.TryGetValue(type, out innerCache);
            if (innerCache == null)
            {
                innerCache = new ConcurrentDictionary<Type, object>();
                innerCache = _multipleCache.GetOrAdd(type, innerCache);
            }
            innerCache.TryGetValue(attributeType, out temp);
            if (temp == null)
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
            else
            {
                attrs = (IEnumerable<T>)temp;
            }

            foreach (var attr in attrs)
            {
                if (initialise && attr is IInitialisableAttribute && !(attr as IInitialisableAttribute).Initialised)
                {
                    (attr as IInitialisableAttribute).Initialise(type);
                }
            }

            return (IEnumerable<T>)innerCache.GetOrAdd(attributeType, attrs);
        }

        /// <summary>
        /// Gets a collection of attributes of type T which are applied to the input type
        /// </summary>
        /// <typeparam name="T">The type of attribute to look for</typeparam>
        /// <param name="type">The type to look for attributes on</param>
        /// <param name="initialise">True to use GetInitialisedAttribute when first loading the attribute, false to use GetCustomAttribute</param>
        /// <returns>a collection of attributes of type T which are applied to the input type, or an empty collection if none is found</returns>
        internal static IEnumerable<T> GetManyWithInheritance<T>(Type type, bool initialise = true) where T : MultipleCodeFirstAttribute
        {
            ConcurrentDictionary<Type, object> innerCache;

            Type attributeType = typeof(T);
            object temp;
            IEnumerable<T> attrs = null;
            _multipleCache.TryGetValue(type, out innerCache);
            if (innerCache == null)
            {
                innerCache = new ConcurrentDictionary<Type, object>();
                innerCache = _multipleCache.GetOrAdd(type, innerCache);
            }
            innerCache.TryGetValue(attributeType, out temp);
            if (temp == null)
            {
                if (initialise)
                {
                    attrs = type.GetInitialisedAttributesWithInheritance<T>();
                }
                else
                {
                    attrs = type.GetCustomAttributes<T>(true);
                }
            }
            else
            {
                attrs = (IEnumerable<T>)temp;
            }

            foreach (var attr in attrs)
            {
                if (initialise && attr is IInitialisableAttribute && !(attr as IInitialisableAttribute).Initialised)
                {
                    (attr as IInitialisableAttribute).Initialise(type);
                }
            }

            return (IEnumerable<T>)innerCache.GetOrAdd(attributeType, attrs);
        }

        /// <summary>
        /// Gets an attribute of type T which is applied to the input type
        /// </summary>
        /// <typeparam name="T">The type of attribute to look for</typeparam>
        /// <param name="property">The type to look for attributes on</param>
        /// <param name="initialise">True to use GetInitialisedAttribute when first loading the attribute, false to use GetCustomAttribute</param>
        /// <returns>an attribute of type T which is applied to the input type, or null if none is found</returns>
        internal static T Get<T>(PropertyInfo property, bool initialise = true) where T : CodeFirstAttribute
        {
            ConcurrentDictionary<Type, CodeFirstAttribute> innerCache;
            Type attributeType = typeof(T);
            CodeFirstAttribute attr = null;
            _propertyCache.TryGetValue(property, out innerCache);
            if (innerCache == null)
            {
                innerCache = new ConcurrentDictionary<Type, CodeFirstAttribute>();
                innerCache = _propertyCache.GetOrAdd(property, innerCache);
            }
            innerCache.TryGetValue(attributeType, out attr);
            if (attr == null)
            {
                if (initialise)
                {
                    attr = property.GetInitialisedAttribute<T>();
                }
                else
                {
                    attr = property.GetCustomAttribute<T>();
                }
            }

            if (initialise && attr is IInitialisablePropertyAttribute && !(attr as IInitialisablePropertyAttribute).Initialised)
            {
                (attr as IInitialisablePropertyAttribute).Initialise(property);
            }

            return (T)innerCache.GetOrAdd(attributeType, attr);
        }

        /// <summary>
        /// Gets a collection of attributes of type T which are applied to the input type
        /// </summary>
        /// <typeparam name="T">The type of attribute to look for</typeparam>
        /// <param name="property">The type to look for attributes on</param>
        /// <param name="initialise">True to use GetInitialisedAttribute when first loading the attribute, false to use GetCustomAttribute</param>
        /// <returns>a collection of attributes of type T which are applied to the input type, or an empty collection if none is found</returns>
        internal static IEnumerable<T> GetMany<T>(PropertyInfo property, bool initialise = true) where T : MultipleCodeFirstAttribute
        {
            ConcurrentDictionary<Type, object> innerCache;
            Type attributeType = typeof(T);
            IEnumerable<T> attrs = null;
            object temp;
            _multipleCache.TryGetValue(attributeType, out innerCache);
            if (innerCache == null)
            {
                innerCache = new ConcurrentDictionary<Type, object>();
                innerCache = _multiplePropertyCache.GetOrAdd(property, innerCache);
            }
            innerCache.TryGetValue(attributeType, out temp);
            if (temp == null)
            {
                if (initialise)
                {
                    attrs = property.GetInitialisedAttributes<T>();
                }
                else
                {
                    attrs = property.GetCustomAttributes<T>();
                }
            }
            else
            {
                attrs = (IEnumerable<T>)temp;
            }

            foreach (var attr in attrs)
            {
                if (initialise && attr is IInitialisablePropertyAttribute && !(attr as IInitialisablePropertyAttribute).Initialised)
                {
                    (attr as IInitialisablePropertyAttribute).Initialise(property);
                }
            }

            return (IEnumerable<T>)innerCache.GetOrAdd(attributeType, attrs);
        }

        /// <summary>
        /// Gets a collection of attributes of type T which are applied to the input type
        /// </summary>
        /// <typeparam name="T">The type of attribute to look for</typeparam>
        /// <param name="property">The type to look for attributes on</param>
        /// <param name="initialise">True to use GetInitialisedAttribute when first loading the attribute, false to use GetCustomAttribute</param>
        /// <returns>a collection of attributes of type T which are applied to the input type, or an empty collection if none is found</returns>
        internal static IEnumerable<T> GetManyWithInheritance<T>(PropertyInfo property, bool initialise = true) where T : MultipleCodeFirstAttribute
        {
            ConcurrentDictionary<Type, object> innerCache;
            Type attributeType = typeof(T);
            IEnumerable<T> attrs = null;
            object temp;
            _multipleCache.TryGetValue(attributeType, out innerCache);
            if (innerCache == null)
            {
                innerCache = new ConcurrentDictionary<Type, object>();
                innerCache = _multiplePropertyCache.GetOrAdd(property, innerCache);
            }
            innerCache.TryGetValue(attributeType, out temp);
            if (temp == null)
            {
                if (initialise)
                {
                    attrs = property.GetInitialisedAttributesWithInheritance<T>();
                }
                else
                {
                    attrs = property.GetCustomAttributes<T>(true);
                }
            }
            else
            {
                attrs = (IEnumerable<T>)temp;
            }

            foreach (var attr in attrs)
            {
                if (initialise && attr is IInitialisablePropertyAttribute && !(attr as IInitialisablePropertyAttribute).Initialised)
                {
                    (attr as IInitialisablePropertyAttribute).Initialise(property);
                }
            }

            return (IEnumerable<T>)innerCache.GetOrAdd(attributeType, attrs);
        }
    }

}
