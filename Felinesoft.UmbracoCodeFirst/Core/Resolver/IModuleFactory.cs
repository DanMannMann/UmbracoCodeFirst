using Marsman.UmbracoCodeFirst.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Marsman.UmbracoCodeFirst.Core.Resolver
{
    public interface IModuleFactory
    {
        IEnumerable<Type> GetAttributeTypesToFilterOn();
        Type GetResolvedType();
        ICodeFirstEntityModule CreateInstance(IEnumerable<ICodeFirstEntityModule> dependencies);
        IEnumerable<Type> GetPrerequisites();
    }
}