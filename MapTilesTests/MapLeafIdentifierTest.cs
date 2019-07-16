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
            Assert.AreEqual(1.0, identifier.WidthScale);
            Assert.AreEqual(1.0, identifier.HeightScale);
            Assert.IsNull(identifier.GetParentIdentifier());
        }

        [TestMethod]
        public void CreateScale100000()
        {
            var identifier = new MapLeafIdentifier("V31");
            Assert.AreEqual(0.5, identifier.WidthScale);
            Assert.AreEqual(0.5, identifier.HeightScale);
            Assert.AreEqual(new MapLeafIdentifier("V3"), identifier.GetParentIdentifier());
        }

        [TestMethod]
        public void CreateScale50000()
        {
            var identifier = new MapLeafIdentifier("V313");
            Assert.AreEqual(0.25, identifier.WidthScale);
            Assert.AreEqual(0.25, identifier.HeightScale);
            Assert.AreEqual(new MapLeafIdentifier("V31"), identifier.GetParentIdentifier());
        }

        [TestMethod]
        public void CreateScale25000()
        {
            var identifier = new MapLeafIdentifier("V3133");
            Assert.AreEqual(0.125, identifier.WidthScale);
            Assert.AreEqual(0.125, identifier.HeightScale);
            Assert.AreEqual(new MapLeafIdentifier("V313"), identifier.GetParentIdentifier());
        }

        [TestMethod]
        public void CreateScale10000()
        {
            var identifier = new MapLeafIdentifier("V3133A");
            Assert.AreEqual(0.03125, identifier.WidthScale);
            Assert.AreEqual(0.0625, identifier.HeightScale);
            Assert.AreEqual(new MapLeafIdentifier("V3133"), identifier.GetParentIdentifier());
        }

        [TestMethod]
        public void CreateScale5000()
        {
            var identifier = new MapLeafIdentifier("V3133A3");
            Assert.AreEqual(0.015625, identifier.WidthScale);
            Assert.AreEqual(0.03125, identifier.HeightScale);
            Assert.AreEqual(new MapLeafIdentifier("V3133A"), identifier.GetParentIdentifier());
        }
    }
}
