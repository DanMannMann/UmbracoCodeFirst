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
using Felinesoft.UmbracoCodeFirst.Core;

namespace Felinesoft.UmbracoCodeFirst.Extensions
{
    /// <summary>
    /// Extensions to retrieve <see cref="CodeFirstAttribute"/>s applied to types
    /// </summary>
    public static class AttributeHelperExtensions
    {
        public static IEnumerable<T> GetAttributesFromContextTree<T>(this object key) where T : CodeFirstContextualAttribute
        {
            var attributes = new List<T>();

            if (!CodeFirstManager.Current.Features.UseContextualAttributes)
            {
                return attributes;
            }

            var context = CodeFirstModelContext.GetContext(key);
            GetContextAttributes(context, attributes);

            var current = context.ParentContext;
            while (current != null)
            {
                GetContextAttributes(current, attributes);
                current = current.ParentContext;
            }
            return attributes;
        }

        private static void GetContextAttributes<T>(CodeFirstModelContext context, List<T> attributes) where T : CodeFirstContextualAttribute
        {
            if (!CodeFirstManager.Current.Features.UseContextualAttributes)
            {
                return;
            }

            if (context.CurrentProperty != null)
            {
                //Add classes from property
                attributes.AddRange(context.CurrentProperty.Metadata.GetCodeFirstAttributesWithInheritance<T>());
            }

            if (context.ContentType != null)
            {
                //Add classes from current content type
                attributes.AddRange(context.ContentType.ClrType.GetCodeFirstAttributesWithInheritance<T>());
            }

            if (context.CurrentDataType != null)
            {
                //Add classes from the current data type
                attributes.AddRange(context.CurrentDataType.ClrType.GetCodeFirstAttributesWithInheritance<T>());
            }
        }

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
        /// Gets attributes of type T if any are applied to the given type
        /// </summary>
        /// <param name="type">The type to inspect</param>
        /// <param name="initialise">True to initialise the attribute if it is initialisable</param>
        /// <returns>The attribute, or null if none is found</returns>
        internal static IEnumerable<T> GetCodeFirstAttributesWithInheritance<T>(this Type type, bool initialise = true) where T : MultipleCodeFirstAttribute
        {
            return CodeFirstAttributeCache.GetManyWithInheritance<T>(type, initialise);
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

        /// <summary>
        /// Gets a collection of attributes of type T if any are applied to the given property
        /// </summary>
        /// <param name="info">The property to inspect</param>
        /// <param name="initialise">True to initialise the attribute if it is initialisable</param>
        /// <returns>The attributes, or an empty collection if none is found</returns>
        internal static IEnumerable<T> GetCodeFirstAttributesWithInheritance<T>(this PropertyInfo info, bool initialise = true) where T : MultipleCodeFirstAttribute
        {
            return CodeFirstAttributeCache.GetManyWithInheritance<T>(info, initialise);
        }
    }
}
