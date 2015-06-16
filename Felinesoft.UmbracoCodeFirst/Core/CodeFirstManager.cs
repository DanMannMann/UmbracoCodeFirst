using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using Umbraco.Core;
using Felinesoft.UmbracoCodeFirst.Attributes;
using System.Web.Hosting;
using Umbraco.Core.Models;
using Umbraco.Core.Models.Membership;
using Felinesoft.UmbracoCodeFirst.Content;
using System.Collections.ObjectModel;
using Felinesoft.UmbracoCodeFirst.DocumentTypes;
using Felinesoft.InitialisableAttributes;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Umbraco.Core.Services;
using System.Text;
using System.Globalization;
using System.Collections.Concurrent;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Felinesoft.UmbracoCodeFirst.Converters;
using Felinesoft.UmbracoCodeFirst.Content.Factories;
using Felinesoft.UmbracoCodeFirst.Content.Interfaces;
using System.Configuration;
using Felinesoft.UmbracoCodeFirst.T4Generators;
using Felinesoft.UmbracoCodeFirst.DataTypes;

namespace Felinesoft.UmbracoCodeFirst
{
    /// <summary>
    /// Manages the UmbracoCodeFirst core, allowing data type registration and content type and instance discovery and creation.
    /// </summary>
    /// <example>
    /// <code>
    ///   protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
    ///   {
    ///       base.ApplicationStarted(umbracoApplication, applicationContext);
    ///       
    ///       //Initialise code-first using the types in the current assembly
    ///       Felinesoft.UmbracoCodeFirst.CodeFirstManager.Current.Initialise(this.GetType().Assembly);
    ///    }
    /// </code>
    /// </example>
    public class CodeFirstManager
    {
        private IDefaultContentManager _defaultContentManager;
        private const string ViewsFolderDefaultLocation = "~/Views";
        private static CodeFirstManager _current;
        private static object _managerLock = new object();
        private object _documentTypeBasesLock = new object();
        private List<Type> _documentTypeBases = new List<Type>();

        internal ReadOnlyCollection<Type> DocumentTypeBases
        {
            get
            {
                lock (_documentTypeBasesLock)
                {
                    return _documentTypeBases.Select(x => x).ToList().AsReadOnly(); //return a copy of the list frozen at the point of access
                }
            }
        }

        public void AddDocumentTypeBase<T>() where T : DocumentTypeBase
        {
            lock (_documentTypeBasesLock)
            {
                _documentTypeBases.Add(typeof(T));
            }
        }

        /// <summary>
        /// Constructs the singleton instance
        /// </summary>
        /// <param name="contentManager">The <see cref="IDefaultContentManager"/> to use for creating and retrieving content instances</param>
        private CodeFirstManager(IDefaultContentManager contentManager)
        {
            _defaultContentManager = contentManager;
            _documentTypeBases.Add(typeof(DocumentTypeBase));
            _documentTypeBases.Add(typeof(ListViewDocumentType<>));
        }

        /// <summary>
        /// Gets the current singleton instance of <see cref="CodeFirstManager"/>
        /// </summary>
        public static CodeFirstManager Current
        {
            get
            {
                lock (_managerLock)
                {
                    if (_current == null)
                    {
                        _current = new CodeFirstManager(new DefaultContentManager());
                    }
                }
                return _current;
            }
        }

        #region Entry Points
        /// <summary>
        /// Scans the supplied collection of assemblies for code-first document types, media types, data types and document instances.
        /// All items are added or updated before control is returned; after running this method all code-first items found in the collection should exist in Umbraco.
        /// It is important to include *all* required elements in a single call to initialise; custom data types used within a document type must be available when the document type is created.
        /// </summary>
        /// <param name="assemblies">The assemblies to scan</param>
        /// <param name="refreshCache">True to refresh the Umbraco XML cache after creating document instances</param>
        public void Initialise(IEnumerable<Assembly> assemblies, bool refreshCache = true)
        {
            Initialise(
                assemblies
                    .AsParallel()
                    .SelectMany(x => x.GetTypes().AsParallel().Where(y => y.GetCustomAttribute(typeof(ContentFactoryAttribute), false) != null).AsSequential())
                    .AsSequential(),
                assemblies
                    .AsParallel()
                    .SelectMany(x => x.GetTypes().AsParallel().Where(y => y.GetCustomAttribute(typeof(DocumentTypeAttribute)) != null).AsSequential())
                    .AsSequential(),
                assemblies
                    .AsParallel()
                    .SelectMany(x => x.GetTypes().AsParallel().Where(y => y.GetCustomAttribute(typeof(DataTypeAttribute)) != null).AsSequential())
                    .AsSequential(),
                assemblies
                    .AsParallel()
                    .SelectMany(x => x.GetTypes().AsParallel().Where(y => y.GetCustomAttribute(typeof(MediaTypeAttribute)) != null).AsSequential())
                    .AsSequential(),
                refreshCache);
        }

