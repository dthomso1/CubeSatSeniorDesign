﻿using System;
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
    }
}
