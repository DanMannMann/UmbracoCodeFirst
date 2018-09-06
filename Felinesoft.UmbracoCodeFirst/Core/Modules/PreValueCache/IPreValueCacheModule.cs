using Marsman.UmbracoCodeFirst.Core.Resolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models;

namespace Marsman.UmbracoCodeFirst.Core.Modules
{
    public interface IPreValueCacheModule : ICodeFirstEntityModule
    {
        IReadOnlyList<PreValue> Get(DataTypeRegistration registration);
    }
}
