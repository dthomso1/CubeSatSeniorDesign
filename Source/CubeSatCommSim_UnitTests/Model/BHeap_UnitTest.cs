using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CubeSatCommSim.Model;

namespace CubeSatCommSim_UnitTests.Model
{
    [TestClass]
    public class BHeap_UnitTest
    {
        [TestMethod]
        public void BHeap_SetGetCount()
        {
            BHeap<CSPPacket> newHeapArray = new BHeap<CSPPacket>();
            int expectedVal = 0;

            int actualValCount = newHeapArray.Count;

            Assert.AreEqual(expectedVal, actualValCount);
        }
    }
}
