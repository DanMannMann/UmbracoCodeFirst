using Felinesoft.UmbracoCodeFirst.Core.Modules;
using Felinesoft.UmbracoCodeFirst.TestTarget.TestFramework;
using Felinesoft.UmbracoCodeFirst.TestTarget.Tests;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace Felinesoft.UmbracoCodeFirst.TestTarget
{
    public class startup : ApplicationEventHandler
    {
        private readonly ICodeFirstTest[] _tests = new ICodeFirstTest[] { new DocTypeTests_DefaultSet(), new DocTypeTests_TypeSet2() };
        private const string _namespacePrefix = "Felinesoft.UmbracoCodeFirst.TestTarget.";
        private const string _targetNamespace = "TypeSet2";

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            base.ApplicationStarted(umbracoApplication, applicationContext);

            #region bypass tests here
            if (false)
            {
                var types = typeof(startup).Assembly.GetTypes().Where(x => x.Namespace.StartsWith(_namespacePrefix + _targetNamespace)).ToList();
                CodeFirstManager.Current.Initialise(types);
                return;
            }
            #endregion

            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (System.Diagnostics.Debugger.IsAttached)
            {
                foreach (var test in _tests)
                {
                    test.Run();
                    CodeFirstManager.Invalidate();
                }
                sw.Stop();
                //throw new TestsPassedException("All tests passed. Runtime: " + sw.ElapsedMilliseconds);
            }
            else
            {
                try
                {
                    foreach (var test in _tests)
                    {
                        test.Run();
                        CodeFirstManager.Invalidate();
                    }
                    sw.Stop();
                    throw new TestsPassedException("All tests passed. Run time: " + sw.ElapsedMilliseconds);
                }
                catch (TestFailureException ex)
                {
                    sw.Stop();
                    throw new TestsStoppedException("Test failures occurred. Run time: " + sw.ElapsedMilliseconds, ex);
                }
            }
        }
    }

    public class TestsPassedException : Exception { public TestsPassedException(string msg) : base(msg) { } }
    public class TestsStoppedException : Exception { public TestsStoppedException(string msg, Exception ex) : base(msg, ex) { } }
}