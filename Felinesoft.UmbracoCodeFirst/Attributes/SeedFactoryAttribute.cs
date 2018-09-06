using System;
using System.Linq;
using System.Collections.Generic;
using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.ContentTypes;
using Marsman.UmbracoCodeFirst.Core.Resolver;
using Marsman.UmbracoCodeFirst.Exceptions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;

namespace Marsman.UmbracoCodeFirst.Attributes
{

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class SeedFactoryAttribute : CodeFirstAttribute, IInitialisableAttribute
	{
		private bool _init;
		private bool _publishOnCreate;
		private bool _raiseEventsOnCreate;
		private int _sortOrder;
		private int _userId;

		public SeedFactoryAttribute(int userId = 0, bool publishOnCreate = false, bool raiseEventsOnCreate = false, int sortOrder = 0)
		{
			_userId = userId;
			_publishOnCreate = publishOnCreate;
			_raiseEventsOnCreate = raiseEventsOnCreate;
			_sortOrder = sortOrder;
		}

		public bool Initialised
		{
			get
			{
				return _init;
			}
		}

		public bool PublishOnCreate
		{
			get
			{
				return _publishOnCreate;
			}
		}

		public bool RaiseEventsOnCreate
		{
			get
			{
				return _raiseEventsOnCreate;
			}
		}

		public int SortOrder
		{
			get
			{
				return _sortOrder;
			}
		}

		public int UserId
		{
			get
			{
				return _userId;
			}
		}

		public void Initialise(Type decoratedType)
		{
			if (decoratedType.GetInterface("ISeedFactory`1") == null)
			{
				throw new CodeFirstException(decoratedType.Name + " does not implement ISeedFactory<T>. Classes with [SeedFactory] must implement ISeedFactory<T>.");
			}
			_init = true;
		}
	}

}