using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Felinesoft.UmbracoCodeFirst.Attributes;

namespace Felinesoft.UmbracoCodeFirst.Extensions
{
    public static class AttributeExtensions
    {
        public static T GetInitialisedAttribute<T>(this Type type) where T : Attribute
        {
            T attr = type.GetCustomAttribute<T>(false);
            if (attr == null)
            {
                return null;
            }
            if (attr is IInitialisableAttribute && !(attr as IInitialisableAttribute).Initialised)
            {
                (attr as IInitialisableAttribute).Initialise(type);
            }
            return attr;
        }

        public static IEnumerable<T> GetInitialisedAttributes<T>(this Type type) where T : Attribute
        {
            var attrs = type.GetCustomAttributes<T>(false);

            foreach (var attr in attrs)
            {
                if (attr is IInitialisableAttribute && !(attr as IInitialisableAttribute).Initialised)
                {
                    (attr as IInitialisableAttribute).Initialise(type);
                }
            }
            return attrs;
        }

        public static T GetInitialisedAttribute<T>(this PropertyInfo info) where T : Attribute
        {
            T attr = info.GetCustomAttribute<T>(false);
            if (attr == null)
            {
                return null;
            }
            if (attr is IInitialisablePropertyAttribute && !(attr as IInitialisablePropertyAttribute).Initialised)
            {
                (attr as IInitialisablePropertyAttribute).Initialise(info);
            }
            return attr;
        }

        public static IEnumerable<T> GetInitialisedAttributes<T>(this PropertyInfo info) where T : Attribute
        {
            var attrs = info.GetCustomAttributes<T>(false);

            foreach (var attr in attrs)
            {
                if (attr is IInitialisablePropertyAttribute && !(attr as IInitialisablePropertyAttribute).Initialised)
                {
                    (attr as IInitialisablePropertyAttribute).Initialise(info);
                }
            }
            return attrs;
        }
    }
}
