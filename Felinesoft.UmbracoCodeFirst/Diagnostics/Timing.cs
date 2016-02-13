using Felinesoft.UmbracoCodeFirst.Core.Modules;
using Felinesoft.UmbracoCodeFirst.Core;
using Felinesoft.UmbracoCodeFirst.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Felinesoft.UmbracoCodeFirst.Diagnostics
{
    public static class Timing
    {
        internal static readonly Guid ModuleResolverTimer = Guid.NewGuid();
        internal static readonly Guid DocTypeModuleTimer = Guid.NewGuid();
        internal static readonly Guid MediaTypeModuleTimer = Guid.NewGuid(); 
        internal static readonly Guid BuiltIn4 = Guid.NewGuid();
        internal static readonly Guid BuiltIn5 = Guid.NewGuid();
        internal static readonly Guid BuiltIn6 = Guid.NewGuid();
        internal static readonly Guid BuiltIn7 = Guid.NewGuid();
        internal static readonly Guid BuiltIn8 = Guid.NewGuid();

        private static bool _enabled;
        private static ConcurrentDictionary<Guid, object> _locks = new ConcurrentDictionary<Guid, object>();

        internal static bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; if (!_enabled) { _timers.Values.ToList().ForEach(x => { if (x.Stopwatch != null) { x.Stopwatch.Stop(); } }); _timers.Clear(); } }
        }

        public static void SaveReport(string folderPath)
        {
            var result = new Dictionary<string, string>();
            foreach (var timer in _usedTimers)
            {
                result.Add(timer.Name, timer.ToString());
            }
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }
            foreach (var report in result)
            {
                System.IO.File.WriteAllText(System.IO.Path.Combine(folderPath, report.Key + ".log"), report.Value);
            }
        }

        private const int MAX_TIMERS = 4;
        private static Dictionary<Guid, Timer> _timers = new Dictionary<Guid, Timer>();
        private static List<Timer> _usedTimers = new List<Timer>();

        internal static void StartTimer(Guid timerId, string traceName, string stepDescription)
        {
            if (!Enabled)
            {
                return;
            }

            _locks.TryAdd(timerId, new object());

            lock (_locks[timerId])
            {
                if (_timers.ContainsKey(timerId) || _timers.Count > MAX_TIMERS)
                {
                    throw new InvalidOperationException(_timers.Count > MAX_TIMERS ? "Too many timers" : "Timer already in use");
                }
                else
                {
                    var timer = new Timer(timerId, traceName, stepDescription);
                    _timers.Add(timerId, timer);
                }
            }
        }

        internal static void MarkTimer(Guid timerId, string stepDescription)
        {
            if (!Enabled)
            {
                return;
            }

            _locks.TryAdd(timerId, new object());

            lock (_locks[timerId])
            {
                if (!_timers.ContainsKey(timerId))
                {
                    throw new InvalidOperationException("Timer not in use");
                }
                else
                {
                    var timer = _timers[timerId];
                    timer.Mark(stepDescription);
                }
            }
        }

        internal static void EndTimer(Guid timerId, string stepDescription)
        {
            if (!Enabled)
            {
                _timers.Clear();
                return;
            }

            _locks.TryAdd(timerId, new object());

            lock (_locks[timerId])
            {
                if (!_timers.ContainsKey(timerId))
                {
                    throw new InvalidOperationException("Timer not in use");
                }
                else
                {
                    var timer = _timers[timerId];
                    timer.End(stepDescription);
                    _timers.Remove(timerId);
                    _usedTimers.Add(timer);
                }
            }
        }
    }

    internal class Timer
    {
        internal Timer(Guid timerId, string traceName, string initialStepDescription)
        {
            Id = timerId;
            Name = traceName;
            Marks = new List<TimerMark>();
            Stopwatch = new Stopwatch();
            Marks.Add(new TimerMark() { Milliseconds = 0, Ticks = 0, StepDescription = initialStepDescription });
            Stopwatch.Start();
        }

        internal Guid Id { get; set; }
        public string Name { get; internal set; }
        internal List<TimerMark> Marks { get; set; }
        internal Stopwatch Stopwatch { get; set; }

        internal void Mark(string stepDescription, int recursion = 0)
        {
            long tickMark = Stopwatch.ElapsedTicks;
            long msMark = Stopwatch.ElapsedMilliseconds;
            var tm = new TimerMark() { Milliseconds = msMark, Ticks = tickMark, StepDescription = stepDescription };
            lock (Marks)
            {
                Marks.Add(tm);
            }
            #region test
            //            if (tm != null)
            //            {
            //                Marks.Add(tm);
            //            }
            //            else
            //            {
            //#if DEBUG
            //                throw new CodeFirstException("Null timer mark");
            //#else
            //                if(recursion < 10)
            //                {
            //                    Mark(stepDescription, recursion++);
            //                }
            //                else
            //                {
            //                    throw new CodeFirstException("Null timer mark - tried 10 times");
            //                }
            //#endif
            //            }
            #endregion
        }

        internal void End(string stepDescription, int recursion = 0)
        {
            Stopwatch.Stop();
            var tm = new TimerMark() { Milliseconds = Stopwatch.ElapsedMilliseconds, Ticks = Stopwatch.ElapsedTicks, StepDescription = stepDescription };
            lock (Marks)
            {
                Marks.Add(tm);
                Marks = Marks.Select(x => x != null ? x : new TimerMark() { StepDescription = "MISSINGNO" }).OrderBy(x => x.Ticks).ToList();
            }
            #region test
            //            if (tm != null)
            //            {

            //            }
            //            else
            //            {
            //#if DEBUG
            //                throw new CodeFirstException("Null timer mark");
            //#else
            //                if(recursion < 10)
            //                {
            //                    End(stepDescription, recursion++);
            //                }
            //                else
            //                {
            //                    throw new CodeFirstException("Null timer mark - tried 10 times");
            //                }
            //#endif
            //            }
            #endregion
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            lock (Marks)
            {
                foreach (var mark in Marks)
                {
                    sb.AppendFormat("{0},{1},{2}{3}", mark.StepDescription, mark.Milliseconds, mark.Ticks, Environment.NewLine);
                }
            }
            return sb.ToString();
        }
    }

    internal class TimerMark
    {
        internal long Milliseconds { get; set; }
        internal long Ticks { get; set; }
        internal string StepDescription { get; set; }
    }
}
