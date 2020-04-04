using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CubeSatCommSim.Model;

namespace CubeSatCommSim_UnitTests.Model
{
    [TestClass]
    public class EventLog_UnitTest
    {
       
        [TestMethod]
        public void EventLog_Test_AddEvent_ClearLog()
        {

            EventLog eventLog = new EventLog();
            String log = "Test log";
            EventSeverity severity = (EventSeverity)Enum.Parse(typeof(EventSeverity), "INFO");
            SimEvent testEvent = new SimEvent(log, severity);
            eventLog.EventList();
            int initNumOfEvents = eventLog.Count();
            eventLog.AddLog(testEvent);
            Assert.areEquals(initNumOfEvents + 1, eventLog.Count());
            eventLog.ClearLog();
            Assert.areEquals(initNumOfEvents, eventLog.Count());

        }
        
    }
}