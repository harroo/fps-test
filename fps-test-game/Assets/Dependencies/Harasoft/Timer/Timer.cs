
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;

namespace Harasoft {

    public static class Timer {

        private static Mutex mutex = new Mutex();

        private static Dictionary<int, Stopwatch> timers
            = new Dictionary<int, Stopwatch>();

        public static void Start (int id) {

            mutex.WaitOne(); try {

                if (!timers.ContainsKey(id))
                    timers.Add(id, new Stopwatch());

                timers[id].Start();

            } finally { mutex.ReleaseMutex(); }
        }

        public static double Stop (int id) {

            double milliseconds = 0.0;

            mutex.WaitOne(); try {

                if (!timers.ContainsKey(id))
                    timers.Add(id, new Stopwatch());

                timers[id].Stop();

                milliseconds = timers[id].Elapsed.TotalMilliseconds;

                timers.Remove(id);

            } finally { mutex.ReleaseMutex(); }

            return milliseconds;
        }

        public static void StopAll () {

            mutex.WaitOne(); try {

                foreach (var id in timers.Keys) {

                    timers[id].Stop();

                    timers.Remove(id);
                }

            } finally { mutex.ReleaseMutex(); }
        }
    }
}
