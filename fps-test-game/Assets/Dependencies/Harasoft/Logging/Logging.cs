
using System;
using System.IO;
using System.Threading;

namespace Harasoft {

    public static class Logging {

        private static string logFilePath = string.Empty;

        public static void StartNewLog () {

            DateTime dateTime = DateTime.Now;

            string dateString = (dateTime.Month + "-" + dateTime.Day + "-" + dateTime.Year);

            if (!Directory.Exists("logs"))
                Directory.CreateDirectory("logs");

            logFilePath = "logs/" + ("log_" + dateString + "_0.txt");
            int logFileCount = 0;

            while (File.Exists(logFilePath)) {

                logFileCount++;

                logFilePath = "logs/" + ("log_" + dateString + "_" + logFileCount.ToString() + ".txt");
            }
        }

        public static void StartLog () {

            if (logFilePath != string.Empty)
                throw new Exception("Cannot start Log when Log is already started!");

            StartNewLog();
        }

        public static void StopLog () {

            if (logFilePath == string.Empty)
                throw new Exception("Cannot stop Log when Log is already stopped!");

            logFilePath = string.Empty;
        }

        private static Mutex mutex = new Mutex();

        public static void Log (string message) {

            string callingMethod =
                (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().DeclaringType.FullName + "." +
                (new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name;

            Log(callingMethod, message);
        }

        public static void Log (string sender, string message) {

            if (logFilePath == string.Empty)
                throw new Exception("Cannot call Logging.Log when Logging is stopped!");

            mutex.WaitOne(); try {

                string logMessage = "[" + DateTime.Now + "] [" + sender + "]: " + message;

                Console.WriteLine(logMessage);

                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);

            } finally { mutex.ReleaseMutex(); }
        }
    }
}
