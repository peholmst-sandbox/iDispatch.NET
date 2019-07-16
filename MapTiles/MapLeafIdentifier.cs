using System;

namespace iDispatch.MapTiles
{
    /// <summary>
    /// An identifier of an official Finnish map leaf.
    /// See <a href="http://docs.jhs-suositukset.fi/jhs-suositukset/JHS197_liite8/JHS197_liite8.html">JHS 197: Appendix 8</a>
    /// for more information.
    /// </summary>
    public readonly struct MapLeafIdentifier
    {
        /// <summary>
        /// The scale factor to use when calculating the width of a map leaf at this <see cref="Level">level</see>.
        /// The scale is realtive to the width of the top level leaves.
        /// </summary>
        public double WidthScale { get; }
        /// <summary>
        /// The scale factor to use when calculating the height of a map leaf at this <see cref="Level">level</see>.
        /// This scale is relative to the height of the top level leaves.
        /// </summary>
        public double HeightScale { get; }
        /// <summary>
        /// Returns the level of this map leaf. A value of 0 is the top level and a value of 5 is the lowest level.
        /// </summary>
        public int Level { get; }

        private readonly string _identifier;

        /// <summary>
        /// Creates a new map leaf identifier.
        /// </summary>
        /// <param name="identifier">the string identifier of the map leaf (e.g. V3133A3)</param>
        /// <exception cref="ArgumentException">if the identifier is not valid</exception>
        public MapLeafIdentifier(string identifier)
        {
            if (identifier.Length < 2)
            {
                throw new ArgumentException("The identifier is too short");
            }
            var leafLetter = identifier[0];
            var leafNumber = (int)Char.GetNumericValue(identifier[1]);

            if (leafLetter == 'K')
            {
                RequireLeafNumber(leafNumber, 2, 4);
            }
            else if (leafLetter == 'L')
            {
                RequireLeafNumber(leafNumber, 2, 5);
            }
            else if (leafLetter == 'M' || leafLetter == 'Q' || leafLetter == 'V' || leafLetter == 'W')
            {
                RequireLeafNumber(leafNumber, 3, 5);
            }
            else if (leafLetter == 'N' || leafLetter == 'P')
            {
                RequireLeafNumber(leafNumber, 3, 6);
            }
            else if (leafLetter == 'R' || leafLetter == 'S' || leafLetter == 'T' || leafLetter == 'U' || leafLetter == 'X')
            {
                RequireLeafNumber(leafNumber, 4, 5);
            }
            else
            {
                throw new ArgumentException("Invalid leaf letter: " + leafLetter);
            }
            WidthScale = 1.0;
            HeightScale = 1.0;
            Level = 0;

            if (identifier.Length >= 3)
            {
                var leafNumber2 = (int)Char.GetNumericValue(identifier[2]);
                if (leafNumber == 2 && (leafLetter == 'K' || leafLetter == 'L'))
                {
                    RequireLeafNumber(leafNumber2, 3, 4);
                }
                else if (leafNumber == 6 && (leafLetter == 'N' || leafLetter == 'P'))
                {
                    RequireLeafNumber(leafNumber2, 1, 2);
                }
                else
                {
                    RequireLeafNumber(leafNumber2, 1, 4);
                }
                WidthScale /= 2;
                HeightScale /= 2;
                Level = 1;

                if (identifier.Length >= 4)
                {
                    var leafNumber3 = (int)Char.GetNumericValue(identifier[3]);
                    RequireLeafNumber(leafNumber3, 1, 4);
                    WidthScale /= 2;
                    HeightScale /= 2;
                    Level = 2;

                    if (identifier.Length >= 5)
                    {
                        var leafNumber4 = (int)Char.GetNumericValue(identifier[4]);
                        RequireLeafNumber(leafNumber4, 1, 4);
                        WidthScale /= 2;
                        HeightScale /= 2;
                        Level = 3;

                        if (identifier.Length >= 6)
                        {
                            var leafLetter2 = identifier[5];
                            if (leafLetter2 < 'A' || leafLetter2 > 'G')
                            {
                                throw new ArgumentException("Invalid leaf letter: " + leafLetter2);
                            }
                            WidthScale /= 4; // At this stage, the leaf is split up into 8 squares
                            HeightScale /= 2;
                            Level = 4;

                            if (identifier.Length == 7)
                            {
                                var leafNumber5 = (int)Char.GetNumericValue(identifier[6]);
                                RequireLeafNumber(leafNumber5, 1, 4);
                                WidthScale /= 2;
                                HeightScale /= 2;
                                Level = 5;
                            }
                            else if (identifier.Length > 7)
                            {
                                throw new ArgumentException("Invalid identifier: " + identifier);
                            }
                        }
                    }
                }
            }
            _identifier = identifier;
        }

        private static void RequireLeafNumber(int number, int min, int max)
        {
            if (number < min || number > max)
            {
                throw new ArgumentOutOfRangeException("Invalid leaf number: " + number);
            }
        }

        /// <summary>
        /// Returns the identifier of the parent leaf, if any.
        /// </summary>
        /// <returns>the parent leaf or null if this is a top level leaf.</returns>
        public MapLeafIdentifier? GetParentIdentifier()
        {
            if (_identifier.Length == 2)
            {
                return null;
            }
            else
            {
                return new MapLeafIdentifier(_identifier.Substring(0, _identifier.Length - 1));
            }
        }

        public override string ToString()
        {
            return _identifier;
        }

        public override bool Equals(object obj)
        {
            return obj is MapLeafIdentifier && this == (MapLeafIdentifier)obj;
        }

        public override int GetHashCode()
        {
            return _identifier.GetHashCode();
        }

        public static bool operator ==(MapLeafIdentifier x, MapLeafIdentifier y)
        {
            return x._identifier == y._identifier;
        }

        public static bool operator !=(MapLeafIdentifier x, MapLeafIdentifier y)
        {
            return !(x == y);
        }
    }
}
