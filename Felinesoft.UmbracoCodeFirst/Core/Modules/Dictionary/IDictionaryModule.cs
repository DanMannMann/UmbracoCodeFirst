using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Core.Modules;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using Felinesoft.UmbracoCodeFirst.Dictionaries;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umbraco;
using umbraco.cms.businesslogic;
using Umbraco.Core;
using Umbraco.Web;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
	public interface IDictionaryModule : ICodeFirstEntityModule
	{
		Tdict GetDictionary<Tdict>(CultureInfo culture = null) where Tdict : DictionaryBase;
	}

	public class DictionaryModuleFactory : ModuleFactoryBase<IDictionaryModule>
	{
		public override IEnumerable<Type> GetAttributeTypesToFilterOn()
		{
			return new Type[] { typeof(DictionaryAttribute) };
		}

		public override IDictionaryModule CreateInstance(IEnumerable<ICodeFirstEntityModule> dependencies)
		{
			return new DictionaryModule();
		}
	}

	public class DictionaryInstance
	{
		public List<string> Keys { get; set; } = new List<string>();

		public Dictionary<CultureInfo, object> Instances { get; set; } = new Dictionary<CultureInfo, object>();

		public bool Invalidated { get; set; }
	}

	public class DictionaryModule : IDictionaryModule
	{
		private Dictionary<Type, DictionaryInstance> _localisedDictionaries = new Dictionary<Type, DictionaryInstance>();
		private List<string> _invalidated = new List<string>();

		public Tdict GetDictionary<Tdict>(CultureInfo culture = null) where Tdict : DictionaryBase
		{
			lock (_localisedDictionaries)
			{
				if (!_localisedDictionaries.ContainsKey(typeof(Tdict)))
				{
					throw new CodeFirstException(typeof(Tdict) + " is not registered as a dictionary with the DictionaryModule");
				}

				if (culture == null)
				{
					culture = CultureInfo.CurrentCulture;
				}

				if (!_localisedDictionaries[typeof(Tdict)].Instances.ContainsKey(culture))
				{
					var dict = PopulateDictionary<Tdict>(culture);
					_localisedDictionaries[typeof(Tdict)].Instances.Add(culture, dict);
				}
				else if (_localisedDictionaries[typeof(Tdict)].Invalidated)
				{
					var dict = PopulateDictionary<Tdict>(culture);
					_localisedDictionaries[typeof(Tdict)].Instances[culture] = dict;
					_localisedDictionaries[typeof(Tdict)].Invalidated = false;
				}

				return (Tdict)_localisedDictionaries[typeof(Tdict)].Instances[culture];
			}
		}

		private object PopulateDictionary<Tdict>(CultureInfo culture) where Tdict : DictionaryBase
		{
			UmbracoHelper h = UmbracoContext.Current == null ? new UmbracoHelper() : new UmbracoHelper(UmbracoContext.Current);
			var dict = Activator.CreateInstance<Tdict>();
			var attr = typeof(Tdict).GetCodeFirstAttribute<DictionaryAttribute>();
			var props = typeof(Tdict).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(x => x.GetCodeFirstAttribute<ItemAttribute>() != null);
			foreach (var prop in props)
			{
				var key = prop.GetCodeFirstAttribute<ItemAttribute>();
				var completeKey = string.Format("{0}_{1}", attr.DictionaryName, key.Key);
				prop.SetValue(dict, h.GetDictionaryValue(completeKey) ?? string.Empty);
			}
			return dict;
		}

		public void Initialise(IEnumerable<Type> classes)
		{
			foreach (var type in classes)
			{
				var attr = type.GetCodeFirstAttribute<DictionaryAttribute>();
				var props = type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).Where(x => x.GetCodeFirstAttribute<ItemAttribute>() != null);
				var wrapper = new DictionaryInstance();
				foreach(var prop in props)
				{
					var key = prop.GetCodeFirstAttribute<ItemAttribute>();
					var completeKey = string.Format("{0}_{1}", attr.DictionaryName, key.Key);
					if (!Dictionary.DictionaryItem.hasKey(completeKey))
					{
						Dictionary.DictionaryItem.addKey(completeKey, key.DefaultValue);
					}
					wrapper.Keys.Add(completeKey);
				}
				_localisedDictionaries.Add(type, wrapper);
			}

			Dictionary.DictionaryItem.Saving += DictionaryItem_Saving;
		}

		private void DictionaryItem_Saving(Dictionary.DictionaryItem sender, EventArgs e)
		{
			foreach(var dict in _localisedDictionaries.Where(x => x.Value.Keys.Contains(sender.key)))
			{
				dict.Value.Invalidated = true;
			}
		}
	}
}

namespace Felinesoft.UmbracoCodeFirst.Extensions
{
	public static class DictionaryModuleExtensions
	{
		public static void AddDefaultDictionaryModule(this CodeFirstModuleResolver resolver)
		{
			resolver.RegisterModule<IDictionaryModule>(new DictionaryModuleFactory());
		}
	}
}
