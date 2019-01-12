using System;

namespace ImageApp.Services.Logging
{
    public interface ILogger
    {
        bool IsEnabled { get; set; }
        void Log(string message);
        void Log(string message, Exception exception);
    }
}
