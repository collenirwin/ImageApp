using ImageApp.Utils;
using System;
using System.IO;

namespace ImageApp.Services.Logging
{
    public class SimpleFileLogger : ILogger
    {
        public readonly string FilePath = Path.Combine(IoHelper.LocalAppDataPath, "log.txt");

        /// <summary>
        /// Should we be logging right now?
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// How should we format the date prepended to each log message?
        /// </summary>
        public string DateFormatString { get; set; } = "MM/dd/yyyy hh:mm:ss tt";

        /// <summary>
        /// Log the specified message to <see cref="FilePath"/>
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Log(string message)
        {
            IoHelper.TryCreateDirectoryIfNotExists(IoHelper.LocalAppDataPath);

            IoHelper.TryAppendToFile(FilePath,
                $"[{DateTime.Now.ToString(DateFormatString)}]: {message}\n");
        }

        /// <summary>
        /// Log the specified message and Exception (Message and StackTrace) to <see cref="FilePath"/>
        /// </summary>
        /// <param name="message">Message to log</param>
        /// <param name="exception">Exception that was thrown</param>
        public void Log(string message, Exception exception)
        {
            Log($"({message}) Exception: {exception.Message} Trace: {exception.StackTrace}");
        }
    }
}