        /// <summary>
        /// Scans the supplied assembly for code-first document types, media types, data types and document instances.
        /// All items are added or updated before control is returned; after running this method all code-first items found in the assembly should exist in Umbraco.
        /// It is important to include *all* required elements in a single call to initialise; custom data types used within a document type must be available when the document type is created.
        /// </summary>
        /// <param name="assembly">The assembly to scan</param>
        /// <param name="refreshCache">True to refresh the Umbraco XML cache after creating document instances</param>
        public void Initialise(Assembly assembly, bool refreshCache = true)
        {
            Initialise(
                assembly
                    .GetTypes()
                    .AsParallel()
                    .Where(y => y.GetCustomAttribute(typeof(ContentFactoryAttribute), false) != null)
                    .AsSequential(),
                assembly
                    .GetTypes()
                    .AsParallel()
                    .Where(y => y.GetCustomAttribute(typeof(DocumentTypeAttribute)) != null)
                    .AsSequential(),
                assembly
                    .GetTypes()
                    .AsParallel()
                    .Where(y => y.GetCustomAttribute(typeof(DataTypeAttribute)) != null)
                    .AsSequential(),
                assembly
                    .GetTypes()
                    .AsParallel()
                    .Where(y => y.GetCustomAttribute(typeof(MediaTypeAttribute)) != null)
                    .AsSequential(),
                refreshCache);
        }

        /// <summary>
        /// Scans the supplied collection of types for code-first document types, media types, data types and document instances.
        /// All items are added or updated before control is returned; after running this method all code-first items found in the collection should exist in Umbraco.
        /// It is important to include *all* required elements in a single call to initialise; custom data types used within a document type must be available when the document type is created.
        /// </summary>
        /// <param name="types">The types to scan</param>
        /// <param name="refreshCache">True to refresh the Umbraco XML cache after creating document instances</param>
        public void Initialise(IEnumerable<Type> types, bool refreshCache = true)
        {
            Initialise(
                types.Where(y => y.GetCustomAttribute(typeof(ContentFactoryAttribute), false) != null),
                types.Where(y => y.GetCustomAttribute(typeof(DocumentTypeAttribute)) != null),
                types.Where(y => y.GetCustomAttribute(typeof(DataTypeAttribute)) != null),
                types.Where(y => y.GetCustomAttribute(typeof(MediaTypeAttribute)) != null),
                refreshCache);
        }

        /// <summary>
        /// Initialises the specified items. 
        /// All items are added or updated before control is returned; after running this method all code-first items found in the collection should exist in Umbraco.
        /// It is important to include *all* required elements in a single call to initialise; custom data types used within a document type must be available when the document type is created.
        /// </summary>
        /// <param name="contentTypes">A collection of types which have default content</param>
        /// <param name="docTypes">A collection of types which inherit DocumentTypeBase and represent Umbraco document types</param>
        /// <param name="mediaTypes">A collection of types which represent Umbraco media types</param>
        /// <param name="dataTypes">A collection of types which implement IUmbracoDataType[T] and represent Umbraco data types</param>
        /// <param name="refreshCache">True to refresh the Umbraco XML cache after creating document instances</param>
        /// <param name="sort">True to automatically sort the elements by inheritance. Unsorted collections will usually fail, as dependencies will not be available when needed.</param>
        public void Initialise(IEnumerable<Type> contentTypes, IEnumerable<Type> docTypes, IEnumerable<Type> dataTypes, IEnumerable<Type> mediaTypes, bool refreshCache = true, bool sort = true)
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains("GenerateCodeFirstTypes") && bool.Parse(ConfigurationManager.AppSettings["GenerateCodeFirstTypes"]))
            {
                DataTypeClassGenerator.GenerateDataTypes();
                DocumentTypeClassGenerator.GenerateDocTypes();
            }

            //pre-register the built-in datatypes
            var builtInTypes = typeof(CodeFirstManager).Assembly.GetTypes().Where(x => x.GetCustomAttribute<BuiltInDataTypeAttribute>(false) != null);
            foreach (var type in builtInTypes)
            {
                CreateOrUpdateDataType(type, false);
            }

            if (sort)
            {
                contentTypes = contentTypes.SortByContentInheritance();
                docTypes = docTypes.SortByDocTypeInheritance();
            }

            foreach (var dataType in dataTypes)
            {
                //Create or update the data type
                CreateOrUpdateDataType(dataType);
            }

            //Do this twice to ensure all relations are present when needed (e.g. when a type is allowed 
            //as a child of one of its' derived types two passes are needed to set the inheritance 
            //first and the allowed ancestory second)
            for (int i = 1; i <= 2; i++) 
            {
                foreach (var mediaType in mediaTypes)
                {
                    //Create or update the data type
                    CreateOrUpdateMediaType(mediaType);
                }

                foreach (var docType in docTypes)
                {
                    //Create or update the doc type
                    CreateOrUpdateDocumentType(docType, i == 2); //suppress warnings on the second pass

                    if (i == 1) //only associate template on the first pass
                    {
                        //associate the existing template file if required
                        RegisterTemplate(docType);
                    }
                }
            }

            foreach (var contentItem in contentTypes)
            {
                //Create default content if required
                _defaultContentManager.CreateDefaultContent(contentItem);
            }

