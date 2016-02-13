using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Core.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Publishing;
using Umbraco.Web;

namespace Felinesoft.UmbracoCodeFirst.Events
{
	internal class ModelEventHandler<Tservice,Tentity,Tnodedetails> where Tentity : IContentBase where Tnodedetails : ContentNodeDetails
	{
		#region Fields
		private Dictionary<string, ContentTypeRegistration> _onCreate = new Dictionary<string, ContentTypeRegistration>();
		private Dictionary<string, ContentTypeRegistration> _onSave = new Dictionary<string, ContentTypeRegistration>();
		private Dictionary<string, ContentTypeRegistration> _onDelete = new Dictionary<string, ContentTypeRegistration>();
		private Dictionary<string, ContentTypeRegistration> _onMove = new Dictionary<string, ContentTypeRegistration>();
		private Dictionary<string, ContentTypeRegistration> _onCopy = new Dictionary<string, ContentTypeRegistration>();
		private Dictionary<string, ContentTypeRegistration> _onPublish = new Dictionary<string, ContentTypeRegistration>();
		private Dictionary<string, ContentTypeRegistration> _onUnpublish = new Dictionary<string, ContentTypeRegistration>();
		private Func<IContentBase, ContentTypeRegistration, object> _createInstance;
		private Func<Tentity, string> _getAliasFromContentInstance;
		private Action<Tentity, CodeFirstContentBase<Tnodedetails>, ContentTypeRegistration> _mapModelToContent;
		private IContentTypeModuleBase _contentTypeModuleBase;
		private Action<TypedEventHandler<Tservice, MoveEventArgs<Tentity>>, SubscribeType> _trashEventSubscriber;
		private Action<TypedEventHandler<Tservice, DeleteEventArgs<Tentity>>, SubscribeType> _deleteEventSubscriber;
		private Action<TypedEventHandler<Tservice, NewEventArgs<Tentity>>, SubscribeType> _createEventSubscriber;
		private Action<TypedEventHandler<Tservice, SaveEventArgs<Tentity>>, SubscribeType> _saveEventSubscriber;
		private Action<TypedEventHandler<Tservice, MoveEventArgs<Tentity>>, SubscribeType> _moveEventSubscriber;
		private Action<TypedEventHandler<Tservice, CopyEventArgs<Tentity>>, SubscribeType> _copyEventSubscriber;
		private Action<TypedEventHandler<IPublishingStrategy, PublishEventArgs<Tentity>>, SubscribeType> _publishEventSubscriber;
		private Action<TypedEventHandler<IPublishingStrategy, PublishEventArgs<Tentity>>, SubscribeType> _unpublishEventSubscriber;
		#endregion

		internal ModelEventHandler(
								 IContentTypeModuleBase contentTypeModuleBase,
								 Action<TypedEventHandler<Tservice, MoveEventArgs<Tentity>>, SubscribeType> trashEventSubscriber,
								 Action<TypedEventHandler<Tservice, DeleteEventArgs<Tentity>>, SubscribeType> deleteEventSubscriber,
						 		 Action<TypedEventHandler<Tservice, NewEventArgs<Tentity>>, SubscribeType> createEventSubscriber,
						 		 Action<TypedEventHandler<Tservice, SaveEventArgs<Tentity>>, SubscribeType> saveEventSubscriber,
						 		 Action<TypedEventHandler<Tservice, MoveEventArgs<Tentity>>, SubscribeType> moveEventSubscriber,
						    	 Action<TypedEventHandler<Tservice, CopyEventArgs<Tentity>>, SubscribeType> copyEventSubscriber,
								 Action<TypedEventHandler<IPublishingStrategy, PublishEventArgs<Tentity>>, SubscribeType> publishEventSubscriber,
								 Action<TypedEventHandler<IPublishingStrategy, PublishEventArgs<Tentity>>, SubscribeType> unpublishEventSubscriber,
                                 Func<IContentBase, ContentTypeRegistration, object> createInstance, 
								 Func<Tentity, string> getAliasFromContentInstance,
								 Action<Tentity, CodeFirstContentBase<Tnodedetails>, ContentTypeRegistration> mapModelToContent)
		{
			_contentTypeModuleBase = contentTypeModuleBase;
			_createInstance = createInstance;
			_getAliasFromContentInstance = getAliasFromContentInstance;
			_mapModelToContent = mapModelToContent;

			_trashEventSubscriber = trashEventSubscriber;
			_deleteEventSubscriber = deleteEventSubscriber;
			_createEventSubscriber = createEventSubscriber;
			_saveEventSubscriber = saveEventSubscriber;
			_moveEventSubscriber = moveEventSubscriber;
			_copyEventSubscriber = copyEventSubscriber;
			_publishEventSubscriber = publishEventSubscriber;
			_unpublishEventSubscriber = unpublishEventSubscriber;
        }

		internal void Initialise(IEnumerable<Type> types)
		{
			foreach (var type in types)
			{
				RegisterEvents<IOnCreateBase>(type, _onCreate);
				RegisterEvents<IOnSaveBase>(type, _onSave);
				RegisterEvents<IOnDeleteBase>(type, _onDelete);
				RegisterEvents<IOnMoveBase>(type, _onMove);
				RegisterEvents<IOnCopyBase>(type, _onCopy);
				RegisterEvents<IOnPublishBase>(type, _onPublish);
				RegisterEvents<IOnUnpublishBase>(type, _onUnpublish);
			}

			_trashEventSubscriber?.Invoke(Service_Trashing, SubscribeType.Subscribe);
			_saveEventSubscriber?.Invoke(Service_Saving, SubscribeType.Subscribe);
			_createEventSubscriber?.Invoke(Service_Created, SubscribeType.Subscribe);
			_deleteEventSubscriber?.Invoke(Service_Deleting, SubscribeType.Subscribe);
			_moveEventSubscriber?.Invoke(Service_Moving, SubscribeType.Subscribe);
			_copyEventSubscriber?.Invoke(Service_Copying, SubscribeType.Subscribe);
			_publishEventSubscriber?.Invoke(Service_Publishing, SubscribeType.Subscribe);
			_unpublishEventSubscriber?.Invoke(Service_UnPublishing, SubscribeType.Subscribe);
		}

