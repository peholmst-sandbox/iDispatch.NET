using System;

namespace iDispatch.MapTiles
{
    /// <summary>
    /// A map tile with a constant pixel size. The combination of zoom level, column and row is sufficient to uniquely
    /// identify any map tile and can be used as a basis when creating file names or database keys.
    /// </summary>
    public sealed class MapTile
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

            double width = ZoomLevel.PixelsToMetersOnXAxis(TileSizeInPixels);
            double height = ZoomLevel.PixelsToMetersOnYAxis(TileSizeInPixels);

            var southWestPoint = new EtrsTm35FinPoint(
                MapLeaf.SouthGridBound + row * height,
                MapLeaf.WestGridBound + column * width);

            if (!MapLeaf.GridBounds.Contains(southWestPoint))
            {
                throw new ArgumentOutOfRangeException("The map tile is outside the grid");
            }

            var northEastPoint = new EtrsTm35FinPoint(
                southWestPoint.Northing + height,
                southWestPoint.Easting + width);        

            Bounds = new GeoRect<EtrsTm35FinPoint>(southWestPoint, northEastPoint);
        }

        public override string ToString()
        {
            return string.Format("[Zoom: {0}; Column: {1}; Row: {2}]", ZoomLevel.Zoom, Column, Row);
        }

        public override bool Equals(object obj)
        {
            return obj is MapTile && this == (MapTile)obj;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + ZoomLevel.GetHashCode();
                hash = (hash * 7) + Column.GetHashCode();
                hash = (hash * 7) + Row.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(MapTile x, MapTile y)
        {
            return x.ZoomLevel == y.ZoomLevel
                && x.Column == y.Column
                && x.Row == y.Row;
        }

        public static bool operator !=(MapTile x, MapTile y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Finds the map tile that contains the given point on the given zoom level. The point must be within <see cref="MapLeaf.GridBounds"/>.
        /// </summary>
        /// <param name="point">the point that should reside inside the returned map tile</param>
        /// <param name="zoomLevel">the zoom level of the returned map tile</param>
        /// <returns>the map tile that contains the given point</returns>
        /// <exception cref="ArgumentOutOfRangeException">if the point is outside the grid bounds</exception>
        public static MapTile FindMapTileContainingPoint(EtrsTm35FinPoint point, MapTileZoomLevel zoomLevel)
        {
            if (!MapLeaf.GridBounds.Contains(point))
            {
                throw new ArgumentOutOfRangeException("The point is outside the grid");
            }

            double width = zoomLevel.PixelsToMetersOnXAxis(TileSizeInPixels);
            double height = zoomLevel.PixelsToMetersOnYAxis(TileSizeInPixels);

            int column = (int)((point.Easting - MapLeaf.WestGridBound) / width);
            int row = (int)((point.Northing - MapLeaf.SouthGridBound) / height);

            return new MapTile(zoomLevel, column, row);
        }
    }
}
