//@author Harsha Chady

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CubeSatCommSim.Model{
  [TestClass]

  public class Bus_Unit_Test{
    [TestMethod]

    public void TestSetGetName(){
      Bus newBus = new Bus();
      string expectedName = "Name";
      newBus.Name;
      string actualName = newBus.Name;

      Assert.AreEqual(actualName, expectedName);

    }


  }

}
