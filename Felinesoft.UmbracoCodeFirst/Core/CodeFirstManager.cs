using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using Umbraco.Core;
using Felinesoft.UmbracoCodeFirst.Attributes;
using System.Web.Hosting;
using Umbraco.Core.Models;
using Umbraco.Core.Models.Membership;
using System.Collections.ObjectModel;
using Felinesoft.UmbracoCodeFirst.ContentTypes;

using Felinesoft.UmbracoCodeFirst.Extensions;
using Umbraco.Core.Services;
using System.Text;
using System.Globalization;
using System.Collections.Concurrent;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using Felinesoft.UmbracoCodeFirst.Converters;
using System.Configuration;
using Felinesoft.UmbracoCodeFirst.DataTypes;
using Felinesoft.UmbracoCodeFirst.Core.Resolver;
using Felinesoft.UmbracoCodeFirst.Core.Modules;
using Umbraco.Web.Trees;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using Felinesoft.UmbracoCodeFirst.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Felinesoft.UmbracoCodeFirst
{
    /// <summary>
    /// Manages the UmbracoCodeFirst core, allowing data type registration and content type and instance discovery and creation.
    /// </summary>
    /// <example>
    /// <code>
    ///   protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
    ///   {
    ///       base.ApplicationStarted(umbracoApplication, applicationContext);
    ///       
    ///       //Initialise code-first using the types in the current assembly
    ///       Felinesoft.UmbracoCodeFirst.CodeFirstManager.Current.Initialise(this.GetType().Assembly);
    ///    }
    /// </code>
    /// </example>
    public sealed class CodeFirstManager
    {
        private static CodeFirstManager _current;
        private static event EventHandler<InvalidatingEventArgs> _onInvalidate;
        private static object _managerLock = new object();

        private CodeFirstModuleResolver _resolver = new CodeFirstModuleResolver();
        private object _treeFilterLock = new object();
        private Dictionary<string, List<IEntityTreeFilter>> _treeFilters = new Dictionary<string, List<IEntityTreeFilter>>();
        private object _logLock = new object();
        private Features _features = new Features();

        public Features Features
        {
            get { return _features; }
            set { _features = value; }
        }

        public bool EnableLogging { get; set; }

        public void Log(string message, object source, [CallerMemberName]string sourceMethod = null)
        {
            if (EnableLogging)
            {
                lock (_logLock)
                {
                    var path = System.IO.Path.Combine(AppDomain.CurrentDomain.GetData("DataDirectory") as string, "CodeFirstLog.log");
                    System.IO.File.AppendAllText(path, string.Format("{0}\t\t{1}\t\t{2}\t\t{3}{4}", DateTime.UtcNow.ToString(), source.GetType().FullName, sourceMethod, message, Environment.NewLine));
                }
            }
            if (Debugger.IsAttached)
            {
                Debug.WriteLine(message);
            }
            //TODO forward to Umbraco log service as Log
        }

        public void Warn(string message, object source, [CallerMemberName]string sourceMethod = null)
        {
            Log(message, source, sourceMethod);
            //TODO forward to Umbraco log service as Warn
        }

        public CodeFirstModuleResolver Modules
        {
            get
            {
                return _resolver;
            }
        }

        public static event EventHandler<InvalidatingEventArgs> Invalidating { add { _onInvalidate += value; } remove { _onInvalidate -= value; } }

        public static void Invalidate()
        {
            lock (_managerLock)
            {
                if (_onInvalidate != null)
                {
                    _onInvalidate.Invoke(null, new InvalidatingEventArgs());
                }
                _current = null;
            }
        }

        /// <summary>
        /// Constructs the singleton instance
        /// </summary>
        /// <param name="contentManager">The <see cref="IDefaultContentManager"/> to use for creating and retrieving content instances</param>
        private CodeFirstManager()
        {
            try
            {
                EnableLogging = ConfigurationManager.AppSettings.AllKeys.Contains("CodeFirstLogEnabled") && bool.Parse(ConfigurationManager.AppSettings["CodeFirstLogEnabled"]);
            }
            catch
            {
                EnableLogging = false;
            }
            Invalidating += CodeFirstManager_Invalidating;
        }

        void CodeFirstManager_Invalidating(object sender, InvalidatingEventArgs e)
        {
            TreeControllerBase.TreeNodesRendering -= FilterTreeNodes;
        }

        /// <summary>
        /// Gets the current singleton instance of <see cref="CodeFirstManager"/>
        /// </summary>
        public static CodeFirstManager Current
        {
            get
            {
                lock (_managerLock)
                {
                    if (_current == null)
                    {
                        _current = new CodeFirstManager();
                    }
                }
                return _current;
            }
        }

        #region Entry Points
        /// <summary>
        /// Scans the supplied collection of assemblies for code-first document types, media types, data types and document instances.
        /// All items are added or updated before control is returned; after running this method all code-first items found in the collection should exist in Umbraco.
        /// It is important to include *all* required elements in a single call to initialise; custom data types used within a document type must be available when the document type is created.
        /// </summary>
        /// <param name="assemblies">The assemblies to scan</param>
        /// <param name="refreshCache">True to refresh the Umbraco XML cache after creating document instances</param>
        public void Initialise(IEnumerable<Assembly> assemblies, bool refreshCache = true)
        {
            Initialise(
                assemblies
                    .AsParallel()
                    .SelectMany(x => x.GetTypes())
                    .AsSequential(),
                refreshCache);
        }

        /// <summary>
        /// Scans the supplied assembly for code-first document types, media types, data types and document instances.
        /// All items are added or updated before control is returned; after running this method all code-first items found in the assembly should exist in Umbraco.
        /// It is important to include *all* required elements in a single call to initialise; custom data types used within a document type must be available when the document type is created.
        /// </summary>
        /// <param name="assembly">The assembly to scan</param>
        /// <param name="refreshCache">True to refresh the Umbraco XML cache after creating document instances</param>
        public void Initialise(Assembly assembly, bool refreshCache = true)
        {
            Initialise(assembly.GetTypes(), refreshCache);
        }

        /// <summary>
        /// Scans the supplied collection of types for code-first document types, media types, data types and document instances.
        /// All items are added or updated before control is returned; after running this method all code-first items found in the collection should exist in Umbraco.
        /// It is important to include *all* required elements in a single call to initialise; custom data types used within a document type must be available when the document type is created.
        /// </summary>
        /// <param name="types">The types to scan</param>
        /// <param name="refreshCache">True to refresh the Umbraco XML cache after creating document instances</param>
        public void Initialise(IEnumerable<Type> types, bool refreshCache = true)
        {
            InitialiseModules(types);
            lock (_treeFilterLock)
            {
                TreeControllerBase.TreeNodesRendering += FilterTreeNodes;
            }
            if (refreshCache)
            {
                umbraco.library.RefreshContent();
            }
        }

        private void InitialiseModules(IEnumerable<Type> types)
        {
            //pre-register the built-in datatypes
            var builtInTypes = typeof(CodeFirstManager).Assembly.GetTypes().Where(x => x.GetCustomAttribute<BuiltInDataTypeAttribute>(false) != null);
            var allTypes = types.Concat(builtInTypes).ToList();
            if (Modules.IsPristine) //If the resolver hasn't been explicitly configured do the default stuff
            {
                Modules.AddDefaultModules();
            }
            Modules.Initialise(allTypes);
        }

        public void GenerateTypeFilesFromDatabase(string folderPath, string nameSpace = "UmbracoCodeFirst.GeneratedTypes")
        {
            if (!Modules.IsFrozen)
            {
                throw new CodeFirstException("The module resolver is not frozen. Cannot generate class files.");
            }

            foreach (var module in Modules.Where(x => x is IClassFileGenerator))
            {
                (module as IClassFileGenerator).GenerateClassFiles(nameSpace, folderPath);
            }
        }

        private void FilterTreeNodes(TreeControllerBase sender, TreeNodesRenderingEventArgs e)
        {
            if (!Features.HideCodeFirstEntityTypesInTrees)
            {
                return;
            }
            string alias;
            try
            {
                alias = sender.TreeAlias == null ? e.QueryStrings.Single(x => x.Key.InvariantEquals("treeType")).Value : sender.TreeAlias;
            }
            catch (Exception ex)
            {
                throw new CodeFirstException("No valid legacy or new-style tree alias found for a rendering tree.", ex);
            }

            if (!Modules.IsFrozen)
            {
                throw new CodeFirstException("The module resolver is not frozen. Cannot filter tree nodes.");
            }

            lock (_treeFilterLock)
            {
                if (!_treeFilters.ContainsKey(alias))
                {
                    var list = new List<IEntityTreeFilter>();
                    _treeFilters.Add(alias, list);
                    foreach (var module in Modules)
                    {
                        if (module is IEntityTreeFilter && (module as IEntityTreeFilter).IsFilter(alias))
                        {
                            list.Add(module as IEntityTreeFilter);
                        }
                    }
                }
            }

            bool changesMade = false;
            foreach (var module in _treeFilters[alias])
            {
                module.Filter(e.Nodes, out changesMade);
            }

            if (changesMade)
            {
                //TODO show toast somehow? - functionality due in 7.3.0 - http://issues.umbraco.org/issue/U4-5927
            }
        }
        #endregion
    }

    public class InvalidatingEventArgs : EventArgs { }
}