namespace iDispatch.MapTiles.Converters
{
    /// <summary>
    /// Utility class for converting between different coordinate systems.
    /// </summary>
    public static class GeoConverters
    {
        /// <summary>
        /// Converts the given ETRS-TM35FIN point to an ETRS-89 point.
        /// </summary>
        public static Etrs89Point ConvertToEtrs89(EtrsTm35FinPoint source)
        {
            return Etrs89EtrsTm35FinConverter.Instance.Convert(source);
        }

        /// <summary>
        /// Converts the given ETRS-89 point to an ETRS-TM35FIN point.
        /// </summary>
        public static EtrsTm35FinPoint ConvertToEtrsTm35Fin(Etrs89Point source)
        {
            return Etrs89EtrsTm35FinConverter.Instance.Convert(source);
        }
    }
}
