using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace iDispatch.MapTiles.Tests
{
    [TestClass]
    public class MapTileTest
    {
        [TestMethod]
        public void BoundsOnZoomLevel0()
        {
            var tile = new MapTile(MapTileZoomLevels.Zoom0, 0, 0);
            Assert.AreEqual(0, tile.ZoomLevel.Zoom);
            Assert.AreEqual(0, tile.Column);
            Assert.AreEqual(0, tile.Row);
            Assert.AreEqual(-76000, tile.Bounds.West);
            Assert.AreEqual(6570000, tile.Bounds.South);
            Assert.AreEqual(448288, tile.Bounds.East);
            Assert.AreEqual(7094288, tile.Bounds.North);
        }

        [TestMethod]
        public void FindMapTileContainingPoint_OnSouthWestEdgeOfTile00()
        {
            var tile = MapTile.FindMapTileContainingPoint(new EtrsTm35FinPoint(MapLeaf.SouthGridBound, MapLeaf.WestGridBound), MapTileZoomLevels.Zoom1);
            Assert.AreEqual(1, tile.ZoomLevel.Zoom);
            Assert.AreEqual(0, tile.Column);
            Assert.AreEqual(0, tile.Row);
        }

        [TestMethod]
        public void FindMapTileContainingPoint_OnNorthEastEdgeOfTile00()
        {
            var tile = MapTile.FindMapTileContainingPoint(new EtrsTm35FinPoint(
                MapLeaf.SouthGridBound + MapTileZoomLevels.Zoom1.PixelsToMetersOnYAxis(MapTile.TileSizeInPixels - 1),
                MapLeaf.WestGridBound + MapTileZoomLevels.Zoom1.PixelsToMetersOnXAxis(MapTile.TileSizeInPixels - 1)),
                MapTileZoomLevels.Zoom1);
            Assert.AreEqual(1, tile.ZoomLevel.Zoom);
            Assert.AreEqual(0, tile.Column);
            Assert.AreEqual(0, tile.Row);
        }

        public void FindMapTileContainingPoint_InTheMiddleOfTile11()
        {
            var tile = MapTile.FindMapTileContainingPoint(new EtrsTm35FinPoint(
                MapLeaf.SouthGridBound + MapTileZoomLevels.Zoom1.PixelsToMetersOnYAxis((int)(MapTile.TileSizeInPixels * 1.5)),
                MapLeaf.WestGridBound + MapTileZoomLevels.Zoom1.PixelsToMetersOnXAxis((int)(MapTile.TileSizeInPixels * 1.5))),
                MapTileZoomLevels.Zoom1);
            Assert.AreEqual(1, tile.ZoomLevel.Zoom);
            Assert.AreEqual(1, tile.Column);
            Assert.AreEqual(1, tile.Row);
        }
    }
}
