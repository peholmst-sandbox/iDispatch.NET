using System;

namespace iDispatch.MapTiles
{
    /// <summary>
    /// The zoom levels that are available for the map tiles. These are based on the rasters that you can download from the NLS web service ("taustakartta").
    /// </summary>
    public static class MapTileZoomLevels
    {
        public static readonly MapTileZoomLevel Zoom0 = new MapTileZoomLevel(0, 2048, 2048, "1:8 000 000");
        public static readonly MapTileZoomLevel Zoom1 = new MapTileZoomLevel(1, 1024, 1024, "1:4 000 000");
        public static readonly MapTileZoomLevel Zoom2 = new MapTileZoomLevel(2, 256, 256, "1:2 000 000");
        public static readonly MapTileZoomLevel Zoom3 = new MapTileZoomLevel(3, 128, 128, "1:800 000");
        public static readonly MapTileZoomLevel Zoom4 = new MapTileZoomLevel(4, 64, 64, "1:320 000");
        public static readonly MapTileZoomLevel Zoom5 = new MapTileZoomLevel(5, 32, 32, "1:160 000");
        public static readonly MapTileZoomLevel Zoom6 = new MapTileZoomLevel(6, 16, 16, "1:80 000");
        public static readonly MapTileZoomLevel Zoom7 = new MapTileZoomLevel(7, 8, 8, "1:40 000");
        public static readonly MapTileZoomLevel Zoom8 = new MapTileZoomLevel(8, 4, 4, "1:20 000");
        public static readonly MapTileZoomLevel Zoom9 = new MapTileZoomLevel(9, 2, 2, "1:10 000");
        public static readonly MapTileZoomLevel Zoom10 = new MapTileZoomLevel(10, 0.5, 0.5, "1:5000");

        /// <summary>
        /// Returns the zoom level metadata for the given zoom level.
        /// </summary>
        /// <param name="zoom">the zoom level (between 0 and 10)</param>
        /// <returns>the zoom level metadata</returns>
        /// <exception cref="ArgumentOutOfRangeException">if the zoom level is invalid</exception>
        public static MapTileZoomLevel GetZoomLevel(int zoom)
        {
            switch (zoom)
            {
                case 0: return Zoom0;
                case 1: return Zoom1;
                case 2: return Zoom2;
                case 3: return Zoom3;
                case 4: return Zoom4;
                case 5: return Zoom5;
                case 6: return Zoom6;
                case 7: return Zoom7;
                case 8: return Zoom8;
                case 9: return Zoom9;
                case 10: return Zoom10;
                default: throw new ArgumentOutOfRangeException("Zoom level must be between 0 and 10");
            }
        }
    }
}
