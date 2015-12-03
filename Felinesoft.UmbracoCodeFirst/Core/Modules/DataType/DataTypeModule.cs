using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using Felinesoft.UmbracoCodeFirst.Core.Modules;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web.Models.Trees;
using System.Threading;
using Umbraco.Web;
using Felinesoft.UmbracoCodeFirst.Core.Modules.DataType.T4;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Converters;
using Umbraco.Core;
using System.Web;
using System.IO;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public class DataTypeModule : IDataTypeModule, IEntityTreeFilter, IClassFileGenerator
    {
        private DataTypeRegister.DataTypeRegisterController _registerController;
        private DataTypeRegister _register;
        private Lazy<IEnumerable<IDataTypeDefinition>> _allDataTypeDefinitions;
        private IDataTypeService _service;
        private SyncLock<MemberInfo> _locks;
        private static Dictionary<DataTypeAttribute, string> _typeDefs;

        public DataTypeModule(IDataTypeService service)
        {
            _service = service;
            _locks = new SyncLock<MemberInfo>();
            _allDataTypeDefinitions = new Lazy<IEnumerable<IDataTypeDefinition>>(() =>
                {
                    return _service.GetAllDataTypeDefinitions();
                });
        }

        #region IDataTypeModule
        public void Initialise(IEnumerable<Type> classes)
        {
            _register = new DataTypeRegister(out _registerController, _service);

            if (CodeFirstManager.Current.Features.UseBuiltInPrimitiveDataTypes)
            {
                RegisterNvarcharType<string, PassThroughConverter<string>>(BuiltInDataTypes.Textbox);
                RegisterIntegerType<bool, BoolTrueFalseConverter>(BuiltInDataTypes.TrueFalse);
                RegisterIntegerType<int, PassThroughConverter<int>>(BuiltInDataTypes.Numeric);
                RegisterDateTimeType<DateTime, PassThroughConverter<DateTime>>(BuiltInDataTypes.DatePickerWithTime);
            }

            List<System.Threading.Tasks.Task> tasks = new List<System.Threading.Tasks.Task>();
            if (CodeFirstManager.Current.Features.UseConcurrentInitialisation)
            {
                InitialiseTypesConcurrent(classes, tasks);
            }
            else
            {
                InitialiseTypes(classes, tasks);
            }
        }

        private void InitialiseTypesConcurrent(IEnumerable<Type> classes, List<System.Threading.Tasks.Task> tasks)
        {

            foreach (var t in classes)
            {
                tasks.Add(System.Threading.Tasks.Task.Run(() =>
                {
                    GetDataType(t, t.GetCustomAttribute<DoNotSyncDataTypeAttribute>(false) == null);
                }));
            }
            System.Threading.Tasks.Task.WaitAll(tasks.ToArray());
        }

        private void InitialiseTypes(IEnumerable<Type> classes, List<System.Threading.Tasks.Task> tasks)
        {
            foreach (var t in classes)
            {
                GetDataType(t, t.GetCustomAttribute<DoNotSyncDataTypeAttribute>(false) == null);
            }
        }

        public DataTypeRegister DataTypeRegister
        {
            get { return _register; }
        }

        public DataTypeRegistration GetDataType(PropertyInfo instance, bool updateDataTypeDefinition = true)
        {
            _locks.TakeOrWait(instance);
            DataTypeRegistration dataTypeRegistration;
            if (_register.TryGetRegistration(instance, out dataTypeRegistration))
            {
                _locks.Release(instance);
                return dataTypeRegistration;
            }

            _locks.TakeOrWait(instance.PropertyType);
            bool typeExisted = false;
            dataTypeRegistration = instance.PropertyType.IsEnum ? 
                                                    BuildEnumRegistration(instance.PropertyType)
                                                        : 
                                                    BuildDataTypeRegistration(instance, out typeExisted);
            var at = instance.GetCustomAttribute<NodePickerConfigAttribute>(false);
            IDictionary<string, PreValue> codeFirstPreValues = typeExisted ? null : GetPreValuesFromProperty(instance);
            UpdateOrCreateDataTypeDefinition(codeFirstPreValues, (!typeExisted) && updateDataTypeDefinition, dataTypeRegistration);

            _locks.Release(instance.PropertyType);
            _locks.Release(instance);
            
            return dataTypeRegistration;
        }

        /// <summary>
        /// Create or update a dataType
        /// </summary>
        public DataTypeRegistration GetDataType(Type type, bool updateDataTypeDefinition = true)
        {
            _locks.TakeOrWait(type);
            DataTypeRegistration dataTypeRegistration;
            if (DataTypeRegister.TryGetRegistration(type, out dataTypeRegistration))
            {
                _locks.Release(type);
                return dataTypeRegistration;
            }

            if (type.GetCustomAttribute<DoNotSyncDataTypeAttribute>(false) != null)
            {
                //Never persist the built-in types, they already exist!
                updateDataTypeDefinition = false;
            }

            dataTypeRegistration = type.IsEnum ?
                                            BuildEnumRegistration(type)
                                                :
                                            BuildDataTypeRegistration(type);
            IDictionary<string, PreValue> codeFirstPreValues = GetPreValuesFromDataType(type);
            UpdateOrCreateDataTypeDefinition(codeFirstPreValues, updateDataTypeDefinition, dataTypeRegistration);
            _locks.Release(type);
            return dataTypeRegistration;
        }

        public void RegisterNtextType<T, Tconverter>(string dataTypeName) where Tconverter : IDataTypeConverter<string, T>
        {
            DataTypeRegistration reg = new DataTypeRegistration();
            reg.ClrType = typeof(T);
            reg.CodeFirstControlled = false; //no real definition so don't sync
            reg.DataTypeInstanceName = dataTypeName;
            reg.ConverterType = typeof(Tconverter);
            reg.DbType = DatabaseType.Ntext;
            reg.UmbracoDatabaseType = DataTypeDatabaseType.Ntext;
            reg.Definition = _service.GetDataTypeDefinitionByName(dataTypeName);
            reg.PropertyEditorAlias = reg.Definition.PropertyEditorAlias;
            _registerController.Register(typeof(T), reg);
        }

        public void RegisterNvarcharType<T, Tconverter>(string dataTypeName) where Tconverter : IDataTypeConverter<string, T>
        {
            DataTypeRegistration reg = new DataTypeRegistration();
            reg.ClrType = typeof(T);
            reg.CodeFirstControlled = false; //no real definition so don't sync
            reg.DataTypeInstanceName = dataTypeName;
            reg.ConverterType = typeof(Tconverter);
            reg.DbType = DatabaseType.Nvarchar;
            reg.UmbracoDatabaseType = DataTypeDatabaseType.Nvarchar;
            reg.Definition = _service.GetDataTypeDefinitionByName(dataTypeName);
            reg.PropertyEditorAlias = reg.Definition.PropertyEditorAlias;
            _registerController.Register(typeof(T), reg);
        }

        public void RegisterIntegerType<T, Tconverter>(string dataTypeName) where Tconverter : IDataTypeConverter<int, T>
        {
            DataTypeRegistration reg = new DataTypeRegistration();
            reg.ClrType = typeof(T);
            reg.CodeFirstControlled = false; //no real definition so don't sync
            reg.DataTypeInstanceName = dataTypeName;
            reg.ConverterType = typeof(Tconverter);
            reg.DbType = DatabaseType.Integer;
            reg.UmbracoDatabaseType = DataTypeDatabaseType.Integer;
            reg.Definition = _service.GetDataTypeDefinitionByName(dataTypeName);
            reg.PropertyEditorAlias = reg.Definition.PropertyEditorAlias;
            _registerController.Register(typeof(T), reg);
        }

        public void RegisterDateTimeType<T, Tconverter>(string dataTypeName) where Tconverter : IDataTypeConverter<DateTime, T>
        {
            DataTypeRegistration reg = new DataTypeRegistration();
            reg.ClrType = typeof(T);
            reg.CodeFirstControlled = false; //no real definition so don't sync
            reg.DataTypeInstanceName = dataTypeName;
            reg.ConverterType = typeof(Tconverter);
            reg.DbType = DatabaseType.Date;
            reg.UmbracoDatabaseType = DataTypeDatabaseType.Date;
            reg.Definition = _service.GetDataTypeDefinitionByName(dataTypeName);
            reg.PropertyEditorAlias = reg.Definition.PropertyEditorAlias;
            _registerController.Register(typeof(T), reg);
        }

        #region Private
        private IDictionary<string, PreValue> MergePreValuesWithExisting(DataTypeRegistration dataTypeRegistration, IDictionary<string, PreValue> codeFirstPreValues, ref bool modified)
        {
            bool preValuesChanged = false;
            IDictionary<string, PreValue> preValues = new Dictionary<string, PreValue>();
            //Ensure that pre-value IDs stay the same (correlate by alias)
            if (dataTypeRegistration.Definition.Id != -1)
            {
                var existingValues = _service.GetPreValuesCollectionByDataTypeId(dataTypeRegistration.Definition.Id);
                if (existingValues.IsDictionaryBased)
                {
                    var dict = existingValues.PreValuesAsDictionary;
                    foreach (var value in codeFirstPreValues)
                    {
                        var match = dict.Where(x => x.Key == value.Key);
                        if (match.Count() == 0)
                        {
                            preValues.Add(value);
                            preValuesChanged = true;
                        }
                        else
                        {
                            var existing = match.First();
                            preValuesChanged = preValuesChanged || value.Value.Value != existing.Value.Value || value.Value.SortOrder != existing.Value.SortOrder;
                            preValues.Add(existing.Key, new PreValue(existing.Value.Id, value.Value.Value, value.Value.SortOrder));
                        }
                    }
                }
            }
            modified = modified || preValuesChanged;
            return preValues;
        }

        private IDictionary<string, PreValue> GetPreValuesFromProperty(PropertyInfo instance)
        {
            IDictionary<string, PreValue> preValues;
            var factoryAttr = instance.GetCodeFirstAttribute<InstancePreValueFactoryAttribute>();
            var instanceAttributes = instance.GetCodeFirstAttributes<InstancePreValueAttribute>();
            var redirect = instance.GetCustomAttributes().FirstOrDefault(x => x.GetType().Implements<IDataTypeRedirect>()) as IDataTypeRedirect;

            PreValueContext context = new PreValueContext(instance);

            if (factoryAttr != null)
            {
                preValues = factoryAttr.GetFactory().GetPreValues(context);
            }
            else if (instanceAttributes.Count() > 0)
            {
                try
                {
                    preValues = instanceAttributes.ToDictionary(x => x.Alias, x => x.PreValue);
                }
                catch (ArgumentException ex)
                {
                    throw new CodeFirstException("Duplicate pre-value alias on data type instance " + instance.DeclaringType.Name + "." + instance.Name, ex);
                }
            }
            else if (redirect != null)
            {
                if (redirect is IInitialisablePropertyAttribute)
                {
                    (redirect as IInitialisablePropertyAttribute).Initialise(instance);
                }
                preValues = GetPreValuesFromDataType(redirect.Redirect(instance), context);
            }
            else
            {
                preValues = GetPreValuesFromDataType(instance.PropertyType, context);
            }
            return preValues;
        }

        private IDictionary<string, PreValue> GetPreValuesFromDataType(Type type, PreValueContext context = null)
        {
            try
            {
                IDictionary<string, PreValue> preValues;
                var factoryAttr = type.GetCodeFirstAttribute<PreValueFactoryAttribute>();
                if (context == null)
                {
                    context = new PreValueContext(type);
                }

                if (factoryAttr != null)
                {
                    preValues = factoryAttr.GetFactory().GetPreValues(context);
                }
                else if (type.IsEnum)
                {
                    preValues = GetEnumPreValues(type);
                }
                else if (type.Implements<IPreValueFactory>())
                {
                    preValues = ((IPreValueFactory)Activator.CreateInstance(type)).GetPreValues(context);
                }
                else
                {
                    preValues = type.GetCodeFirstAttributes<PreValueAttribute>().ToDictionary(x => x.Alias, x => x.PreValue);
                }

                return preValues;
            }
            catch (Exception ex)
            {
                throw new CodeFirstException("Failed to get pre-values for type " + type.FullName, ex);
            }
        }

        private DataTypeRegistration BuildDataTypeRegistration(Type type)
        {
            DataTypeRegistration dataTypeRegistration;
            DataTypeAttribute dataTypeAttribute = type.GetCodeFirstAttribute<DataTypeAttribute>();
            bool controlled = type.GetCustomAttribute<DoNotSyncDataTypeAttribute>(false) == null && type.GetCustomAttribute<BuiltInDataTypeAttribute>(false) == null;
            if (dataTypeAttribute == null)
            {
                throw new CodeFirstException(type.Name + " is not a valid data type");
            }

            dataTypeRegistration = new DataTypeRegistration()
            {
                ClrType = type,
                ConverterType = dataTypeAttribute.ConverterType,
                DataTypeInstanceName = dataTypeAttribute.Name,
                PropertyEditorAlias = dataTypeAttribute.PropertyEditorAlias,
                UmbracoDatabaseType = dataTypeAttribute.DbType,
                CodeFirstControlled = controlled
            };
            _registerController.Register(type, dataTypeRegistration);

            return dataTypeRegistration;
        }

        private DataTypeRegistration BuildDataTypeRegistration(PropertyInfo instance, out bool typeExisted)
        {
            DataTypeInstanceAttribute dataTypeInstanceAttribute = instance.GetCodeFirstAttribute<DataTypeInstanceAttribute>();
            if (dataTypeInstanceAttribute == null && instance.GetCustomAttributes(false).Any(x => x.GetType().Implements<IDataTypeInstance>()))
            {
                dataTypeInstanceAttribute = new DataTypeInstanceAttribute();
            }

            var dataType = instance.GetCodeFirstAttribute<ContentPropertyAttribute>().DataType;
            typeExisted = true;

            var redirect = instance.GetCustomAttributes().FirstOrDefault(x => x.GetType().Implements<IDataTypeRedirect>()) as IDataTypeRedirect;
            Type targetType;
            if (redirect != null)
            {
                if (redirect is IInitialisablePropertyAttribute)
                {
                    (redirect as IInitialisablePropertyAttribute).Initialise(instance);
                }
                targetType = redirect.Redirect(instance);
            }
            else
            {
                targetType = instance.PropertyType;
            }

            if (targetType.IsConstructedGenericType)
            {
                EnsureGenericTypeRegistration(targetType, ref typeExisted);
            }

            if (dataTypeInstanceAttribute == null && string.IsNullOrWhiteSpace(dataType)) //no data type override specified
            {
                return CheckTypeRegistrationForProperty(instance, targetType);
            }
            else if (!string.IsNullOrWhiteSpace(dataType)) //data type override specified in property attribute
            {
                typeExisted = true; //never modify any data type when using PEVCs - we only support back-office managed data types with PEVCs
                return CreateUmbracoConverterDataType(instance, dataType);
            }
            
            if (dataTypeInstanceAttribute.HasNullProperties) //data type override specified in instance attribute
            {
                InferInstanceRegistrationProperties(instance, targetType, dataTypeInstanceAttribute);
            }

            typeExisted = false;
            DataTypeRegistration dataTypeRegistration = new DataTypeRegistration()
            {
                ClrType = targetType,
                ConverterType = dataTypeInstanceAttribute.ConverterType,
                DataTypeInstanceName = dataTypeInstanceAttribute.Name,
                PropertyEditorAlias = dataTypeInstanceAttribute.PropertyEditorAlias,
                DbType = dataTypeInstanceAttribute.DbType,
                CodeFirstControlled = true
            };
            _registerController.Register(instance, dataTypeRegistration);
            return dataTypeRegistration;
        }

        private DataTypeRegistration CreateUmbracoConverterDataType(PropertyInfo instance, string dataType)
        {
            DataTypeRegistration existingTypeRegistration = new DataTypeRegistration()
            {
                ClrType = instance.PropertyType,
                ConverterType = null,
                DataTypeInstanceName = dataType,
                PropertyEditorAlias = null,
                DbType = DatabaseType.None,
                CodeFirstControlled = false //data type req'd to exist in Umbraco, shouldn't be modified
            };
            _registerController.Register(instance, existingTypeRegistration);
            return existingTypeRegistration;
        }

        private DataTypeRegistration EnsureGenericTypeRegistration(Type type, ref bool typeExisted)
        {
            DataTypeRegistration result;
            typeExisted = true;
            if (!DataTypeRegister.TryGetRegistration(type, out result) && type.GetGenericTypeDefinition().GetCodeFirstAttribute<DataTypeAttribute>(false) != null)
            {
                //Register this specific constructed type, as generic data types must be registered in Umbraco once for each set of type params used
                result = GetDataType(type);
                result.CodeFirstControlled = true;
                typeExisted = false;
            }
            return result;
        }

        private void InferInstanceRegistrationProperties(PropertyInfo instance, Type type, DataTypeInstanceAttribute dataTypeInstanceAttribute)
        {
            //Try to get properties from underlying data type if any essential properties are null
            DataTypeRegistration underlyingType;
            if (DataTypeRegister.TryGetRegistration(type, out underlyingType))
            {
                MergeDataTypeInstanceWithAncestor(dataTypeInstanceAttribute, instance, underlyingType);
            }
            else if (dataTypeInstanceAttribute.Name == null)
            {
                throw new CodeFirstException("A [DataTypeInstance] attribute must specify a value for data type name, unless it is applied to a document property whose type is a valid data type (in which case the data type name of the underlying data type is inherited)");
            }
        }

        private DataTypeRegistration CheckTypeRegistrationForProperty(PropertyInfo instance, Type type)
        {
            DataTypeRegistration underlyingType;
            if (DataTypeRegister.TryGetRegistration(type, out underlyingType))
            {
                _registerController.Register(instance, underlyingType);
                return underlyingType;
            }
            else
            {
                throw new CodeFirstException(instance.DeclaringType.Name + "." + instance.Name + " is not a valid property. Its type is not a valid data type, it does not specify a [DataTypeInstance] attribute and it does not specify a value for DataType in the [ContentProperty] attribute. At least one of these is required.");
            }
        }

        private void MergeDataTypeInstanceWithAncestor(DataTypeInstanceAttribute customDataTypeAttribute, PropertyInfo property, DataTypeRegistration underlyingType)
        {
            if (customDataTypeAttribute.Name == null)
            {
                customDataTypeAttribute.Name = underlyingType.DataTypeInstanceName + " (" + property.DeclaringType.Name + "." + property.Name + ")";
            }

            if (customDataTypeAttribute.PropertyEditorAlias == null)
            {
                customDataTypeAttribute.PropertyEditorAlias = underlyingType.PropertyEditorAlias;
            }

            if (customDataTypeAttribute.ConverterType == null)
            {
                customDataTypeAttribute.ConverterType = underlyingType.ConverterType;
            }

            if (customDataTypeAttribute.DbType == DatabaseType.None)
            {
                customDataTypeAttribute.DbType = underlyingType.DbType;
            }
        }

        private IDictionary<string, PreValue> GetEnumPreValues(Type type)
        {
            var enumNames = new List<string>();
            foreach (var num in type.GetEnumValues())
            {
                if ((num.ToString().Equals("none", StringComparison.InvariantCultureIgnoreCase) ||
                    num.ToString().Equals("all", StringComparison.InvariantCultureIgnoreCase)) &&
                    (int)num == 0)
                {
                    continue;
                }
                else
                {
                    enumNames.Add(num.ToString());
                }
            }
            Dictionary<string, PreValue> preValues = new Dictionary<string, PreValue>();
            var sort = 1;

            foreach (var name in enumNames)
            {
                preValues.Add(name, new PreValue(-1, name.ToProperCase(), sort++));
            }

            return preValues;
        }

        private DataTypeRegistration BuildEnumRegistration(Type type)
        {
            _locks.TakeOrWait(type);
            DataTypeRegistration dataTypeRegistration;
            if (_register.TryGetRegistration(type, out dataTypeRegistration))
            {
                _locks.Release(type);
                return dataTypeRegistration;
            }

            var attr = type.GetCodeFirstAttribute<DataTypeAttribute>();

            if (attr == null)
            {
                attr = new EnumDataTypeAttribute();
                attr.Initialise(type);
            }

            dataTypeRegistration = new DataTypeRegistration() 
            { 
                ClrType = type, 
                ConverterType = attr.ConverterType, 
                PropertyEditorAlias = attr.PropertyEditorAlias, 
                DataTypeInstanceName = attr.Name,
                UmbracoDatabaseType = attr.DbType,
                CodeFirstControlled = true 
            };

            _registerController.Register(type, dataTypeRegistration);
            _locks.Release(type);
            return dataTypeRegistration;
        }

        private IDataTypeDefinition UpdateOrCreateDataTypeDefinition(IDictionary<string, PreValue> codeFirstPreValues, bool updateDataTypeDefinition, DataTypeRegistration dataTypeRegistration)
        {
            IDataTypeDefinition dataTypeDefinition = _allDataTypeDefinitions.Value.SingleOrDefault(x => string.Equals(x.Name, dataTypeRegistration.DataTypeInstanceName, StringComparison.InvariantCultureIgnoreCase));

            if (updateDataTypeDefinition)
            {
                bool modified = false;
                if (dataTypeDefinition == null)
                {
                    CodeFirstManager.Current.Log("Creating data type " + dataTypeRegistration.DataTypeInstanceName, this);
                    dataTypeDefinition = CreateDataTypeDefinition(dataTypeRegistration);
                    modified = true;
                }
                else
                {
                    CodeFirstManager.Current.Log("Syncing data type " + dataTypeRegistration.DataTypeInstanceName, this);
                    modified = UpdateDataTypeDefinition(dataTypeRegistration, dataTypeDefinition);
                }

                dataTypeRegistration.Definition = dataTypeDefinition;
                if (dataTypeRegistration.CodeFirstControlled || modified)
                {
                    IDictionary<string, PreValue> preValues = MergePreValuesWithExisting(dataTypeRegistration, codeFirstPreValues, ref modified);
                    if (modified)
                    {
                        PersistDataTypeAndPreValues(dataTypeRegistration, dataTypeDefinition, preValues);
                    }
                }
            }
            else if (dataTypeRegistration.Definition == null)
            {
                dataTypeRegistration.Definition = dataTypeDefinition;
            }

            return dataTypeDefinition;
        }

        private void PersistDataTypeAndPreValues(DataTypeRegistration dataTypeRegistration, IDataTypeDefinition dataTypeDefinition, IDictionary<string, PreValue> preValues)
        {
            CodeFirstManager.Current.Log("Saving data type " + dataTypeRegistration.DataTypeInstanceName, this);
            if (CodeFirstManager.Current.Features.InitialisationMode == InitialisationMode.Ensure)
            {
                throw new CodeFirstPassiveInitialisationException("The data types or prevalues defined in the database do not match the types passed in to initialise. In InitialisationMode.Ensure the types must match or the site will be prevented from starting.");
            }
            else if (CodeFirstManager.Current.Features.InitialisationMode == InitialisationMode.Sync)
            {
                UmbracoContext.EnsureContext(new HttpContextWrapper(new HttpContext(new HttpRequest("", "http://tempuri.org", ""), new HttpResponse(new StringWriter()))), ApplicationContext.Current, true);

                _service.SaveDataTypeAndPreValues(dataTypeDefinition, preValues);
                //reset the collection if we've modified a type
                _allDataTypeDefinitions = new Lazy<IEnumerable<IDataTypeDefinition>>(() =>
                {
                    return _service.GetAllDataTypeDefinitions();
                });
            }
            else if (CodeFirstManager.Current.Features.InitialisationMode != InitialisationMode.Passive)
            {
                throw new CodeFirstException("Unknown initialisation mode");
            }
        }

        private bool UpdateDataTypeDefinition(DataTypeRegistration dataTypeRegistration, IDataTypeDefinition dataTypeDefinition)
        {
            bool modified = false;
            if (dataTypeRegistration.DbType == DatabaseType.None)
            {
                modified = false;
                dataTypeRegistration.UmbracoDatabaseType = dataTypeDefinition.DatabaseType;
            }
            else
            {
                modified = modified || dataTypeDefinition.DatabaseType != dataTypeRegistration.UmbracoDatabaseType;
                dataTypeDefinition.DatabaseType = dataTypeRegistration.UmbracoDatabaseType;
            }

            if (string.IsNullOrWhiteSpace(dataTypeRegistration.PropertyEditorAlias))
            {
                modified = false;
                dataTypeRegistration.PropertyEditorAlias = dataTypeDefinition.PropertyEditorAlias;
            }
            else
            {
                modified = modified || dataTypeRegistration.PropertyEditorAlias != dataTypeDefinition.PropertyEditorAlias;
                dataTypeDefinition.PropertyEditorAlias = dataTypeRegistration.PropertyEditorAlias;
            }
            return modified;
        }

        private IDataTypeDefinition CreateDataTypeDefinition(DataTypeRegistration dataTypeRegistration)
        {
            IDataTypeDefinition dataTypeDefinition;
            if (dataTypeRegistration.DbType == DatabaseType.None)
            {
                throw new CodeFirstException("Database type not specified for " + dataTypeRegistration.DataTypeInstanceName);
            }
            else if (string.IsNullOrWhiteSpace(dataTypeRegistration.PropertyEditorAlias))
            {
                throw new CodeFirstException("Property Editor Alias not specified for " + dataTypeRegistration.DataTypeInstanceName);
            }
            else
            {
                dataTypeDefinition = new Umbraco.Core.Models.DataTypeDefinition(-1, dataTypeRegistration.PropertyEditorAlias);
                dataTypeDefinition.Name = dataTypeRegistration.DataTypeInstanceName;
                dataTypeDefinition.DatabaseType = dataTypeRegistration.UmbracoDatabaseType;
            }
            return dataTypeDefinition;
        }
        #endregion
        #endregion

        #region IEntityTreeFilter
        public bool IsFilter(string treeAlias)
        {
            return treeAlias == "datatype";
        }

        public void Filter(Umbraco.Web.Models.Trees.TreeNodeCollection nodes, out bool changesMade)
        {
            var toRemove = new List<TreeNode>();

            foreach (var node in nodes)
            {
                var n = node.Alias;
                if (_register.Registrations.Any(x => x.DataTypeInstanceName == node.Name && x.CodeFirstControlled))
                {
                    toRemove.Add(node);
                }
            }

            changesMade = toRemove.Count > 0;

            foreach (var node in toRemove)
            {
                nodes.Remove(node);
            }
        }
        #endregion

        #region IClassFileGenerator
        public void GenerateClassFiles(string nameSpace, string folderPath)
        {
            GenerateDataTypes(nameSpace, folderPath);
        }     

        private void GenerateClassFiles(string nspace, string folderPath, List<DataTypeDescription> types)
        {
            string dataDirectory = System.IO.Path.Combine(folderPath, "DataTypes");
            if (!System.IO.Directory.Exists(dataDirectory))
            {
                System.IO.Directory.CreateDirectory(dataDirectory);
            }

            foreach (var type in types)
            {
                UmbracoCodeFirstDataType dt = new UmbracoCodeFirstDataType();
                dt.Namespace = nspace;
                dt.Model = type;
                var output = dt.TransformText();
                System.IO.File.WriteAllText(System.IO.Path.Combine(dataDirectory, type.DataTypeClassName + ".cs"), output);
            }
        }

        private void GenerateDataTypes(string nameSpace, string folderPath)
        {
            var defs = ApplicationContext.Current.Services.DataTypeService.GetAllDataTypeDefinitions();
            if (_typeDefs == null)
            {
                _typeDefs = this.GetType().Assembly.GetTypes().Where(x => x.GetCustomAttribute<BuiltInDataTypeAttribute>() != null && !x.IsGenericTypeDefinition).ToDictionary(x => x.GetInitialisedAttribute<DataTypeAttribute>(), x => x.Name);
            }
            List<DataTypeDescription> types = new List<DataTypeDescription>();
            foreach (var def in defs)
            {
                if (!_typeDefs.Any(x => x.Key.Name == def.Name)) //not a known built-in type
                {
                    var dataType = new DataTypeDescription();
                    dataType.PreValues = ApplicationContext.Current.Services.DataTypeService
                        .GetPreValuesCollectionByDataTypeId(def.Id)
                        .PreValuesAsDictionary
                        .Where(x => x.Value != null && x.Value.Value != null)
                        .Select(x => new PreValueDescription() { Alias = x.Key, Value = x.Value.Value.Replace("\"", "\"\"") })
                        .ToList();

                    if (_typeDefs.Any(x => x.Key.PropertyEditorAlias == def.PropertyEditorAlias)) //can base on a known built-in type
                    {
                        var builtIn = _typeDefs.First(x => x.Key.PropertyEditorAlias == def.PropertyEditorAlias);
                        dataType.CustomType = false;
                        dataType.InheritanceBase = builtIn.Key.DecoratedType.Name;
                        dataType.DataTypeClassName = TypeGeneratorUtils.GetDataTypeClassName(def.Id, null);
                        dataType.DataTypeInstanceName = def.Name;
                        dataType.PropertyEditorAlias = def.PropertyEditorAlias;
                        dataType.DbType = Enum.GetName(typeof(DataTypeDatabaseType), def.DatabaseType);
                    }
                    else
                    {
                        dataType.CustomType = true;
                        switch (def.DatabaseType)
                        {
                            case DataTypeDatabaseType.Date:
                                dataType.InheritanceBase = "IUmbracoDateDataType";
                                dataType.SerializedTypeName = "DateTime";
                                break;
                            case DataTypeDatabaseType.Integer:
                                dataType.InheritanceBase = "IUmbracoIntegerDataType";
                                dataType.SerializedTypeName = "int";
                                break;
                            case DataTypeDatabaseType.Ntext:
                                dataType.InheritanceBase = "IUmbracoNtextDataType";
                                dataType.SerializedTypeName = "string";
                                break;
                            case DataTypeDatabaseType.Nvarchar:
                                dataType.InheritanceBase = "IUmbracoNvarcharDataType";
                                dataType.SerializedTypeName = "string";
                                break;
                        }
                        dataType.DataTypeClassName = TypeGeneratorUtils.GetDataTypeClassName(def.Id, null);
                        dataType.DataTypeInstanceName = def.Name;
                        dataType.PropertyEditorAlias = def.PropertyEditorAlias;
                        dataType.DbType = Enum.GetName(typeof(DataTypeDatabaseType), def.DatabaseType);
                    }
                    types.Add(dataType);
                }
            }
            GenerateClassFiles(nameSpace, folderPath, types);
        }
        #endregion
    }
}

namespace Felinesoft.UmbracoCodeFirst.Extensions
{
    public static class DataTypeModuleExtensions
    {
        public static void AddDefaultDataTypeModule(this CodeFirstModuleResolver resolver)
        {
            resolver.RegisterModule<IDataTypeModule>(new DataTypeModuleFactory());
        }
    }
}
