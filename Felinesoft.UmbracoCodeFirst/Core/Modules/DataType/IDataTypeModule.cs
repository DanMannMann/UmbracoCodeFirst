using Felinesoft.UmbracoCodeFirst.Attributes;
using Felinesoft.UmbracoCodeFirst.Converters;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using System;
using System.Collections.Generic;
using System.Reflection;
using Umbraco.Core.Services;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
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