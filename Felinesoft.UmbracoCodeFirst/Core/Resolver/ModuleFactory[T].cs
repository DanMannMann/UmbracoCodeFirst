using System;
using System.Collections.Generic;

namespace Felinesoft.UmbracoCodeFirst.Core.Resolver
{
    internal class ModuleFactory<Tinterface> : ModuleFactoryBase<Tinterface> where Tinterface : ICodeFirstEntityModule
    {
        private IEnumerable<Type> _attributes;
        Func<IEnumerable<ICodeFirstEntityModule>, Tinterface> _moduleFactory;

        public ModuleFactory(IEnumerable<Type> attributeTypesToFilterOn, Func<IEnumerable<ICodeFirstEntityModule>, Tinterface> moduleFactory, params Type[] prerequisites)
            : base(prerequisites)
        {
            _attributes = attributeTypesToFilterOn;
            _moduleFactory = moduleFactory;
        }

        public override IEnumerable<Type> GetAttributeTypesToFilterOn()
        {
            return _attributes;
        }

        public override Tinterface CreateInstance(IEnumerable<ICodeFirstEntityModule> dependencies)
        {
            return _moduleFactory.Invoke(dependencies);
        }
    }
}