using System;
using log4net.Core;

namespace log4net.SignalR
{
    public class SignalrAppender : Appender.AppenderSkeleton
    {
        private FixFlags _fixFlags = FixFlags.All;

        public Action<LogEntry> MessageLogged;

        public static SignalrAppender LocalInstance { get; private set; }



        public SignalrAppender()
        {

            LocalInstance = this;
        }

        virtual public FixFlags Fix
        {
            get { return _fixFlags; }
            set { _fixFlags = value; }
        }

        override protected void Append(LoggingEvent loggingEvent)
        {
            // LoggingEvent may be used beyond the lifetime of the Append()
            // so we must fix any volatile data in the event
            loggingEvent.Fix = Fix;

            var formattedEvent = RenderLoggingEvent(loggingEvent);

            var logEntry = new LogEntry(formattedEvent, new JsonLoggingEventData(loggingEvent));

        if (MessageLogged != null)
            {
                MessageLogged(logEntry);
            }
        }
    }


    public class LogEntry
    {
        public string FormattedEvent { get; set; }
        public JsonLoggingEventData LoggingEvent { get; set; }

        public LogEntry(string formttedEvent, JsonLoggingEventData loggingEvent)
        {
            FormattedEvent = formttedEvent;
            LoggingEvent = loggingEvent;
        }
    }
}
