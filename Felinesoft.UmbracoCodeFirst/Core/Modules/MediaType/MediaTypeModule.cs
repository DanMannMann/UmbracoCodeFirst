using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using Felinesoft.UmbracoCodeFirst.Core.Modules;
using System.Reflection;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Felinesoft.UmbracoCodeFirst.Diagnostics;
using Umbraco.Web.Models.Trees;
using System.IO;
using System.Web;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public class MediaTypeModule : ContentTypeModuleBase, IMediaTypeModule, IClassFileGenerator
    {
        private IPropertyModule _propertyModule;
        private IContentTypeService _service;

        public MediaTypeModule(IPropertyModule propertyModule, IContentTypeService service)
            : base(propertyModule, Timing.MediaTypeModuleTimer)
        {
            _service = service;
            _propertyModule = propertyModule;
        }

        #region IMediaTypeModule
        public bool TryGetMediaType(string alias, out MediaTypeRegistration registration)
        {
            ContentTypeRegistration reg;
            if (ContentTypeRegister.TryGetContentType(alias, out reg))
            {
                registration = reg as MediaTypeRegistration;
                return registration != null;
            }
            else
            {
                registration = null;
                return false;
            }
        }

        public bool TryGetMediaType(Type type, out MediaTypeRegistration registration)
        {
            ContentTypeRegistration reg;
            if (ContentTypeRegister.TryGetContentType(type, out reg))
            {
                registration = reg as MediaTypeRegistration;
                return registration != null;
            }
            else
            {
                registration = null;
                return false;
            }
        }

        public bool TryGetMediaType<T>(out MediaTypeRegistration registration) where T : MediaTypeBase
        {
            return TryGetMediaType(typeof(T), out registration);
        }
        #endregion

        #region Create/Update Content Type overrides
        public override void Initialise(IEnumerable<Type> types)
        {
            var inputTypes = new List<Type>(types);
            if (CodeFirstManager.Current.Features.UseBuiltInMediaTypes)
            {
                inputTypes.AddRange(this.GetType().Assembly.GetTypes().Where(x => x.GetCustomAttribute<BuiltInMediaTypeAttribute>(false) != null).Except(inputTypes));
            }
            base.Initialise(inputTypes);
        }

        protected override void SyncAllowedChildren(ContentTypeRegistration registration)
        {
            if (CodeFirstManager.Current.Features.AllowAllMediaTypesInDefaultFolder && registration.ClrType == typeof(MediaFolder))
            {
                //TODO make this an attribute so it can be used on user-created folders too. Maybe all it on doc types too. Need separate attrs though as separate types of potential children.
                AddAllTypesToAllowedChildren(registration);
            }
            base.SyncAllowedChildren(registration);
        }

        protected override ContentTypeRegistration CreateRegistration(Type type)
        {
            var mediaTypeAttribute = type.GetCodeFirstAttribute<MediaTypeAttribute>();

            if (mediaTypeAttribute != null)
            {
                var props = new List<PropertyRegistration>();
                var tabs = new List<TabRegistration>();
                var comps = new List<ContentTypeCompositionRegistration>();
                MediaTypeRegistration registration = new MediaTypeRegistration(
                                                        props,
                                                        tabs,
                                                        comps,
                                                        mediaTypeAttribute.Alias,
                                                        mediaTypeAttribute.Name,
                                                        type,
                                                        mediaTypeAttribute);
                return registration;
            }
            else
            {
                throw new CodeFirstException("The specified type does not have a MediaTypeAttribute. Type: " + type.Name);
            }
        }

        private void AddAllTypesToAllowedChildren(ContentTypeRegistration mediaTypeReg)
        {
            var list = new List<Type>();
            if (mediaTypeReg.ContentTypeAttribute.AllowedChildren != null) //Add specified children
            {
                list.AddRange(mediaTypeReg.ContentTypeAttribute.AllowedChildren);
            }
            foreach (var reg in ContentTypeRegister.Registrations) //Add all other types
            {
                if (!list.Contains(reg.ClrType))
                {
                    list.Add(reg.ClrType);
                }
            }
            if (!list.Contains(mediaTypeReg.ClrType)) //Add self
            {
                list.Add(mediaTypeReg.ClrType);
            }
            mediaTypeReg.ContentTypeAttribute.AllowedChildren = list.ToArray();
        }
        #endregion

        #region IEntityTreeFilter
        public override bool IsFilter(string treeAlias)
        {
            return treeAlias.Equals("mediaTypes", StringComparison.InvariantCultureIgnoreCase);
        }
        #endregion

        #region IClassFileGenerator
        public void GenerateClassFiles(string nameSpace, string folderPath)
        {
            string dataDirectory = Path.Combine(folderPath, "MediaTypes");

            if (!System.IO.Directory.Exists(dataDirectory))
            {
                System.IO.Directory.CreateDirectory(dataDirectory);
            }

            GenerateContentTypes(nameSpace, dataDirectory, "MediaType", () => ApplicationContext.Current.Services.ContentTypeService.GetAllMediaTypes(), (x, y) =>
                {
                    if (x.ParentId == -1)
                    {
                        y.ParentAlias = "MediaTypeBase";
                        y.ParentClassName = "MediaTypeBase";
                    }
                    else
                    {
                        var parent = ApplicationContext.Current.Services.ContentTypeService.GetMediaType(x.ParentId);
                        y.ParentAlias = parent == null ? "MediaTypeBase" : parent.Alias;
                        y.ParentClassName = parent == null ? "MediaTypeBase" : TypeGeneratorUtils.GetFormattedMemberName(parent.Alias);
                    }
                });
        }
        #endregion

        #region Content Type Service Adapter
        protected override IEnumerable<IContentTypeBase> GetAllContentTypes()
        {
            return _service.GetAllMediaTypes();
        }

        protected override void SaveContentType(IContentTypeBase contentType)
        {
            _service.Save((IMediaType)contentType);
        }

        protected override void DeleteContentType(IContentTypeBase contentType)
        {
            _service.Delete((IMediaType)contentType);
        }

        protected override IContentTypeComposition GetContentTypeByAlias(string alias)
        {
            return _service.GetMediaType(alias);
        }

        protected override IContentTypeComposition CreateContentType(IContentTypeBase parent)
        {
            return parent == null ? new MediaType(-1) : new MediaType((IMediaType)parent);
        }

        protected override IEnumerable<IContentTypeBase> GetChildren(IContentTypeBase contentType)
        {
            return _service.GetMediaTypeChildren(contentType.Id);
        }
        #endregion
    }
}

namespace Felinesoft.UmbracoCodeFirst.Extensions
{
    public static class MediaTypeModuleExtensions
    {
        public static void AddDefaultMediaTypeModule(this CodeFirstModuleResolver resolver)
        {
            resolver.RegisterModule<IMediaTypeModule>(new MediaTypeModuleFactory());
        }
    }
}
