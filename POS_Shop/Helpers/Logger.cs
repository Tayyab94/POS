using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Helpers
{
    public static class Logger
    {
        private static readonly string logFilePath;
        private static readonly object lockObj = new object();

        static Logger()
        {
            // set the log file path in the application directory
            string appDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            logFilePath = Path.Combine(appDirectory, "application.log");
        }

        /// <summary>
        /// Writes a message to the log file.
        /// </summary>
        /// <param name="message">The message to write.</param>
        /// 
        public static void LogMessage(string message)
        {

            try
            {
                // Format the log entry with a timestamp
                string logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [INFO] {message}";

                // Use a lock to prevent multiple threads from writing at the same time
                lock (lockObj)
                {
                    File.AppendAllText(logFilePath, logEntry + Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                // In case logging fails, write to the console for debugging
                Console.WriteLine($"Failed to write to log file: {ex.Message}");
            }
        }

        /// <summary>
        /// Writes a formatted exception to the log file.
        /// </summary>
        /// <param name="ex">The exception to log.</param>
        public static void LogException(Exception ex)
        {
            // Build a detailed log entry for the exception
            string errorMessage = $"An unhandled exception occurred: {ex.Message}\n\nStack Trace:\n{ex.StackTrace}";
            LogMessage($"[ERROR] {errorMessage}");
        }
    }
}
