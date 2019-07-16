using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iDispatch.MapTiles.Tests
{
    [TestClass]
    public class MapLeafTest
    {
        [TestMethod]
        public void TestSpecialCase_K2()
        {
            var K2 = new MapLeaf(new MapLeafIdentifier("K2"));
            Assert.AreEqual(6570000.0, K2.Bounds.South);
            Assert.AreEqual(6666000.0, K2.Bounds.North);
            Assert.AreEqual(20000.0, K2.Bounds.West);
            Assert.AreEqual(116000.0, K2.Bounds.East);
        }

        [TestMethod]
        public void TestSpecialCase_L2()
        {
            var L2 = new MapLeaf(new MapLeafIdentifier("L2"));
            Assert.AreEqual(6666000.0, L2.Bounds.South);
            Assert.AreEqual(6762000.0, L2.Bounds.North);
            Assert.AreEqual(20000.0, L2.Bounds.West);
            Assert.AreEqual(116000.0, L2.Bounds.East);
        }

        [TestMethod]
        public void TestSpecialCase_L23()
        {
            var L23 = new MapLeaf(new MapLeafIdentifier("L23"));
            Assert.AreEqual(6666000.0, L23.Bounds.South);
            Assert.AreEqual(6714000.0, L23.Bounds.North);
            Assert.AreEqual(20000.0, L23.Bounds.West);
            Assert.AreEqual(116000.0, L23.Bounds.East);
        }

        [TestMethod]
        public void TestSpecialCase_L24()
        {
            var L23 = new MapLeaf(new MapLeafIdentifier("L24"));
            Assert.AreEqual(6714000.0, L23.Bounds.South);
            Assert.AreEqual(6762000.0, L23.Bounds.North);
            Assert.AreEqual(20000.0, L23.Bounds.West);
            Assert.AreEqual(116000.0, L23.Bounds.East);
        }

        [TestMethod]
        public void TestSpecialCase_N6()
        {
            var N6 = new MapLeaf(new MapLeafIdentifier("N6"));
            Assert.AreEqual(6858000.0, N6.Bounds.South);
            Assert.AreEqual(6954000.0, N6.Bounds.North);
            Assert.AreEqual(692000.0, N6.Bounds.West);
            Assert.AreEqual(788000.0, N6.Bounds.East);
        }

        [TestMethod]
        public void TestSpecialCase_N61()
        {
            var N6 = new MapLeaf(new MapLeafIdentifier("N61"));
            Assert.AreEqual(6858000.0, N6.Bounds.South);
            Assert.AreEqual(6906000.0, N6.Bounds.North);
            Assert.AreEqual(692000.0, N6.Bounds.West);
            Assert.AreEqual(788000.0, N6.Bounds.East);
        }

        [TestMethod]
        public void TestSpecialCase_N62()
        {
            var N6 = new MapLeaf(new MapLeafIdentifier("N62"));
            Assert.AreEqual(6906000.0, N6.Bounds.South);
            Assert.AreEqual(6954000.0, N6.Bounds.North);
            Assert.AreEqual(692000.0, N6.Bounds.West);
            Assert.AreEqual(788000.0, N6.Bounds.East);
        }

        [TestMethod]
        public void TestSpecialCase_P6()
        {
            var P6 = new MapLeaf(new MapLeafIdentifier("P6"));
            Assert.AreEqual(6954000.0, P6.Bounds.South);
            Assert.AreEqual(7050000.0, P6.Bounds.North);
            Assert.AreEqual(692000.0, P6.Bounds.West);
            Assert.AreEqual(788000.0, P6.Bounds.East);
        }

        [TestMethod]
        public void Test_L433()
        {
            var L433 = new MapLeaf(new MapLeafIdentifier("L433"));
            Assert.AreEqual(6666000.0, L433.Bounds.South);
            Assert.AreEqual(6690000.0, L433.Bounds.North);
            Assert.AreEqual(452000.0, L433.Bounds.West);
            Assert.AreEqual(500000.0, L433.Bounds.East);
        }

        [TestMethod]
        public void Test_L4333()
        {
            var L4333 = new MapLeaf(new MapLeafIdentifier("L4333"));
            Assert.AreEqual(6666000.0, L4333.Bounds.South);
            Assert.AreEqual(6678000.0, L4333.Bounds.North);
            Assert.AreEqual(476000.0, L4333.Bounds.West);
            Assert.AreEqual(500000.0, L4333.Bounds.East);
        }

        [TestMethod]
        public void Test_L4333G()
        {
            var L4333G = new MapLeaf(new MapLeafIdentifier("L4333G"));
            Assert.AreEqual(6666000.0, L4333G.Bounds.South);
            Assert.AreEqual(6672000.0, L4333G.Bounds.North);
            Assert.AreEqual(494000.0, L4333G.Bounds.West);
            Assert.AreEqual(500000.0, L4333G.Bounds.East);
        }

        [TestMethod]
        public void Test_L4333G3()
        {
            var L4333G3 = new MapLeaf(new MapLeafIdentifier("L4333G3"));
            Assert.AreEqual(6666000.0, L4333G3.Bounds.South);
            Assert.AreEqual(6669000.0, L4333G3.Bounds.North);
            Assert.AreEqual(497000.0, L4333G3.Bounds.West);
            Assert.AreEqual(500000.0, L4333G3.Bounds.East);
        }
    }
}