            if(refreshCache)
            {
                umbraco.library.RefreshContent();
            }
        }
        #endregion

        #region Template Registration
        /// <summary>
        /// Registers the specified template for the given doctype, if no template is already associated. Creates a basic default cshtml file if none exists at the specified path.
        /// </summary>
        private void RegisterTemplate(Type docType)
        {
            var attribute = docType.GetCodeFirstAttribute<DocumentTypeAttribute>();
            if (attribute is DocumentTypeAttribute)
            {
                var templateAttribute = attribute as DocumentTypeAttribute;
                if (templateAttribute.RegisterTemplate)
                {
                    var templates = ApplicationContext.Current.Services.FileService.GetTemplates().ToList();
                    var type = ApplicationContext.Current.Services.ContentTypeService.GetContentType(attribute.DocumentTypeAlias);

                    //Only do this if the doc type has no templates already associated
                    if (type.AllowedTemplates.Count() == 0)
                    {
                        var template = templates.FirstOrDefault(x => x.Alias == templateAttribute.TemplateAlias);
                        if (template == null)
                        {
                            template = new Template(templateAttribute.TemplatePath, templateAttribute.TemplateName, templateAttribute.TemplateAlias);

                            if (System.IO.File.Exists(HostingEnvironment.MapPath(templateAttribute.TemplatePath)))
                            {
                                //get the existing content from the file so it isn't overwritten when we save the template.
                                template.Content = System.IO.File.ReadAllText(HostingEnvironment.MapPath(templateAttribute.TemplatePath));
                            }
                            else
                            {
                                //TODO get this from a file resource containing a default view
                                var content = "@inherits Umbraco.Web.Mvc.UmbracoViewPage<" + templateAttribute.DecoratedTypeFullName + ">" + Environment.NewLine + Environment.NewLine;
                                //System.IO.File.WriteAllText(HostingEnvironment.MapPath(templateAttribute.TemplatePath), content);
                                template.Content = content;
                            }

                            ApplicationContext.Current.Services.FileService.SaveTemplate(template);
                        }
                        type.AllowedTemplates = new ITemplate[] { template };
                        type.SetDefaultTemplate(template);
                        ApplicationContext.Current.Services.ContentTypeService.Save(type);
                    }
                }
            }
        }
        #endregion

        #region Content Type Creation and Update
        /// <summary>
        /// This method will create or update the Document Type in Umbraco.
        /// It's possible that you need to run this method a few times to create all relations between Content Types.
        /// </summary>
        /// <param name="type">The type of your model that contains a DocumentTypeAttribute</param>
        /// <param name="suppressDuplicateWarnings">Set to true on the second pass to prevent attempts to add duplicates to the cache</param>
        private void CreateOrUpdateDocumentType(Type type, bool suppressDuplicateWarnings = false)
        {
            var contentTypeService = ApplicationContext.Current.Services.ContentTypeService;
            var fileService = ApplicationContext.Current.Services.FileService;
            var dataTypeService = ApplicationContext.Current.Services.DataTypeService;

            var contentTypeAttribute = type.GetCodeFirstAttribute<DocumentTypeAttribute>();
            if (contentTypeAttribute != null)
            {
                if (!suppressDuplicateWarnings && !DocumentTypeRegister.DocumentTypeCache.TryAdd(contentTypeAttribute.DocumentTypeAlias, type))
                {
                    throw new CodeFirstException("Failed to add to document type cache - possible duplicate document type alias " + contentTypeAttribute.DocumentTypeAlias);
                }
                if (!contentTypeService.GetAllContentTypes().Any(x => x != null && string.Equals(x.Alias, contentTypeAttribute.DocumentTypeAlias, StringComparison.InvariantCultureIgnoreCase)))
                {
                    CreateContentType(contentTypeService, fileService, contentTypeAttribute, type, dataTypeService);
                }
                else
                {
                    //update
                    IContentType contentType = contentTypeService.GetContentType(contentTypeAttribute.DocumentTypeAlias);
                    UpdateContentType(contentTypeService, fileService, contentTypeAttribute, contentType, type, dataTypeService);
                }
                return;
            }
        }

        private void CreateOrUpdateMediaType(Type type)
        {
            var contentTypeService = ApplicationContext.Current.Services.ContentTypeService;
            var fileService = ApplicationContext.Current.Services.FileService;
            var dataTypeService = ApplicationContext.Current.Services.DataTypeService;

            var mediaTypeAttribute = type.GetCodeFirstAttribute<MediaTypeAttribute>();
            if (mediaTypeAttribute != null)
            {
                if (!contentTypeService.GetAllMediaTypes().Any(x => x != null && string.Equals(x.Alias, mediaTypeAttribute.MediaTypeAlias, StringComparison.InvariantCultureIgnoreCase)))
                {
                    CreateMediaType(contentTypeService, fileService, mediaTypeAttribute, type, dataTypeService);
                }
                else
                {
                    //update
                    IMediaType mediaType = contentTypeService.GetMediaType(mediaTypeAttribute.MediaTypeAlias);
                    UpdateMediaType(contentTypeService, fileService, mediaTypeAttribute, mediaType, type, dataTypeService);
                }
                return;
            }
        }

        #region Create content types
        /// <summary>
        /// This method is called when the Content Type declared in the attribute hasn't been found in Umbraco
        /// </summary>
        /// <param name="contentTypeService"></param>
        /// <param name="fileService"></param>
        /// <param name="attribute"></param>
        /// <param name="type"></param>
        /// <param name="dataTypeService"></param>
        private void CreateContentType(IContentTypeService contentTypeService, IFileService fileService,
            DocumentTypeAttribute attribute, Type type, IDataTypeService dataTypeService)
        {
            IContentType newContentType;
            Type parentType = type.BaseType;
            if (parentType != null && !DocumentTypeBaseRegistered(parentType) && parentType.GetBaseTypes(false).Any(x => x == typeof(DocumentTypeBase)))
            {
                DocumentTypeAttribute parentAttribute = parentType.GetCodeFirstAttribute<DocumentTypeAttribute>();
                if (parentAttribute != null)
                {
                    string parentAlias = parentAttribute.DocumentTypeAlias;
                    IContentType parentContentType = contentTypeService.GetContentType(parentAlias);
                    newContentType = new ContentType(parentContentType);
                }
                else
                {
                    throw new Exception("The given base class has no DocumentTypeAttribute");
                }
            }
            else
            {
                newContentType = new ContentType(-1);
            }

            newContentType.Name = attribute.DocumentTypeName;
            newContentType.Alias = attribute.DocumentTypeAlias;
            newContentType.Icon = attribute.Icon;
            newContentType.Description = attribute.Description;

            newContentType.AllowedAsRoot = attribute.AllowedAtRoot;
            newContentType.IsContainer = attribute.EnableListView;
            newContentType.AllowedContentTypes = FetchAllowedContentTypes(attribute.AllowedChildren, contentTypeService);

            //create tabs
            CreateTabs(newContentType, type, dataTypeService);

            //create properties on the generic tab
            var propertiesOfRoot = type.GetProperties().Where(x => x.GetCodeFirstAttribute<DocumentPropertyAttribute>() != null);
            foreach (var item in propertiesOfRoot)
            {
                CreateProperty(newContentType, null, dataTypeService, true, item);
            }

            //Save and persist the content Type
            contentTypeService.Save(newContentType, 0);
        }

        /// <summary>
        /// Scans for properties on the model which have the UmbracoTab attribute
        /// </summary>
        /// <param name="newContentType"></param>
        /// <param name="model"></param>
        /// <param name="dataTypeService"></param>
        private void CreateTabs(IContentTypeBase newContentType, Type model, IDataTypeService dataTypeService)
        {
            var properties = model.GetProperties().Where(x => x.DeclaringType == model && x.GetCodeFirstAttribute<DocumentTabAttribute>() != null).ToArray();
            int length = properties.Length;

            for (int i = 0; i < length; i++)
            {
                var tabAttribute = properties[i].GetCodeFirstAttribute<DocumentTabAttribute>();

                newContentType.AddPropertyGroup(tabAttribute.Name);
                newContentType.PropertyGroups.Where(x => x.Name == tabAttribute.Name).FirstOrDefault().SortOrder = tabAttribute.SortOrder;

                CreateProperties(properties[i], newContentType, tabAttribute.Name, dataTypeService);
            }
        }

        /// <summary>
        /// Every property on the Tab object is scanned for the UmbracoProperty attribute
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <param name="newContentType"></param>
        /// <param name="tabName"></param>
        /// <param name="dataTypeService"></param>
        /// <param name="atTabGeneric"></param>
        private void CreateProperties(PropertyInfo propertyInfo, IContentTypeBase newContentType, string tabName, IDataTypeService dataTypeService, bool atTabGeneric = false)
        {
            //type is from TabBase
            Type type = propertyInfo.PropertyType;
            var properties = type.GetProperties().Where(x => x.GetCustomAttribute<DocumentPropertyAttribute>() != null); //Do NOT initialise attributes here, causes exception
            if (properties.Count() > 0)
            {
                foreach (var item in properties)
                {
                    CreateProperty(newContentType, tabName, dataTypeService, atTabGeneric, item);
                }
            }
        }

        /// <summary>
        /// Creates a new property on the ContentType under the correct tab
        /// </summary>
        /// <param name="newContentType"></param>
        /// <param name="tabName"></param>
        /// <param name="dataTypeService"></param>
        /// <param name="atTabGeneric"></param>
        /// <param name="item"></param>
        private void CreateProperty(IContentTypeBase newContentType, string tabName, IDataTypeService dataTypeService, bool atTabGeneric, PropertyInfo item)
        {
            if (item.PropertyType.IsEnum)
            {
                CreateOrUpdateEnumDataType(dataTypeService, item);
            }

            DocumentPropertyAttribute attribute = item.GetCodeFirstAttribute<DocumentPropertyAttribute>();

            IDataTypeDefinition dataTypeDef;
            if (string.IsNullOrEmpty(attribute.DataTypeInstanceName))
            {
                dataTypeDef = dataTypeService.GetDataTypeDefinitionByPropertyEditorAlias(attribute.PropertyEditorAlias).FirstOrDefault();
            }
            else
            {
                dataTypeDef = dataTypeService.GetDataTypeDefinitionByName(attribute.DataTypeInstanceName);
            }

            if (dataTypeDef != null)
            {
                PropertyType propertyType = new PropertyType(dataTypeDef);
                propertyType.Name = attribute.Name;
                propertyType.Alias = ((atTabGeneric || !attribute.AddTabAliasToPropertyAlias) ? attribute.Alias : StringHelperExtensions.HyphenToUnderscore(StringHelperExtensions.ParseUrl(attribute.Alias + "_" + tabName, false)));
                propertyType.Description = attribute.Description;
                propertyType.Mandatory = attribute.Mandatory;
                propertyType.SortOrder = attribute.SortOrder;
                propertyType.ValidationRegExp = attribute.ValidationRegularExpression;

                if (atTabGeneric)
                {
                    newContentType.AddPropertyType(propertyType);
                }
                else
                {
                    newContentType.AddPropertyType(propertyType, tabName);
                }
            }
        }

        private void CreateOrUpdateEnumDataType(IDataTypeService dataTypeService, PropertyInfo item)
        {
            var service = ApplicationContext.Current.Services.DataTypeService;
            var attr = item.PropertyType.GetCodeFirstAttribute<DataTypeAttribute>();
            var dtdType = typeof(EnumDataTypeRegistration<>).MakeGenericType(item.PropertyType);
            DataTypeRegistration dataTypeRegistration;

            if (DataTypeRegister.Current.IsRegistered(item.PropertyType))
            {
                dataTypeRegistration = DataTypeRegister.Current.GetRegistration(item.PropertyType);
            }
            else
            {
                if (attr == null)
                {
                    dataTypeRegistration = (DataTypeRegistration)Activator.CreateInstance(dtdType, string.Empty, string.Empty, null, true);
                }
                else
                {
                    dataTypeRegistration = new DataTypeRegistration() { ConverterType = attr.ConverterType, PropertyEditorAlias = attr.PropertyEditorAlias, DataTypeInstanceName = attr.Name };
                }
                DataTypeRegister.Current.Register(item.PropertyType, dataTypeRegistration);
            }

            IDataTypeDefinition dataTypeDefinition = service.GetAllDataTypeDefinitions().SingleOrDefault(x => string.Equals(x.Name, dataTypeRegistration.DataTypeInstanceName, StringComparison.InvariantCultureIgnoreCase));
            if (dataTypeDefinition == null)
            {
                dataTypeDefinition = new Umbraco.Core.Models.DataTypeDefinition(-1, dataTypeRegistration.PropertyEditorAlias);
                dataTypeDefinition.Name = dataTypeRegistration.DataTypeInstanceName;
            }
            dataTypeDefinition.PropertyEditorAlias = dataTypeRegistration.PropertyEditorAlias;
            dataTypeDefinition.DatabaseType = DataTypeDatabaseType.Nvarchar;

            var enumNames = item.PropertyType.GetEnumNames();
            Dictionary<string, PreValue> preValues = new Dictionary<string, PreValue>();
            if (dataTypeDefinition.Id != -1)
            {
                var existingValues = dataTypeService.GetPreValuesCollectionByDataTypeId(dataTypeDefinition.Id);
                if (existingValues.IsDictionaryBased)
                {
                    var dict = existingValues.PreValuesAsDictionary;
                    var sort = 1;
                    foreach (var name in enumNames.Select(x => x.ToProperCase()))
                    {
                        var match = dict.Where(x => x.Value.Value == name);
                        if (match.Count() == 0)
                        {
                            preValues.Add(Guid.NewGuid().ToString(), new PreValue(-1, name, sort++));
                        }
                        else
                        {
                            var existing = match.First();
                            preValues.Add(existing.Key, new PreValue(existing.Value.Id, name, sort++));
                        }
                    }
                }
            }

            dataTypeService.SaveDataTypeAndPreValues(dataTypeDefinition, preValues);
            dataTypeRegistration.Definition = dataTypeDefinition;
        }

        /// <summary>
        /// Creates or update a dataType
        /// </summary>
        private IDataTypeDefinition CreateOrUpdateDataType(Type type, bool persist = true)
        {
            if (type.GetCustomAttribute<BuiltInDataTypeAttribute>(false) != null)
            {
                //Never persist the built-in types, they already exist!
                persist = false;
            }

            DataTypeRegistration dataTypeRegistration;
            if (DataTypeRegister.Current.IsRegistered(type))
            {
                dataTypeRegistration = DataTypeRegister.Current.GetRegistration(type);
            }
            else
            {
                DataTypeAttribute customDataTypeAttribute = type.GetCodeFirstAttribute<DataTypeAttribute>();
                if (customDataTypeAttribute == null)
                {
                    throw new CodeFirstException(type.Name + " is not a valid data type");
                }
                dataTypeRegistration = new DataTypeRegistration()
                {
                    ConverterType = customDataTypeAttribute.ConverterType,
                    DataTypeInstanceName = customDataTypeAttribute.Name,
                    PropertyEditorAlias = customDataTypeAttribute.PropertyEditorAlias,
                    DbType = customDataTypeAttribute.DbType
                };
                DataTypeRegister.Current.Register(type, dataTypeRegistration);
            }

            var dataTypeService = ApplicationContext.Current.Services.DataTypeService;
            IDataTypeDefinition dataTypeDefinition = dataTypeService.GetAllDataTypeDefinitions().SingleOrDefault(x => string.Equals(x.Name, dataTypeRegistration.DataTypeInstanceName, StringComparison.InvariantCultureIgnoreCase));

            if (persist) //the wrappers for built-in types should never modify the actual type in Umbraco, and so are passed in with persist = false
            {
                if (dataTypeDefinition == null)
                {
                    dataTypeDefinition = new Umbraco.Core.Models.DataTypeDefinition(-1, dataTypeRegistration.PropertyEditorAlias);
                    dataTypeDefinition.Name = dataTypeRegistration.DataTypeInstanceName;
                }
                dataTypeDefinition.PropertyEditorAlias = dataTypeRegistration.PropertyEditorAlias;
                dataTypeDefinition.DatabaseType = dataTypeRegistration.DbType;

                IDictionary<string, PreValue> preValues;
                var factoryAttr = type.GetCodeFirstAttribute<PreValueFactoryAttribute>();

                
                if (factoryAttr != null)
                {
                    preValues = ((IPreValueFactory)factoryAttr.GetFactory()).GetPreValues();
                }
                else if (type.Implements<IPreValueFactory>())
                {
                    preValues = ((IPreValueFactory)Activator.CreateInstance(type)).GetPreValues();
                }
                else
                {
                    preValues = type.GetCodeFirstAttributes<PreValueAttribute>().ToDictionary(x => x.Alias, x => x.PreValue);
                }

                dataTypeService.SaveDataTypeAndPreValues(dataTypeDefinition, preValues); //Umbraco deals internally with updating/merging changes to the PreValue list and removing missing entries.
            }

            dataTypeRegistration.Definition = dataTypeDefinition;
            return dataTypeDefinition;
        }

        #endregion Create content types

        #region Create media types
        /// <summary>
        /// This method is called when the Media Type declared in the attribute hasn't been found in Umbraco
        /// </summary>
        /// <param name="contentTypeService"></param>
        /// <param name="fileService"></param>
        /// <param name="attribute"></param>
        /// <param name="type"></param>
        /// <param name="dataTypeService"></param>
        private void CreateMediaType(IContentTypeService contentTypeService, IFileService fileService, MediaTypeAttribute attribute, Type type, IDataTypeService dataTypeService)
        {
            IMediaType newMediaType;
            Type parentType = type.BaseType;
            if (parentType != null && parentType != typeof(DocumentTypeBase) && parentType.GetBaseTypes(false).Any(x => x == typeof(DocumentTypeBase)))
            {
                MediaTypeAttribute parentAttribute = parentType.GetCodeFirstAttribute<MediaTypeAttribute>();
                if (parentAttribute != null)
                {
                    string parentAlias = parentAttribute.MediaTypeAlias;
                    IMediaType parentContentType = contentTypeService.GetMediaType(parentAlias);
                    newMediaType = new MediaType(parentContentType);
                }
                else
                {
                    throw new Exception("The given base class has no UmbracoMediaTypeAttribute");
                }
            }
            else
            {
                newMediaType = new MediaType(-1);
            }

            newMediaType.Name = attribute.MediaTypeName;
            newMediaType.Alias = attribute.MediaTypeAlias;
            newMediaType.Icon = attribute.Icon;
            newMediaType.Description = attribute.Description;
            newMediaType.AllowedAsRoot = attribute.AllowedAtRoot;
            newMediaType.IsContainer = attribute.EnableListView;
            newMediaType.AllowedContentTypes = FetchAllowedContentTypes(attribute.AllowedChildren, contentTypeService);

            //create tabs
            CreateTabs(newMediaType, type, dataTypeService);

            //create properties on the generic tab
            var propertiesOfRoot = type.GetProperties().Where(x => x.GetCodeFirstAttribute<DocumentPropertyAttribute>() != null);
            foreach (var item in propertiesOfRoot)
            {
                CreateProperty(newMediaType, null, dataTypeService, true, item);
            }

            //Save and persist the media Type
            contentTypeService.Save(newMediaType, 0);
        }
        #endregion

        #region Update content types
        /// <summary>
        /// Update the existing content Type based on the data in the attributes
        /// </summary>
        /// <param name="contentTypeService"></param>
        /// <param name="fileService"></param>
        /// <param name="attribute"></param>
        /// <param name="contentType"></param>
        /// <param name="type"></param>
        /// <param name="dataTypeService"></param>
        private void UpdateContentType(IContentTypeService contentTypeService, IFileService fileService, DocumentTypeAttribute attribute, IContentType contentType, Type type, IDataTypeService dataTypeService)
        {
            contentType.Name = attribute.DocumentTypeName;
            contentType.Alias = attribute.DocumentTypeAlias;
            contentType.Icon = attribute.Icon;
            contentType.Description = attribute.Description;
            contentType.IsContainer = attribute.EnableListView;
            contentType.AllowedContentTypes = FetchAllowedContentTypes(attribute.AllowedChildren, contentTypeService);
            contentType.AllowedAsRoot = attribute.AllowedAtRoot;

            Type parentType = type.BaseType;
            if (parentType != null && !DocumentTypeBaseRegistered(parentType) && parentType.GetBaseTypes(false).Any(x => x == typeof(DocumentTypeBase)))
            {
                DocumentTypeAttribute parentAttribute = parentType.GetCodeFirstAttribute<DocumentTypeAttribute>();
                if (parentAttribute != null)
                {
                    string parentAlias = parentAttribute.DocumentTypeAlias;
                    IContentType parentContentType = contentTypeService.GetContentType(parentAlias);
                    contentType.ParentId = parentContentType.Id;
                }
                else
                {
                    throw new Exception("The given base class has no UmbracoContentTypeAttribute");
                }
            }

            VerifyProperties(contentType, type, dataTypeService);

            //verify if a tab has no properties, if so remove
            var propertyGroups = contentType.PropertyGroups.ToArray();
            int length = propertyGroups.Length;
            for (int i = 0; i < length; i++)
            {
                if (propertyGroups[i].PropertyTypes.Count == 0)
                {
                    //remove
                    contentType.RemovePropertyGroup(propertyGroups[i].Name);
                }
            }

            //persist
            contentTypeService.Save(contentType, 0);
        }

        internal bool DocumentTypeBaseRegistered(Type parentType)
        {
            return DocumentTypeBases.Any(x => x == parentType || (x.IsGenericType && x.GetGenericTypeDefinition() == parentType));
        }

        /// <summary>
        /// Loop through all properties and remove existing ones if necessary
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="type"></param>
        /// <param name="dataTypeService"></param>
        private void VerifyProperties(IContentTypeBase contentType, Type type, IDataTypeService dataTypeService)
        {
            var properties = type.GetProperties().Where(x => x.GetCodeFirstAttribute<DocumentTabAttribute>() != null).ToArray();
            List<string> propertiesThatShouldExist = new List<string>();

            foreach (var propertyTab in properties)
            {
                var tabAttribute = propertyTab.GetCodeFirstAttribute<DocumentTabAttribute>();
                if (!contentType.PropertyGroups.Any(x => x.Name == tabAttribute.Name))
                {
                    contentType.AddPropertyGroup(tabAttribute.Name);
                }

                propertiesThatShouldExist.AddRange(VerifyAllPropertiesOnTab(propertyTab, contentType, tabAttribute.Name, dataTypeService));
            }

            var propertiesOfRoot = type.GetProperties().Where(x => x.GetCodeFirstAttribute<DocumentPropertyAttribute>() != null);
            foreach (var item in propertiesOfRoot)
            {
                //TODO: check for correct name
                propertiesThatShouldExist.Add(VerifyExistingProperty(contentType, null, dataTypeService, item, true));
            }

            //loop through all the properties on the ContentType to see if they should be removed;
            var existingUmbracoProperties = contentType.PropertyTypes.ToArray();
            int length = contentType.PropertyTypes.Count();
            for (int i = 0; i < length; i++)
            {
                if (!propertiesThatShouldExist.Contains(existingUmbracoProperties[i].Alias))
                {
                    //remove the property
                    contentType.RemovePropertyType(existingUmbracoProperties[i].Alias);
                }
            }

            ////loop through all the properties on the propertiesThatShouldExist to see if they should be added;
            //existingUmbracoProperties = contentType.PropertyTypes.ToArray();
            //length = propertiesThatShouldExist.Count();
            //for (int i = 0; i < length; i++)
            //{
            //    if (!existingUmbracoProperties.Contains(propertiesThatShouldExist[i]))
            //    {
            //        //remove the property
            //        contentType.RemovePropertyType(existingUmbracoProperties[i].Alias);
            //    }
            //}
        }

        /// <summary>
        /// Scan the properties on tabs
        /// </summary>
        /// <param name="propertyTab"></param>
        /// <param name="contentType"></param>
        /// <param name="tabName"></param>
        /// <param name="dataTypeService"></param>
        /// <returns></returns>
        private IEnumerable<string> VerifyAllPropertiesOnTab(PropertyInfo propertyTab, IContentTypeBase contentType, string tabName, IDataTypeService dataTypeService)
        {
            Type type = propertyTab.PropertyType;
            var properties = type.GetProperties().Where(x => x.GetCustomAttribute<DocumentPropertyAttribute>() != null); //Do NOT initialise attribute here, causes initialisation exception
            if (properties.Count() > 0)
            {
                List<string> propertyAliases = new List<string>();
                foreach (var item in properties)
                {
                    propertyAliases.Add(VerifyExistingProperty(contentType, tabName, dataTypeService, item));
                }
                return propertyAliases;
            }
            return new string[0];
        }

        /// <summary>
        /// Checks whether a property exists and adds if if it does not. The data type, alias, description and mandatory flag are update for existing properties, but not persisted.
        /// Callers should persist the value.
        /// </summary>
        private string VerifyExistingProperty(IContentTypeBase contentType, string tabName, IDataTypeService dataTypeService, PropertyInfo item, bool atGenericTab = false)
        {
            if (item.PropertyType.IsEnum)
            {
                CreateOrUpdateEnumDataType(dataTypeService, item);
            }

            DocumentPropertyAttribute attribute = item.GetCodeFirstAttribute<DocumentPropertyAttribute>();
            IDataTypeDefinition dataTypeDef;
            if (string.IsNullOrEmpty(attribute.DataTypeInstanceName))
            {
                dataTypeDef = dataTypeService.GetDataTypeDefinitionByPropertyEditorAlias(attribute.PropertyEditorAlias).FirstOrDefault();
            }
            else
            {
                dataTypeDef = dataTypeService.GetAllDataTypeDefinitions().FirstOrDefault(x => x.Name == attribute.DataTypeInstanceName);
            }

            if (dataTypeDef == null)
            {
                dataTypeDef = CreateOrUpdateDataType(item.PropertyType);
            }

            bool alreadyExisted = contentType.PropertyTypeExists(attribute.Alias);
            PropertyType property = new PropertyType(dataTypeDef);

            property.Name = attribute.Name;
            property.Alias = ((atGenericTab || !attribute.AddTabAliasToPropertyAlias) ? attribute.Alias : StringHelperExtensions.HyphenToUnderscore(StringHelperExtensions.ParseUrl(attribute.Alias + "_" + tabName, false)));
            property.Description = attribute.Description;
            property.Mandatory = attribute.Mandatory;

            if (!alreadyExisted)
            {
                if (atGenericTab)
                {
                    contentType.AddPropertyType(property);
                }
                else
                {
                    contentType.AddPropertyType(property, tabName);
                }
            }

            return property.Alias;
        }
        #endregion Update content types

        #region Update media types
        /// <summary>
        /// Update the existing content Type based on the data in the attributes
        /// </summary>
        /// <param name="contentTypeService"></param>
        /// <param name="fileService"></param>
        /// <param name="attribute"></param>
        /// <param name="mediaType"></param>
        /// <param name="type"></param>
        /// <param name="dataTypeService"></param>
        private void UpdateMediaType(IContentTypeService contentTypeService, IFileService fileService, MediaTypeAttribute attribute, IMediaType mediaType, Type type, IDataTypeService dataTypeService)
        {
            mediaType.Name = attribute.MediaTypeName;
            mediaType.Alias = attribute.MediaTypeAlias;
            mediaType.Icon = attribute.Icon;
            mediaType.Description = attribute.Description;
            mediaType.IsContainer = attribute.EnableListView;
            mediaType.AllowedContentTypes = FetchAllowedContentTypes(attribute.AllowedChildren, contentTypeService);
            mediaType.AllowedAsRoot = attribute.AllowedAtRoot;

            Type parentType = type.BaseType;
            if (parentType != null && parentType != typeof(DocumentTypeBase) && parentType.GetBaseTypes(false).Any(x => x == typeof(DocumentTypeBase)))
            {
                MediaTypeAttribute parentAttribute = parentType.GetCodeFirstAttribute<MediaTypeAttribute>();
                if (parentAttribute != null)
                {
                    string parentAlias = parentAttribute.MediaTypeAlias;
                    IMediaType parentContentType = contentTypeService.GetMediaType(parentAlias);
                    mediaType.ParentId = parentContentType.Id;
                }
                else
                {
                    throw new Exception("The given base class has no UmbracoMediaTypeAttribute");
                }
            }

            VerifyProperties(mediaType, type, dataTypeService);

            //verify if a tab has no properties, if so remove
            var propertyGroups = mediaType.PropertyGroups.ToArray();
            int length = propertyGroups.Length;
            for (int i = 0; i < length; i++)
            {
                if (propertyGroups[i].PropertyTypes.Count == 0)
                {
                    //remove
                    mediaType.RemovePropertyGroup(propertyGroups[i].Name);
                }
            }

            //persist
            contentTypeService.Save(mediaType, 0);
        }
        #endregion

        #region Shared logic
        /// <summary>
        /// Gets the allowed children
        /// </summary>
        /// <param name="types"></param>
        /// <param name="contentTypeService"></param>
        /// <returns></returns>
        private IEnumerable<ContentTypeSort> FetchAllowedContentTypes(Type[] types, IContentTypeService contentTypeService)
        {
            if (types == null) return new ContentTypeSort[0];

            List<ContentTypeSort> contentTypeSorts = new List<ContentTypeSort>();

            List<string> aliases = GetAliasesFromTypes(types);

            var contentTypes = contentTypeService.GetAllContentTypes().Where(x => aliases.Contains(x.Alias)).ToArray();

            int length = contentTypes.Length;
            for (int i = 0; i < length; i++)
            {
                ContentTypeSort sort = new ContentTypeSort();
                sort.Alias = contentTypes[i].Alias;
                int id = contentTypes[i].Id;
                sort.Id = new Lazy<int>(() => { return id; });
                sort.SortOrder = i;
                contentTypeSorts.Add(sort);
            }
            return contentTypeSorts;
        }

        /// <summary>
        /// Gets all the document type aliases from the supplied list of types
        /// </summary>
        private List<string> GetAliasesFromTypes(Type[] types)
        {
            List<string> aliases = new List<string>();

            foreach (Type type in types)
            {
                DocumentTypeAttribute attribute = type.GetCodeFirstAttribute<DocumentTypeAttribute>();
                if (attribute != null)
                {
                    aliases.Add(attribute.DocumentTypeAlias);
                }
            }

            return aliases;
        }
        #endregion Shared logic
        #endregion
    }
}