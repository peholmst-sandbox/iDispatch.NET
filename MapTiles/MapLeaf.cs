using System;

namespace iDispatch.MapTiles
{
    /// <summary>
    /// An official Finnish map leaf consisting of its identifier and its bounds in ETRS-TM35FIN coordinates.
    /// See <a href="http://docs.jhs-suositukset.fi/jhs-suositukset/JHS197_liite8/JHS197_liite8.html">JHS 197: Appendix 8</a>
    /// for more information.
    /// </summary>
    public readonly struct MapLeaf
    {
        private static readonly char[] RowLetters = { 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X' };
        internal const double SouthBound = 6570000;
        internal const double WestBound = -76000;
        private const double WidthInMaxScale = 192000;
        private const double HeightInMaxScale = 96000;

        /// <summary>
        /// The map leaf identifier (e.g. V3133A3).
        /// </summary>
        public MapLeafIdentifier Identifier { get; }
        /// <summary>
        /// The bounds of the map leaf in ETRS-TM35FIN coordinates.
        /// </summary>
        public GeoRect<EtrsTm35FinPoint> Bounds { get; }

        /// <summary>
        /// Creates a new map leaf with the given identifier. Its bounds will be automatically calculated based on the identifier.
        /// </summary>
        /// <param name="identifier">the map leaf identifier</param>
        public MapLeaf(MapLeafIdentifier identifier)
        {
            Identifier = identifier;
            var identifierString = identifier.Identifier;
            var rowIdentifier = identifierString[0];
            var columnIdentifier = identifierString[1];

            // We know for sure that the identifier is valid. Now we just have to calculate the bounds.
            double height = HeightInMaxScale * identifier.HeightScale;
            double width = WidthInMaxScale * identifier.WidthScale;

            // Calculate south-west point of the top leaf
            double southOrigo = SouthBound + HeightInMaxScale * Array.IndexOf(RowLetters, rowIdentifier);
            double westOrigo = WestBound + WidthInMaxScale * ((int)Char.GetNumericValue(columnIdentifier) - 2);

            double southOffset = 0;
            double westOffset = 0;
            if (identifierString.Length >= 3) // E.g. V31
            {
                var c2 = identifierString[2];
                southOffset += AdjustSouthOffset(c2, 2);
                westOffset += AdjustWestOffset(c2, 2);

                if (identifierString.Length >= 4) // E.g. V313
                {
                    var c3 = identifierString[3];
                    southOffset += AdjustSouthOffset(c3, 4);
                    westOffset += AdjustWestOffset(c3, 4);

                    if (identifierString.Length >= 5) // E.g. V3133
                    {
                        var c4 = identifierString[4];
                        southOffset += AdjustSouthOffset(c4, 8);
                        westOffset += AdjustWestOffset(c4, 8);

                        if (identifierString.Length >= 6) // E.g. V3133A 
                        {
                            var c5 = identifierString[5];
                            southOffset += AdjustSouthOffset(c5, 16);
                            westOffset += AdjustWestOffset(c5, 32);

                            if (identifierString.Length == 7) // E.g. V3133A1
                            {
                                var c6 = identifierString[6];
                                southOffset += AdjustSouthOffset(c6, 32);
                                westOffset += AdjustWestOffset(c6, 64);
                            }
                        }
                    }
                }
            }
            // The leaves in columns 2 and 6 have only half the width. We only need this correction on the top-level tiles.
            else if (columnIdentifier == '2')
            {
                width /= 2;
                // Column 2 contains the second half of the leaf
                westOrigo += width;
            }
            else if (columnIdentifier == '6')
            {
                width /= 2;
                // Column 6 contains the first half of the leaf
            }

            var southWestPoint = new EtrsTm35FinPoint(southOrigo + southOffset, westOrigo + westOffset);
            var northEastPoint = new EtrsTm35FinPoint(southOrigo + southOffset + height, westOrigo + westOffset + width);
            Bounds = new GeoRect<EtrsTm35FinPoint>(southWestPoint, northEastPoint);
        }

        private static double AdjustSouthOffset(char c, int scaleFraction)
        {
            if (c == '2' || c == '4' || c == 'B' || c == 'D' || c == 'F' || c == 'H')
            {
                return HeightInMaxScale / scaleFraction;
            }
            return 0;
        }

        private static double AdjustWestOffset(char c, int scaleFraction)
        {
            if (c == '4' || c == '3' || c == 'D' || c == 'C')
            {
                return WidthInMaxScale / scaleFraction;
            }
            else if (c == 'F' || c == 'F')
            {
                return 2 * WidthInMaxScale / scaleFraction;
            }
            else if (c == 'H' || c == 'G')
            {
                return 3 * WidthInMaxScale / scaleFraction;
            }
            return 0;
        }

        public override string ToString()
        {
            return string.Format("[Identifier: {0}; Bounds: {1}]", Identifier, Bounds);
        }

        public override bool Equals(object obj)
        {
            return obj is MapLeaf && this == (MapLeaf)obj;
        }

        public override int GetHashCode()
        {
            return Identifier.GetHashCode();
        }

        public static bool operator ==(MapLeaf x, MapLeaf y)
        {
            return x.Identifier == y.Identifier;
        }

        public static bool operator !=(MapLeaf x, MapLeaf y)
        {
            return !(x == y);
        }
    }
}
