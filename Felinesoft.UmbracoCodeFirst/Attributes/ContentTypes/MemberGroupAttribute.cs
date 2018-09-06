using Marsman.UmbracoCodeFirst.Extensions;
using System;

namespace Marsman.UmbracoCodeFirst.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MemberGroupAttribute : CodeFirstAttribute, IInitialisableAttribute
    {
        public MemberGroupAttribute(string name = null)
        {
            Name = name;
        }

        public void Initialise(Type decoratedType)
        {
            if (Name == null)
            {
                Name = decoratedType.Name.ToProperCase();
            }
            Initialised = true;
        }

        public bool Initialised
        {
            get;
            private set;
        }

        public string Name { get; protected set; }
    }
}