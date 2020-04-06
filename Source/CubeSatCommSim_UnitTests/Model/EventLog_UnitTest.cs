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

            String log = "Test log";
            SimEvent testEvent = new SimEvent(log, CubeSatCommSim.Model.EventSeverity.WARNING);
            
            int initNumOfEvents = EventLog.EventList.Count;
            EventLog.AddLog(testEvent);
            Assert.AreEqual(initNumOfEvents + 1, EventLog.EventList.Count);
            EventLog.ClearLog();
            Assert.AreEqual(initNumOfEvents, EventLog.EventList.Count);

        }
        
    }
}