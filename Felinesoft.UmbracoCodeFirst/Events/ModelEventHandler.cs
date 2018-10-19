using Marsman.UmbracoCodeFirst.ContentTypes;
using Marsman.UmbracoCodeFirst.Core.Modules;
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

namespace Marsman.UmbracoCodeFirst.Events
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

		private Dictionary<string, ContentTypeRegistration> _onSaved = new Dictionary<string, ContentTypeRegistration>();
		private Dictionary<string, ContentTypeRegistration> _onDeleted = new Dictionary<string, ContentTypeRegistration>();
		private Dictionary<string, ContentTypeRegistration> _onPublished = new Dictionary<string, ContentTypeRegistration>();
		private Dictionary<string, ContentTypeRegistration> _onUnpublished = new Dictionary<string, ContentTypeRegistration>();
		private Dictionary<string, ContentTypeRegistration> _onCopied = new Dictionary<string, ContentTypeRegistration>();
		private Dictionary<string, ContentTypeRegistration> _onMoved = new Dictionary<string, ContentTypeRegistration>();

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
		private Action<TypedEventHandler<Tservice, MoveEventArgs<Tentity>>, SubscribeType> _trashedEventSubscriber;
		private Action<TypedEventHandler<IPublishingStrategy, PublishEventArgs<Tentity>>, SubscribeType> _unpublishedEventSubscriber;
		private Action<TypedEventHandler<Tservice, DeleteEventArgs<Tentity>>, SubscribeType> _deletedEventSubscriber;
		private Action<TypedEventHandler<Tservice, SaveEventArgs<Tentity>>, SubscribeType> _savedEventSubscriber;
		private Action<TypedEventHandler<Tservice, MoveEventArgs<Tentity>>, SubscribeType> _movedEventSubscriber;
		private Action<TypedEventHandler<Tservice, CopyEventArgs<Tentity>>, SubscribeType> _copiedEventSubscriber;
		private Action<TypedEventHandler<IPublishingStrategy, PublishEventArgs<Tentity>>, SubscribeType> _publishedEventSubscriber;
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
								 Action<TypedEventHandler<Tservice, MoveEventArgs<Tentity>>, SubscribeType> trashedEventSubscriber,
								 Action<TypedEventHandler<Tservice, DeleteEventArgs<Tentity>>, SubscribeType> deletedEventSubscriber,
						 		 Action<TypedEventHandler<Tservice, SaveEventArgs<Tentity>>, SubscribeType> savedEventSubscriber,
						 		 Action<TypedEventHandler<Tservice, MoveEventArgs<Tentity>>, SubscribeType> movedEventSubscriber,
								 Action<TypedEventHandler<Tservice, CopyEventArgs<Tentity>>, SubscribeType> copiedEventSubscriber,
								 Action<TypedEventHandler<IPublishingStrategy, PublishEventArgs<Tentity>>, SubscribeType> publishedEventSubscriber,
								 Action<TypedEventHandler<IPublishingStrategy, PublishEventArgs<Tentity>>, SubscribeType> unpublishedEventSubscriber,
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

			_trashedEventSubscriber = trashedEventSubscriber;
			_deletedEventSubscriber = deletedEventSubscriber;
			_savedEventSubscriber = savedEventSubscriber;
			_movedEventSubscriber = movedEventSubscriber;
			_copiedEventSubscriber = copiedEventSubscriber;
			_publishedEventSubscriber = publishedEventSubscriber;
			_unpublishedEventSubscriber = unpublishedEventSubscriber;
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

				RegisterEvents<IOnSavedBase>(type, _onSaved);
				RegisterEvents<IOnDeletedBase>(type, _onDeleted);
				RegisterEvents<IOnMovedBase>(type, _onMoved);
				RegisterEvents<IOnCopiedBase>(type, _onCopied);
				RegisterEvents<IOnPublishedBase>(type, _onPublished);
				RegisterEvents<IOnUnpublishedBase>(type, _onUnpublished);
			}

			_trashEventSubscriber?.Invoke(Service_Trashing, SubscribeType.Subscribe);
			_saveEventSubscriber?.Invoke(Service_Saving, SubscribeType.Subscribe);
			_createEventSubscriber?.Invoke(Service_Created, SubscribeType.Subscribe);
			_deleteEventSubscriber?.Invoke(Service_Deleting, SubscribeType.Subscribe);
			_moveEventSubscriber?.Invoke(Service_Moving, SubscribeType.Subscribe);
			_copyEventSubscriber?.Invoke(Service_Copying, SubscribeType.Subscribe);
			_publishEventSubscriber?.Invoke(Service_Publishing, SubscribeType.Subscribe);
			_unpublishEventSubscriber?.Invoke(Service_UnPublishing, SubscribeType.Subscribe);

			_trashedEventSubscriber?.Invoke(Service_Trashed, SubscribeType.Subscribe);
			_savedEventSubscriber?.Invoke(Service_Saved, SubscribeType.Subscribe);
			_deletedEventSubscriber?.Invoke(Service_Deleted, SubscribeType.Subscribe);
			_movedEventSubscriber?.Invoke(Service_Moved, SubscribeType.Subscribe);
			_copiedEventSubscriber?.Invoke(Service_Copied, SubscribeType.Subscribe);
			_publishedEventSubscriber?.Invoke(Service_Published, SubscribeType.Subscribe);
			_unpublishedEventSubscriber?.Invoke(Service_UnPublished, SubscribeType.Subscribe);
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

			_trashedEventSubscriber?.Invoke(Service_Trashed, SubscribeType.Unsubscribe);
			_savedEventSubscriber?.Invoke(Service_Saved, SubscribeType.Unsubscribe);
			_deletedEventSubscriber?.Invoke(Service_Deleted, SubscribeType.Unsubscribe);
			_movedEventSubscriber?.Invoke(Service_Moved, SubscribeType.Unsubscribe);
			_copiedEventSubscriber?.Invoke(Service_Copied, SubscribeType.Unsubscribe);
			_publishedEventSubscriber?.Invoke(Service_Published, SubscribeType.Unsubscribe);
			_unpublishedEventSubscriber?.Invoke(Service_UnPublished, SubscribeType.Unsubscribe);
		}

		private void HandleEvent(Dictionary<string, ContentTypeRegistration> eventList, Tentity entity, CancellableEventArgs e, Func<object, IContentBase, HttpContextBase, UmbracoContext, ApplicationContext, CancellableEventArgs, bool> eventDispatcher)
		{
			if (eventList.ContainsKey(_getAliasFromContentInstance.Invoke(entity)) && CodeFirstManager.Current.Features.EnableContentEvents)
			{
				var instance = _createInstance.Invoke(entity, eventList[_getAliasFromContentInstance.Invoke(entity)]);
				(instance as CodeFirstContentBase).NodeDetails = (ContentNodeDetails)typeof(Tnodedetails).GetConstructor(new Type[] { entity.GetType() }).Invoke(new object[] { entity });
				if (e.CanCancel)
				{
					
					e.Cancel |= !eventDispatcher.Invoke(instance, entity, new HttpContextWrapper(HttpContext.Current), UmbracoContext.Current, ApplicationContext.Current, e);
				}
				else
				{
					eventDispatcher.Invoke(instance, entity, new HttpContextWrapper(HttpContext.Current), UmbracoContext.Current, ApplicationContext.Current, e);
				}
				_mapModelToContent.Invoke(entity, (instance as CodeFirstContentBase<Tnodedetails>), eventList[_getAliasFromContentInstance.Invoke(entity)]);
			}
		}

		#region Event Handlers


		private void Service_UnPublished(IPublishingStrategy sender, PublishEventArgs<Tentity> e)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				lock (_onUnpublished)
				{
					foreach (var entity in e.PublishedEntities)
					{
						HandleEvent(_onUnpublished, entity, e, ModelEventDispatcher.OnUnpublishedObject);
					}
				}
			}
		}

		private void Service_Published(IPublishingStrategy sender, PublishEventArgs<Tentity> e)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				lock (_onPublished)
				{
					foreach (var entity in e.PublishedEntities)
					{
						HandleEvent(_onPublished, entity, e, ModelEventDispatcher.OnPublishObject);
					}
				}
			}
		}

		private void Service_Copied(Tservice sender, CopyEventArgs<Tentity> e)
		{

			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				lock (_onCopied)
				{
					HandleEvent(_onCopied, e.Copy, e, ModelEventDispatcher.OnCopiedObject);
				}
			}
		}

		private void Service_Moved(Tservice sender, MoveEventArgs<Tentity> e)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				lock (_onMoved)
				{
					foreach (var entity in e.MoveInfoCollection.Select(x => x.Entity))
					{
						HandleEvent(_onMoved, entity, e, ModelEventDispatcher.OnMovedObject);
					}
				}
			}
		}

		private void Service_Deleted(Tservice sender, DeleteEventArgs<Tentity> e)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				lock (_onDeleted)
				{
					foreach (var entity in e.DeletedEntities)
					{
						HandleEvent(_onDeleted, entity, e, ModelEventDispatcher.OnDeleteObject);
					}
				}
			}
		}

		private void Service_Saved(Tservice sender, SaveEventArgs<Tentity> e)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				lock (_onSaved)
				{
					foreach (var entity in e.SavedEntities)
					{
						HandleEvent(_onSaved, entity, e, ModelEventDispatcher.OnSavedObject);
					}
				}
			}
		}

		private void Service_Trashed(Tservice sender, MoveEventArgs<Tentity> e)
		{
			if (CodeFirstManager.Current.Features.EnableContentEvents)
			{
				lock (_onDeleted)
				{
					foreach (var entity in e.MoveInfoCollection.Select(x => x.Entity))
					{
						HandleEvent(_onDeleted, entity, e, ModelEventDispatcher.OnDeletedObject);
					}
				}
			}
		}

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
