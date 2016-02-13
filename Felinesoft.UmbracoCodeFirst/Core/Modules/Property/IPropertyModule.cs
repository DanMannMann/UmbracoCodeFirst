using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Felinesoft.UmbracoCodeFirst.Core.Modules
{
    public interface IPropertyModule : ICodeFirstEntityModule
    {
        PropertyRegistration CreateProperty(IContentTypeBase newContentType, TabRegistration tab, PropertyInfo item, Type documentClrType);
        PropertyRegistration VerifyExistingProperty(IContentTypeBase contentType, TabRegistration tab, PropertyInfo item, Type documentClrType, ref bool modified);
    }
}
