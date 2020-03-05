using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CubeSatCommSim.Model;
using System.Collections.Specialized;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace CubeSatCommSim_UnitTests.Model
{
    [TestClass]
    public class Module_UnitTest
    {
        [TestMethod]
        public void TestSetGetName()
        {
            string expectedName = "Name";

            Module newModule = new Module("BadTest", 100);
            newModule.Name = expectedName;

            string actualName = newModule.Name;

            Assert.AreEqual(expectedName, actualName);

        }

        [TestMethod]
        public void TestSetGetAddress()
        {
            int expectedAddress = 26;
            Module newModule = new Module("Test", 100);
            newModule.Address = expectedAddress;

            int actualAddress = newModule.Address;

            Assert.AreEqual(expectedAddress, actualAddress);

        }

        [TestMethod]
        public void Module_ConnectBus()
        {
            Bus bus1 = new CSPBus("Bus1");
            CSPBus bus2 = new CSPBus("Bus2");
            Module module1 = new Module("Module",100);

            module1.ConnectBus(bus1);
            module1.ConnectBus(bus2);

            Module actualModule = bus1.ConnectedModules[0];
            Bus actualBus = module1.BusConnections[0];
            
            Assert.AreEqual(module1, actualModule);
            Assert.AreEqual(bus1, actualBus);

            actualBus = module1.BusConnections[1];
            
            Assert.AreEqual(bus2, actualBus);
        }

        [TestMethod]
        public void Module_DisconnectBus() {
            Bus bus1 = new CSPBus("Bus1");
            CSPBus bus2 = new CSPBus("Bus2");
            Module module1 = new Module("Module", 100);

            module1.ConnectBus(bus1);
            module1.ConnectBus(bus2);

            module1.DisconnectBus(bus1);

            Bus actualBus = module1.BusConnections[0];
            Boolean checkBus = module1.BusConnections.Contains(bus1);
            Boolean checkModule = bus1.ConnectedModules.Contains(module1);

            Assert.AreEqual(bus2, actualBus);
            Assert.AreEqual(false, checkBus);
            Assert.AreEqual(false, checkModule);
        }
        
        [TestMethod]
        public void Module_SendCSPPacket()
        {
            Module module1 = new Module("Module", 41);
            CSPBus bus = new CSPBus("Bus");
            
            byte destination_addr = 40;
            byte destination_port = 100;
            byte source_port = 101;
            byte priority = 2;
            short dataSize = 5000;

            BitVector32 packetHeader = new BitVector32(0x00000000);
            packetHeader[CSPPacket.SourceAddress] = module1.Address;
            packetHeader[CSPPacket.DestinationAddress] = destination_addr;
            packetHeader[CSPPacket.SourcePort] = source_port;
            packetHeader[CSPPacket.DestinationPort] = destination_port;
            packetHeader[CSPPacket.Priority] = priority;
            CSPPacket packet = new CSPPacket(packetHeader, dataSize);

            //Log failed send
            module1.SendCSPPacket(bus, destination_addr, destination_port, source_port, priority, dataSize);
            String expected1 = "Module " + module1.Name + " failed to send packet " + packet.ToString() + 
                " because it is not connected to bus " + bus.Name;

            Assert.AreEqual(expected1, EventLog.EventList.Last().Log);
            Assert.AreEqual(CubeSatCommSim.Model.EventSeverity.ERROR, EventLog.EventList.Last().Severity);
            
            //Connect bus to module
            module1.ConnectBus(bus);

            //Log sending packet
            module1.SendCSPPacket(bus, destination_addr, destination_port, source_port, priority, dataSize);

            String expected2 = "Module " + module1.Name + " sends packet " + packet.ToString() + " to bus " + bus.Name;

            Assert.AreEqual(expected2, EventLog.EventList.Last().Log);
            Assert.AreEqual(CubeSatCommSim.Model.EventSeverity.INFO, EventLog.EventList.Last().Severity);
        }

        [TestMethod]
        public void Module_ReceiveCSPPacket() {
            Module module1 = new Module("Module", 41);
            CSPPacket packet = new CSPPacket(-1, 10000);

            String expected1 = "Module " + module1.Name + " received packet: " + packet.ToString();
            module1.ReceiveCSPPacket(packet);

            Assert.AreEqual(expected1, EventLog.EventList.Last().Log);
            Assert.AreEqual(CubeSatCommSim.Model.EventSeverity.INFO, EventLog.EventList.Last().Severity);
        }

    }
}
