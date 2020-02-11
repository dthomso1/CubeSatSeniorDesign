using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CubeSatCommSim.Model;
using System.Collections.Specialized;

namespace CubeSatCommSim_UnitTests
{
    [TestClass]
    public class CSPPacket_UnitTest
    {
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
                        + "},DataSize=" + size;
            String actual = packet.ToString();

            Assert.AreEqual(actual, expected);

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
                        + "},DataSize=" + size;
            String actual = packet.ToString();

            Assert.AreEqual(actual, expected);

        }

    }
}
