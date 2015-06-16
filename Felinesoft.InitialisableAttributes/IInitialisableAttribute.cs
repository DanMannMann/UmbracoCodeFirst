using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Felinesoft.InitialisableAttributes
{
    public interface IInitialisableAttribute
    {
        /// <summary>
        /// Initialises the Factory property based on the type to which the attribute is applied.
        /// </summary>
        /// <param name="targetType">The type to which the attribute is applied</param>
        /// <returns>Returns itself, for method chaining.</returns>
        void Initialise(Type decoratedType);

        /// <summary>
        /// Returns true if the attribute instance has been initialised.
        /// Until the attribute is initialised accessing the Factory or FactoryType properties will cause an exception.
        /// </summary>
        bool Initialised { get; }
    }

    public interface IInitialisablePropertyAttribute
    {
        /// <summary>
        /// Initialises the Factory property based on the property to which the attribute is applied.
        /// </summary>
        /// <param name="propertyTarget">The property to which the attribute is applied</param>
        /// <returns>Returns itself, for method chaining.</returns>
        void Initialise(PropertyInfo propertyTarget);

        /// <summary>
        /// Returns true if the attribute instance has been initialised.
        /// Until the attribute is initialised accessing the Factory or FactoryType properties will cause an exception.
        /// </summary>
        bool Initialised { get; }
    }
}
