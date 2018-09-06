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
using Marsman.UmbracoCodeFirst.ContentTypes;
using System.Text.RegularExpressions;
using System.Globalization;

using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.Converters;
using Marsman.UmbracoCodeFirst.Core.Modules;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Marsman.UmbracoCodeFirst.Core;
using Marsman.UmbracoCodeFirst.DataTypes;

namespace Marsman.UmbracoCodeFirst.Extensions
{
    /// <summary>
    /// Extensions used to allow conversion between Umbraco interfaces
    /// and strongly typed models
    /// </summary>
    public static class ContentExtensions
    {
        #region Get Model from IContent
        /// <summary>
        /// Extension used to convert an IPublishedContent back to a Typed model instance.
        /// Your model does need to inherit from UmbracoGeneratedBase and contain the correct attributes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T ConvertToModel<T>(this IContent content, CodeFirstModelContext parentContext = null) where T : DocumentTypeBase
        {
            return CodeFirstManager.Current.Modules.DocumentModelModule.ConvertToModel<T>(content, parentContext);
        }

        public static T ConvertToModel<T>(this IMedia content, CodeFirstModelContext parentContext = null) where T : MediaTypeBase
        {
            return CodeFirstManager.Current.Modules.MediaModelModule.ConvertToModel<T>(content, parentContext);
        }

        public static T ConvertToModel<T>(this IMember content, CodeFirstModelContext parentContext = null) where T : MemberTypeBase
        {
            return CodeFirstManager.Current.Modules.MemberModelModule.ConvertToModel<T>(content, parentContext);
        }
        #endregion

        #region Get Model from IPublishedContent
        /// <summary>
        /// Extension used to convert an IPublishedContent back to a Typed model instance.
        /// Your model does need to inherit from UmbracoGeneratedBase and contain the correct attributes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T ConvertDocumentToModel<T>(this IPublishedContent content, CodeFirstModelContext parentContext = null) where T : DocumentTypeBase
        {
            return CodeFirstManager.Current.Modules.DocumentModelModule.ConvertToModel<T>(content, parentContext);
        }

        public static T ConvertMediaToModel<T>(this IPublishedContent content, CodeFirstModelContext parentContext = null) where T : MediaTypeBase
        {
            return CodeFirstManager.Current.Modules.MediaModelModule.ConvertToModel<T>(content, parentContext);
        }

        public static T ConvertMemberToModel<T>(this IPublishedContent content, CodeFirstModelContext parentContext = null) where T : MemberTypeBase
        {
            return CodeFirstManager.Current.Modules.MemberModelModule.ConvertToModel<T>(content, parentContext);
        }

        /// <summary>
        /// Extension used to convert an IPublishedContent back to a Typed model instance.
        /// Your model does need to inherit from UmbracoGeneratedBase and contain the correct attributes
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static object ConvertToModel(this IPublishedContent content, CodeFirstModelContext parentContext = null)
        {
            switch (content.ItemType)
            {
                case PublishedItemType.Content:
                    return CodeFirstManager.Current.Modules.DocumentModelModule.ConvertToModel(content, parentContext);

                case PublishedItemType.Media:
                    return CodeFirstManager.Current.Modules.MediaModelModule.ConvertToModel(content, parentContext);

                case PublishedItemType.Member:
                    return CodeFirstManager.Current.Modules.MemberModelModule.ConvertToModel(content, parentContext);

                default:
                    throw new NotImplementedException();
            }
        }
        #endregion

		public static string GetContextualAttributes(this CodeFirstContentBase input)
		{
			return DataTypeUtils.GetHtmlTagContentFromContextualAttributes(input);
		}
    }
}
