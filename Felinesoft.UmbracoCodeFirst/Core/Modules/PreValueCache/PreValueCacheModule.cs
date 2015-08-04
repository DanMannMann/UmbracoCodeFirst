using Felinesoft.UmbracoCodeFirst.Core.Modules;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public class PreValueCacheModule : IPreValueCacheModule
    {
        private ConcurrentDictionary<DataTypeRegistration, List<PreValue>> _cache = new ConcurrentDictionary<DataTypeRegistration, List<PreValue>>();
        private  IDataTypeModule _dataTypeModule;
        private IDataTypeService _service;

        public PreValueCacheModule(IDataTypeModule dataTypeModule, IDataTypeService service)
        {
            _service = service;
            _dataTypeModule = dataTypeModule;
        }

        /// <summary>
        /// Gets the prevalues for a specified data type
        /// </summary>
        public IReadOnlyList<PreValue> Get(DataTypeRegistration registration)
        {
            List<PreValue> result;
            if (!_cache.TryGetValue(registration, out result))
            {
                result = GetPreValues(registration.Definition.Id);
                if (!_cache.TryAdd(registration, result))
                {
                    throw new CodeFirstException("Unable to cache prevalues");
                }
            }
            return result;
        }

        private void DataTypeService_Saved(Umbraco.Core.Services.IDataTypeService sender, Umbraco.Core.Events.SaveEventArgs<IDataTypeDefinition> e)
        {
            var types = CodeFirstManager.Current.Modules.DataTypeModule.DataTypeRegister.GetTypesByDataTypeDefinitionIds(e.SavedEntities.Select(x => x.Id));
            foreach (var type in types)
            {
                List<PreValue> val;
                _cache.TryRemove(type, out val); //invalidate any prevalue cache so prevalues are refreshed when next needed
            }
        }

        private List<PreValue> GetPreValues(int dataTypeId)
        {
            List<Umbraco.Core.Models.PreValue> result = new List<Umbraco.Core.Models.PreValue>();
            var preValues = _service.GetPreValuesCollectionByDataTypeId(dataTypeId);

            if (preValues.IsDictionaryBased)
            {
                result.AddRange(preValues.PreValuesAsDictionary.Values.OrderBy(x => x.SortOrder));
            }
            else
            {
                result.AddRange(preValues.PreValuesAsArray.OrderBy(x => x.SortOrder));
            }

            return result;
        }

        public void Initialise(IEnumerable<Type> classes)
        {
            Umbraco.Core.Services.DataTypeService.Saved += DataTypeService_Saved;
        }
    }

}

namespace Felinesoft.UmbracoCodeFirst.Extensions
{
    public static class PreValueCacheModuleExtensions
    {
        public static void AddDefaultPreValueCacheModule(this CodeFirstModuleResolver resolver)
        {
            resolver.RegisterModule<IPreValueCacheModule>(new PreValueCacheModuleFactory());
        }
    }
}
