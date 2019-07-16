using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iDispatch.MapTiles.Tests
{
    [TestClass]
    public class MapLeafIdentifierTest
    {
        [TestMethod]
        public void CreateScale200000()
        {
            var identifier = new MapLeafIdentifier("V3");
            Assert.AreEqual(200000, identifier.Scale);
        }

        [TestMethod]
        public void CreateScale100000()
        {
            var identifier = new MapLeafIdentifier("V31");
            Assert.AreEqual(100000, identifier.Scale);
        }

        [TestMethod]
        public void CreateScale50000()
        {
            var identifier = new MapLeafIdentifier("V313");
            Assert.AreEqual(50000, identifier.Scale);
        }

        [TestMethod]
        public void CreateScale25000()
        {
            var identifier = new MapLeafIdentifier("V3133");
            Assert.AreEqual(25000, identifier.Scale);
        }

        [TestMethod]
        public void CreateScale10000()
        {
            var identifier = new MapLeafIdentifier("V3133A");
            Assert.AreEqual(10000, identifier.Scale);
        }

        [TestMethod]
        public void CreateScale5000()
        {
            var identifier = new MapLeafIdentifier("V3133A3");
            Assert.AreEqual(5000, identifier.Scale);
        }
    }
}
