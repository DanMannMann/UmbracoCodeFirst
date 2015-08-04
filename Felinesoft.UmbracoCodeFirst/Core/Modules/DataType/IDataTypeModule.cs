using Felinesoft.UmbracoCodeFirst.Attributes;
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
        void RegisterType(Type clrType, DataTypeRegistration registration);
    }
}