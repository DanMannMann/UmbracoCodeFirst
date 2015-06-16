using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using System.Reflection;
using System.ComponentModel;
using Umbraco.Web;
using Umbraco.Core;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using System.Text.RegularExpressions;
using System.Globalization;
using Felinesoft.InitialisableAttributes;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Content;
using System.Collections.Concurrent;
using Felinesoft.UmbracoCodeFirst.CodeFirst;

namespace Felinesoft.UmbracoCodeFirst.Extensions
{
    /// <summary>
    /// Extensions to retrieve <see cref="CodeFirstAttribute"/>s applied to types
    /// </summary>
    public static class AttributeHelperExtensions
    {
        /// <summary>
        /// Gets a <see cref="GetContentFactoryAttribute"/> if one is applied to the given type
        /// </summary>
        /// <param name="type">The type to inspect</param>
        /// <param name="initialise">True to initialise the attribute if it is initialisable</param>
        /// <returns>The attribute, or null if none is found</returns>
        public static ContentFactoryAttribute GetContentFactoryAttribute(this Type type, bool initialise = true)
        {
            if (initialise)
            {
                return type.GetInitialisedAttribute<ContentFactoryAttribute>();
            }
            else
            {
                return type.GetCustomAttribute<ContentFactoryAttribute>();
            }
        }

        /// <summary>
        /// Gets a <see cref="GetContentDependencyAttribute"/> if one is applied to the given type
        /// </summary>
        /// <param name="type">The type to inspect</param>
        /// <returns>The attribute, or null if none is found</returns>
        public static ContentSiblingDependencyAttribute GetContentDependencyAttribute(this Type type)
        {
            return type.GetInitialisedAttribute<ContentSiblingDependencyAttribute>();
        }

        /// <summary>
        /// Gets and initialises a code-first attribute applied to a type
        /// </summary>
        /// <typeparam name="T">The type of <see cref="CodeFirstAttribute"/> to get</typeparam>
        /// <param name="type">The type to get the attribute from</param>
        /// <param name="initialise">True to initialse the attribute if it is a IInitialisableAttribute</param>
        /// <returns></returns>
        public static T GetCodeFirstAttribute<T>(this Type type, bool initialise = true) where T : CodeFirstAttribute
        {
            return CodeFirstAttributeCache.Get<T>(type, initialise);
        }

        /// <summary>
        /// Gets an attribute of type T if one is applied to the given type
        /// </summary>
        /// <param name="type">The type to inspect</param>
        /// <param name="initialise">True to initialise the attribute if it is initialisable</param>
        /// <returns>The attribute, or null if none is found</returns>
        public static IEnumerable<T> GetCodeFirstAttributes<T>(this Type type, bool initialise = true) where T : MultipleCodeFirstAttribute
        {
            if (initialise)
            {
                return type.GetInitialisedAttributes<T>();
            }
            else
            {
                return type.GetCustomAttributes<T>();
            }
        }

        /// <summary>
        /// Gets an attribute of type T if one is applied to the given property
        /// </summary>
        /// <param name="info">The property to inspect</param>
        /// <param name="initialise">True to initialise the attribute if it is initialisable</param>
        /// <returns>The attribute, or null if none is found</returns>
        public static T GetCodeFirstAttribute<T>(this PropertyInfo info, bool initialise = true) where T : CodeFirstAttribute
        {
            if (initialise)
            {
                return info.GetInitialisedAttribute<T>();
            }
            else
            {
                return info.GetCustomAttribute<T>();
            }
        }

        /// <summary>
        /// Gets a collection of attributes of type T if any are applied to the given property
        /// </summary>
        /// <param name="info">The property to inspect</param>
        /// <param name="initialise">True to initialise the attribute if it is initialisable</param>
        /// <returns>The attributes, or an empty collection if none is found</returns>
        public static IEnumerable<T> GetCodeFirstAttributes<T>(this PropertyInfo info, bool initialise = true) where T : MultipleCodeFirstAttribute
        {
            if (initialise)
            {

                return info.GetInitialisedAttributes<T>();
            }
            else
            {
                return info.GetCustomAttributes<T>();
            }
        }
    }
}
