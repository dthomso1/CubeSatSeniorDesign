//@author Harsha Chady

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CubeSatCommSim.Model{
  [TestClass]

  public class ErrorObject_Unit_Test{
    [TestMethod]
    int expectedId = 25;
    bool expectedSelect = value;

    public void TestSetGetID(){
      ErrorObject newErrorObject = new ErrorObject(expectedId, expectedSelect);
      newErrorObject.id;

      int actualID = newErrorObject.id;

      Assert.AreEqual(actualID, expectedId);

    }

    public void TestSetGetSelect(){
      ErrorObject newErrorObject = new ErrorObject(expectedId, expectedSelect);
      newErrorObject.isSelected;

      bool actualSelect = newErrorObject.isSelected;

      Assert.AreEqual(actualSelect, expectedSelect);

    }


  }

}
