//@author Harsha Chady

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CubeSatCommSim.Model{
  [TestClass]

  public class CSPPacket_Unit_Test{
    [TestMethod]
    int headerVal = 25;
    short exdataS = value;
    BitVector32 exheader = value;
    short ExpectedPartTransmitted = 0;

    public void TestSetGetHeader(){
      CSPPacket newCSPPacket = new CSPPacket(exheader, exdataS);
      newCSPPacket.Header;

      BitVector32 actualHeader = newCSPPacket.Header;

      Assert.AreEqual(actualHeader, exheader);

    }

    public void TestSetGetDataSize(){
      CSPPacket newCSPPacket = new CSPPacket(headerVal, exdataS);
      newCSPPacket.DataSize;

      short actualDataSize = newCSPPacket.DataSize;
      Assert.AreEqual(actualDataSize, exdataS);
    }

    public void TestSetGetPartTransmitted(){
      CSPPacket newCSPPacket = new CSPPacket(headerVal, exdataS);
      newCSPPacket.PartTransmitted;

      short actualPart = newCSPPacket.PartTransmitted;
      Assert.AreEqual(actualPart, ExpectedPartTransmitted);
    }


  }

}
