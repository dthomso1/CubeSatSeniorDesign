using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CubeSatCommSim.Model;
using System.Collections;

namespace CubeSatCommSim_UnitTests.Model
{
    [TestClass]
    public class PriorityQueue_UnitTest
    {
        [TestMethod]
        public void PriorityQueue_CSPPacket_Enqueue()
        {
            PriorityQueue<CSPPacket> queue = new PriorityQueue<CSPPacket>();
            CSPPacket packet = new CSPPacket(0, 10000);

            int actualCount = queue.Count;

            Assert.AreEqual(actualCount, 0);

            queue.Enqueue(packet);
            actualCount = queue.Count;

            Assert.AreEqual(actualCount, 1);

            CSPPacket actualPacket = queue.Peek();

            Assert.AreEqual(actualPacket, packet);

        }

        [TestMethod]
        public void PriorityQueue_CSPPacket_Dequeue() {
            PriorityQueue<CSPPacket> queue = new PriorityQueue<CSPPacket>();
            CSPPacket packet = new CSPPacket(0, 10000);

            queue.Enqueue(packet);
            queue.Dequeue();
            int actualCount = queue.Count;

            Assert.AreEqual(actualCount, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Empty heap")]
        public void PriorityQueue_CSPPacket_EmptyDequeue()
        {
            PriorityQueue<CSPPacket> queue = new PriorityQueue<CSPPacket>();
            queue.Dequeue();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Empty heap")]
        public void PriorityQueue_CSPPacket_EnqueueEmptyDequeue()
        {
            PriorityQueue<CSPPacket> queue = new PriorityQueue<CSPPacket>();
            CSPPacket packet = new CSPPacket(0, 10000);
            queue.Enqueue(packet);
            queue.Dequeue();
            queue.Dequeue();
        }
    }
}
