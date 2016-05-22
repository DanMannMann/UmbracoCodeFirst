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
        public static T FirstDocument<T>(this IEnumerable<IPublishedContent> input) where T : CodeFirstContentBase
        {
            return ExecuteSingle<T>(input, c => c.First(x => x.DocumentTypeAlias == GetDocumentTypeAlias<T>()));
        }

        public static T FirstOrDefaultDocument<T>(this IEnumerable<IPublishedContent> input) where T : CodeFirstContentBase
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

        public static T LastDocument<T>(this IEnumerable<IPublishedContent> input) where T : CodeFirstContentBase
        {
            return ExecuteSingle<T>(input, c => c.Last(x => x.DocumentTypeAlias == GetDocumentTypeAlias<T>()));
        }

        public static T LastOrDefaultDocument<T>(this IEnumerable<IPublishedContent> input) where T : CodeFirstContentBase
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

        public static T SingleDocument<T>(this IEnumerable<IPublishedContent> input) where T : CodeFirstContentBase
        {
            return ExecuteSingle<T>(input, c => c.Single(x => x.DocumentTypeAlias == GetDocumentTypeAlias<T>()));
        }

        public static T SingleOrDefaultDocument<T>(this IEnumerable<IPublishedContent> input) where T : CodeFirstContentBase
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

        private static T ExecuteSingle<T>(IEnumerable<IPublishedContent> input, Func<IEnumerable<IPublishedContent>, IPublishedContent> method) where T : CodeFirstContentBase
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
                return (T)match.ConvertToModel();
            }
            catch (Exception ex)
            {
                throw new CodeFirstException("Failed to convert the matched document (ID:" + match.Id + ") to CLR type " + typeof(T).Name, ex);
            }
        }
		#endregion

		#region Get Enumerable of Document From Published Content
		public static IEnumerable<T> ContentOfTypeOrDescendedFromType<T>(this IEnumerable<IPublishedContent> input) where T : CodeFirstContentBase
		{
			IEnumerable<IPublishedContent> match = input;

			try
			{
				return match.Select(x => x.ConvertToModel()).Where(x => x is T).Cast<T>();
			}
			catch (Exception ex)
			{
				throw new CodeFirstException("Failed to convert one of the matched nodes to CLR type " + typeof(T).Name, ex);
			}
		}

		public static IEnumerable<T> ContentOfType<T>(this IEnumerable<IPublishedContent> input) where T : CodeFirstContentBase
        {
            IEnumerable<IPublishedContent> match;
            try
            {
				match = input.Where(y => y.DocumentTypeAlias == GetDocumentTypeAlias<T>());
			}
            catch (Exception ex)
            {
                throw new CodeFirstException("No node of type " + typeof(T).Name + " was found in the collection", ex);
            }

            try
            {
                return match.Select(x => x.ConvertToModel()).Where(x => x is T).Cast<T>();
            }
            catch (Exception ex)
            {
                throw new CodeFirstException("Failed to convert one of the matched nodes to CLR type " + typeof(T).Name, ex);
            }
        }

        public static IEnumerable<T> ChildrenOfType<T>(this IPublishedContent input) where T : CodeFirstContentBase
        {
            return input.Children.ContentOfType<T>();
        }

        public static IEnumerable<T> DescendantsOfType<T>(this IPublishedContent input) where T : CodeFirstContentBase
        {
            return input.Descendants().ContentOfType<T>();
        }

        public static IEnumerable<T> AncestorsOfType<T>(this IPublishedContent input) where T : CodeFirstContentBase
        {
            return input.Ancestors().ContentOfType<T>();
        }

        public static IEnumerable<T> SiblingsOfType<T>(this IPublishedContent input) where T : CodeFirstContentBase
        {
            if (input.Parent == null || input.Parent.Children == null)
            {
                return new List<T>();
            }
            return input.Parent.Children.Where(x => x != input).ContentOfType<T>();
        }
		#endregion

		#region Get Enmumerable Of Document From Model
		public static IEnumerable<T> ChildrenOfTypeOrDescendedFromType<T>(this CodeFirstContentBase input) where T : CodeFirstContentBase
		{
			return input.NodeDetails.PublishedContent.Children.ContentOfTypeOrDescendedFromType<T>();
		}

		public static IEnumerable<T> ChildrenOfType<T>(this CodeFirstContentBase input) where T : CodeFirstContentBase
        {
            return input.NodeDetails.PublishedContent.Children.ContentOfType<T>();
        }

        public static IEnumerable<T> DescendantsOfType<T>(this CodeFirstContentBase input) where T : CodeFirstContentBase
        {
            return input.NodeDetails.PublishedContent.Descendants().ContentOfType<T>();
        }

        public static IEnumerable<T> AncestorsOfType<T>(this CodeFirstContentBase input) where T : CodeFirstContentBase
        {
            return input.NodeDetails.PublishedContent.Ancestors().ContentOfType<T>();
        }

        public static IEnumerable<T> SiblingsOfType<T>(this CodeFirstContentBase input) where T : CodeFirstContentBase
        {
            if (input.NodeDetails.PublishedContent == null || input.NodeDetails.PublishedContent.Parent == null || input.NodeDetails.PublishedContent.Parent.Children == null)
            {
                return new List<T>();
            }
            return input.NodeDetails.PublishedContent.Parent.Children.Where(x => x != input).ContentOfType<T>();
        }
        #endregion

        #region Navigate By Type From Published Content
        public static IPublishedContent FirstChild<T>(this IPublishedContent input) where T : CodeFirstContentBase
        {
            return input.Children.First(x => x.DocumentTypeAlias == GetDocumentTypeAlias<T>());
        }

        public static T FirstChildModel<T>(this IPublishedContent input) where T : CodeFirstContentBase
        {
            return (T)FirstChild<T>(input).ConvertToModel();
        }

        public static IPublishedContent LastChild<T>(this IPublishedContent input) where T : CodeFirstContentBase
        {
            return input.Children.Last(x => x.DocumentTypeAlias == GetDocumentTypeAlias<T>());
        }

        public static T LastChildModel<T>(this IPublishedContent input) where T : CodeFirstContentBase
        {
            return (T)LastChild<T>(input).ConvertToModel();
        }

        public static IPublishedContent SingleChild<T>(this IPublishedContent input) where T : CodeFirstContentBase
        {
            return input.Children.Single(x => x.DocumentTypeAlias == GetDocumentTypeAlias<T>());
        }

        public static T SingleChildModel<T>(this IPublishedContent input) where T : CodeFirstContentBase
        {
            return (T)SingleChild<T>(input).ConvertToModel();
        }

        public static IPublishedContent NearestAncestor<T>(this IPublishedContent input) where T : CodeFirstContentBase
        {
            return input.Ancestors(GetDocumentTypeAlias<T>()).FirstOrDefault();
        }

        public static T NearestAncestorModel<T>(this IPublishedContent input) where T : CodeFirstContentBase
        {
            return (T)NearestAncestor<T>(input).ConvertToModel();
        }

        public static IPublishedContent NearestDescendant<T>(this IPublishedContent input) where T : CodeFirstContentBase
        {
            return input.Descendants(GetDocumentTypeAlias<T>()).FirstOrDefault();
        }

        public static T NearestDescendantModel<T>(this IPublishedContent input) where T : CodeFirstContentBase
        {
            return (T)NearestDescendant<T>(input).ConvertToModel();
        }
        #endregion

        #region Navigate By Type From Model
        public static T FirstChild<T>(this CodeFirstContentBase input) where T : CodeFirstContentBase
        {
            return (T)input.NodeDetails.PublishedContent.FirstChild<T>().ConvertToModel();
        }

        public static T LastChild<T>(this CodeFirstContentBase input) where T : CodeFirstContentBase
        {
            return (T)input.NodeDetails.PublishedContent.LastChild<T>().ConvertToModel();
        }

        public static T SingleChild<T>(this CodeFirstContentBase input) where T : CodeFirstContentBase
        {
            return (T)input.NodeDetails.PublishedContent.SingleChild<T>().ConvertToModel();
        }

        public static T NearestAncestor<T>(this CodeFirstContentBase input) where T : CodeFirstContentBase
        {
            return (T)input.NodeDetails.PublishedContent.NearestAncestor<T>().ConvertToModel();
        }

        public static T NearestDescendant<T>(this CodeFirstContentBase input) where T : CodeFirstContentBase
        {
            return (T)input.NodeDetails.PublishedContent.NearestDescendant<T>().ConvertToModel();
        }
		#endregion

		#region Scalar
		public static bool IsAncestorOf<T>(this CodeFirstContentBase input, T comparator) where T : CodeFirstContentBase
		{
			var path = comparator.NodeDetails.PublishedContent?.Path.Split(',').Select(x => int.Parse(x));
			try
			{
				return path.Contains(input.NodeDetails.UmbracoId);
			}
			catch (NullReferenceException)
			{
				throw new CodeFirstException("All code-first LINQ methods can only be used against published content. One or more of the node arguments was constructed from IContent");
			}
		}
		#endregion

		#region Doc Type Def
		private static string GetDocumentTypeAlias<T>() where T : CodeFirstContentBase
        {
            var type = GetDocumentTypeRegistration<T>()?.Alias;
            return type;
        }

        private static ContentTypeRegistration GetDocumentTypeRegistration<T>() where T : CodeFirstContentBase
        {
            if (typeof(T).Inherits(typeof(DocumentTypeBase)))
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
            else if (typeof(T).Inherits(typeof(MediaTypeBase)))
            {
                MediaTypeRegistration type;
                if (CodeFirstManager.Current.Modules.MediaTypeModule.TryGetMediaType(typeof(T), out type))
                {
                    return type;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new CodeFirstException("Unsupported content type");
            }
        }
        #endregion
    }
}
