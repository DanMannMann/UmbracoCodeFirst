using Felinesoft.UmbracoCodeFirst.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Core.Modules;
using Umbraco.Core;
using Umbraco.Web;
using Felinesoft.UmbracoCodeFirst.ContentTypes;

namespace Felinesoft.UmbracoCodeFirst.Linq
{
    public static class ContentEnumerableExtensions
    {
        #region Get Single Document From Published Content
        public static T FirstDocument<T>(this IEnumerable<IPublishedContent> input) where T : DocumentTypeBase
        {
            return ExecuteSingle<T>(input, c => c.First(x => x.DocumentTypeAlias == GetDocumentTypeAlias<T>()));
        }

        public static T FirstOrDefaultDocument<T>(this IEnumerable<IPublishedContent> input) where T : DocumentTypeBase
        {
            try
            {
                return ExecuteSingle<T>(input, c => c.First(x => x.DocumentTypeAlias == GetDocumentTypeAlias<T>()));
            }
            catch
            {
                return default(T);
            }
        }

        public static T LastDocument<T>(this IEnumerable<IPublishedContent> input) where T : DocumentTypeBase
        {
            return ExecuteSingle<T>(input, c => c.Last(x => x.DocumentTypeAlias == GetDocumentTypeAlias<T>()));
        }

        public static T LastOrDefaultDocument<T>(this IEnumerable<IPublishedContent> input) where T : DocumentTypeBase
        {
            try
            {
                return ExecuteSingle<T>(input, c => c.Last(x => x.DocumentTypeAlias == GetDocumentTypeAlias<T>()));
            }
            catch
            {
                return default(T);
            }
        }

        public static T SingleDocument<T>(this IEnumerable<IPublishedContent> input) where T : DocumentTypeBase
        {
            return ExecuteSingle<T>(input, c => c.Single(x => x.DocumentTypeAlias == GetDocumentTypeAlias<T>()));
        }

        public static T SingleOrDefaultDocument<T>(this IEnumerable<IPublishedContent> input) where T : DocumentTypeBase
        {
            try
            {
                return ExecuteSingle<T>(input, c => c.Single(x => x.DocumentTypeAlias == GetDocumentTypeAlias<T>()));
            }
            catch
            {
                return default(T);
            }
        }

        private static T ExecuteSingle<T>(IEnumerable<IPublishedContent> input, Func<IEnumerable<IPublishedContent>, IPublishedContent> method) where T : DocumentTypeBase
        {
            IPublishedContent match;
            try
            {
                match = method.Invoke(input);
            }
            catch (Exception ex)
            {
                throw new CodeFirstException("No document of type " + typeof(T).Name + " was found in the collection", ex);
            }

            try
            {
                return match.ConvertToModel<T>();
            }
            catch (Exception ex)
            {
                throw new CodeFirstException("Failed to convert the matched document (ID:" + match.Id + ") to CLR type " + typeof(T).Name, ex);
            }
        }
        #endregion

        #region Get Enumerable of Document From Published Content
        public static IEnumerable<T> DocumentsOfType<T>(this IEnumerable<IPublishedContent> input) where T : DocumentTypeBase
        {
            return Execute<T>(input, x => x.Where(y => y.DocumentTypeAlias == GetDocumentTypeAlias<T>()));
        }

        public static IEnumerable<T> DescendantsOfType<T>(this IPublishedContent input) where T : DocumentTypeBase
        {
            return input.Descendants().DocumentsOfType<T>();
        }

        public static IEnumerable<T> AncestorsOfType<T>(this IPublishedContent input) where T : DocumentTypeBase
        {
            return input.Ancestors().DocumentsOfType<T>();
        }

        public static IEnumerable<T> SiblingsOfType<T>(this IPublishedContent input) where T : DocumentTypeBase
        {
            if (input.Parent == null || input.Parent.Children == null)
            {
                return new List<T>();
            }
            return input.Parent.Children.Where(x => x != input).DocumentsOfType<T>();
        }

        private static IEnumerable<T> Execute<T>(IEnumerable<IPublishedContent> input, Func<IEnumerable<IPublishedContent>, IEnumerable<IPublishedContent>> method) where T : DocumentTypeBase
        {
            IEnumerable<IPublishedContent> match;
            try
            {
                match = method.Invoke(input);
            }
            catch (Exception ex)
            {
                throw new CodeFirstException("No document of type " + typeof(T).Name + " was found in the collection", ex);
            }

            try
            {
                return match.Select(x => x.ConvertToModel<T>());
            }
            catch (Exception ex)
            {
                throw new CodeFirstException("Failed to convert one of the matched documents to CLR type " + typeof(T).Name, ex);
            }
        }
        #endregion

