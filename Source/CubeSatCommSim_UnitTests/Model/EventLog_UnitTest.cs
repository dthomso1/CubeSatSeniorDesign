using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CubeSatCommSim.Model;

namespace CubeSatCommSim_UnitTests
{
    [TestClass]
    public class EventLog_UnitTest
    {
        [TestMethod]
        public void EventLog_Test_AddEvent_ClearLog()
        {

            EventLog eventLog = new EventLog();
            SimEvent simEvent = new SimEvent();
            eventLog.EventList();
            int initNumOfEvents = eventLog.Count();
            eventLog.AddLog(simEvent);
            Assert.areEquals(initNumOfEvents + 1, eventLog.Count());
            eventLog.ClearLog();
            Assert.areEquals(initNumOfEvents, eventLog.Count());

        }
    }
}