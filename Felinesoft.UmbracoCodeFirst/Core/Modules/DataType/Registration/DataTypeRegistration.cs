using Marsman.UmbracoCodeFirst.Converters;
using Marsman.UmbracoCodeFirst.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Marsman.UmbracoCodeFirst.Attributes;
using Umbraco.Core.Models;
using System.Collections.Concurrent;
using Marsman.UmbracoCodeFirst.DataTypes;
using Umbraco.Core.PropertyEditors;
using Marsman.UmbracoCodeFirst.Core;

namespace Marsman.UmbracoCodeFirst
{
    /// <summary>
    /// Represents the information needed to map an Umbraco data type to a .NET property type
    /// </summary>
    public class DataTypeRegistration : CodeFirstRegistration
    {
        public override bool Equals(object obj)
        {
            if (obj != null && (obj is DataTypeRegistration))
            {
                return (obj as DataTypeRegistration).DataTypeInstanceName == DataTypeInstanceName;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return DataTypeInstanceName == null ? string.Empty.GetHashCode() : DataTypeInstanceName.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        internal DataTypeRegistration() { }

        /// <summary>
        /// Represents the information needed to map an Umbraco data type to a .NET property type
        /// </summary>
        /// <param name="dataTypeInstanceName">The data type instance name</param>
        /// <param name="propertyEditorAlias">The property editor alias</param>
        /// <param name="converterType">The type converter to use to transform the Umbraco value into its .NET counterpart</param>
        public DataTypeRegistration(string dataTypeInstanceName, string propertyEditorAlias, Type converterType, DatabaseType dbType, IDataTypeDefinition definition, bool codeFirstControlled)
        {
            DataTypeInstanceName = dataTypeInstanceName;
            PropertyEditorAlias = propertyEditorAlias;
            ConverterType = converterType;
            DbType = dbType;
            Definition = definition;
            CodeFirstControlled = codeFirstControlled;
        }

        /// <summary>
        /// The data type instance name
        /// </summary>
        public string DataTypeInstanceName { get; internal set; }

        /// <summary>
        /// The type converter to use to transform the Umbraco value into its .NET counterpart
        /// </summary>
        public Type ConverterType { get; internal set; }

        /// <summary>
        /// The property editor alias
        /// </summary>
        public string PropertyEditorAlias { get; internal set; }

        /// <summary>
        /// The database storage type used to store the value
        /// </summary>
        public DatabaseType DbType { get; internal set; }

        public DataTypeDatabaseType UmbracoDatabaseType
        {
            get
            {
                return (DataTypeDatabaseType)((int)DbType - 1);
            }
            internal set
            {
                DbType = (DatabaseType)((int)value + 1);
            }
        }

        /// <summary>
        /// The Umbraco data type definition
        /// </summary>
        public IDataTypeDefinition Definition { get; internal set; }

        public bool CodeFirstControlled { get; internal set; }

        public Type ClrType { get; internal set; }
    }
}
