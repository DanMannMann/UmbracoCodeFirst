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

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public class MemberTypeModule : ContentTypeModuleBase, IMemberTypeModule, IClassFileGenerator
    {
        private IPropertyModule _propertyModule;
        private IMemberTypeService _service;
        private IMemberService _memberService;

        public MemberTypeModule(IPropertyModule propertyModule, IMemberTypeService service, IMemberService memberService)
            : base(propertyModule, Timing.MediaTypeModuleTimer)
        {
            _service = service;
            _propertyModule = propertyModule;
            _memberService = memberService;
        }

        #region IMemberTypeModule
        public bool TryGetMemberType(string alias, out MemberTypeRegistration registration)
        {
            ContentTypeRegistration reg;
            if (ContentTypeRegister.TryGetContentType(alias, out reg))
            {
                registration = reg as MemberTypeRegistration;
                return registration != null;
            }
            else
            {
                registration = null;
                return false;
            }
        }

        public bool TryGetMemberType(Type type, out MemberTypeRegistration registration)
        {
            ContentTypeRegistration reg;
            if (ContentTypeRegister.TryGetContentType(type, out reg))
            {
                registration = reg as MemberTypeRegistration;
                return registration != null;
            }
            else
            {
                registration = null;
                return false;
            }
        }

        public bool TryGetMemberType<T>(out MemberTypeRegistration registration) where T : MemberTypeBase
        {
            return TryGetMemberType(typeof(T), out registration);
        }
        #endregion

        #region Create/Update Content Type overrides
        public override void Initialise(IEnumerable<Type> types)
        {
            base.Initialise(types.Where(x => x.GetCodeFirstAttribute<MemberTypeAttribute>() != null));
            InitialiseGroups(types.Where(x => x.GetCodeFirstAttribute<MemberGroupAttribute>() != null));
        }

        private void InitialiseGroups(IEnumerable<Type> groupTypes)
        {
            foreach (var group in groupTypes)
            {
                var attr = group.GetCodeFirstAttribute<MemberGroupAttribute>();
                if (!_memberService.GetAllRoles().Contains(attr.Name))
                {
                    _memberService.AddRole(attr.Name);
                }
            }
        }

        protected override ContentTypeRegistration CreateRegistration(Type type)
        {
            var memberTypeAttribute = type.GetCodeFirstAttribute<MemberTypeAttribute>();

            if (memberTypeAttribute != null)
            {
                var props = new List<PropertyRegistration>();
                var tabs = new List<TabRegistration>();
                var comps = new List<ContentTypeCompositionRegistration>();
                MemberTypeRegistration registration = new MemberTypeRegistration(
                                                        props,
                                                        tabs,
                                                        comps,
                                                        memberTypeAttribute.Alias,
                                                        memberTypeAttribute.Name,
                                                        type,
                                                        memberTypeAttribute);
                return registration;
            }
            else
            {
                throw new CodeFirstException("The specified type does not have a MemberTypeAttribute. Type: " + type.Name);
            }
        }

        protected override IContentTypeBase CreateContentType(ContentTypeRegistration registration, out bool modified)
        {
            if (CodeFirstManager.Current.Features.InitialisationMode == InitialisationMode.Ensure)
            {
                throw new CodeFirstPassiveInitialisationException("The types defined in the database do not match the types passed in to initialise. In InitialisationMode.Ensure the types must match or the site will be prevented from starting.");
            }

            var result = base.CreateContentType(registration, out modified);

            if (CodeFirstManager.Current.Features.InitialisationMode == InitialisationMode.Sync)
            {
                return SyncMemberType(registration, result);
            }
            else if (CodeFirstManager.Current.Features.InitialisationMode == InitialisationMode.Passive)
            {
                return result;
            }
            else
            {
                throw new CodeFirstException("Unknown initialisation type");
            }
        }

        protected override IContentTypeBase UpdateContentType(ContentTypeRegistration registration, out bool modified)
        {
            var result = base.UpdateContentType(registration, out modified);

            if (CodeFirstManager.Current.Features.InitialisationMode == InitialisationMode.Ensure)
            {
                if (modified)
                {
                    throw new CodeFirstPassiveInitialisationException("The types defined in the database do not match the types passed in to initialise. In InitialisationMode.Ensure the types must match or the site will be prevented from starting.");
                }
                else
                {
                    return result;
                }
            }
            else if (CodeFirstManager.Current.Features.InitialisationMode == InitialisationMode.Sync)
            {
                if (modified)
                {
                    return SyncMemberType(registration, result);
                }
                else
                {
                    return result;
                }
            }
            else if (CodeFirstManager.Current.Features.InitialisationMode == InitialisationMode.Passive)
            {
                return result;
            }
            else
            {
                throw new CodeFirstException("Unknown initialisation type");
            }
        }

        private IContentTypeBase SyncMemberType(ContentTypeRegistration registration, IContentTypeBase result)
        {
            result.ResetDirtyProperties(false);
            Save(result); //need to save to ensure no sync issues when we load from the legacy API
            bool modified = false;
            var member = new umbraco.cms.businesslogic.member.MemberType(result.Id);
            foreach (var prop in member.PropertyTypes)
            {
                var propReg = registration.Properties.SingleOrDefault(x => x.Alias == prop.Alias);
                if (propReg != null)
                {
                    var attr = propReg.Metadata.GetCodeFirstAttribute<MemberPropertyAttribute>();
                    if (attr != null)
                    {
                        if (attr.MemberCanEdit != member.MemberCanEdit(prop))
                        {
                            member.setMemberCanEdit(prop, attr.MemberCanEdit);
                            modified = true;
                        }
                        if (attr.ShowOnProfile != member.ViewOnProfile(prop))
                        {
                            member.setMemberViewOnProfile(prop, attr.ShowOnProfile);
                            modified = true;
                        }
                    }
                }
            }

            if (modified)
            {
                modified = false;
                member.Save();
            }

            return _service.Get(member.Id);
        }
        #endregion

        #region IEntityTreeFilter
        public override bool IsFilter(string treeAlias)
        {
            return treeAlias.Equals("memberTypes", StringComparison.InvariantCultureIgnoreCase);
        }
        #endregion

        #region IClassFileGenerator
        public void GenerateClassFiles(string nameSpace, string folderPath)
        {
            string dataDirectory = Path.Combine(folderPath, "MemberTypes");

            if (!System.IO.Directory.Exists(dataDirectory))
            {
                System.IO.Directory.CreateDirectory(dataDirectory);
            }

            GenerateContentTypes(nameSpace, dataDirectory, "MemberType", () => ApplicationContext.Current.Services.ContentTypeService.GetAllMediaTypes(), (x, y) =>
                {
                    y.ParentAlias = x.ParentId == -1 ? "MemberTypeBase" : ApplicationContext.Current.Services.ContentTypeService.GetContentType(x.ParentId).Alias;
                    y.ParentClassName = x.ParentId == -1 ? "MemberTypeBase" : y.ParentAlias.Replace('.', '_').Replace('-', '_').Replace("?", "").ToPascalCase();
                });
        }
        #endregion

        #region Content Type Service Adapter
        protected override IEnumerable<IContentTypeBase> GetAllContentTypes()
        {
            return _service.GetAll();
        }

        protected override void SaveContentType(IContentTypeBase contentType)
        {
            _service.Save((IMemberType)contentType);
        }

        protected override void DeleteContentType(IContentTypeBase contentType)
        {
            _service.Delete((IMemberType)contentType);
        }

        protected override IContentTypeComposition GetContentTypeByAlias(string alias)
        {
            return _service.Get(alias);
        }

        protected override IContentTypeComposition CreateContentType(IContentTypeBase parent)
        {
            return parent == null ? new MemberType(-1) : new MemberType((IMemberType)parent);
        }

        protected override IEnumerable<IContentTypeBase> GetChildren(IContentTypeBase contentType)
        {
            return _service.GetAll().Where(x => x.ParentId == contentType.Id);
        }
        #endregion
    }
}

namespace Felinesoft.UmbracoCodeFirst.Extensions
{
    public static class MemberTypeModuleExtensions
    {
        public static void AddDefaultMemberTypeModule(this CodeFirstModuleResolver resolver)
        {
            resolver.RegisterModule<IMemberTypeModule>(new MemberTypeModuleFactory());
        }
    }
}
