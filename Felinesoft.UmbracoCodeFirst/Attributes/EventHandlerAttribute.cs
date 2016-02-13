using System;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
	public class EventHandlerAttribute : CodeFirstAttribute, IInitialisableAttribute
	{
		private object _instance;

		public EventHandlerAttribute(Type eventHandlerType = null)
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

		public bool Initialised { get; private set; }

		public bool ContentIsSelfHandling { get; private set; }

		public void Initialise(Type decoratedType)
		{
			if (EventHandlerType == null)
			{
				EventHandlerType = decoratedType;
				ContentIsSelfHandling = true;
            }
			Initialised = true;
        }
	}
}