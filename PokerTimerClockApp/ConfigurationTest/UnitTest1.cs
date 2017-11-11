using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business;
using PokerTimerClock;

namespace ConfigurationTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestConfiguration()
        {           
            Configuration config = ConfigurationLoader.GetConfiguration();
            Assert.IsNotNull(config.Blinds);
        }
    }
}
