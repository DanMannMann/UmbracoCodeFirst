using Felinesoft.UmbracoCodeFirst.Core.Modules;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using Felinesoft.UmbracoCodeFirst.Diagnostics;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Felinesoft.UmbracoCodeFirst.Core.Resolver
{
    public sealed class CodeFirstModuleResolver : IEnumerable<ICodeFirstEntityModule>
    {
        private ConcurrentDictionary<Type, IModuleFactory> _modules = new ConcurrentDictionary<Type, IModuleFactory>();
        private List<Type> _order = new List<Type>();
        private bool _frozen = false;
        private bool _pristine = true;
        private Dictionary<Type, ICodeFirstEntityModule> _instances;
        private static readonly Type[] REQUIRED_MODULES = new Type[] { typeof(IDataTypeModule), typeof(IPropertyModule), typeof(IDocumentTypeModule), typeof(IDocumentModelModule), typeof(IPreValueCacheModule) };
        private bool _initialised;

        public IDataTypeModule DataTypeModule
        {
            get
            {
                return Resolve<IDataTypeModule>();
            }
        }

        public IDocumentTypeModule DocumentTypeModule
        {
            get
            {
                return Resolve<IDocumentTypeModule>();
            }
        }

        public IDocumentModelModule DocumentModelModule
        {
            get
            {
                return Resolve<IDocumentModelModule>();
            }
        }

        public IPreValueCacheModule PreValueCacheModule
        {
            get
            {
                return Resolve<IPreValueCacheModule>();
            }
        }

        public IMediaTypeModule MediaTypeModule
        {
            get
            {
                return Resolve<IMediaTypeModule>();
            }
        }

        public IMediaModelModule MediaModelModule
        {
            get
            {
                return Resolve<IMediaModelModule>();
            }
        }

        public void RegisterModule<Tinterface>(IModuleFactory<Tinterface> moduleFactory) where Tinterface : ICodeFirstEntityModule
        {
            FreezeCheck(false);
            if (_modules.ContainsKey(typeof(Tinterface)))
            {
                throw new CodeFirstResolverException(typeof(Tinterface).Name + " already has a registered factory");
            }
            if (moduleFactory.GetPrerequisites().Except(_order).Any())
            {
                throw new CodeFirstResolverException(typeof(Tinterface).Name + " has dependencies which are not satisfied.");
            }
            _modules.TryAdd(typeof(Tinterface), moduleFactory);
            _order.Add(typeof(Tinterface));
            _pristine = false;
        }

        public Tinterface Resolve<Tinterface>() where Tinterface : class, ICodeFirstEntityModule
        {
            FreezeCheck(true);
            return _instances[typeof(Tinterface)] as Tinterface;
        }

        public bool IsFrozen
        {
            get
            {
                return _frozen;
            }
        }

        public void FreezeResolver()
        {
            Freeze();
        }

        internal bool IsPristine { get { return _pristine; } }

        internal void Initialise(IEnumerable<Type> types)
        {
            Timing.StartTimer(Timing.ModuleResolverTimer, "Resolver Initialise", "Freeze Check");
            InitCheck(false);
            Freeze();

            Timing.MarkTimer(Timing.ModuleResolverTimer, "Get Filters");
            //Key: module type
            //value: attributes that module wants
            var filters = _modules.ToDictionary(x => x.Key, x => x.Value.GetAttributeTypesToFilterOn());

            Timing.MarkTimer(Timing.ModuleResolverTimer, "Get desired class attributes");
            //All class attributes wanted by all modules
            var classAttributes = filters
                                    .Values
                                    .SelectMany(x => x)
                                    .Where(x => 
                                        {
                                            var attr = x.GetCustomAttribute<AttributeUsageAttribute>();
                                            return attr == null || attr.ValidOn.HasFlag(AttributeTargets.Class);
                                        })
                                    .Distinct()
                                    .ToList();

            Timing.MarkTimer(Timing.ModuleResolverTimer,"Find types with class attributes");
            var equalComparer = new TypeAssignableEqualityComparer(classAttributes);

            //Key: matched code-first type
            //value: all relevant attributes on that type
            var classAttributeMatches = types.ToDictionary(type => type, type => type.GetCustomAttributes().Select(y => y.GetType()).Intersect(classAttributes, equalComparer)).Where(x => x.Value.Any()).ToDictionary(x => x.Key, x => x.Value);
            var satisfiedDependencies = new List<Type>();
            var queue = new List<Type>(_order);

            while (queue.Count > 0)
            {
                var tasks = new List<Task>();
                var allModulesWhichCanBeInitialised = queue.Where(x => _modules[x].GetPrerequisites().Except(satisfiedDependencies).Count() == 0).ToList();
                foreach (Type t in allModulesWhichCanBeInitialised)
                {
                    queue.Remove(t);
                    var task = new Task(() =>
                    {
                        InitialiseModule(filters, equalComparer, classAttributeMatches, t);
                        satisfiedDependencies.Add(t);
                    });
                    tasks.Add(task);
                    task.Start();
                }
                Task.WaitAll(tasks.ToArray());
            }
            _initialised = true;
            Timing.EndTimer(Timing.ModuleResolverTimer, "Completed Initialisation of modules");
        }

        private void InitialiseModule(Dictionary<Type, IEnumerable<Type>> filters, TypeAssignableEqualityComparer equalComparer, Dictionary<Type, IEnumerable<Type>> classAttributeMatches, Type t)
        {
            Timing.MarkTimer(Timing.ModuleResolverTimer, "Get types to initialise module " + t.Name);
            var classes = classAttributeMatches.Where(x => x.Value.Intersect(filters[t], equalComparer).Any()).Select(x => x.Key);
            Timing.MarkTimer(Timing.ModuleResolverTimer, "Initialising module " + t.Name);
            CodeFirstManager.Current.Log("Initialising module " + t.FullName + " with " + classes.Count() + " types", this);
            _instances[t].Initialise(classes);
        }

        private void Freeze()
        {
            if (!_frozen)
            {
                foreach (var requiredModule in REQUIRED_MODULES)
                {
                    if (!_order.Contains(requiredModule))
                    {
                        throw new CodeFirstResolverException("The required module " + requiredModule.Name + " is not registered, cannot freeze resolver");
                    }
                }

                _instances = new Dictionary<Type, ICodeFirstEntityModule>();
                foreach (Type t in _order)
                {
                    _instances.Add(t, _modules[t].CreateInstance(_instances.Values.ToList()));
                }
                _frozen = true;
            }
        }

        private void FreezeCheck(bool frozen)
        {
            if (_frozen != frozen)
            {
                throw new CodeFirstResolverException(_frozen ? "Service resolver is already frozen and cannot be modified" : "Service resolver is not frozen. Resolution not allowed until resolver is frozen.");
            }
        }

        private void InitCheck(bool initialised)
        {
            if (_initialised != initialised)
            {
                throw new CodeFirstResolverException(_initialised ? "Service resolver is already initialised and cannot be modified" : "Service resolver is not initialised. Resolution not allowed until resolver is initialised.");
            }
        }

        public IEnumerator<ICodeFirstEntityModule> GetEnumerator()
        {
            FreezeCheck(true);
            return new ModuleEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            FreezeCheck(true);
            return new ModuleEnumerator(this);
        }

        private class ModuleEnumerator : IEnumerator<ICodeFirstEntityModule>
        {
            private CodeFirstModuleResolver _parent;
            private Type _currentKey = null;

            internal ModuleEnumerator(CodeFirstModuleResolver parent) 
            {
                _parent = parent;
            }

            public ICodeFirstEntityModule Current
            {
                get
                {
                    if (_currentKey == null)
                    {
                        return null;
                    }
                    else
                    {
                        return _parent._instances[_currentKey];
                    }
                }
            }

            public void Dispose()
            {
                //do nowt
            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    if (_currentKey == null)
                    {
                        return null;
                    }
                    else
                    {
                        return _parent._instances[_currentKey];
                    }
                }
            }

            public bool MoveNext()
            {
                if (_currentKey == null)
                {
                    _currentKey = _parent._order.FirstOrDefault();
                    return _currentKey != null;
                }
                else
                {
                    var newIndex = _parent._order.IndexOf(_currentKey) + 1;
                    if (newIndex < _parent._order.Count)
                    {
                        _currentKey = _parent._order[newIndex];
                        return true;
                    }
                    else
                    {
                        _currentKey = null;
                        return false;
                    }
                }
            }

            public void Reset()
            {
                _currentKey = null;
            }
        }

        private class TypeAssignableEqualityComparer : IEqualityComparer<Type>
        {
            private List<Type> _classAttributes;

            public TypeAssignableEqualityComparer(List<Type> classAttributes)
            {
                _classAttributes = classAttributes;
            }

            public bool Equals(Type x, Type y)
            {
                var val = x.IsAssignableFrom(y);
                return val;
            }

            public int GetHashCode(Type obj)
            {
                foreach (var attr in _classAttributes)
                {
                    if (attr == obj)
                    {
                        return attr.GetHashCode();
                    }
                    else if (attr.IsAssignableFrom(obj))
                    {
                        return attr.GetHashCode();
                    }
                }
                return obj.GetHashCode();
            }
        }
    }
}

namespace Felinesoft.UmbracoCodeFirst.Extensions
{
    public static class DefaultPluginExtensions
    {
        public static void AddDefaultModules(this CodeFirstModuleResolver resolver)
        {
            resolver.AddDefaultDataTypeModule();
            resolver.AddDefaultPreValueCacheModule();
            resolver.AddDefaultPropertyModule();
            resolver.AddDefaultDocumentTypeModule();
            resolver.AddDefaultTemplateModule();
            resolver.AddDefaultMediaTypeModule();
            resolver.AddDefaultDocumentModelModule();
            resolver.AddDefaultMediaModelModule();
        }
    }
}