using Felinesoft.UmbracoCodeFirst.Core.Modules;
using Felinesoft.UmbracoCodeFirst.TestTarget.TestFramework;
using Felinesoft.UmbracoCodeFirst.TestTarget.Tests;
using Felinesoft.UmbracoCodeFirst.TestTarget.TypeSet3;
using Felinesoft.UmbracoCodeFirst.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace Felinesoft.UmbracoCodeFirst.TestTarget
{
    public class startup : ApplicationEventHandler
    {
        private readonly List<ICodeFirstTest> _tests = NewList<ICodeFirstTest>.With(new TypeSet1Tests(), new TypeSet2Tests(), new TypeSet3Tests(), new ContentTests());

        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            base.ApplicationStarted(umbracoApplication, applicationContext);

            #region bypass tests here
            if (false)
            {
                var types = typeof(startup).Assembly.GetTypes().Where(x => x.Namespace.StartsWith("Felinesoft.UmbracoCodeFirst.TestTarget.TypeSet3")).ToList();
                CodeFirstManager.Current.Initialise(types);

                #region Test create and get member
                //new ContentTests().Run();
                //var tm = new TestMember();
                //tm.Name = "Dave Daveson";
                //tm.Username = "Dave";
                //tm.Email = "dave@dave.com";
                //tm.Preferences = MemberPreferences.ReceiveMarketingEmail | MemberPreferences.UseTwoFactorViaSMS;
                //tm.PhoneNumber = new DataTypes.BuiltIn.Textstring() { Value = "56" };
                //tm.PasswordQuestion = "How much wood would a wood chuck chuck if a wood chuck was on crack?";
                //tm.IsApproved = true;
                //tm.Comments = "SOME COMMENTZ";
                //tm.Persist();
                //var tm2 = tm.NodeDetails.Content.ConvertToModel<TestMember>();
                //var uh = new UmbracoHelper(UmbracoContext.Current).TypedMember(tm.NodeDetails.UmbracoId);
                //var modl = uh.ConvertMemberToModel<TestMember>();
                ////var val = modl.Preferences;
                //var grp = new GroupFace();
                //grp.AddToGroup(modl);
                //var mem = grp.GetMembers<TestMember>();
                //var mem2 = grp.GetAllMembers();
                #endregion
                return;
            }
            #endregion
            List<string> history = new List<string>();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (System.Diagnostics.Debugger.IsAttached)
            {
                foreach (var test in _tests)
                {
                    test.Run();
                    CodeFirstManager.Invalidate();
                    history.Add(test.GetType().Name);
                }
                foreach (var test in _tests.PickRandom(8))
                {
                    test.Run();
                    CodeFirstManager.Invalidate();
                    history.Add(test.GetType().Name);
                }
                sw.Stop();
            }
            else
            {
                try
                {
                    foreach (var test in _tests)
                    {
                        test.Run();
                        CodeFirstManager.Invalidate();
                        history.Add(test.GetType().Name);
                    }
                    foreach (var test in _tests.PickRandom(8))
                    {
                        test.Run();
                        CodeFirstManager.Invalidate();
                        history.Add(test.GetType().Name);
                    }
                    sw.Stop();
                    throw new TestsPassedException("All tests passed. Sequence: " + string.Join(", ", history) + ". Run time: " + sw.ElapsedMilliseconds);
                }
                catch (TestFailureException ex)
                {
                    sw.Stop();
                    throw new TestsStoppedException("Test failures occurred. Sequence: " + string.Join(", ", history) + ". Run time: " + sw.ElapsedMilliseconds, ex);
                }
            }
        }
    }

    public class TestsPassedException : Exception { public TestsPassedException(string msg) : base(msg) { } }
    public class TestsStoppedException : Exception { public TestsStoppedException(string msg, Exception ex) : base(msg, ex) { } }

    public static class Extensions
    {
        public static List<T> PickRandom<T>(this List<T> input, int count)
        {
            var result = new List<T>();
            var rand = new Random();
            for (int i = 0; i < count; i++)
            {
                result.Add(input[rand.Next(input.Count)]);
            }
            return result;
        }
    }

    public static class NewList<T>
    {
        public static List<T> With(params T[] values)
        {
            return new List<T>(values);
        }
    }
}