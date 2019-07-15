namespace iDispatch.MapTiles
{
    /**
     * <summary>A geographical point in some coordinate system.</summary>
     */
    interface IGeoPoint
    {
        /// <summary>The coordinate on the X-axis (typically Easting or Longitude)</summary>
        double X { get; }
        /// <summary>The coordinate on the Y-axis (typically Northing or Latitude)</summary>
        double Y { get; }
    }
}
