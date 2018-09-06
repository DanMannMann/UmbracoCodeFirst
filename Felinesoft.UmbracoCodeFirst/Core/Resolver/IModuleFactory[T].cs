using System;
using System.Collections.Generic;

namespace Marsman.UmbracoCodeFirst.Core.Resolver
{
    public interface IModuleFactory<Tinterface> : IModuleFactory where Tinterface : ICodeFirstEntityModule
    {
        new Tinterface CreateInstance(IEnumerable<ICodeFirstEntityModule> dependencies);
    }
}