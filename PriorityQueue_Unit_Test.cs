//@author Harsha Chady

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CubeSatCommSim.Model{
  [TestClass]

  public class PriorityQueue_Unit_Test{
    [TestMethod]
    int expectedCount = 5;

    public void TestGetCount(){
      PriorityQueue newPriorityQueue = new PriorityQueue();

      int actualCount = newPriorityQueue.Count;

      Assert.AreEqual(actualCount, expectedCount);

    }

  }

}
