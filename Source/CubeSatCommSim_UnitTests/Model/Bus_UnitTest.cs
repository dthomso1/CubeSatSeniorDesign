using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CubeSatCommSim.Model;

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
            Bus_UnitTest testBus = new CSPBus("test bus");
            ObservableCollection<Module> testModules;
            testBus.ConnectedModules = testModules;
            Assert.areEqual(testModules, testBus.ConnectedModules);
        }
        [TestMethod]
        public void TestIdleBus()
        {
            Bus testBus = new CSPBus("test bus");
            testBus.Idle = true;
            boolean idle = testBus.idle;
            Assert.areEqual(true, idle);
        }
        [TestMethod]
        public void TestConnectAndDisconnectModules()
        {
            Module testModule = new Module("test module", 1);
            Bus testBus = new CSPBus("test bus");
            testBus.addModule(testModule);
            ObservableCollection<Module> testModules = testBus.ConnectedModules;
            boolean test = testModules.Contains(testModule);
            Assert.AreEqual(test, true);
            testBus.DisconnectModule(testModule);
            ObservableCollection<Module> emptyListOfModules;
            Assert.areEquals(emptyListOfModules, testBus.ConnectedModules);
        }
    }
}
