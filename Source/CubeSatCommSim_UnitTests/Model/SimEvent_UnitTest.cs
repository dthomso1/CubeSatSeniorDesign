using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CubeSatCommSim.Model;

namespace CubeSatCommSim_UnitTestss
{
    [TestClass]
    public class SimEvent_UnitTest
    {
        [TestMethod]
        public void SimEvent_Test_AddingInfo_ToString()
        {
            String log = "Test log";
            EventSeverity severity = new EventSeverity();
            severity.set(SimEvent.INFO);
            SimEvent testEvent = new SimEvent(log, severity);
            Assert.areEquals("[INFO] Test log", testEvent.toString());

        }
    }
}
