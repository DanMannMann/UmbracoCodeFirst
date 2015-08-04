using System;
using System.Collections.Generic;
using Umbraco.Core;
using System.Linq;

namespace Felinesoft.UmbracoCodeFirst.Core.Resolver
{
    public abstract class ModuleFactoryBase<Tinterface> : IModuleFactory<Tinterface> where Tinterface : ICodeFirstEntityModule
    {
        protected List<Type> _prerequisites;

        public abstract IEnumerable<Type> GetAttributeTypesToFilterOn();

        public abstract Tinterface CreateInstance(IEnumerable<ICodeFirstEntityModule> dependencies);

        protected ModuleFactoryBase(params Type[] prerequisites)
        {
            _prerequisites = prerequisites == null ? new List<Type>() : prerequisites.ToList();
        }

        public Type GetResolvedType()
        {
            return typeof(Tinterface);
        }

        ICodeFirstEntityModule IModuleFactory.CreateInstance(IEnumerable<ICodeFirstEntityModule> dependencies)
        {
            return (this as ModuleFactoryBase<Tinterface>).CreateInstance(dependencies);
        }

        public IEnumerable<Type> GetPrerequisites()
        {
            return _prerequisites;
        }
    }

    public abstract class ModuleFactoryBase<Tinterface, Tdependency> : ModuleFactoryBase<Tinterface>
        where Tinterface : ICodeFirstEntityModule
        where Tdependency : ICodeFirstEntityModule
    {
        public ModuleFactoryBase()
            : base(typeof(Tdependency)) { }

        public abstract Tinterface CreateInstance(Tdependency dependency);

        public override Tinterface CreateInstance(IEnumerable<ICodeFirstEntityModule> dependencies)
        {
            return CreateInstance((Tdependency)dependencies.First(x => x is Tdependency));
        }
    }

    public abstract class ModuleFactoryBase<Tinterface, Tdependency1, Tdependency2> : ModuleFactoryBase<Tinterface> 
        where Tinterface : ICodeFirstEntityModule
        where Tdependency1 : ICodeFirstEntityModule
        where Tdependency2 : ICodeFirstEntityModule
    {
        public ModuleFactoryBase()
            : base(typeof(Tdependency1), typeof(Tdependency2)) { }

        public abstract Tinterface CreateInstance(Tdependency1 dependency1, Tdependency2 dependency2);

        public override Tinterface CreateInstance(IEnumerable<ICodeFirstEntityModule> dependencies)
        {
            return CreateInstance((Tdependency1)dependencies.First(x => x is Tdependency1), 
                                  (Tdependency2)dependencies.First(x => x is Tdependency2));
        }
    }

    public abstract class ModuleFactoryBase<Tinterface, Tdependency1, Tdependency2, Tdependency3> : ModuleFactoryBase<Tinterface> 
        where Tinterface : ICodeFirstEntityModule
        where Tdependency1 : ICodeFirstEntityModule
        where Tdependency2 : ICodeFirstEntityModule
        where Tdependency3 : ICodeFirstEntityModule
    {
        public ModuleFactoryBase()
            : base(typeof(Tdependency1), typeof(Tdependency2), typeof(Tdependency3)) { }

        public abstract Tinterface CreateInstance(Tdependency1 dependency1, Tdependency2 dependency2, Tdependency3 dependency3);

        public override Tinterface CreateInstance(IEnumerable<ICodeFirstEntityModule> dependencies)
        {
            return CreateInstance((Tdependency1)dependencies.First(x => x is Tdependency1), 
                                  (Tdependency2)dependencies.First(x => x is Tdependency2),
                                  (Tdependency3)dependencies.First(x => x is Tdependency3));
        }
    }

