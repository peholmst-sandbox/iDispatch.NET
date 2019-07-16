using System;

namespace iDispatch.MapTiles
{
    /// <summary>
    /// A map tile with a constant pixel size. The combination of zoom level, column and row is sufficient to uniquely
    /// identify any map tile and can be used as a basis when creating file names or database keys.
    /// </summary>
    public readonly struct MapTile
    {
        /// <summary>
        /// The tile size in pixels (each tile is a square).
        /// </summary>
        public const int TileSizeInPixels = 256;

        /// <summary>
        /// The zoom level of this tile.
        /// </summary>
        public MapTileZoomLevel ZoomLevel { get; }

        /// <summary>
        /// The geographical bounds of this tile in ETRS-TM35FIN coordinates.
        /// </summary>
        public GeoRect<EtrsTm35FinPoint> Bounds { get; }

        /// <summary>
        /// The column of this tile.
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// The row of this tile.
        /// </summary>
        public int Row { get; }

        /// <summary>
        /// Creates a new map tile.
        /// </summary>
        /// <param name="zoomLevel">the zoom level of the tile</param>
        /// <param name="column">the column of the tile</param>
        /// <param name="row">the row of the tile</param>
        public MapTile(MapTileZoomLevel zoomLevel, int column, int row)
        {
            ZoomLevel = zoomLevel;

            if (column < 0)
            {
                throw new ArgumentOutOfRangeException("Column cannot be negative");
            }
            Column = column;

            if (row < 0)
            {
                throw new ArgumentOutOfRangeException("Row cannot be negative");
            }
            Row = row;

            double width = TileSizeInPixels * zoomLevel.PixelInMetersOnXAxis;
            double height = TileSizeInPixels * zoomLevel.PixelInMetersOnYAxis;

            var southWestPoint = new EtrsTm35FinPoint(
                MapLeaf.SouthBound + row * height,
                MapLeaf.WestBound + column * width);

            var northEastPoint = new EtrsTm35FinPoint(
                southWestPoint.Northing + height,
                southWestPoint.Easting + width);

            Bounds = new GeoRect<EtrsTm35FinPoint>(southWestPoint, northEastPoint);
        }

        public override string ToString()
        {
            return string.Format("[Zoom: {0}; Column: {1}; Row: {2}]", ZoomLevel.Zoom, Column, Row);
        }
    }
}
