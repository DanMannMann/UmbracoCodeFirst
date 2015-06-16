using Felinesoft.UmbracoCodeFirst.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;

namespace Felinesoft.UmbracoCodeFirst
{
    /// <summary>
    /// Maintains a cache of retrieved prevalues, selectively invalidating the cache if a given
    /// data type is saved from the back-office.
    /// </summary>
    internal static class PreValueCache
    {
        private static ConcurrentDictionary<Type, List<PreValue>> _cache = new ConcurrentDictionary<Type, List<PreValue>>();

        static PreValueCache()
        {
            Umbraco.Core.Services.DataTypeService.Saved += DataTypeService_Saved;
        }

        /// <summary>
        /// Gets the prevalues for a specified data type
        /// </summary>
        internal static IReadOnlyList<PreValue> Get(Type type)
        {
            List<PreValue> result;
            if (!_cache.TryGetValue(type, out result))
            {
                if (!DataTypeRegister.Current.IsRegistered(type))
                {
                    throw new CodeFirstException(type.Name + " is not registered. Cannot get prevalues.");
                }

                var reg = DataTypeRegister.Current.GetRegistration(type);
                result = GetPreValues(reg.Definition.Id);
                if (!_cache.TryAdd(type, result))
                {
                    throw new CodeFirstException("Unable to cache prevalues");
                }
            }
            return result;
        }

        private static void DataTypeService_Saved(Umbraco.Core.Services.IDataTypeService sender, Umbraco.Core.Events.SaveEventArgs<IDataTypeDefinition> e)
        {
            var types = DataTypeRegister.Current.GetTypesByDataTypeDefinitionIds(e.SavedEntities.Select(x => x.Id));
            foreach (var type in types)
            {
                List<PreValue> val;
                _cache.TryRemove(type, out val); //invalidate any prevalue cache so prevalues are refreshed when next needed
            }
        }

        private static List<PreValue> GetPreValues(int dataTypeId)
        {
            List<Umbraco.Core.Models.PreValue> result = new List<Umbraco.Core.Models.PreValue>();
            var preValues = ApplicationContext.Current.Services.DataTypeService.GetPreValuesCollectionByDataTypeId(dataTypeId);

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
    }
}
