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

        /// <summary>
        /// Converts the given number of pixels to meters on the X-axis.
        /// </summary>
        /// <param name="pixels">the number of pixels</param>
        /// <returns>the number of meters</returns>
        public double PixelsToMetersOnXAxis(int pixels)
        {
            return pixels * PixelInMetersOnXAxis;
        }

        /// <summary>
        /// Converts the given number of meters to pixels on the X-axis.
        /// </summary>
        /// <param name="meters">the number of meters</param>
        /// <returns>the number of pixels</returns>
        public int MetersToPixelsOnXAxis(double meters)
        {
            return (int)(meters / PixelInMetersOnXAxis);
        }

        /// <summary>
        /// Converts the given number of pixels to meters on the Y-axis.
        /// </summary>
        /// <param name="pixels">the number of pixels</param>
        /// <returns>the number of meters</returns>
        public double PixelsToMetersOnYAxis(int pixels)
        {
            return pixels * PixelInMetersOnYAxis;
        }

        /// <summary>
        /// Converts the given number of meters to pixels on the Y-axis.
        /// </summary>
        /// <param name="meters">the number of meters</param>
        /// <returns>the number of pixels</returns>
        public int MetersToPixelsOnYAxis(double meters)
        {
            return (int)(meters / PixelInMetersOnYAxis);
        }

        public override string ToString()
        {
            return Description; // TODO Replace with more details
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
