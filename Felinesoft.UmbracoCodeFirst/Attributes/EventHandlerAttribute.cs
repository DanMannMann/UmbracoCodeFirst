using System;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
	public class EventHandlerAttribute : CodeFirstAttribute
	{
		private object _instance;

		public EventHandlerAttribute(Type eventHandlerType)
		{
			EventHandlerType = eventHandlerType;
		}

		public Type EventHandlerType { get; private set; }

		public object EventHandler
		{
			get
			{
				_instance = _instance ?? Activator.CreateInstance(EventHandlerType ?? typeof(object));
				return _instance;
			}
		}
	}
}