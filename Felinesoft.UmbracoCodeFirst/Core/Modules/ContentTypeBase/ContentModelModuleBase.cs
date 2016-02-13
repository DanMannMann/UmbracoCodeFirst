using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Converters;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Core;
using System.Collections.Generic;
using Marsman.Reflekt;
using Felinesoft.UmbracoCodeFirst.Events;
using Umbraco.Core.Services;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using Umbraco.Core.Events;
using System.Web;
using Umbraco.Core.Publishing;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
	public enum SubscribeType
	{
		Subscribe,
		Unsubscribe
	}

    public class ContentModelModuleBase<T,Tservice,Tentity> : ICodeFirstEntityModule, IContentModelModule where T : ContentNodeDetails, new() where Tentity : IContentBase
    {
        private MethodInfo _convertToModelGenericMethod;
        private ConcurrentDictionary<Type, MethodInfo> _runtimeConvertToModelMethods = new ConcurrentDictionary<Type, MethodInfo>();
        private IDataTypeModule _dataTypeModule;
        private IContentTypeModuleBase _contentTypeModule;
		private ModelEventHandler<Tservice, Tentity, T> _eventHandler;

		public ContentModelModuleBase(IDataTypeModule dataTypeModule, 
									  IContentTypeModuleBase contentTypeModule,
									  Func<Tentity,string> aliasGetter,
									  Action<TypedEventHandler<Tservice, MoveEventArgs<Tentity>>, SubscribeType> trashEventSubscriber,
									  Action<TypedEventHandler<Tservice, DeleteEventArgs<Tentity>>, SubscribeType> deleteEventSubscriber,
									  Action<TypedEventHandler<Tservice, NewEventArgs<Tentity>>, SubscribeType> createEventSubscriber,
									  Action<TypedEventHandler<Tservice, SaveEventArgs<Tentity>>, SubscribeType> saveEventSubscriber,
									  Action<TypedEventHandler<Tservice, MoveEventArgs<Tentity>>, SubscribeType> moveEventSubscriber,
									  Action<TypedEventHandler<Tservice, CopyEventArgs<Tentity>>, SubscribeType> copyEventSubscriber,
									  Action<TypedEventHandler<IPublishingStrategy, PublishEventArgs<Tentity>>, SubscribeType> publishEventSubscriber,
									  Action<TypedEventHandler<IPublishingStrategy, PublishEventArgs<Tentity>>, SubscribeType> unpublishEventSubscriber)
        {
            _dataTypeModule = dataTypeModule;
            _contentTypeModule = contentTypeModule;

			_eventHandler = new ModelEventHandler<Tservice, Tentity, T>(
				contentTypeModule,
				trashEventSubscriber,
				deleteEventSubscriber,
				createEventSubscriber,
				saveEventSubscriber,
				moveEventSubscriber,
				copyEventSubscriber,
				publishEventSubscriber,
				unpublishEventSubscriber,
				(x, y) => CreateInstanceFromContent(x, y),
				aliasGetter,
				(x, y, z) => MapModelToContent(x, y, z)
			);
		}

		#region Events
		public virtual void Initialise(IEnumerable<Type> types)
		{
			CodeFirstManager.Invalidating += CodeFirstManager_Invalidating;
			_eventHandler.Initialise(types);
		}

		void CodeFirstManager_Invalidating(object sender, InvalidatingEventArgs e)
		{
			_eventHandler.Invalidate();
        }
		#endregion

		#region IContentModelModule
		public object ConvertToModel(IPublishedContent content, CodeFirstModelContext parentContext = null)
        {
            ContentTypeRegistration docType;
            if (_contentTypeModule.TryGetContentType(content.DocumentTypeAlias, out docType))
            {
                MethodInfo convertToModel = GetConvertToModelMethod(docType.ClrType);
                return convertToModel.Invoke(this, new object[] { content, parentContext });
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Convert Content to Model
        /// <summary>
        /// Extension used to convert an IPublishedContent back to a Typed model instance.
        /// Your model does need to inherit from UmbracoGeneratedBase and contain the correct attributes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        protected Tmodel ConvertToModelInternal<Tmodel>(IPublishedContent content, CodeFirstModelContext parentContext = null) where Tmodel : CodeFirstContentBase<T>
        {
            ContentTypeRegistration registration;

            if (content == null)
            {
                throw new ArgumentNullException("content", Environment.StackTrace);
            }

            if (!_contentTypeModule.TryGetContentType(content.ContentType.Alias, out registration))
            {
                throw new CodeFirstException("Could not find content type registration for content type alias " + content.ContentType.Alias);
            }
            if (registration.ClrType != typeof(Tmodel))
            {
                if (registration.ClrType.Inherits(typeof(Tmodel)))
                {
                    //Redirect to the underlying type and make one of those instead
                    if (!_contentTypeModule.TryGetContentType(typeof(Tmodel), out registration))
                    {
                        throw new CodeFirstException("Could not find content type registration for underlying type " + typeof(Tmodel).FullName);
                    }
                }
                else
                {
                    throw new CodeFirstException("Registered type for content type " + content.ContentType.Alias + " is " + registration.ClrType.Name + ", not " + typeof(Tmodel).Name);
                }
            }

            Tmodel instance = (Tmodel)CreateInstanceFromPublishedContent(content, registration, parentContext);

            if (instance == null)
            {
                throw new CodeFirstException("Model could not be created. Target type: " + typeof(Tmodel).Name);
            }

            if ((instance as CodeFirstContentBase<T>) == null)
            {
                throw new CodeFirstException("Created model could not be cast to CodeFirstContentBase<T>. Type of T: " + typeof(T).Name);
            }

            (instance as CodeFirstContentBase<T>).NodeDetails = new T();
            ((instance as CodeFirstContentBase<T>).NodeDetails as ContentNodeDetails).Initialise(content);
			ModelEventDispatcher<Tmodel>.OnLoad(instance, content, new HttpContextWrapper(HttpContext.Current), UmbracoContext.Current, ApplicationContext.Current, CodeFirstModelContext.GetContext(instance));
			return instance;
        }

        protected bool TryConvertToModelInternal<Tmodel>(IPublishedContent content, out Tmodel model) where Tmodel : CodeFirstContentBase<T>
        {
            ContentTypeRegistration docType;
            if (_contentTypeModule.TryGetContentType(content.ContentType.Alias, out docType) && docType.ClrType == typeof(Tmodel))
            {
                try
                {
                    model = ConvertToModelInternal<Tmodel>(content);
                    return true;
                }
                catch (Exception ex)
                {
					CodeFirstManager.Current.Warn("TryConvertToModel failed. Exception: " + ex, this);
                    model = default(Tmodel);
                    return false;
                }
            }
            else
            {
                model = default(Tmodel);
                return false;
            }
        }

        protected object CreateInstanceFromPublishedContent(IPublishedContent content, ContentTypeRegistration registration, CodeFirstModelContext parentContext = null)
        {
            Dictionary<PropertyInfo, CodeFirstLazyInitialiser> dict;
            var instance = CodeFirstModelContext.CreateContextualInstance(registration.ClrType, registration, out dict, parentContext);

            //properties on Generic Tab
            foreach (var property in registration.Properties)
            {
                if (CodeFirstManager.Current.Features.UseLazyLoadingProxies && property.Metadata.GetGetMethod().IsVirtual)
                {
                    dict.Add(property.Metadata, new CodeFirstLazyInitialiser(() => CopyPropertyValueToModel(content, instance, property)));
                }
                else
                {
                    CopyPropertyValueToModel(content, instance, property);
                }
            }

            foreach (var tab in registration.Tabs)
            {
                //tab instance
                Dictionary<PropertyInfo, CodeFirstLazyInitialiser> tabDict;
                var tabInstance = CodeFirstModelContext.CreateContextualInstance(tab.ClrType, tab, out tabDict);
                foreach (var property in tab.Properties)
                {
                    if (CodeFirstManager.Current.Features.UseLazyLoadingProxies && property.Metadata.GetGetMethod().IsVirtual)
                    {
                        tabDict.Add(property.Metadata, new CodeFirstLazyInitialiser(() => CopyPropertyValueToModel(content, tabInstance, property)));
                    }
                    else
                    {
                        CopyPropertyValueToModel(content, tabInstance, property);
                    }
                }
                tab.PropertyOfParent.SetValue(instance, tabInstance);
            }

            foreach (var composition in registration.Compositions)
            {
                var parent = CodeFirstModelContext.GetCompositionParentContext(composition);
                if (CodeFirstManager.Current.Features.UseLazyLoadingProxies && composition.PropertyOfContainer.GetGetMethod().IsVirtual)
                {
                    dict.Add(composition.PropertyOfContainer, new CodeFirstLazyInitialiser(() => composition.PropertyOfContainer.SetValue(instance, CreateInstanceFromPublishedContent(content, composition, parent))));
                }
                else
                {
                    composition.PropertyOfContainer.SetValue(instance, CreateInstanceFromPublishedContent(content, composition, parent));
                }
            }

            CodeFirstModelContext.ResetContext();
            return instance;
        }

        protected object CreateInstanceFromContent(IContentBase content, ContentTypeRegistration registration, CodeFirstModelContext parentContext = null)
        {
            Dictionary<PropertyInfo, CodeFirstLazyInitialiser> dict;
            var instance = CodeFirstModelContext.CreateContextualInstance(registration.ClrType, registration, out dict, parentContext, false);

            //properties on Generic Tab
            foreach (var property in registration.Properties)
            {
                CopyPropertyValueToModel(content, instance, property);
            }

            foreach (var tab in registration.Tabs)
            {
                //tab instance
                Dictionary<PropertyInfo, CodeFirstLazyInitialiser> tabDict;
                var tabInstance = CodeFirstModelContext.CreateContextualInstance(tab.ClrType, tab, out tabDict);
                foreach (var property in tab.Properties)
                {
                    CopyPropertyValueToModel(content, tabInstance, property);
                }
                tab.PropertyOfParent.SetValue(instance, tabInstance);
            }

            foreach (var composition in registration.Compositions)
            {
                CodeFirstModelContext.MoveNextContext(instance, composition);
                composition.PropertyOfContainer.SetValue(instance, CreateInstanceFromContent(content, composition, CodeFirstModelContext.GetContext(instance)));
            }

            return instance;
        }

        private void CopyPropertyValueToModel(IContentBase content, object objectInstance, PropertyRegistration registration)
        {
            var umbracoStoredValue = content.GetValue(registration.Alias);
            SetPropertyValueOnModel(objectInstance, registration, umbracoStoredValue);
        }

        private void CopyPropertyValueToModel(IPublishedContent content, object objectInstance, PropertyRegistration registration)
        {
            object umbracoStoredValue = null;
            var converterType = registration.DataType.ConverterType;

            //If a data type converter is used then bypass Umbraco's property value converter
            if (converterType != null)
            {
                var property = content.GetProperty(registration.Alias);
				
                object propertyValue;
                if (TryGetPropertyAsDbType(property, registration.DataType.DbType, out propertyValue, registration.DataType.DataTypeInstanceName))
                {
                    //Always prefer the underlying value, cast to the database type (int, string or DateTime), as this
                    //matches the type of the property on an instance of IContent, therefore allowing converters to be used for
                    //both IContent and IPublishedContent
                    umbracoStoredValue = propertyValue;
                }
                else
                {
                    umbracoStoredValue = content.GetPropertyValue(registration.Alias);
                }
            }
            else
            {
                umbracoStoredValue = content.GetPropertyValue(registration.Alias);
            }

            SetPropertyValueOnModel(objectInstance, registration, umbracoStoredValue);
        }

        private bool TryGetPropertyAsDbType(IPublishedProperty property, DatabaseType type, out object propertyValue, string dataTypeName)
        {
            switch (type)
            {
                case DatabaseType.Date:
					propertyValue = property.GetValue<DateTime>();
					return true;

                case DatabaseType.Integer:
					propertyValue = property.GetValue<int>();
					return true;
				case DatabaseType.Ntext:
                case DatabaseType.Nvarchar:
                default:
					propertyValue = property.DataValue;
					return true;
			}
        }

        private void SetPropertyValueOnModel(object objectInstance, PropertyRegistration registration, object umbracoStoredValue)
        {
            if (registration.DataType.ConverterType != null)
            {
                IDataTypeConverter converter = (IDataTypeConverter)Activator.CreateInstance(registration.DataType.ConverterType);
                var val = converter.Create(umbracoStoredValue, x => CodeFirstModelContext.MoveNextContext(x, registration));
                if (val != null)
                {
                    CodeFirstModelContext.MoveNextContext(val, registration);
                    var attr = registration.Metadata.GetCodeFirstAttribute<ContentPropertyAttribute>();
                    if (attr != null && attr is IDataTypeRedirect)
                    {
                        val = (attr as IDataTypeRedirect).GetRedirectedValue(val);
                        if (val != null)
                        {
                            //Keep a second context so wrapped types can still find their property 
                            //Will add nothing if the Redirector registered a context already (e.g. called ConvertToModel to create the value).
                            //Hopefully said redirector passed in a parent context so the converted value can still find its way back here.
                            CodeFirstModelContext.MoveNextContext(val, registration); 
                        }
                    }
                    registration.Metadata.SetValue(objectInstance, val);
                }
            }
            else if (umbracoStoredValue != null)
            {
                //Context currently not supported for PEVCs - many are value types, very unlikely to have unique hashes for all values in a request context, none except custom ones would have
                //code to use the context anyway.
                registration.Metadata.SetValue(objectInstance, umbracoStoredValue);
            }
        }

        private MethodInfo GetConvertToModelMethod(Type docType)
        {
            if (!_runtimeConvertToModelMethods.ContainsKey(docType))
            {
                if (_convertToModelGenericMethod == null)
                {
                    _convertToModelGenericMethod = this.Reflekt()
                                                       .method<Type1>()
                                                       .GenericDefinition() //Get this as a generic method definition, discarding the references to Type1
                                                       .Parameters<IPublishedContent, CodeFirstModelContext>(x => x.ConvertToModelInternal<Type1>);
                }
                var convertToModel = _convertToModelGenericMethod.MakeGenericMethod(docType);
                _runtimeConvertToModelMethods.TryAdd(docType, convertToModel);
            }
            return _runtimeConvertToModelMethods[docType];
        }

        private class Type1 : CodeFirstContentBase<T> { }
        #endregion

        #region Convert Model to Content
        protected void MapModelToContent(IContentBase node, CodeFirstContentBase<T> model, ContentTypeRegistration registration, bool firstLevel = true)
        {
            if (firstLevel)
            {
                node.Name = model.NodeDetails.Name;
                node.SortOrder = model.NodeDetails.SortOrder;
            }

            foreach (var prop in registration.Properties)
            {
				var val = prop.Metadata.GetValue(model);
				if (val != null)
				{
					SetPropertyOnContent(node, prop, val);
				}
            }

            //Get and then set all the properties from any tabs
            var tabs = registration.Tabs;
            foreach (var tab in tabs)
            {
                var tabInstance = tab.PropertyOfParent.GetValue(model);

                if (tabInstance != null)
                {
                    foreach (var prop in tab.Properties)
                    {
						var val = prop.Metadata.GetValue(tabInstance);
						if (val != null)
						{
							SetPropertyOnContent(node, prop, val);
						}
                    }
                }
            }

            foreach (var comp in registration.Compositions)
            {
                MapModelToContent(node, (CodeFirstContentBase<T>)comp.PropertyOfContainer.GetValue(model), comp, false);
            }
        }

        protected void SetPropertyOnContent(IContentBase content, PropertyRegistration property, object propertyValue)
        {
            object convertedValue;
            if (property.DataType.ConverterType != null)
            {
                object toConvert;
                var attr = property.Metadata.GetCodeFirstAttribute<ContentPropertyAttribute>();
                
                if (attr != null && attr is IDataTypeRedirect)
                {
                    toConvert = (attr as IDataTypeRedirect).GetOriginalDataTypeObject(propertyValue);
                    if (toConvert != null)
                    {
                        //Keep a second context so wrapped types can still find their property 
                        //Will add nothing if the Redirector registered a context already (e.g. called ConvertToModel to create the value).
                        //Hopefully said redirector passed in a parent context so the converted value can still find its way back here.
                        CodeFirstModelContext.MoveNextContext(toConvert, property);
                    }
                }
                else
                {
                    toConvert = propertyValue;
                }

                IDataTypeConverter converter = (IDataTypeConverter)Activator.CreateInstance(property.DataType.ConverterType);
                convertedValue = converter.Serialise(toConvert);
            }
            else if (!property.DataType.CodeFirstControlled && property.DataType.DbType == DatabaseType.None)
            {
                throw new CodeFirstException("Cannot persist PEVC-based properties or use events which attempt to persist PEVC-based properties. " + Environment.NewLine +  
											 "PEVCs only support retrieving a value from IPublishedContent & cannot persist a property back to IContent." + Environment.NewLine +
											 "Property: " + property.Metadata.DeclaringType.FullName + "." + property.Name);
            }
            else
            {
                //No converter is given so we push the value back into umbraco as is (this will fail in many cases for PEVC properties)
                convertedValue = propertyValue;
            }

            content.SetValue(property.Alias, convertedValue);
        }
        #endregion
    }
}