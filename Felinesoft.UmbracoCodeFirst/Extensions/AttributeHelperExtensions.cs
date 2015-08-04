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
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using System.Text.RegularExpressions;
using System.Globalization;

using Felinesoft.UmbracoCodeFirst.Attributes;
using System.Collections.Concurrent;
using Felinesoft.UmbracoCodeFirst.CodeFirst;
using Felinesoft.UmbracoCodeFirst.Exceptions;

namespace Felinesoft.UmbracoCodeFirst.Extensions
{
    /// <summary>
    /// Extensions to retrieve <see cref="CodeFirstAttribute"/>s applied to types
    /// </summary>
    public static class AttributeHelperExtensions
    {
        /// <summary>
        /// Gets and initialises a code-first attribute applied to a member. The member must be a type or property.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="CodeFirstAttribute"/> to get</typeparam>
        /// <param name="type">The type to get the attribute from</param>
        /// <param name="initialise">True to initialse the attribute if it is a IInitialisableAttribute</param>
        /// <returns></returns>
        public static T GetCodeFirstAttribute<T>(this MemberInfo member, bool initialise = true) where T : CodeFirstAttribute
        {
            if (member is Type)
            {
                return CodeFirstAttributeCache.Get<T>(member as Type, initialise);
            }
            else if (member is PropertyInfo)
            {
                return CodeFirstAttributeCache.Get<T>(member as PropertyInfo, initialise);
            }
            else
            {
                throw new AttributeInitialisationException("Code-first attributes only support Type and Property member types, and cannot be applied to or retrieved from " + member.MemberType.ToString());
            }
        }

        /// <summary>
        /// Gets attributes of type T if any are applied to the given member. The member must be a type or property.
        /// </summary>
        /// <param name="type">The type to inspect</param>
        /// <param name="initialise">True to initialise the attribute if it is initialisable</param>
        /// <returns>The attribute, or null if none is found</returns>
        public static IEnumerable<T> GetCodeFirstAttributes<T>(this MemberInfo member, bool initialise = true) where T : MultipleCodeFirstAttribute
        {
            if (member is Type)
            {
                return CodeFirstAttributeCache.GetMany<T>(member as Type, initialise);
            }
            else if (member is PropertyInfo)
            {
                return CodeFirstAttributeCache.GetMany<T>(member as PropertyInfo, initialise);
            }
            else
            {
                throw new AttributeInitialisationException("Code-first attributes only support Type and Property member types, and cannot be applied to or retrieved from " + member.MemberType.ToString());
            }
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
        /// Gets attributes of type T if any are applied to the given type
        /// </summary>
        /// <param name="type">The type to inspect</param>
        /// <param name="initialise">True to initialise the attribute if it is initialisable</param>
        /// <returns>The attribute, or null if none is found</returns>
        public static IEnumerable<T> GetCodeFirstAttributes<T>(this Type type, bool initialise = true) where T : MultipleCodeFirstAttribute
        {
            return CodeFirstAttributeCache.GetMany<T>(type, initialise);
        }

        /// <summary>
        /// Gets an attribute of type T if one is applied to the given property
        /// </summary>
        /// <param name="info">The property to inspect</param>
        /// <param name="initialise">True to initialise the attribute if it is initialisable</param>
        /// <returns>The attribute, or null if none is found</returns>
        public static T GetCodeFirstAttribute<T>(this PropertyInfo info, bool initialise = true) where T : CodeFirstAttribute
        {
            return CodeFirstAttributeCache.Get<T>(info, initialise);
        }

        /// <summary>
        /// Gets a collection of attributes of type T if any are applied to the given property
        /// </summary>
        /// <param name="info">The property to inspect</param>
        /// <param name="initialise">True to initialise the attribute if it is initialisable</param>
        /// <returns>The attributes, or an empty collection if none is found</returns>
        public static IEnumerable<T> GetCodeFirstAttributes<T>(this PropertyInfo info, bool initialise = true) where T : MultipleCodeFirstAttribute
        {
            return CodeFirstAttributeCache.GetMany<T>(info, initialise);
        }
    }
}
