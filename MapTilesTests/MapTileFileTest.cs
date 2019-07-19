using System.IO;
using iDispatch.MapTiles.Files.MimeTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iDispatch.MapTiles.Files.Tests
{
    [TestClass]
    public class MapTileFileTest
    {
        [TestMethod]
        public void CreateTileFile_NoNeedToPrefixDirectoryNames()
        {
            var tileFile = new MapTileFile(new MapTile(MapTileZoomLevels.Zoom9, 99, 88), new DirectoryInfo("C:\\FooBar\\"), PngMimeType.Instance);
            Assert.AreEqual("C:\\FooBar\\9\\99\\88\\9-99-88.png", tileFile.File.FullName);
        }

        [TestMethod]
        public void CreateTileFile_SplitDirectoryNamesOnTwoLevels()
        {
            var tileFile = new MapTileFile(new MapTile(MapTileZoomLevels.Zoom9, 123, 456), new DirectoryInfo("C:\\FooBar\\"), PngMimeType.Instance);
            Assert.AreEqual("C:\\FooBar\\9\\12\\123\\45\\456\\9-123-456.png", tileFile.File.FullName);
        }
    }
}
