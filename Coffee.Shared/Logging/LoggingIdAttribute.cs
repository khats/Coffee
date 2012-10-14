using System;

namespace Coffee.Shared.Logging
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class LoggingIdAttribute : Attribute
    {
        public LoggingIdAttribute(string loggingId)
        {
            if (string.IsNullOrEmpty(loggingId))
            {
                throw new ArgumentNullException("loggingId");
            }

            if (loggingId.Length > 64)
            {
                throw new InvalidOperationException("Превышен лимит длины идентификатора");
            }

            LoggingId = loggingId;
        }

        public string LoggingId { get; private set; }
    }
}