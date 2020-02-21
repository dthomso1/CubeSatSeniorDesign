//@author Harsha Chady

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CubeSatCommSim.Model{
  [TestClass]

  public class BHeap_Unit_Test{
    [TestMethod]

    public void TestSetGetCount(){
      BHeap newHeapArray = new BHeap();
      int expectedVal = 100;
      newHeapArray.Count(); //set count????
      int actualValCount = newHeapArray.Count; //get Count???

      Assert.areEqual(actualValCount, expectedVal);

    }


  }

}
