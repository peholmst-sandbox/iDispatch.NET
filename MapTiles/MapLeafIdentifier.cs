using System;

namespace iDispatch.MapTiles
{
    /// <summary>
    /// An identifier of an official Finnish map leaf.
    /// </summary>
    public struct MapLeafIdentifier
    {
        /// <summary>
        /// The scale of the map leaf. A value of 5000 means a scale of 1:5000.
        /// </summary>
        public int Scale { get; }

        private string _identifier;

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
            Scale = 200000;

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
                Scale = 100000;

                if (identifier.Length >= 4)
                {
                    var leafNumber3 = (int)Char.GetNumericValue(identifier[3]);
                    RequireLeafNumber(leafNumber3, 1, 4);
                    Scale = 50000;

                    if (identifier.Length >= 5)
                    {
                        var leafNumber4 = (int)Char.GetNumericValue(identifier[4]);
                        RequireLeafNumber(leafNumber4, 1, 4);
                        Scale = 25000;

                        if (identifier.Length >= 6)
                        {
                            var leafLetter2 = identifier[5];
                            if (leafLetter2 < 'A' || leafLetter2 > 'G')
                            {
                                throw new ArgumentException("Invalid leaf letter: " + leafLetter2);
                            }
                            Scale = 10000;

                            if (identifier.Length == 7)
                            {
                                var leafNumber5 = (int)Char.GetNumericValue(identifier[6]);
                                RequireLeafNumber(leafNumber5, 1, 4);
                                Scale = 5000;
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