    public abstract class ModuleFactoryBase<Tinterface, Tdependency1, Tdependency2, Tdependency3, Tdependency4> : ModuleFactoryBase<Tinterface>
        where Tinterface : ICodeFirstEntityModule
        where Tdependency1 : ICodeFirstEntityModule
        where Tdependency2 : ICodeFirstEntityModule
        where Tdependency3 : ICodeFirstEntityModule
        where Tdependency4 : ICodeFirstEntityModule
    {
        public ModuleFactoryBase()
            : base(typeof(Tdependency1), typeof(Tdependency2), typeof(Tdependency3), typeof(Tdependency4)) { }

        public abstract Tinterface CreateInstance(Tdependency1 dependency1, Tdependency2 dependency2, Tdependency3 dependency3, Tdependency4 dependency4);

        public override Tinterface CreateInstance(IEnumerable<ICodeFirstEntityModule> dependencies)
        {
            return CreateInstance((Tdependency1)dependencies.First(x => x is Tdependency1),
                                  (Tdependency2)dependencies.First(x => x is Tdependency2),
                                  (Tdependency3)dependencies.First(x => x is Tdependency3),
                                  (Tdependency4)dependencies.First(x => x is Tdependency4));
        }
    }

    public abstract class ModuleFactoryBase<Tinterface, Tdependency1, Tdependency2, Tdependency3, Tdependency4, Tdependency5> : ModuleFactoryBase<Tinterface>
        where Tinterface : ICodeFirstEntityModule
        where Tdependency1 : ICodeFirstEntityModule
        where Tdependency2 : ICodeFirstEntityModule
        where Tdependency3 : ICodeFirstEntityModule
        where Tdependency4 : ICodeFirstEntityModule
        where Tdependency5 : ICodeFirstEntityModule
    {
        public ModuleFactoryBase()
            : base(typeof(Tdependency1), typeof(Tdependency2), typeof(Tdependency3), typeof(Tdependency4), typeof(Tdependency5)) { }

        public abstract Tinterface CreateInstance(Tdependency1 dependency1, Tdependency2 dependency2, Tdependency3 dependency3, Tdependency4 dependency4, Tdependency5 dependency5);

        public override Tinterface CreateInstance(IEnumerable<ICodeFirstEntityModule> dependencies)
        {
            return CreateInstance((Tdependency1)dependencies.First(x => x is Tdependency1),
                                  (Tdependency2)dependencies.First(x => x is Tdependency2),
                                  (Tdependency3)dependencies.First(x => x is Tdependency3),
                                  (Tdependency4)dependencies.First(x => x is Tdependency4),
                                  (Tdependency5)dependencies.First(x => x is Tdependency5));
        }
    }

    public abstract class ModuleFactoryBase<Tinterface, Tdependency1, Tdependency2, Tdependency3, Tdependency4, Tdependency5, Tdependency6> : ModuleFactoryBase<Tinterface>
        where Tinterface : ICodeFirstEntityModule
        where Tdependency1 : ICodeFirstEntityModule
        where Tdependency2 : ICodeFirstEntityModule
        where Tdependency3 : ICodeFirstEntityModule
        where Tdependency4 : ICodeFirstEntityModule
        where Tdependency5 : ICodeFirstEntityModule
        where Tdependency6 : ICodeFirstEntityModule
    {
        public ModuleFactoryBase()
            : base(typeof(Tdependency1), typeof(Tdependency2), typeof(Tdependency3), typeof(Tdependency4), typeof(Tdependency5), typeof(Tdependency6)) { }

        public abstract Tinterface CreateInstance(Tdependency1 dependency1, Tdependency2 dependency2, Tdependency3 dependency3, Tdependency4 dependency4, Tdependency5 dependency5, Tdependency6 dependency6);

        public override Tinterface CreateInstance(IEnumerable<ICodeFirstEntityModule> dependencies)
        {
            return CreateInstance((Tdependency1)dependencies.First(x => x is Tdependency1),
                                  (Tdependency2)dependencies.First(x => x is Tdependency2),
                                  (Tdependency3)dependencies.First(x => x is Tdependency3),
                                  (Tdependency4)dependencies.First(x => x is Tdependency4),
                                  (Tdependency5)dependencies.First(x => x is Tdependency5),
                                  (Tdependency6)dependencies.First(x => x is Tdependency6));
        }
    }

