namespace iDispatch.MapTiles
{
    /// <summary>
    /// Specifies a zoom level for a specific map tile.
    /// </summary>
    public sealed class MapTileZoomLevel
    {
        /// <summary>
        /// The zoom level as a number. The lower the number, the higher the zoom level (and the lesser the details on the map).
        /// </summary>
        public int Zoom { get; }

        /// <summary>
        /// The number of meters a single pixel represents on the X axis.
        /// </summary>
        public double PixelInMetersOnXAxis { get; }

        /// <summary>
        /// The number of meters a single pixel represents on the Y axis.
        /// </summary>
        public double PixelInMetersOnYAxis { get; }

        /// <summary>
        /// A human readable description of the zoom level, for example for showing in the UI.
        /// </summary>
        public string Description { get; }

        internal MapTileZoomLevel(int zoom, double pixelInMetersOnXAxis, double pixelInMetersOnYAxis, string description)
        {
            Zoom = zoom;
            PixelInMetersOnXAxis = pixelInMetersOnXAxis;
            PixelInMetersOnYAxis = pixelInMetersOnYAxis;
            Description = description;
        }

        public override string ToString()
        {
            return Description;
        }

        public override bool Equals(object obj)
        {
            return obj is MapTileZoomLevel && this == (MapTileZoomLevel)obj;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;
                hash = (hash * 7) + Zoom.GetHashCode();
                hash = (hash * 7) + PixelInMetersOnXAxis.GetHashCode();
                hash = (hash * 7) + PixelInMetersOnYAxis.GetHashCode();
                hash = (hash * 7) + Description.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(MapTileZoomLevel x, MapTileZoomLevel y)
        {
            return x.Zoom == y.Zoom
                && x.PixelInMetersOnXAxis == y.PixelInMetersOnXAxis
                && x.PixelInMetersOnYAxis == y.PixelInMetersOnYAxis
                && x.Description == y.Description;
        }

        public static bool operator !=(MapTileZoomLevel x, MapTileZoomLevel y)
        {
            return !(x == y);
        }
    }
}
