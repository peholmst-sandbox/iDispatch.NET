using System;
using System.IO;
using iDispatch.MapTiles.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iDispatch.MapTiles.Importers.Tests
{
    [TestClass]
    public class WorldFileTest
    {
        [TestMethod]
        public void ReadWorldFile()
        {
            var rasterFile = new FileInfo(
                Path.Combine(
                    Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,
                    TestFiles.MapLeaf
                )
            );
            var worldFile = new WorldFile(rasterFile);

            Assert.AreEqual(0.5, worldFile.ScaleX);
            Assert.AreEqual(-0.5, worldFile.ScaleY);
            Assert.AreEqual(236000.25, worldFile.UpperLeftX);
            Assert.AreEqual(6653999.75, worldFile.UpperLeftY);
            Assert.AreSame(rasterFile, worldFile.RasterFile);
        }
    }
}