    public abstract class ModuleFactoryBase<Tinterface, Tdependency1, Tdependency2, Tdependency3, Tdependency4, Tdependency5, Tdependency6, Tdependency7> : ModuleFactoryBase<Tinterface>
        where Tinterface : ICodeFirstEntityModule
        where Tdependency1 : ICodeFirstEntityModule
        where Tdependency2 : ICodeFirstEntityModule
        where Tdependency3 : ICodeFirstEntityModule
        where Tdependency4 : ICodeFirstEntityModule
        where Tdependency5 : ICodeFirstEntityModule
        where Tdependency6 : ICodeFirstEntityModule
        where Tdependency7 : ICodeFirstEntityModule
    {
        public ModuleFactoryBase()
            : base(typeof(Tdependency1), typeof(Tdependency2), typeof(Tdependency3), typeof(Tdependency4), typeof(Tdependency5), typeof(Tdependency6), typeof(Tdependency7)) { }

        public abstract Tinterface CreateInstance(Tdependency1 dependency1, Tdependency2 dependency2, Tdependency3 dependency3, Tdependency4 dependency4, Tdependency5 dependency5, Tdependency6 dependency6, Tdependency7 dependency7);

        public override Tinterface CreateInstance(IEnumerable<ICodeFirstEntityModule> dependencies)
        {
            return CreateInstance((Tdependency1)dependencies.First(x => x is Tdependency1),
                                  (Tdependency2)dependencies.First(x => x is Tdependency2),
                                  (Tdependency3)dependencies.First(x => x is Tdependency3),
                                  (Tdependency4)dependencies.First(x => x is Tdependency4),
                                  (Tdependency5)dependencies.First(x => x is Tdependency5),
                                  (Tdependency6)dependencies.First(x => x is Tdependency6),
                                  (Tdependency7)dependencies.First(x => x is Tdependency7));
        }
    }

    public abstract class ModuleFactoryBase<Tinterface, Tdependency1, Tdependency2, Tdependency3, Tdependency4, Tdependency5, Tdependency6, Tdependency7, Tdependency8> : ModuleFactoryBase<Tinterface>
        where Tinterface : ICodeFirstEntityModule
        where Tdependency1 : ICodeFirstEntityModule
        where Tdependency2 : ICodeFirstEntityModule
        where Tdependency3 : ICodeFirstEntityModule
        where Tdependency4 : ICodeFirstEntityModule
        where Tdependency5 : ICodeFirstEntityModule
        where Tdependency6 : ICodeFirstEntityModule
        where Tdependency7 : ICodeFirstEntityModule
        where Tdependency8 : ICodeFirstEntityModule
    {
        public ModuleFactoryBase()
            : base(typeof(Tdependency1), typeof(Tdependency2), typeof(Tdependency3), typeof(Tdependency4), typeof(Tdependency5), typeof(Tdependency6), typeof(Tdependency7), typeof(Tdependency8)) { }

        public abstract Tinterface CreateInstance(Tdependency1 dependency1, Tdependency2 dependency2, Tdependency3 dependency3, Tdependency4 dependency4, Tdependency5 dependency5, Tdependency6 dependency6, Tdependency7 dependency7, Tdependency8 dependency8);

        public override Tinterface CreateInstance(IEnumerable<ICodeFirstEntityModule> dependencies)
        {
            return CreateInstance((Tdependency1)dependencies.First(x => x is Tdependency1),
                                  (Tdependency2)dependencies.First(x => x is Tdependency2),
                                  (Tdependency3)dependencies.First(x => x is Tdependency3),
                                  (Tdependency4)dependencies.First(x => x is Tdependency4),
                                  (Tdependency5)dependencies.First(x => x is Tdependency5),
                                  (Tdependency6)dependencies.First(x => x is Tdependency6),
                                  (Tdependency7)dependencies.First(x => x is Tdependency7),
                                  (Tdependency8)dependencies.First(x => x is Tdependency8));
        }
    }
}