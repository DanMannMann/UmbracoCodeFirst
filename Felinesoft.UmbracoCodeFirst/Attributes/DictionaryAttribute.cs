using System;
using System.Linq;
using System.Collections.Generic;
using Marsman.UmbracoCodeFirst.Exceptions;
using System.Reflection;

namespace Marsman.UmbracoCodeFirst.Attributes
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class ItemAttribute : CodeFirstAttribute, IInitialisablePropertyAttribute
	{
		public ItemAttribute(string key = null, string defaultValue = null)
		{
			Key = key;
			DefaultValue = defaultValue;
		}

		public bool Initialised { get; private set; }

		public string Key { get; private set; }

		public string DefaultValue { get; private set; }

		public void Initialise(PropertyInfo propertyTarget)
		{
			if (Key == null)
			{
				Key = propertyTarget.Name;
			}
			Initialised = true;
		}
	}

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class DictionaryAttribute : CodeFirstAttribute, IInitialisableAttribute
	{
		public DictionaryAttribute(string dictionaryName = null)
		{
			DictionaryName = dictionaryName;
		}

		public string DictionaryName { get; private set; }

		public bool Initialised { get; private set; }

		public void Initialise(Type decoratedType)
		{
			if (Initialised)
			{
				throw new CodeFirstException("Already initialised");
			}

			if (DictionaryName == null)
			{
				DictionaryName = decoratedType.Name;
			}

			Initialised = true;
		}
	}
}