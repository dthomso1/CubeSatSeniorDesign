using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CubeSatCommSim.Model;

namespace CubeSatCommSim_UnitTests.Model
{
    [TestClass]
    public class SimEvent_UnitTest
    {
        [TestMethod]
        public void TestSetGetLog()
        {
            string expectedLog = "log";

            SimEvent newSimEvent = new SimEvent("BadTest", CubeSatCommSim.Model.EventSeverity.INFO);
            newSimEvent.Log = expectedLog;
            string actualLog = newSimEvent.Log;

            Assert.AreEqual(expectedLog, actualLog);
        }

        [TestMethod]
        public void TestSetGetSeverity()
        {
            CubeSatCommSim.Model.EventSeverity expectedSeverity = CubeSatCommSim.Model.EventSeverity.WARNING;

            SimEvent newSimEvent = new SimEvent("log", CubeSatCommSim.Model.EventSeverity.INFO);
            newSimEvent.Severity = CubeSatCommSim.Model.EventSeverity.WARNING;
            EventSeverity actualSeverity = newSimEvent.Severity;

            Assert.AreEqual(expectedSeverity, actualSeverity);
        }
 
        [TestMethod]
        public void SimEvent_Test_AddingInfo_ToString()
        {
            String log = "Test log";
            EventSeverity severity = (EventSeverity)Enum.Parse(typeof(EventSeverity), "INFO");
            SimEvent testEvent = new SimEvent(log, severity);
            Assert.areEquals("[INFO] Test log", testEvent.toString());

        }
        
    }
}