        #region Get Enmumerable Of Document From Model
        public static IEnumerable<T> DescendantsOfType<T>(this DocumentTypeBase input) where T : DocumentTypeBase
        {
            return input.NodeDetails.PublishedContent.Descendants().DocumentsOfType<T>();
        }

        public static IEnumerable<T> AncestorsOfType<T>(this DocumentTypeBase input) where T : DocumentTypeBase
        {
            return input.NodeDetails.PublishedContent.Ancestors().DocumentsOfType<T>();
        }

        public static IEnumerable<T> SiblingsOfType<T>(this DocumentTypeBase input) where T : DocumentTypeBase
        {
            if (input.NodeDetails.PublishedContent == null || input.NodeDetails.PublishedContent.Parent == null || input.NodeDetails.PublishedContent.Parent.Children == null)
            {
                return new List<T>();
            }
            return input.NodeDetails.PublishedContent.Parent.Children.Where(x => x != input).DocumentsOfType<T>();
        }
        #endregion

        #region Navigate By Type From Published Content
        public static IPublishedContent FirstChild<T>(this IPublishedContent input) where T : DocumentTypeBase
        {
            return input.Children.First(x => x.DocumentTypeAlias == GetDocumentTypeAlias<T>());
        }

        public static T FirstChildModel<T>(this IPublishedContent input) where T : DocumentTypeBase
        {
            return FirstChild<T>(input).ConvertToModel<T>();
        }

        public static IPublishedContent LastChild<T>(this IPublishedContent input) where T : DocumentTypeBase
        {
            return input.Children.Last(x => x.DocumentTypeAlias == GetDocumentTypeAlias<T>());
        }

        public static T LastChildModel<T>(this IPublishedContent input) where T : DocumentTypeBase
        {
            return LastChild<T>(input).ConvertToModel<T>();
        }

        public static IPublishedContent SingleChild<T>(this IPublishedContent input) where T : DocumentTypeBase
        {
            return input.Children.Single(x => x.DocumentTypeAlias == GetDocumentTypeAlias<T>());
        }

        public static T SingleChildModel<T>(this IPublishedContent input) where T : DocumentTypeBase
        {
            return SingleChild<T>(input).ConvertToModel<T>();
        }

        public static IPublishedContent NearestAncestor<T>(this IPublishedContent input) where T : DocumentTypeBase
        {
            return input.Ancestors(GetDocumentTypeAlias<T>()).FirstOrDefault();
        }

        public static T NearestAncestorModel<T>(this IPublishedContent input) where T : DocumentTypeBase
        {
            return NearestAncestor<T>(input).ConvertToModel<T>();
        }

        public static IPublishedContent NearestDescendant<T>(this IPublishedContent input) where T : DocumentTypeBase
        {
            return input.Descendants(GetDocumentTypeAlias<T>()).FirstOrDefault();
        }

        public static T NearestDescendantModel<T>(this IPublishedContent input) where T : DocumentTypeBase
        {
            return NearestDescendant<T>(input).ConvertToModel<T>();
        }
        #endregion

        #region Navigate By Type From Model
        public static T FirstChild<T>(this DocumentTypeBase input) where T : DocumentTypeBase
        {
            return input.NodeDetails.PublishedContent.FirstChild<T>().ConvertToModel<T>();
        }

        public static T LastChild<T>(this DocumentTypeBase input) where T : DocumentTypeBase
        {
            return input.NodeDetails.PublishedContent.LastChild<T>().ConvertToModel<T>();
        }

        public static T SingleChild<T>(this DocumentTypeBase input) where T : DocumentTypeBase
        {
            return input.NodeDetails.PublishedContent.SingleChild<T>().ConvertToModel<T>();
        }

        public static T NearestAncestor<T>(this DocumentTypeBase input) where T : DocumentTypeBase
        {
            return input.NodeDetails.PublishedContent.NearestAncestor<T>().ConvertToModel<T>();
        }

        public static T NearestDescendant<T>(this DocumentTypeBase input) where T : DocumentTypeBase
        {
            return input.NodeDetails.PublishedContent.NearestDescendant<T>().ConvertToModel<T>();
        }
        #endregion

        #region Doc Type Def
        private static string GetDocumentTypeAlias<T>() where T : DocumentTypeBase
        {
            var type = GetDocumentTypeRegistration<T>().Alias;
            return type;
        }

        private static ContentTypeRegistration GetDocumentTypeRegistration<T>() where T : DocumentTypeBase
        {
            DocumentTypeRegistration type;
            if (CodeFirstManager.Current.Modules.DocumentTypeModule.TryGetDocumentType(typeof(T), out type))
            {
                return type;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
