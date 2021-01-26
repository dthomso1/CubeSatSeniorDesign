//@author Harsha Chady

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CubeSatCommSim.Model{
  [TestClass]

  public class Module_Unit_Test{
    [TestMethod]
    string expectedName = "Name";
    int expectedAddress = 26;
    int expectedPriority = 9;
    ObservableCollection<Bus> expectedBusConnections = value;

    public void TestSetGetName(){
      Module newModule = new Module(expectedName, expectedAddress);
      newModule.Name;

      string actualName = newModule.Name;

      Assert.AreEqual(actualName, expectedName);

    }

    public void TestSetGetAddress(){
      Module newModule = new Module(expectedName, expectedAddress);
      newModule.Address;

      int actualAddress = newModule.Address;

      Assert.AreEqual(actualAddress, expectedAddress);

    }

    public void TestSetGetBusConnections(){
      Module newModule = new Module(expectedName, expectedAddress);
      newModule.BusConnections;

       ObservableCollection<Bus> actualBusConnections = newModule.BusConnections;

      Assert.AreEqual(actualBusConnections, expectedBusConnections);

    }


  }

}