		private void RegisterEvents<Tevent>(Type type, Dictionary<string, ContentTypeRegistration> register)
		{
			if (ModelEventDispatcher.HasEvent<Tevent>(type))
			{
				ContentTypeRegistration reg;
				if (_contentTypeModuleBase.TryGetContentType(type, out reg))
				{
					register.Add(reg.Alias, reg);
				}
			}
		}

		internal void Invalidate()
		{
			_trashEventSubscriber?.Invoke(Service_Trashing, SubscribeType.Unsubscribe);
			_saveEventSubscriber?.Invoke(Service_Saving, SubscribeType.Unsubscribe);
			_createEventSubscriber?.Invoke(Service_Created, SubscribeType.Unsubscribe);
			_deleteEventSubscriber?.Invoke(Service_Deleting, SubscribeType.Unsubscribe);
			_moveEventSubscriber?.Invoke(Service_Moving, SubscribeType.Unsubscribe);
			_copyEventSubscriber?.Invoke(Service_Copying, SubscribeType.Unsubscribe);
			_publishEventSubscriber?.Invoke(Service_Publishing, SubscribeType.Unsubscribe);
			_unpublishEventSubscriber?.Invoke(Service_UnPublishing, SubscribeType.Unsubscribe);
		}

		private void HandleEvent(Dictionary<string, ContentTypeRegistration> eventList, Tentity entity, CancellableEventArgs e, Func<object, IContentBase, HttpContextBase, UmbracoContext, ApplicationContext, bool> eventDispatcher)
		{
			if (eventList.ContainsKey(_getAliasFromContentInstance.Invoke(entity)) && CodeFirstManager.Current.Features.EnableContentEvents)
			{
				var instance = _createInstance.Invoke(entity, eventList[_getAliasFromContentInstance.Invoke(entity)]);
				(instance as CodeFirstContentBase).NodeDetails = (ContentNodeDetails)typeof(Tnodedetails).GetConstructor(new Type[] { entity.GetType() }).Invoke(new object[] { entity });
				if (e.CanCancel)
				{
					e.Cancel |= !eventDispatcher.Invoke(instance, entity, new HttpContextWrapper(HttpContext.Current), UmbracoContext.Current, ApplicationContext.Current);
				}
				else
				{
					eventDispatcher.Invoke(instance, entity, new HttpContextWrapper(HttpContext.Current), UmbracoContext.Current, ApplicationContext.Current);
				}
				_mapModelToContent.Invoke(entity, (instance as CodeFirstContentBase<Tnodedetails>), eventList[_getAliasFromContentInstance.Invoke(entity)]);
			}
		}

		#region Event Handlers
		private void Service_Saving(Tservice sender, SaveEventArgs<Tentity> e)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				lock (_onSave)
				{
					foreach (var entity in e.SavedEntities)
					{
						HandleEvent(_onSave, entity, e, ModelEventDispatcher.OnSaveObject);
                    }
				}
			}
		}

		private void Service_Created(Tservice sender, NewEventArgs<Tentity> e)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				lock (_onCreate)
				{
					HandleEvent(_onCreate, e.Entity, e, ModelEventDispatcher.OnCreateObject);
				}
			}
		}

		private void Service_Trashing(Tservice sender, MoveEventArgs<Tentity> e)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				lock (_onDelete)
				{
					foreach (var entity in e.MoveInfoCollection.Select(x => x.Entity))
					{
						HandleEvent(_onDelete, entity, e, ModelEventDispatcher.OnDeleteObject);
					}
				}
			}
		}

		private void Service_UnPublishing(IPublishingStrategy sender, PublishEventArgs<Tentity> e)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				lock (_onUnpublish)
				{
					foreach (var entity in e.PublishedEntities)
					{
						HandleEvent(_onUnpublish, entity, e, ModelEventDispatcher.OnUnpublishObject);
					}
				}
			}
		}

		private void Service_Publishing(IPublishingStrategy sender, PublishEventArgs<Tentity> e)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				lock (_onPublish)
				{
					foreach (var entity in e.PublishedEntities)
					{
						HandleEvent(_onPublish, entity, e, ModelEventDispatcher.OnPublishObject);
					}
				}
			}
		}

		private void Service_Copying(Tservice sender, CopyEventArgs<Tentity> e)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				lock (_onCopy)
				{
					HandleEvent(_onCopy, e.Copy, e, ModelEventDispatcher.OnCopyObject);
				}
			}
		}

		private void Service_Moving(Tservice sender, MoveEventArgs<Tentity> e)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				lock (_onMove)
				{
					foreach (var entity in e.MoveInfoCollection.Select(x => x.Entity))
					{
						HandleEvent(_onMove, entity, e, ModelEventDispatcher.OnMoveObject);
					}
				}
			}
		}

		private void Service_Deleting(Tservice sender, DeleteEventArgs<Tentity> e)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				lock (_onDelete)
				{
					foreach (var entity in e.DeletedEntities)
					{
						HandleEvent(_onDelete, entity, e, ModelEventDispatcher.OnDeleteObject);
					}
				}
			}
		}
		#endregion
	}
}
