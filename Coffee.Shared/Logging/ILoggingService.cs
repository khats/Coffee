using System;

namespace Coffee.Shared.Logging
{
    public interface ILoggingService
    {
        void Log(object component, string message, LogType logType, Exception e = null);
    }
}