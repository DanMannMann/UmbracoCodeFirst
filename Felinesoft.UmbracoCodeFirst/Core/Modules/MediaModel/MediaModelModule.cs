using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Core.Modules;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using Felinesoft.UmbracoCodeFirst.Events;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public class MediaModelModule : ContentModelModuleBase<MediaNodeDetails,IMediaService,IMedia>, IMediaModelModule
    {
        private IDataTypeModule _dataTypeModule;
        private IMediaTypeModule _mediaTypeModule;

		public MediaModelModule(IDataTypeModule dataTypeModule, IMediaTypeModule mediaTypeModule)
            : base(dataTypeModule, 
				  mediaTypeModule,
				  x => x.ContentType.Alias,
				  (x, type) => { if (type == SubscribeType.Subscribe) { MediaService.Trashing += x; } else { MediaService.Trashing -= x; } }, //trashing
				  (x, type) => { }, //deleting
				  (x, type) => { if (type == SubscribeType.Subscribe) { MediaService.Created += x; } else { MediaService.Created -= x; } }, //creating
				  (x, type) => { if (type == SubscribeType.Subscribe) { MediaService.Saving += x; } else { MediaService.Saving -= x; } }, //saving
				  (x, type) => { if (type == SubscribeType.Subscribe) { MediaService.Moving += x; } else { MediaService.Moving -= x; } }, //moving
				  (x, type) => { }, //copying
				  (x, type) => { }, //publishing
				  (x, type) => { }) //unpublishing
		{
            _dataTypeModule = dataTypeModule;
            _mediaTypeModule = mediaTypeModule;
        }

        #region Convert Model to Content
        public bool TryConvertToContent<Tmodel>(Tmodel model, out IMedia media, int parentId = -1) where Tmodel : MediaTypeBase
        {
            try
            {
                media = ConvertToContent(model, parentId);
                return true;
            }
            catch
            {
                media = null;
                return false;
            }
        }

        public IMedia ConvertToContent(MediaTypeBase model, int parentId = -1)
        {
            var contentId = model.NodeDetails.UmbracoId;
            MediaTypeRegistration registration;
            if (!_mediaTypeModule.TryGetMediaType(model.GetType(), out registration))
            {
                throw new CodeFirstException("Media type not registered. Type: " + model.GetType());
            }

            //Create or update object
            if (contentId == -1)
            {
                return CreateContent(parentId, model, registration);
            }
            else
            {
                return UpdateContent(model, registration);
            }
        }

        /// <summary>
        /// <para>Creates an IContent populated with the current values of the model</para>
        /// </summary>
        private IMedia CreateContent(int parentId, MediaTypeBase model, ContentTypeRegistration registration)
        {
            //Get the type alias and create the content
            var typeAlias = registration.Alias;
            var node = ApplicationContext.Current.Services.MediaService.CreateMedia(model.NodeDetails.Name, parentId, typeAlias);
            MapModelToContent(node, model, registration);
            return node;
        }

        /// <summary>
        /// Updates an existing IContent item with the current values of the model
        /// </summary>
        /// <returns></returns>
        private IMedia UpdateContent(MediaTypeBase model, ContentTypeRegistration registration)
        {
            if (model.NodeDetails == null || model.NodeDetails.UmbracoId == -1)
            {
                throw new ArgumentException("Can't update content for a model with no ID. Try calling CreateContent instead. Check that the NodeDetails.UmbracoId property is set before calling UpdateContent.");
            }
            var node = ApplicationContext.Current.Services.MediaService.GetById(model.NodeDetails.UmbracoId);
            MapModelToContent(node, model, registration);
            return node;
        }
        #endregion

        #region Convert Content To Model
        public bool TryConvertToModel<T>(IMedia content, out T model) where T : MediaTypeBase
        {
            //TODO move to derived
            MediaTypeRegistration docType;
            if (_mediaTypeModule.TryGetMediaType(content.ContentType.Alias, out docType) && docType.ClrType == typeof(T))
            {
                try
                {
                    model = ConvertToModel<T>(content);
                    return true;
                }
                catch
                {
                    model = default(T);
                    return false;
                }
            }
            else
            {
                model = default(T);
                return false;
            }
        }

        /// <summary>
        /// Extension used to convert an IPublishedContent back to a Typed model instance.
        /// Your model does need to inherit from UmbracoGeneratedBase and contain the correct attributes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public T ConvertToModel<T>(IMedia content, CodeFirstModelContext parentContext = null) where T : MediaTypeBase
        {
            //TODO move to base class, use a lambda for the type-dependent operations
            MediaTypeRegistration registration;
            if (!_mediaTypeModule.TryGetMediaType(content.ContentType.Alias, out registration))
            {
                throw new CodeFirstException("Could not find media type registration for media type alias " + content.ContentType.Alias);
            }
            if (registration.ClrType != typeof(T))
            {
                if (registration.ClrType.Inherits(typeof(T)))
                {
                    //Redirect to the underlying type and make one of those instead
                    if (!_mediaTypeModule.TryGetMediaType(typeof(T), out registration))
                    {
                        throw new CodeFirstException("Could not find media type registration for underlying type " + typeof(T).FullName);
                    }
                }
                else
                {
                    throw new CodeFirstException("Registered type for media type " + content.ContentType.Alias + " is " + registration.ClrType.Name + ", not " + typeof(T).Name);
                }
            }

            T instance = (T)CreateInstanceFromContent(content, registration, parentContext);
            (instance as MediaTypeBase).NodeDetails = new MediaNodeDetails(content);
            return instance;
        }

        public bool TryConvertToModel<T>(IPublishedContent content, out T model) where T : MediaTypeBase
        {
            return base.TryConvertToModelInternal(content, out model);
        }

        public T ConvertToModel<T>(IPublishedContent content, CodeFirstModelContext parentContext = null) where T : MediaTypeBase
        {
            return base.ConvertToModelInternal<T>(content, parentContext);
        }
        #endregion

        public void ProjectModelToContent(MediaTypeBase model, IMedia content)
        {
            var type = model.GetType();
            MediaTypeRegistration reg;
            if (_mediaTypeModule.TryGetMediaType(type, out reg) && (reg.ClrType == type || reg.ClrType.Inherits(type)))
            {
                MapModelToContent(content, model, reg);
            }
        }
    }
}

namespace Felinesoft.UmbracoCodeFirst.Extensions
{
    public static class MediaModelModuleExtensions
    {
        public static void AddDefaultMediaModelModule(this CodeFirstModuleResolver resolver)
        {
            resolver.RegisterModule<IMediaModelModule>(new MediaModelModuleFactory());
        }
    }
}