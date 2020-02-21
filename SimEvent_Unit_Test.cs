//@author Harsha Chady

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CubeSatCommSim.Model{
  [TestClass]

  public class SimEvent_Unit_Test{
    [TestMethod]
    string expectedLog = "log";
    EventSeverity expectedSeverity = value;

    public void TestSetGetLog(){
      SimEvent newSimEvent = new SimEvent(expectedLog, expectedSeverity);
      newSimEvent.Log;
      string actualLog = newSimEvent.Log;

      Assert.AreEqual(actualLog, expectedLog);

    }

    public void TestSetGetSeverity(){
      SimEvent newSimEvent = new SimEvent(expectedLog, expectedSeverity);
      newSimEvent.Severity;
      EventSeverity actualSeverity = newSimEvent.Severity;

      Assert.AreEqual(actualSeverity, expectedSeverity);

    }

  }

}
