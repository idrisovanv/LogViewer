namespace LogViewer
{
    using System;
    using System.Diagnostics;

    using log4net.SignalR;

    using Microsoft.AspNet.SignalR;

    public class SignalrAppenderHub : Hub
    {
        private const string Log4NetGroup = "Log4NetGroup";

        public SignalrAppenderHub()
        {
            Debug.WriteLine("SignalrAppenderHub.ctor()");
            if (SignalrAppender.LocalInstance != null)
            {
                SignalrAppender.LocalInstance.MessageLogged = this.OnMessageLogged;
            }
        }

        public void Listen() 
        {
            this.Groups.Add(this.Context.ConnectionId, Log4NetGroup);         
        }

        public void OnMessageLogged(LogEntry e)
        {
            this.Clients.Group(Log4NetGroup).onLoggedEvent(e.FormattedEvent, e.LoggingEvent);
        }

        public void Test()
        {
            this.Clients.All.testAll(DateTime.Now);
        }
        
        public void TestGroup()
        {
            this.Clients.Group(Log4NetGroup).testGroup(DateTime.Now);
        }
    }
}