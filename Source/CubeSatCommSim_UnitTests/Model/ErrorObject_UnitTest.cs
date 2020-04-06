using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CubeSatCommSim.Model;

namespace CubeSatCommSim_UnitTests.Model
{
    [TestClass]
    public class ErrorObject_UnitTest
    {
        [TestMethod]
        public void ErrorObject_SetGetID()
        {
            int expected = 25;

            ErrorObject newErrorObject = new ErrorObject();
            newErrorObject.id = expected;

            int actualID = newErrorObject.id;

            Assert.AreEqual(expected, actualID);

        }

        [TestMethod]
        public void ErrorObject_SetGetIsActive()
        {
            bool expected = false;

            ErrorObject newErrorObject = new ErrorObject();
            newErrorObject.IsActive = expected;

            bool actual = newErrorObject.IsActive;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ErrorObject_SetGetModuleAffected()
        {
            string expected = "ErrorTest";

            ErrorObject newErrorObject = new ErrorObject();
            newErrorObject.ModuleAffected = expected;

            string actual = newErrorObject.ModuleAffected;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ErrorObject_SetGetDescription()
        {
            string expected = "ErrorTest";

            ErrorObject newErrorObject = new ErrorObject();
            newErrorObject.Description = expected;

            string actual = newErrorObject.Description;

            Assert.AreEqual(expected, actual);
        }
    }
}
