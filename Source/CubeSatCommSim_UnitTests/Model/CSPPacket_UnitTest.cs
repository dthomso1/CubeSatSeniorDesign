using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CubeSatCommSim.Model;
using System.Collections.Specialized;

namespace CubeSatCommSim_UnitTests.Model
{
    [TestClass]
    public class CSPPacket_UnitTest
    {
        [TestMethod]
        public void TestSetGetHeader()
        {
            int headerVal = 25;
            short exdataS = 5000;
            BitVector32 exheader = new BitVector32(headerVal);
            
            CSPPacket newCSPPacket = new CSPPacket(exheader, exdataS);

            BitVector32 actualHeader = newCSPPacket.Header;

            Assert.AreEqual(exheader, actualHeader);
        }

        [TestMethod]
        public void TestSetGetDataSize()
        {
            int headerVal = 25;
            short exdataS = 5000;
            BitVector32 exheader = new BitVector32(headerVal);

            CSPPacket newCSPPacket = new CSPPacket(headerVal, exdataS);

            short actualDataSize = newCSPPacket.DataSize;
            Assert.AreEqual(exdataS, actualDataSize);
        }

        [TestMethod]
        public void TestSetGetPartTransmitted()
        {
            int headerVal = 25;
            short exdataS = 5000;
            BitVector32 exheader = new BitVector32(headerVal);
            short ExpectedPartTransmitted = 1500;

            CSPPacket newCSPPacket = new CSPPacket(headerVal, exdataS);

            newCSPPacket.PartTransmitted = ExpectedPartTransmitted;

            short actualPart = newCSPPacket.PartTransmitted;
            Assert.AreEqual(ExpectedPartTransmitted, actualPart);
        }

        [TestMethod]
        public void CSPPacket_FromValue_ToString()
        {
            short size = 32000;
            CSPPacket packet = new CSPPacket(-997195777, size);
            //76546047
            //-1040187392

            

            String expected = "Header={" + 3
                        + " " + 2
                        + " " + 8
                        + " " + 63
                        + " " + 63
                        + " " + 15
                        + " " + 1
                        + " " + 1
                        + " " + 1
                        + " " + 1
                        + "},Tx/Size=" + packet.PartTransmitted + "/" + size;
            String actual = packet.ToString();

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void CSPPacket_FromVector_ToString()
        {
            short size = 32000;
            BitVector32 header = new BitVector32(-997195777);
            CSPPacket packet = new CSPPacket(header, size);
            //76546047
            //-1040187392
            String expected = "Header={" + 3
                        + " " + 2
                        + " " + 8
                        + " " + 63
                        + " " + 63
                        + " " + 15
                        + " " + 1
                        + " " + 1
                        + " " + 1
                        + " " + 1
                        + "},Tx/Size=" + packet.PartTransmitted + "/" + size;
            String actual = packet.ToString();

            Assert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void CSPPacket_CompareTo() {
            //priority = 1
            CSPPacket packet1 = new CSPPacket(2147483647, 10000);
            //priority = 3
            CSPPacket packet2 = new CSPPacket(-1, 10000);
            //priority = 3
            CSPPacket packet3 = new CSPPacket(-1073741824, 10000);

            int actual;
            //Test - lower
            actual = packet1.CompareTo(packet2);
            Assert.AreEqual(actual, -1);
            
            //Test - Higher
            actual = packet2.CompareTo(packet1);
            Assert.AreEqual(actual, 1);

            //Test - Equal 3to2
            actual = packet3.CompareTo(packet2);
            Assert.AreEqual(actual, 0);

            //Test - Equal 2to3
            actual = packet2.CompareTo(packet3);
            Assert.AreEqual(0, actual);
        }

    }
}
