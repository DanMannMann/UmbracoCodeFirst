using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Umbraco.Core.Models;
using Umbraco.Core;
using Felinesoft.UmbracoCodeFirst.DataTypes;

namespace Felinesoft.UmbracoCodeFirst
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
        private ConcurrentDictionary<Type, DataTypeRegistration> _register = new ConcurrentDictionary<Type,DataTypeRegistration>();
        private static DataTypeRegister _current;
        private static object _currentLock = new object();

        /// <summary>
        /// Set to false to stop the out-of-the-box types (string, int, bool and DateTime) from being automatically registered.
        /// Needed if you want to bind any of them to a different data type at a global level.
        /// </summary>
        public static bool RegisterBuiltInTypes { get; set; }

        static DataTypeRegister()
        {
            RegisterBuiltInTypes = true;
        }

        /// <summary>
        /// Gets the current singleton instance of <see cref="DataTypeRegister"/>
        /// </summary>
        public static DataTypeRegister Current
        {
            get
            {
                lock (_currentLock)
                {
                    if (_current == null)
                    {
                        Initialise();
                    }
                }
                return _current;
            }
        }

        private static void Initialise()
        {
            _current = new DataTypeRegister();

            if (RegisterBuiltInTypes)
            {
                //Initialise the built-in types
                _current.Register(typeof(string), new DataTypeRegistration()
                {
                    DataTypeInstanceName = BuiltInDataTypes.Textbox
                });
                _current.Register(typeof(bool), new DataTypeRegistration()
                {
                    DataTypeInstanceName = BuiltInDataTypes.TrueFalse,
                    ConverterType = typeof(Felinesoft.UmbracoCodeFirst.Converters.BooleanConverter)
                });
                _current.Register(typeof(DateTime), new DataTypeRegistration()
                {
                    DataTypeInstanceName = BuiltInDataTypes.DatePickerWithTime
                    //ConverterType = typeof(Felinesoft.UmbracoCodeFirst.Converters.UDateTimeConverter)
                });
                _current.Register(typeof(int), new DataTypeRegistration()
                {
                    DataTypeInstanceName = BuiltInDataTypes.Numeric
                    //ConverterType = typeof(Felinesoft.UmbracoCodeFirst.Converters.UIntegerConverter)
                });
            }
        }

        /// <summary>
        /// Returns true if the specified type is registered
        /// </summary>
        public bool IsRegistered(Type dataType)
        {
            return _register.ContainsKey(dataType);
        }

        /// <summary>
        /// Registers the given data type definition for the specified type
        /// </summary>
        /// <exception cref="CodeFirstException">Thrown if the specified type is already registered. 
        /// This operation is not thread safe. If Register may be called from multiple threads in your application
        /// then you are responsible for synchronising those calls.</exception>
        public void Register(Type dataType, DataTypeRegistration definition)
        {
            if (!_register.TryAdd(dataType, definition))
            {
                throw new CodeFirstException("Data type already registered");
            }
        }

        /// <summary>
        /// Gets the registration for the specified type
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if the type is not registered</exception>
        public DataTypeRegistration GetRegistration(Type dataType)
        {
            return _register[dataType];
        }

        /// <summary>
        /// Gets all types with a data type definition id which appears in the given collection
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if the type is not registered</exception>
        internal List<Type> GetTypesByDataTypeDefinitionIds(IEnumerable<int> dataTypes)
        {
            var result = new List<Type>();
            foreach (var dataType in dataTypes)
            {
                var reg = _register.Where(x => x.Value != null && x.Value.Definition != null && x.Value.Definition.Id == dataType);
                if (reg.Count() > 0)
                {
                    result.AddRange(reg.Select(x => x.Key));
                }
            }
            return result;
        }

        /// <summary>
        /// Gets the property editor alias for a given type
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if the type is not registered</exception>
        internal string GetPropertyEditorAlias(Type dataType)
        {
            return _register[dataType].PropertyEditorAlias;
        }

        /// <summary>
        /// Gets the converter type for a given type
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if the type is not registered</exception>
        internal Type GetConverterType(Type dataType)
        {
            return _register[dataType].ConverterType;
        }

        /// <summary>
        /// Gets the instance name for the specified data type
        /// </summary>
        /// <exception cref="KeyNotFoundException">Thrown if the type is not registered</exception>
        internal string GetDataTypeInstanceName(Type dataType)
        {
            return _register[dataType].DataTypeInstanceName;
        }
    }
}
