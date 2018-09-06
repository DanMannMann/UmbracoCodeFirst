using System;
using System.Reflection;

namespace Marsman.UmbracoCodeFirst.Attributes
{
    public interface IDataTypeRedirect
    {
        Type Redirect(PropertyInfo property);
        object GetRedirectedValue(object originalDataTypeObject);
        object GetOriginalDataTypeObject(object redirectedValue);
    }
}