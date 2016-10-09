using System;
using System.Linq;
using System.Collections.Generic;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.ContentTypes;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Umbraco.Core.Models;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{

	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class SeedFactoryAttribute : CodeFirstAttribute, IInitialisableAttribute
	{
		private bool _init;
		private bool _publishOnCreate;
		private bool _raiseEventsOnCreate;
		private int _userId;

		public SeedFactoryAttribute(int userId = 0, bool publishOnCreate = false, bool raiseEventsOnCreate = false)
		{
			_userId = userId;
			_publishOnCreate = publishOnCreate;
			_raiseEventsOnCreate = raiseEventsOnCreate;
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