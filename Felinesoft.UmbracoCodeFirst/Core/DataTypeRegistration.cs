using Felinesoft.UmbracoCodeFirst.Converters;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Felinesoft.UmbracoCodeFirst.Attributes;
using Umbraco.Core.Models;
using System.Collections.Concurrent;
using Felinesoft.UmbracoCodeFirst.DataTypes;

namespace Felinesoft.UmbracoCodeFirst
{
    /// <summary>
    /// Represents the information needed to map an Umbraco data type to a .NET property type
    /// </summary>
    public class DataTypeRegistration
    {
        /// <summary>
        /// Represents the information needed to map an Umbraco data type to a .NET property type
        /// </summary>
        /// <param name="dataTypeInstanceName">The data type instance name</param>
        /// <param name="propertyEditorAlias">The property editor alias</param>
        /// <param name="converterType">The type converter to use to transform the Umbraco value into its .NET counterpart</param>
        public DataTypeRegistration(string dataTypeInstanceName, string propertyEditorAlias, Type converterType = null)
        {
            DataTypeInstanceName = dataTypeInstanceName;
            PropertyEditorAlias = propertyEditorAlias;
            ConverterType = converterType;
        }

        /// <summary>
        /// Internal constructor to allow parameterless instantiation
        /// </summary>
        internal DataTypeRegistration() { }

        /// <summary>
        /// The data type instance name
        /// </summary>
        /// <remarks>This is set internally once it is inferred, but will only be inferred if it is null when the initialiser is called.</remarks>
        public string DataTypeInstanceName { get; internal set; }

        /// <summary>
        /// The type converter to use to transform the Umbraco value into its .NET counterpart
        /// </summary>
        /// <remarks>This is set internally once it is inferred, but will only be inferred if it is null when the initialiser is called.</remarks>
        public Type ConverterType { get; internal set; }

        /// <summary>
        /// The property editor alias
        /// </summary>
        /// <remarks>This is set internally once it is inferred, but will only be inferred if it is null when the initialiser is called.</remarks>
        public string PropertyEditorAlias { get; internal set; }

        /// <summary>
        /// The database storage type used to store the value
        /// </summary>
        /// <remarks>This is set internally once it is inferred</remarks>
        public DataTypeDatabaseType DbType { get; internal set; }

        /// <summary>
        /// The Umbraco data type definition
        /// </summary>
        /// <remarks>This is set internally once it is inferred</remarks>
        public IDataTypeDefinition Definition { get; internal set; }
    }
}
