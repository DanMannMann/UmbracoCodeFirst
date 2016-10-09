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
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Felinesoft.UmbracoCodeFirst.Core.Modules;
using System.Linq.Expressions;
using System.Web.WebPages;
using Felinesoft.UmbracoCodeFirst.Dictionaries;

namespace Felinesoft.UmbracoCodeFirst.Extensions
{
    /// <summary>
    /// Convenience extensions for retrieving and working with metadata
    /// of code-first classes
    /// </summary>
    public static class CodeFirstExtensions
    {
        #region Document Type Alias helpers

        /// <summary>
        /// Accesses the <see cref="DocumentTypeAttribute"/> applied to a class to find
        /// the document type alias for that class
        /// </summary>
        /// <param name="input">The document type instance to get the alias for</param>
        /// <returns>the document type alias</returns>
        /// <exception cref="CodeFirstException">Thrown if the specified type does not have a <see cref="DocumentTypeAttribute"/> attribute.</exception>
        public static string GetDocumentTypeAlias(this DocumentTypeBase input)
        {
            try
            {
                return input.GetType().GetCodeFirstAttribute<DocumentTypeAttribute>().Alias;
            }
            catch (Exception e)
            {
                throw new CodeFirstException(input.GetType().Name, e);
            }
        }

        /// <summary>
        /// Accesses the <see cref="DocumentTypeAttribute"/> applied to a class to find
        /// the document type alias for that class
        /// </summary>
        /// <param name="input">The document type to get the alias for</param>
        /// <returns>the document type alias</returns>
        /// <exception cref="CodeFirstException">Thrown if the specified type does not have a <see cref="DocumentTypeAttribute"/> attribute.</exception>
        internal static string GetDocumentTypeAlias(this Type input)
        {
            try
            {
                return input.GetCodeFirstAttribute<DocumentTypeAttribute>().Alias;
            }
            catch (Exception e)
            {
                throw new Exception(input.GetType().Name, e);
            }
        }
        #endregion

		#region Media Type Alias Helpers
        /// <summary>
        /// Accesses the <see cref="MediaTypeAttribute"/> applied to a class to find
        /// the media type alias for that class
        /// </summary>
        /// <param name="input">The media type instance to get the alias for</param>
        /// <returns>the media type alias</returns>
        /// <exception cref="CodeFirstException">Thrown if the specified type does not have a <see cref="MediaTypeAttribute"/> attribute.</exception>
        public static string GetMediaTypeAlias(this MediaTypeBase input)
        {
            try
            {
                return input.GetType().GetCodeFirstAttribute<MediaTypeAttribute>().Alias;
            }
            catch (Exception e)
            {
                throw new CodeFirstException(input.GetType().Name, e);
            }
        }

        /// <summary>
        /// Accesses the <see cref="MediaTypeAttribute"/> applied to a class to find
        /// the media type alias for that class
        /// </summary>
        /// <param name="input">The media type to get the alias for</param>
        /// <returns>the media type alias</returns>
        /// <exception cref="CodeFirstException">Thrown if the specified type does not have a <see cref="MediaTypeAttribute"/> attribute.</exception>
        internal static string GetMediaTypeAlias(this Type input)
        {
            try
            {
                return input.GetCodeFirstAttribute<MediaTypeAttribute>().Alias;
            }
            catch (Exception e)
            {
                throw new Exception(input.GetType().Name, e);
            }
        }
        #endregion

        #region Initialiser Sorting Extensions
        internal static IEnumerable<Type> SortByContentTypeInheritance(this IEnumerable<Type> input, Type baseType = null)
        {
            var generations = new List<List<Type>>();
            return SortByContentTypeInheritance(input.ToList(), generations, baseType);
        }

        internal static List<List<Type>> GetGenerationsByContentTypeInheritance(this IEnumerable<Type> input, Type baseType = null)
        {
            var generations = new List<List<Type>>();
            SortByContentTypeInheritance(input.ToList(), generations, baseType);
            return generations;
        }

