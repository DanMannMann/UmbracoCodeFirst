using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;
using Felinesoft.UmbracoCodeFirst.Extensions;
using Umbraco.Core.Models;
using System.Configuration;
using Felinesoft.UmbracoCodeFirst.Attributes;
using System.Reflection;

using Felinesoft.UmbracoCodeFirst;

namespace Felinesoft.UmbracoCodeFirst.Core
{
    public static class TypeGeneratorUtils
    {
        public static string GetDataTypeClassName(int dataTypeDefinitionId, string nameSpace)
        {
            var typeDefs = typeof(TypeGeneratorUtils).Assembly.GetTypes().Where(x => x.GetCustomAttribute<BuiltInDataTypeAttribute>() != null && !x.IsGenericTypeDefinition).ToDictionary(x => x.GetCustomAttribute<DataTypeAttribute>(), x => x.Name);

            if (!string.IsNullOrEmpty(nameSpace))
            {
                nameSpace += ".";
            }

            var dtd = ApplicationContext.Current.Services.DataTypeService.GetDataTypeDefinitionById(dataTypeDefinitionId);

            if (dtd != null)
            {
                var builtIn = typeDefs.Where(x => x.Key.Name == dtd.Name).FirstOrDefault().Value;
                if (builtIn == null)
                {
                    return nameSpace + dtd.Name.Replace('.', '_').Replace('-', '_').Replace("/", "").ToPascalCase();
                }
                return builtIn;
            }
            else
            {
                return null;
            }
        }
    }
}
