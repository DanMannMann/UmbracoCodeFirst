using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Core.Modules;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using Felinesoft.UmbracoCodeFirst.Events;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System;
using System.Collections.Generic;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public class MemberModelModule : ContentModelModuleBase<MemberNodeDetails>, IMemberModelModule
    {
        private IDataTypeModule _dataTypeModule;
        private IMemberTypeModule _memberTypeModule;
        private Dictionary<string, MemberTypeRegistration> _onCreate = new Dictionary<string, MemberTypeRegistration>();
		private Dictionary<string, MemberTypeRegistration> _onSave = new Dictionary<string, MemberTypeRegistration>();
		private Dictionary<string, MemberTypeRegistration> _onDelete = new Dictionary<string, MemberTypeRegistration>();

		public MemberModelModule(IDataTypeModule dataTypeModule, IMemberTypeModule memberTypeModule)
            : base(dataTypeModule, memberTypeModule)
        {
            _dataTypeModule = dataTypeModule;
            _memberTypeModule = memberTypeModule;
        }

        public void Initialise(IEnumerable<Type> classes)
        {
            foreach (var type in classes)
            {
                if (ModelEventDispatcher.HasEvent<IOnCreateBase>(type))
                {
                    MemberTypeRegistration reg;
                    if (_memberTypeModule.TryGetMemberType(type, out reg))
                    {
                        _onCreate.Add(reg.Alias, reg);
                    }
                }

				if (ModelEventDispatcher.HasEvent<IOnSaveBase>(type))
				{
					MemberTypeRegistration reg;
					if (_memberTypeModule.TryGetMemberType(type, out reg))
					{
						_onSave.Add(reg.Alias, reg);
					}
				}

				if (ModelEventDispatcher.HasEvent<IOnDeleteBase>(type))
				{
					MemberTypeRegistration reg;
					if (_memberTypeModule.TryGetMemberType(type, out reg))
					{
						_onDelete.Add(reg.Alias, reg);
					}
				}
			}
            CodeFirstManager.Invalidating += CodeFirstManager_Invalidating;
            Umbraco.Core.Services.MemberService.Created += ContentService_Created;
			Umbraco.Core.Services.MemberService.Saving += MemberService_Saving;
			Umbraco.Core.Services.MemberService.Deleting += MemberService_Deleting;
        }

		private void MemberService_Deleting(IMemberService sender, DeleteEventArgs<IMember> e)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				lock (_onDelete)
				{
					foreach (var entity in e.DeletedEntities)
					{
						if (_onDelete.ContainsKey(entity.ContentType.Alias) && CodeFirstManager.Current.Features.EnableContentEvents)
						{
							var instance = CreateInstanceFromContent(entity, _onDelete[entity.ContentType.Alias], null);
							(instance as MemberTypeBase).NodeDetails = new MemberNodeDetails(entity);
							e.Cancel |= !ModelEventDispatcher.OnDeleteObject(instance, entity, new HttpContextWrapper(HttpContext.Current), UmbracoContext.Current, ApplicationContext.Current);
							ProjectModelToContent((instance as MemberTypeBase), entity);
						}
					}
				}
			}
		}

		private void MemberService_Saving(IMemberService sender, SaveEventArgs<IMember> e)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				lock (_onSave)
				{
					foreach (var entity in e.SavedEntities)
					{
						if (_onSave.ContainsKey(entity.ContentType.Alias) && CodeFirstManager.Current.Features.EnableContentEvents)
						{
							var instance = CreateInstanceFromContent(entity, _onSave[entity.ContentType.Alias], null);
							(instance as MemberTypeBase).NodeDetails = new MemberNodeDetails(entity);
							e.Cancel |= !ModelEventDispatcher.OnSaveObject(instance, entity, new HttpContextWrapper(HttpContext.Current), UmbracoContext.Current, ApplicationContext.Current);
							ProjectModelToContent((instance as MemberTypeBase), entity);
						}
					}
				}
			}
		}

		void CodeFirstManager_Invalidating(object sender, InvalidatingEventArgs e)
        {
            Umbraco.Core.Services.MemberService.Created -= ContentService_Created;
			Umbraco.Core.Services.MemberService.Saving -= MemberService_Saving;
			Umbraco.Core.Services.MemberService.Deleting -= MemberService_Deleting;
		}

        private void ContentService_Created(IMemberService sender, NewEventArgs<IMember> e)
        {
            if (CodeFirstManager.Current.Features.EnableContentEvents)
            {
                lock (_onCreate)
                {
                    if (_onCreate.ContainsKey(e.Entity.ContentType.Alias) && CodeFirstManager.Current.Features.EnableContentEvents)
                    {
                        var instance = CreateInstanceFromContent(e.Entity, _onCreate[e.Entity.ContentType.Alias], null);
                        (instance as MemberTypeBase).NodeDetails = new MemberNodeDetails(e.Entity);
						ModelEventDispatcher.OnCreateObject(instance, e.Entity, new HttpContextWrapper(HttpContext.Current), UmbracoContext.Current, ApplicationContext.Current);
						ProjectModelToContent((instance as MemberTypeBase), e.Entity);
                    }
                }
            }
        }

        #region Convert Model to Content
        public bool TryConvertToContent<Tmodel>(Tmodel model, out IMember media) where Tmodel : MemberTypeBase
        {
            try
            {
                media = ConvertToContent(model);
                return true;
            }
            catch
            {
                media = null;
                return false;
            }
        }

        public IMember ConvertToContent(MemberTypeBase model)
        {
            var contentId = model.NodeDetails.UmbracoId;
            MemberTypeRegistration registration;
            if (!_memberTypeModule.TryGetMemberType(model.GetType(), out registration))
            {
                throw new CodeFirstException("Member type not registered. Type: " + model.GetType());
            }

            //Create or update object
            if (contentId == -1)
            {
                return CreateContent(model, registration);
            }
            else
            {
                return UpdateContent(model, registration);
            }
        }

        public void ProjectModelToContent(MemberTypeBase model, IMember content)
        {
            var type = model.GetType();
            MemberTypeRegistration reg;
            if (_memberTypeModule.TryGetMemberType(type, out reg) && (reg.ClrType == type || reg.ClrType.Inherits(type)))
            {
                SetMemberSpecificProperties(model, content);
                MapModelToContent(content, model, reg);
            }
        }

        /// <summary>
        /// <para>Creates an IMember populated with the current values of the model</para>
        /// </summary>
        private IMember CreateContent(MemberTypeBase model, ContentTypeRegistration registration)
        {
            if (model.Name == null)
            {
                throw new CodeFirstException("Name must be set to create a member");
            }
            if (model.Username == null)
            {
                throw new CodeFirstException("Username must be set to create a member");
            }
            if (model.Email == null)
            {
                throw new CodeFirstException("Email must be set to create a member");
            }

            //Get the type alias and create the content
            var typeAlias = registration.Alias;
            var node = ApplicationContext.Current.Services.MemberService.CreateMember(model.Username, model.Email, model.Name, typeAlias);
            SetMemberSpecificProperties(model, node);
            MapModelToContent(node, model, registration);
            return node;
        }

        /// <summary>
        /// Updates an existing IContent item with the current values of the model
        /// </summary>
        /// <returns></returns>
        private IMember UpdateContent(MemberTypeBase model, ContentTypeRegistration registration)
        {
            if (model.NodeDetails == null || model.NodeDetails.UmbracoId == -1)
            {
                throw new ArgumentException("Can't update content for a model with no ID. Try calling CreateContent instead. Check that the NodeDetails.UmbracoId property is set before calling UpdateContent.");
            }
            var node = ApplicationContext.Current.Services.MemberService.GetById(model.NodeDetails.UmbracoId);
            SetMemberSpecificProperties(model, node);
            MapModelToContent(node, model, registration);
            return node;
        }

        private void SetMemberSpecificProperties(MemberTypeBase model, IMember node)
        {
            node.Email = model.Email;
            node.Username = model.Username;
            node.Name = model.Name;
            node.Comments = model.Comments;
            node.IsApproved = model.IsApproved;
            node.IsLockedOut = model.IsLockedOut;
            node.LastLockoutDate = model.LastLockoutDate;
            node.LastLoginDate = model.LastLoginDate;
            node.LastPasswordChangeDate = model.LastPasswordChangeDate;
            node.PasswordQuestion = model.PasswordQuestion;
            node.RawPasswordAnswerValue = model.PasswordAnswer;
        }
        #endregion

        #region Convert Content To Model
        public bool TryConvertToModel<T>(IMember content, out T model) where T : MemberTypeBase
        {
            //TODO move to derived
            MemberTypeRegistration docType;
            if (_memberTypeModule.TryGetMemberType(content.ContentType.Alias, out docType) && docType.ClrType == typeof(T))
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
        public T ConvertToModel<T>(IMember content, CodeFirstModelContext parentContext = null) where T : MemberTypeBase
        {
            //TODO move to base class, use a lambda for the type-dependent operations
            MemberTypeRegistration registration;
            if (!_memberTypeModule.TryGetMemberType(content.ContentType.Alias, out registration))
            {
                throw new CodeFirstException("Could not find member type registration for media type alias " + content.ContentType.Alias);
            }
            if (registration.ClrType != typeof(T))
            {
                if (registration.ClrType.Inherits(typeof(T)))
                {
                    //Redirect to the underlying type and make one of those instead
                    if (!_memberTypeModule.TryGetMemberType(typeof(T), out registration))
                    {
                        throw new CodeFirstException("Could not find member type registration for underlying type " + typeof(T).FullName);
                    }
                }
                else
                {
                    throw new CodeFirstException("Registered type for member type " + content.ContentType.Alias + " is " + registration.ClrType.Name + ", not " + typeof(T).Name);
                }
            }

            T instance = (T)CreateInstanceFromContent(content, registration, parentContext);
            (instance as MemberTypeBase).NodeDetails = new MemberNodeDetails(content);
            GetMemberSpecificProperties(instance as MemberTypeBase, content);
            return instance;
        }

        public bool TryConvertToModel<T>(IPublishedContent content, out T model) where T : MemberTypeBase
        {
            return base.TryConvertToModelInternal(content, out model);
        }

        public T ConvertToModel<T>(IPublishedContent content, CodeFirstModelContext parentContext = null) where T : MemberTypeBase
        {
            var result = base.ConvertToModelInternal<T>(content, parentContext);
            GetMemberSpecificProperties(result as MemberTypeBase, content as Umbraco.Web.PublishedCache.MemberPublishedContent);
            return result;
        }

        private void GetMemberSpecificProperties(MemberTypeBase memberTypeBase, IMember content)
        {
            memberTypeBase.Email = content.Email;
            memberTypeBase.Username = content.Username;
            memberTypeBase.Name = content.Name;
            memberTypeBase.Comments = content.Comments;
            memberTypeBase.IsApproved = content.IsApproved;
            memberTypeBase.IsLockedOut = content.IsLockedOut;
            memberTypeBase.LastLockoutDate = content.LastLockoutDate;
            memberTypeBase.LastLoginDate = content.LastLoginDate;
            memberTypeBase.LastPasswordChangeDate = content.LastPasswordChangeDate;
            memberTypeBase.PasswordQuestion = content.PasswordQuestion;
            memberTypeBase.PasswordAnswer = content.RawPasswordAnswerValue;
            memberTypeBase.FailedPasswordAttempts = content.FailedPasswordAttempts;
        }

        private void GetMemberSpecificProperties(MemberTypeBase memberTypeBase, Umbraco.Web.PublishedCache.MemberPublishedContent content)
        {
            memberTypeBase.Email = content.Email;
            memberTypeBase.Username = content.UserName;
            memberTypeBase.Name = content.Name;
            memberTypeBase.Comments = content.Comments;
            memberTypeBase.IsApproved = content.IsApproved;
            memberTypeBase.IsLockedOut = content.IsLockedOut;
            memberTypeBase.LastLockoutDate = content.LastLockoutDate;
            memberTypeBase.LastLoginDate = content.LastLoginDate;
            memberTypeBase.LastPasswordChangeDate = content.LastPasswordChangedDate;
            memberTypeBase.PasswordQuestion = content.PasswordQuestion;
            memberTypeBase.PasswordAnswer = "[redacted from published version]";
            memberTypeBase.FailedPasswordAttempts = -1;
        }
        #endregion
    }
}

namespace Felinesoft.UmbracoCodeFirst.Extensions
{
    public static class MemberModelModuleExtensions
    {
        public static void AddDefaultMemberModelModule(this CodeFirstModuleResolver resolver)
        {
            resolver.RegisterModule<IMemberModelModule>(new MemberModelModuleFactory());
        }
    }
}