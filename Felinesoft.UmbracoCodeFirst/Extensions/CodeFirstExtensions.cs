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
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using System.Web.Mvc;
using System.Web.Mvc.Html;

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
                return input.GetType().GetCodeFirstAttribute<DocumentTypeAttribute>().DocumentTypeAlias;
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
                return input.GetCodeFirstAttribute<DocumentTypeAttribute>().DocumentTypeAlias;
            }
            catch (Exception e)
            {
                throw new Exception(input.GetType().Name, e);
            }
        }
        #endregion

        #region Initialiser Sorting Extensions
        internal static IEnumerable<Type> SortByContentInheritance(this IEnumerable<Type> input, Type baseType = null)
        {
            return SortByContentInheritance(input.ToList(), baseType);
        }

        internal static List<Type> SortByContentInheritance(this List<Type> input, Type baseType = null)
        {
            var result = new List<Type>();

            //Remove immediate descendents of baseType from the list, keeping them in a second list (NB doing this with baseType = null gets all content with null parents, i.e. root content)
            var roots = input.PopContentChildren(baseType);
            roots = roots.SortByContentDependency();

            foreach (var root in roots)
            {
                result.Add(root);
                var descendants = input.PopContentDescendants(root);
                if (descendants.Count == 0)
                {
                    continue;
                }
                descendants = SortByContentInheritance(descendants, root);
                result.AddRange(descendants);
            }

            return result;
        }

        internal static IEnumerable<Type> SortByDocTypeInheritance(this IEnumerable<Type> input, Type baseType = null)
        {
            return SortByDocTypeInheritance(input.ToList(), baseType);
        }

        internal static List<Type> SortByDocTypeInheritance(this List<Type> input, Type baseType = null)
        {
            //if (baseType == null)
            //{
            //    baseType = typeof(DocumentTypeBase);
            //}

            var result = new List<Type>();

            //Remove immediate descendents of baseType from the list, keeping them in a second list
            var roots = input.PopChildren(baseType);

            foreach (var root in roots)
            {
                result.Add(root);
                var descendants = input.PopDescendants(root);
                if (descendants.Count == 0)
                {
                    continue;
                }
                descendants = SortByDocTypeInheritance(descendants, root);
                result.AddRange(descendants);
            }

            return result;
        }

        private static List<Type> PopChildren(this List<Type> input, Type baseType)
        {
            List<Type> result;
            if (baseType == null)
            {
                result = input.Where(x => CodeFirstManager.Current.DocumentTypeBaseRegistered(x.BaseType)).ToList();
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
            var result = input.Where(x => baseType.IsAncestorOf(x)).ToList();
            result.ForEach(x => input.Remove(x));
            return result;
        }

        private static List<Type> PopContentChildren(this List<Type> input, Type baseType)
        {
            var result = input.Where(x => x.IsContentChildOf(baseType)).ToList();
            result.ForEach(x => input.Remove(x));
            return result;
        }

        private static List<Type> PopContentDescendants(this List<Type> input, Type baseType)
        {
            var result = input.Where(x => baseType.IsContentAncestorOf(x)).ToList();
            result.ForEach(x => input.Remove(x));
            return result;
        }

        private static bool IsContentChildOf(this Type type, Type potentialParentType)
        {
            var attr = type.GetContentFactoryAttribute(false);
            if (attr == null)
            {
                throw new ArgumentException("The type " + type.Name + " does not have a ContentFactory attribute");
            }

            if (attr.ParentContentType == potentialParentType)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static List<Type> SortByContentDependency(this List<Type> input)
        {
            if (input.Count == 0)
            {
                //support sorting an empty list, cos it does no harm
                return input;
            }

            //Get types which specify a content dependency
            var dependents = input.Where(x => x.HasContentDependency()).ToList();

            //put non-dependents first
            var result = input.Except(dependents).ToList();

            if (result.Count == 0)
            {
                //There should be at least one sibling which doesn't depend on any others
                throw new ContentCircularDependencyException("Circular content dependency detected - at least one sibling must have no dependency", dependents);
            }

            //order the dependents
            int previousCount = dependents.Count;
            while (result.Count != input.Count)
            {
                foreach (var dependent in dependents)
                {
                    if (result.Contains(dependent.GetContentDependency()))
                    {
                        //depends on an already-sorted element, add it to the result
                        result.Add(dependent);
                    }
                }

                //prune any dependents which have been sorted on this pass
                dependents = dependents.Except(result).ToList();

                //check for circular dependency
                if (dependents.Count == previousCount)
                {
                    //The remaining types have a circular dependency if no types are sorted between two passes
                    throw new ContentCircularDependencyException("Circular content dependency detected", dependents);
                }

                //Record the count
                previousCount = dependents.Count;
            }

            return result;
        }

        private static bool IsContentAncestorOf(this Type target, Type potentialDescendent)
        {
            var parent = potentialDescendent;
            var attr = potentialDescendent.GetContentFactoryAttribute(false);
            while (parent != null)
            {
                attr = parent.GetContentFactoryAttribute(false);
                if (parent == target)
                {
                    //potentialDescendent is a descendent of target
                    return true;
                }
                parent = attr.ParentContentType;
            }
            return false;
        }

        private static bool HasContentDependency(this Type target)
        {
            var attr = target.GetContentDependencyAttribute();
            return attr != null;
        }

        private static Type GetContentDependency(this Type target)
        {
            var attr = target.GetContentDependencyAttribute();
            if (attr == null)
            {
                return null;
            }
            else
            {
                try
                {
                    if (attr.Dependency.GetContentFactoryAttribute(false).ParentContentType != target.GetContentFactoryAttribute(false).ParentContentType)
                    {
                        throw new ContentDependencyException(string.Format("The types {0} and {1} do not have the same parent type. Only a sibling can be specified as a content dependency.", attr.Dependency, target));
                    }
                }
                catch (NullReferenceException)
                {
                    throw new ContentDependencyException(string.Format("One or more of the types {0} and {1} has no [ContentFactory] attribute", attr.Dependency, target));
                }
                return attr.Dependency;
            }
        }

        private static bool IsAncestorOf(this Type target, Type potentialDescendent)
        {
            bool result = false;
            Type baseType = potentialDescendent.BaseType;
            while (baseType != null)
            {
                if (baseType == target)
                {
                    result = true;
                    break;
                }
                else
                {
                    baseType = baseType.BaseType;
                }
            }
            return result;
        }
        #endregion

        /// <summary>
        /// Returns true if the specified type implements <see cref="IUmbracoDataType{T}"/>
        /// </summary>
        /// <param name="type">The type to inspect</param>
        /// <param name="dbType">Returns the Umbraco database type</param>
        /// <returns>True if the specified type implements <see cref="IUmbracoDataType{T}"/></returns>
        public static bool IsUmbracoDataType(this Type type, out Type dbType, out DataTypeDatabaseType? storageType)
        {
            var result = type.GetInterfaces().FirstOrDefault(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(IUmbracoDataType<>));
            if(result != null)
            {
                GetStorageType(type, out dbType, out storageType);
                if (dbType == null)
                {
                    dbType = result.GetGenericArguments().Single();
                }
            }
            else
            {
                dbType = null;
                storageType = null;
            }
            return result != null;
        }

        private static void GetStorageType(Type type, out Type dbType, out DataTypeDatabaseType? storageType)
        {
            if (type.GetInterfaces().Any(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(IUmbracoNtextDataType<>)))
            {
                storageType = DataTypeDatabaseType.Ntext;
                dbType = type.GetInterfaces().First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IUmbracoNtextDataType<>)).GetGenericArguments().Single();
            }
            else if (type.GetInterfaces().Any(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(IUmbracoNvarcharDataType<>)))
            {
                storageType = DataTypeDatabaseType.Nvarchar;
                dbType = type.GetInterfaces().First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IUmbracoNvarcharDataType<>)).GetGenericArguments().Single();
            }
            else if (type.GetInterfaces().Any(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(IUmbracoDateTimeDataType<>)))
            {
                storageType = DataTypeDatabaseType.Date;
                dbType = type.GetInterfaces().First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IUmbracoDateTimeDataType<>)).GetGenericArguments().Single();
            }
            else if (type.GetInterfaces().Any(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof(IUmbracoIntegerDataType<>)))
            {
                storageType = DataTypeDatabaseType.Integer;
                dbType = type.GetInterfaces().First(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IUmbracoIntegerDataType<>)).GetGenericArguments().Single();
            }
            else
            {
                storageType = null;
                dbType = null;
            }
        }
    }
}
