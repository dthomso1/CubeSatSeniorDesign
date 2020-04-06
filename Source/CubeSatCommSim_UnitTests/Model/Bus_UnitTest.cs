using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CubeSatCommSim.Model;
using System.Collections.ObjectModel;

namespace CubeSatCommSim_UnitTests.Model
{
    [TestClass]
    public class Bus_UnitTest
    {
        [TestMethod]
        public void TestSetGetName()
        {
            Bus bus1 = new CSPBus("bus1");
            
            bus1.Name = "bus2";
            String actual = bus1.Name;
            Assert.AreEqual("bus2", actual);
        }

        [TestMethod]
        public void TestSetGetConnectedModules()
        {
            Bus testBus = new CSPBus("test bus");
            Module testModule = new Module("Test", 100);
            testBus.ConnectModule(testModule);
            Assert.IsTrue(testBus.ConnectedModules.Contains(testModule));
        }

        [TestMethod]
        public void TestIdleBus()
        {
            Bus testBus = new CSPBus("test bus");
            testBus.Idle = true;
            Boolean idle = testBus.Idle;
            Assert.AreEqual(true, idle);
        }
        [TestMethod]
        public void TestConnectAndDisconnectModules()
        {
            Module testModule = new Module("test module", 1);
            Bus testBus = new CSPBus("test bus");
            testBus.ConnectModule(testModule);
            ObservableCollection<Module> modules = testBus.ConnectedModules;
            Boolean test = testBus.ConnectedModules.Contains(testModule);
            Assert.AreEqual(test, true);
            testBus.DisconnectModule(testModule);
            Assert.AreEqual(modules, testBus.ConnectedModules);
        }
    }
}