        private static List<Type> SortByContentTypeInheritance(this List<Type> input, List<List<Type>> generations, Type baseType = null)
        {
            var result = new List<Type>();

            //Remove immediate descendents of baseType from the list, keeping them in a second list
            var roots = input.PopChildren(baseType);
            generations.Add(roots);

            foreach (var root in roots)
            {
                result.Add(root);
                var descendants = input.PopDescendants(root);
                if (descendants.Count == 0)
                {
                    continue;
                }
                descendants = SortByContentTypeInheritance(descendants, generations, root);
                result.AddRange(descendants);
            }

            if (input.Count > 0)
            {
                throw new CodeFirstException("Orphaned types found. The following types inherit a type with a [ContentType] attribute but that type is not in the input list." + Environment.NewLine +  
                                                string.Join(Environment.NewLine, input.Select(x => x.Name + " - inherits " + x.BaseType.Name + " which is not in the input list")));
            }

            return result;
        }

        private static List<Type> PopChildren(this List<Type> input, Type baseType)
        {
            List<Type> result;
            if (baseType == null)
            {
                result = input.Where(x => x.BaseType != null && x.BaseType.Inherits<CodeFirstContentBase>() && x.BaseType.GetCustomAttribute<ContentTypeAttribute>(true) == null).ToList();
            }
            else
            {
                result = input.Where(x => x.BaseType == baseType).ToList();
            }
            
            result.ForEach(x => input.Remove(x));
            return result;
        }

        private static List<Type> PopDescendants(this List<Type> input, Type baseType)
        {
            var result = input.Where(x => x.Inherits(baseType)).ToList();
            result.ForEach(x => input.Remove(x));
            return result;
        }
        #endregion

		public static Tdict Dictionary<Tdict>(this WebPageExecutingBase page, CultureInfo culture = null) where Tdict : DictionaryBase
		{
			return CodeFirstManager.Current?.Modules?.DictionaryModule?.GetDictionary<Tdict>(culture);
		}

		public static Tdict Dictionary<Tdict>(this Controller page, CultureInfo culture = null) where Tdict : DictionaryBase
		{
			return CodeFirstManager.Current?.Modules?.DictionaryModule?.GetDictionary<Tdict>(culture);
		}

		internal static bool IsParentTabFor(this PropertyInfo propertyInfo, TabRegistration tab)
        {
            var attr = propertyInfo.GetCodeFirstAttribute<ContentTabAttribute>();
            //if parent property (inherited or otherwise) has [ContentTab] attr
            if (attr != null)
            {
                //and the same tab name & CLR type (inherited or otherwise) as the current tab
                if (attr.Name == tab.Name && tab.ClrType.Inherits(propertyInfo.PropertyType))
                {
                    //Match
                    return true;
                }
            }

            //No match
            return false;
        }

        public static bool IsUmbracoDataType(this Type type, out Type underlyingValueType, out DataTypeDatabaseType? storageType)
        {
            var result = type.GetInterfaces().FirstOrDefault(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(IUmbracoDataType<>));
            if(result != null)
            {
                GetStorageType(type, out underlyingValueType, out storageType);
                if (underlyingValueType == null)
                {
                    underlyingValueType = result.GetGenericArguments().Single();
                }
            }
            else
            {
                underlyingValueType = null;
                storageType = null;
            }
            return result != null;
        }

        private static void GetStorageType(Type type, out Type underlyingValueType, out DataTypeDatabaseType? storageType)
        {
            if (type.GetInterfaces().Any(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(IUmbracoNtextDataType<>)))
            {
                storageType = DataTypeDatabaseType.Ntext;
                underlyingValueType = type.GetInterfaces().First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IUmbracoNtextDataType<>)).GetGenericArguments().Single();
            }
            else if (type.GetInterfaces().Any(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(IUmbracoNvarcharDataType<>)))
            {
                storageType = DataTypeDatabaseType.Nvarchar;
                underlyingValueType = type.GetInterfaces().First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IUmbracoNvarcharDataType<>)).GetGenericArguments().Single();
            }
            else if (type.GetInterfaces().Any(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(IUmbracoDateTimeDataType<>)))
            {
                storageType = DataTypeDatabaseType.Date;
                underlyingValueType = type.GetInterfaces().First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IUmbracoDateTimeDataType<>)).GetGenericArguments().Single();
            }
            else if (type.GetInterfaces().Any(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(IUmbracoIntegerDataType<>)))
            {
                storageType = DataTypeDatabaseType.Integer;
                underlyingValueType = type.GetInterfaces().First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IUmbracoIntegerDataType<>)).GetGenericArguments().Single();
            }
            else
            {
                storageType = null;
                underlyingValueType = null;
            }
        }
    }
}
