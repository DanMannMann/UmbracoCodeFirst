
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;
using Felinesoft.UmbracoCodeFirst.Converters;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Umbraco.Core;
using umbraco.cms.businesslogic.datatype;
using Felinesoft.UmbracoCodeFirst.Exceptions;

namespace Felinesoft.UmbracoCodeFirst.Attributes
{
    /// <summary>
    /// Specifies that the decorated type is a code-first data type
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
    public class DataTypeAttribute : CodeFirstAttribute, IInitialisableAttribute
    {
        private bool _initialised = false;
        private DatabaseType _dbType;
        private Type _converterType, _decoratedType;
        private bool _useConverter;

        /// <summary>
        /// Specifies that the decorated type is a code-first data type
        /// </summary>
        /// <param name="propertyEditorAlias">The property editor alias for the data type</param>
        /// <param name="name">The instance name for the data type</param>
        /// <param name="converterType">The default converter type which can convert between the code-first class and an Umbraco property of that data type</param>
        /// <param name="dbType">The storage type used to store the data type value in the database</param>
        /// <param name="useConverter">False to use no converter if converterType is null, true to attempt to use an automatic converter.
        /// Using an automatic converter requires the data type class to implement IUmbracoDataType[`Tdb]</param>
        public DataTypeAttribute(string propertyEditorAlias = null, string name = null, Type converterType = null, DatabaseType dbType = DatabaseType.None, bool useConverter = true)
        {
            Name = name;
            PropertyEditorAlias = propertyEditorAlias;
            _converterType = converterType;
            _dbType = dbType;
            _useConverter = useConverter;
        }

        /// <summary>
        /// The default converter type which can convert between the code-first class and an Umbraco property of that data type
        /// </summary>
        public Type ConverterType
        {
            get
            {
                if(!_initialised)
                {
                    throw new AttributeInitialisationException("Not initialised!");
                }
                return _converterType;
            }
            protected set
            {
                _converterType = value;
            }
        }

        /// <summary>
        /// Initialises the Factory property based on the type to which the attribute is applied.
        /// </summary>
        /// <param name="targetType">The type to which the attribute is applied</param>
        /// <returns>Returns itself, for method chaining.</returns>
        public virtual void Initialise(Type targetType)
        {
            if (_initialised)
            {
                throw new AttributeInitialisationException("Already initialised!");
            }

            DataTypeAttribute nearestAncestor = null;
            Type ancestorType = targetType;

            while (nearestAncestor == null && ancestorType != null)
            {
                ancestorType = ancestorType.BaseType;
                if (ancestorType != null)
                {
                    nearestAncestor = ancestorType.GetCodeFirstAttribute<DataTypeAttribute>();
                }
            }

            if (string.IsNullOrEmpty(Name))
            {
                Name = targetType.Name.ToProperCase();
                if (Name.Contains("`"))
                {
                    var args = targetType.GetGenericArguments();
                    for (int i = 1; i <= args.Length; i++)
                    {
                        var typeName = args[i - 1].GetCodeFirstAttribute<ContentTypeAttribute>()?.Name ?? args[i - 1].Name;
                        Name = Name.Replace("` " + i.ToString(), " - " + typeName);
                    }
                }
            }

            if (string.IsNullOrEmpty(PropertyEditorAlias))
            {
                if (nearestAncestor == null)
                {
                    PropertyEditorAlias = targetType.Name.ToCamelCase();
                }
                else
                {
                    PropertyEditorAlias = nearestAncestor.PropertyEditorAlias;
                }
            }

            if (_converterType == null && _useConverter)
            {
                try
                {
                    DatabaseType storageType;
                    _converterType = MakeAutoConverterType(targetType, out storageType);
                    _dbType = storageType;
                }
                catch (CodeFirstException)
                {
                    if (nearestAncestor == null)
                    {
                        throw;
                    }
                    else //If there is an ancestor we can inherit values from then we're away.
                    {
                        _converterType = nearestAncestor.ConverterType;
                        _dbType = nearestAncestor._dbType;
                    }
                }
            }

            if (_dbType == DatabaseType.None)
            {
                throw new AttributeInitialisationException("Database storage type was not set and could not be inferred. Affected type: " + targetType.FullName);
            }
            _decoratedType = targetType;
            _initialised = true;
        }
   
        /// <summary>
        /// The instance name of the data type
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The property editor alias of the data type
        /// </summary>
        public string PropertyEditorAlias { get; protected set; }

        /// <summary>
        /// The database storage type
        /// </summary>
        public DataTypeDatabaseType DbType
        {
            get
            {
                if (!_initialised)
                {
                    throw new AttributeInitialisationException("Not initialised!");
                }
                return (DataTypeDatabaseType)((int)_dbType - 1);
            }
            protected set
            {
                _dbType = (DatabaseType)((int)value + 1);
            }
        }

        /// <summary>
        /// Gets the converter type
        /// </summary>
        protected Type GetConverterType()
        {
            return _converterType;
        }

        /// <summary>
        /// Composes a generic converter which can convert the IUmbracoDataType to which this attribute is applied
        /// </summary>
        protected Type MakeAutoConverterType(Type targetType, out DatabaseType storageType)
        {
            if (targetType == null)
            {
                throw new ArgumentNullException("targetType");
            }

            Type underlyingValueType;
            DataTypeDatabaseType? storeType;
            if (targetType.IsUmbracoDataType(out underlyingValueType, out storeType))
            {
                if (_dbType != DatabaseType.None)
                {
                    storageType = _dbType;
                }
                else if (storeType.HasValue)
                {
                    storageType = (DatabaseType)((int)storeType.Value + 1);
                }
                else if (underlyingValueType == typeof(string))
                {
                    storageType = DatabaseType.Ntext;
                }
                else if (underlyingValueType == typeof(int))
                {
                    storageType = DatabaseType.Integer;
                }
                else if (underlyingValueType == typeof(DateTime))
                {
                    storageType = DatabaseType.Date;
                }
                else
                {
                    throw new CodeFirstException("[DataType] attribute does not specify a database type and the type class does not implement a valid IUmbracoDataType<T> interface. The database storage type cannot be inferred. Invalid Type: " + targetType.FullName);
                }

                return typeof(AutoDataTypeConverter<,>).MakeGenericType(underlyingValueType, targetType);
            }
            else
            {
                throw new CodeFirstException("[DataType] does not specify a valid IUmbracoDataType<T> interface. The database storage type cannot be inferred. Invalid Type: " + targetType.FullName);
            }
        }

        /// <summary>
        /// Returns true if the attribute instance has been initialised.
        /// Until the attribute is initialised accessing the DbType or ConverterType properties will cause an exception.
        /// </summary>
        public virtual bool Initialised
        {
            get
            {
                return _initialised;
            }
            protected set
            {
                _initialised = value;
            }
        }

        /// <summary>
        /// True to use the specified converter, false to do no conversion
        /// </summary>
        protected bool UseConverter
        {
            get
            {
                return _useConverter;
            }
            set
            {
                _useConverter = value;
            }
        }

        /// <summary>
        /// The type to which this instance of the attribute is applied
        /// </summary>
        public Type DecoratedType
        {
            get
            {
                if (!_initialised)
                {
                    throw new AttributeInitialisationException("Not initialised!");
                }
                return _decoratedType;
            }
            protected set
            {
                _decoratedType = value;
            }
        }
    }

}
