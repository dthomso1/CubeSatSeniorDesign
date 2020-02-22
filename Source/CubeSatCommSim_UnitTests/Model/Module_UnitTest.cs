using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CubeSatCommSim.Model;

namespace CubeSatCommSim_UnitTests
{
    [TestClass]
    public class Module_UnitTest
    {
        [TestMethod]
        public void Module_ConnectBus()
        {
            Bus bus1 = new CSPBus("Bus");
            Module module1 = new Module("Module",100);

            module1.ConnectBus(bus1);

            Module actualModule = bus1.ConnectedModules[0];
            Bus actualBus = module1.BusConnections[0];

            Assert.AreEqual(actualModule, module1);
            Assert.AreEqual(actualBus, bus1);

        }
    }
}
