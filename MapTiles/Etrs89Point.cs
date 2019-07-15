namespace iDispatch.MapTiles
{
    /**
     * <summary>A geographical point using ETRS-89 coordinates which are almost the same as WGS-84.</summary>
     */
    public readonly struct Etrs89Point : IGeoPoint
    {
        public double X { get; }
        public double Y { get; }
        public double Longitude { get { return X; } }
        public double Latitude { get { return Y; } }

        /**
         * <summary>Creates a new point with the given coordinates.</summary>
         */
        public Etrs89Point(double latitude, double longitude)
        {
            // TODO Make sure the values are correct
            Y = latitude;
            X = longitude;
        }
    }
}
