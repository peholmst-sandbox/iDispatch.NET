using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iDispatch.MapTiles.Tests
{
    [TestClass]
    public class EtrsTm35FinPointTest
    {
        [TestMethod]
        public void CreateFromEtrs89()
        {
            // The coordinates are taken from this example: http://docs.jhs-suositukset.fi/jhs-suositukset/JHS197_liite3/JHS197_liite3.html

            var degrees = new Etrs89Point(60.3851068722, 19.84813676944);
            var meters = new EtrsTm35FinPoint(degrees);

            Assert.AreEqual(6715706.37708, meters.Northing, 0.00001);
            Assert.AreEqual(106256.35961, meters.Easting, 0.00001);
        }

        [TestMethod]
        public void ConvertToEtrs89()
        {
            // Again, the coordinates are taken from the same source as the previous test case
            var meters = new EtrsTm35FinPoint(6715706.37705, 106256.35958);
            var degrees = meters.ToEtrs89();

            Assert.AreEqual(60.385107, degrees.Latitude, 0.000001);
            Assert.AreEqual(19.848137, degrees.Longitude, 0.000001);
        }
    }
}
