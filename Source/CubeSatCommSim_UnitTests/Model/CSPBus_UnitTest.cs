using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CubeSatCommSim.Model;
using System.Linq;

namespace CubeSatCommSim_UnitTests.Model
{
    [TestClass]
    public class CSPBus_UnitTest
    {
        [TestMethod]
        public void CSPBus_SetGetName()
        {
            CSPBus newBus = new CSPBus("BadTest");

            string expectedName = "Name";

            newBus.Name = expectedName;

            string actualName = newBus.Name;

            Assert.AreEqual(expectedName, actualName);
        }

        [TestMethod]
        public void CSPBus_Process_Nothing()
        {
            //rate = 1000
            CSPBus bus = new CSPBus("testBus", 1000);

            int expected = EventLog.EventList.Count;

            bus.Process(1);

            Assert.AreEqual(expected, EventLog.EventList.Count);
        }

        [TestMethod]
        public void CSPBus_Process_InvalidAddress()
        {
            //rate = 1000
            CSPBus bus = new CSPBus("testBus", 1000);
            //size = 5000
            CSPPacket packet1 = new CSPPacket(-1, 5000, ModuleCommand.SEND);

            bus.EnqueuePacket(packet1);

            bus.Process(1);

            String expected = "Packet was dropped because it has no valid destination: " + packet1.ToString();

            Assert.AreEqual(expected, EventLog.EventList.Last().Log);
            Assert.AreEqual(CubeSatCommSim.Model.EventSeverity.WARNING, EventLog.EventList.Last().Severity);

            Assert.AreEqual(null, bus.CurrentPacket);
        }

        [TestMethod]
        public void CSPBus_Process_ValidAddress_FullTransmit()
        {
            //rate = 1000
            CSPBus bus = new CSPBus("testBus", 5000);
            //size = 5000
            CSPPacket packet1 = new CSPPacket(31457280, 5000, ModuleCommand.SEND);
            //Address = 30
            Module module1 = new Module("Module", 30);

            module1.ConnectBus(bus);
            bus.EnqueuePacket(packet1);

            bus.Process(1);
            String expected1 = "Module " + module1.Name + " received packet: " + packet1.ToString();
            Assert.AreEqual(EventLog.EventList.Last().Log, expected1);
            Assert.AreEqual(EventLog.EventList.Last().Severity, CubeSatCommSim.Model.EventSeverity.INFO);

            Assert.AreEqual(null, bus.CurrentPacket);
        }

        [TestMethod]
        public void CSPBus_Process_ValidAddress_PartTransmit()
        {
            //rate = 1000
            CSPBus bus = new CSPBus("testBus", 1000);
            //size = 5000
            CSPPacket packet1 = new CSPPacket(31457280, 5000, ModuleCommand.SEND);
            //Address = 30
            Module module1 = new Module("Module", 30);

            short expectedTransmit = 2000;
            
            module1.ConnectBus(bus);
            bus.EnqueuePacket(packet1);

            String expectedLog = "New packet transmitting on bus " + bus.Name + ": " + packet1.ToString();

            bus.Process(1);

            Assert.AreEqual(expectedLog, EventLog.EventList.Last().Log);
            Assert.AreEqual(CubeSatCommSim.Model.EventSeverity.INFO, EventLog.EventList.Last().Severity);

            bus.Process(2);

            Assert.AreEqual(expectedTransmit, packet1.PartTransmitted);
            Assert.AreEqual(packet1, bus.CurrentPacket);
        }

        [TestMethod]
        public void CSPBus_Process_ValidAddress_InterruptPacket()
        {
            //rate = 1000
            CSPBus bus = new CSPBus("testBus", 1000);
            //Priority = 2, Address = 30, Size = 5000
            CSPPacket packet1 = new CSPPacket(-2116026368, 5000, ModuleCommand.SEND);
            //Priority = 1, Address = 30, Size = 1000
            CSPPacket packet2 = new CSPPacket(1105199104, 1000, ModuleCommand.SEND);
            //Address = 30
            Module module1 = new Module("Module", 30);
            
            module1.ConnectBus(bus);

            //Part Transmit packet1 2000/5000
            bus.EnqueuePacket(packet1);
            bus.Process(1);
            bus.Process(2);
            
            //Expected Packet Interrupted Log
            String expectedLog1 = "Packet " + packet1.ToString() +
                        " was interrupted by packet " + packet2.ToString() +
                        " on bus " + bus.Name;

            //Fully Transmit packet2
            bus.EnqueuePacket(packet2);
            bus.Process(3);

            Assert.AreEqual(null, bus.CurrentPacket);

            Assert.AreEqual(expectedLog1, EventLog.EventList.ElementAt(EventLog.EventList.Count - 2).Log);
            Assert.AreEqual(CubeSatCommSim.Model.EventSeverity.INFO, EventLog.EventList.ElementAt(EventLog.EventList.Count - 2).Severity);

            //Expected Received Packet Log
            String expectedLog2 = "Module " + module1.Name + " received packet: " + packet2.ToString();

            Assert.AreEqual(expectedLog2, EventLog.EventList.Last().Log);
            Assert.AreEqual(CubeSatCommSim.Model.EventSeverity.INFO, EventLog.EventList.Last().Severity);
        }
    }
}
