using Marsman.UmbracoCodeFirst.Attributes;
using Marsman.UmbracoCodeFirst.Converters;
using Marsman.UmbracoCodeFirst.Core.Resolver;
using System;
using System.Collections.Generic;
using System.Reflection;
using Umbraco.Core.Services;

namespace Marsman.UmbracoCodeFirst.Core.Modules
{
    public interface IDataTypeModule : ICodeFirstEntityModule
    {
        DataTypeRegister DataTypeRegister { get; }
        DataTypeRegistration GetDataType(Type type, bool updateDataTypeDefinition = true);
        DataTypeRegistration GetDataType(PropertyInfo instance, bool updateDataTypeDefinition = true);
        void RegisterNtextType<T, Tconverter>(string dataTypeName) where Tconverter : IDataTypeConverter<string, T>;
        void RegisterNvarcharType<T, Tconverter>(string dataTypeName) where Tconverter : IDataTypeConverter<string, T>;
        void RegisterIntegerType<T, Tconverter>(string dataTypeName) where Tconverter : IDataTypeConverter<int, T>;
        void RegisterDateTimeType<T, Tconverter>(string dataTypeName) where Tconverter : IDataTypeConverter<DateTime, T>;
    }
}