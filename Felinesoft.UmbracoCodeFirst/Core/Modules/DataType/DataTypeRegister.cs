using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.Exceptions;
using Umbraco.Core.Models;
using Umbraco.Core;
using Marsman.UmbracoCodeFirst.DataTypes;
using System.Reflection;
using Umbraco.Core.Services;

namespace Marsman.UmbracoCodeFirst.Core.Modules
{
    /// <summary>
    /// Allows the registration and retrieval of data type definitions.
    /// Used internally when a data type is processed via an attribute, and can be used
    /// by consuming code to register data types which cannot be decorated with an attribute. For example
    /// one could register a <see cref="DataTypeRegistration"/> for an enum from the .NET Framework, for 
    /// a complex type from a library or indeed for any type whose source code is not controlled by the caller.
    /// 
    /// A registration made explicitly via a call to Register will always take precedence over values specified in a [DataType] attribute.
    /// If a data type is used for a property which specifies its own alias, data type name or converter in its property attribute
    /// then these settings take precedence over both the registration and the data type attribute.
    /// </summary>
    public class DataTypeRegister
    {
        private DataTypeRegisterController _controller;
        private ConcurrentDictionary<Type, DataTypeRegistration> _register = new ConcurrentDictionary<Type, DataTypeRegistration>();
        private ConcurrentDictionary<string, DataTypeRegistration> _instanceRegister = new ConcurrentDictionary<string, DataTypeRegistration>();
        private IDataTypeService _service;

        public DataTypeRegister(out DataTypeRegisterController controller, IDataTypeService service)
        {
            _service = service;
            controller = _controller = new DataTypeRegisterController(this);
        }

        public IReadOnlyList<DataTypeRegistration> Registrations
        {
            get
            {
                return _register.Values.Union(_instanceRegister.Values).Distinct().ToList().AsReadOnly();
            }
        }

        /// <summary>
        /// Gets all types with a data type definition id which appears in the given collection
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if the type is not registered</exception>
        public List<DataTypeRegistration> GetTypesByDataTypeDefinitionIds(IEnumerable<int> dataTypes)
        {
            var result = new List<DataTypeRegistration>();
            foreach (var dataType in dataTypes)
            {
                var reg = _register.Where(x => x.Value != null && x.Value.Definition != null && x.Value.Definition.Id == dataType);
                if (reg.Count() > 0)
                {
                    result.AddRange(reg.Select(x => x.Value));
                }
            }
            return result;
        }

        /// <summary>
        /// Returns true if the specified type is registered
        /// </summary>
        public bool IsRegistered(Type dataType)
        {
            return _register.ContainsKey(dataType);
        }

        /// <summary>
        /// Returns true if the property instance is registered or if the property's type is a registered data type
        /// </summary>
        public bool IsRegistered(PropertyInfo instance)
        {
            return _instanceRegister.ContainsKey(instance.DeclaringType.FullName + "." + instance.Name) ? true : _register.ContainsKey(instance.PropertyType);
        }

        /// <summary>
        /// Returns true if the specific property instance is registered. False otherwise, even if the property's type is a registered data type
        /// </summary>
        public bool IsRegisteredInstance(PropertyInfo info)
        {
            return _instanceRegister.ContainsKey(info.DeclaringType.FullName + "." + info.Name);
        }

        /// <summary>
        /// Gets the registration for the specified property instance
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if the property instance is not registered</exception>
        public bool TryGetRegistration(Type dataType, out DataTypeRegistration registration)
        {
            return _register.TryGetValue(dataType, out registration);
        }

        /// <summary>
        /// Gets the registration for the specified type
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if the type is not registered</exception>
        public bool TryGetRegistration(PropertyInfo instance, out DataTypeRegistration registration)
        {
            var key = string.Format("{0}.{1}", instance.DeclaringType.FullName, instance.Name);
            return _instanceRegister.TryGetValue(key, out registration);
        }

        public class DataTypeRegisterController
        {
            private DataTypeRegister _instance;

            internal DataTypeRegisterController(DataTypeRegister instance)
            {
                _instance = instance;
            }

            /// <summary>
            /// Registers the given data type definition for the specified type
            /// </summary>
            /// <exception cref="CodeFirstException">Thrown if the specified type is already registered. 
            /// This operation is not thread safe. If Register may be called from multiple threads in your application
            /// then you are responsible for synchronising those calls.</exception>
            public void Register(Type dataType, DataTypeRegistration definition)
            {
                if (!_instance._register.TryAdd(dataType, definition))
                {
                    throw new CodeFirstException("Data type already registered");
                }
            }

            /// <summary>
            /// Registers the given data type definition for the specified property instance
            /// </summary>
            /// <exception cref="CodeFirstException">Thrown if the specified instance is already registered. 
            /// This operation is not thread safe. If Register may be called from multiple threads in your application
            /// then you are responsible for synchronising those calls.</exception>
            public void Register(PropertyInfo instance, DataTypeRegistration definition)
            {
                if (!_instance._instanceRegister.TryAdd(instance.DeclaringType.FullName + "." + instance.Name, definition))
                {
                    throw new CodeFirstException("Data type instance already registered");
                }
            }
        }
    }
}
