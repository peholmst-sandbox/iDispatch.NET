namespace iDispatch.MapTiles
{
    /// <summary>
    /// A geographical point using ETRS-TM35FIN coordinates.
    /// </summary>
    public readonly struct EtrsTm35FinPoint : IGeoPoint
    {
        public double X { get; }
        public double Y { get; }
        public double Easting { get { return X; } }
        public double Northing { get { return Y; } }

        /// <summary>
        /// Creates a new point with the given coordinates.
        /// </summary>
        /// <param name="northing">the northing coordinate in meters</param>
        /// <param name="easting">the easting coordinate in meters</param>
        public EtrsTm35FinPoint(double northing, double easting)
        {
            // TODO Make sure the values are correct
            Y = northing;
            X = easting;
        }
    }
}
