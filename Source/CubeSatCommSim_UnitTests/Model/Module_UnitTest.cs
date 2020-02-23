using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CubeSatCommSim.Model;
using System.Collections.Specialized;

namespace CubeSatCommSim_UnitTests
{
    [TestClass]
    public class Module_UnitTest
    {
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
            
            Assert.AreEqual(actualModule, module1);
            Assert.AreEqual(actualBus, bus1);

            actualBus = module1.BusConnections[1];
            
            Assert.AreEqual(actualBus, bus2);
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

            Assert.AreEqual(actualBus, bus2);
            Assert.AreEqual(checkBus, false);
            Assert.AreEqual(checkModule, false);
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

            module1.SendCSPPacket(bus, destination_addr, destination_port, source_port, priority, dataSize);
            
            Assert.AreEqual(bus.PacketQueue.contains(packet), true);

            //Assert
        }


    }
}



/*
 EventLog.EventList.Contains(null);

            BitVector32 packetHeader = new BitVector32(0x00000000);
            packetHeader[CSPPacket.SourceAddress] = Address;
            packetHeader[CSPPacket.DestinationAddress] = destination_addr;
            packetHeader[CSPPacket.SourcePort] = source_port;
            packetHeader[CSPPacket.DestinationPort] = destination_port;
            packetHeader[CSPPacket.Priority] = priority;
            CSPPacket packet = new CSPPacket(packetHeader, dataSize);
     */
