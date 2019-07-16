using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iDispatch.MapTiles.Converters
{
    /// <summary>
    /// Converter that converts between <see cref="Etrs89Point"/> and <see cref="EtrsTm35FinPoint"/>.
    /// </summary>
    public sealed class Etrs89EtrsTm35FinConverter : 
        IGeoPointConverter<Etrs89Point, EtrsTm35FinPoint>,
        IGeoPointConverter<EtrsTm35FinPoint, Etrs89Point>
    {
        public static readonly Etrs89EtrsTm35FinConverter Instance = new Etrs89EtrsTm35FinConverter();

        // Converting between latitude, longitude and UTM coordinates (easthing, northing)
        // Source: http://docs.jhs-suositukset.fi/jhs-suositukset/JHS197_liite2/JHS197_liite2.html
        //         http://docs.jhs-suositukset.fi/jhs-suositukset/JHS197_liite3/JHS197_liite3.html

        private static class ConversionParams
        {
            // Parameters:
            public const double a = 6378137.0;
            public const double f_inv = 298.257222101; // 1/f
            public const double k0 = 0.9996;
            public const double E0 = 500000;
            public const double λ0 = 27;

            // Derived values based on the parameters:
            public static readonly double λ0_rad = λ0 * Math.PI / 180;
            public static readonly double f = 1 / f_inv;
            public static readonly double n = f / (2 - f);
            public static readonly double n_pow2 = n * n;
            public static readonly double n_pow3 = Math.Pow(n, 3);
            public static readonly double n_pow4 = Math.Pow(n, 4);
            public static readonly double A1 = a / (1 + n) * (1 + n_pow2 / 4 + n_pow4 / 64);
            public static readonly double e_pow2 = 2 * f - f * f;
            public static readonly double e = Math.Sqrt(e_pow2);
            public static readonly double e_prim_pow2 = e_pow2 / (1 - e_pow2);

            public static readonly double h1 = (n / 2) - (2 * n_pow2 / 3) + (37 * n_pow3 / 96) - (n_pow4 / 360);
            public static readonly double h2 = (n_pow2 / 48) + (n_pow3 / 15) - (437 * n_pow4 / 1440);
            public static readonly double h3 = (17 * n_pow3 / 480) - (37 * n_pow4 / 840);
            public static readonly double h4 = 4397 * n_pow4 / 161280;

            public static readonly double h1_prim = (n / 2) - (2 * n_pow2 / 3) + (5 * n_pow3 / 16) + (41 * n_pow4 / 180);
            public static readonly double h2_prim = (13 * n_pow2 / 48) - (3 * n_pow3 / 5) + (557 * n_pow4 / 1440);
            public static readonly double h3_prim = (61 * n_pow3 / 240) - (103 * n_pow4 / 140);
            public static readonly double h4_prim = 49561 * n_pow4 / 161280;
        }

        private static double Asinh(double x)
        {
            return Math.Log(x + Math.Sqrt(x * x + 1));
        }

        private static double Atanh(double x)
        {
            return Math.Log((1 + x) / (1 - x)) / 2;
        }

        private static double Sech(double x)
        {
            return 1 / Math.Cosh(x);
        }

        public EtrsTm35FinPoint Convert(Etrs89Point source)
        {
            // Convert to radians
            var φ = source.Latitude * Math.PI / 180;
            var λ = source.Longitude * Math.PI / 180;
            var l = λ - ConversionParams.λ0_rad;

            var Q = Asinh(Math.Tan(φ)) - ConversionParams.e * Atanh(ConversionParams.e * Math.Sin(φ));
            var β = Math.Atan(Math.Sinh(Q));
            var η_prim = Atanh(Math.Cos(β) * Math.Sin(l));
            var ξ_prim = Math.Asin(Math.Sin(β) / Sech(η_prim));

            var ξ1 = ConversionParams.h1_prim * Math.Sin(2 * ξ_prim) * Math.Cosh(2 * η_prim);
            var ξ2 = ConversionParams.h2_prim * Math.Sin(4 * ξ_prim) * Math.Cosh(4 * η_prim);
            var ξ3 = ConversionParams.h3_prim * Math.Sin(6 * ξ_prim) * Math.Cosh(6 * η_prim);
            var ξ4 = ConversionParams.h4_prim * Math.Sin(8 * ξ_prim) * Math.Cosh(8 * η_prim);

            var η1 = ConversionParams.h1_prim * Math.Cos(2 * ξ_prim) * Math.Sinh(2 * η_prim);
            var η2 = ConversionParams.h2_prim * Math.Cos(4 * ξ_prim) * Math.Sinh(4 * η_prim);
            var η3 = ConversionParams.h3_prim * Math.Cos(6 * ξ_prim) * Math.Sinh(6 * η_prim);
            var η4 = ConversionParams.h4_prim * Math.Cos(8 * ξ_prim) * Math.Sinh(8 * η_prim);

            var ξ = ξ_prim + ξ1 + ξ2 + ξ3 + ξ4;
            var η = η_prim + η1 + η2 + η3 + η4;

            var N = ConversionParams.A1 * ξ * ConversionParams.k0;
            var E = ConversionParams.A1 * η * ConversionParams.k0 + ConversionParams.E0;
            return new EtrsTm35FinPoint(N, E);
        }

        public Etrs89Point Convert(EtrsTm35FinPoint source)
        {
            var ξ = source.Northing / (ConversionParams.A1 * ConversionParams.k0);
            var η = (source.Easting - ConversionParams.E0) / (ConversionParams.A1 * ConversionParams.k0);

            var ξ1_prim = ConversionParams.h1 * Math.Sin(2 * ξ) * Math.Cosh(2 * η);
            var ξ2_prim = ConversionParams.h2 * Math.Sin(4 * ξ) * Math.Cosh(4 * η);
            var ξ3_prim = ConversionParams.h3 * Math.Sin(6 * ξ) * Math.Cosh(6 * η);
            var ξ4_prim = ConversionParams.h4 * Math.Sin(8 * ξ) * Math.Cosh(8 * η);

            var η1_prim = ConversionParams.h1 * Math.Cos(2 * ξ) * Math.Sinh(2 * η);
            var η2_prim = ConversionParams.h2 * Math.Cos(4 * ξ) * Math.Sinh(4 * η);
            var η3_prim = ConversionParams.h3 * Math.Cos(6 * ξ) * Math.Sinh(6 * η);
            var η4_prim = ConversionParams.h4 * Math.Cos(8 * ξ) * Math.Sinh(8 * η);

            var ξ_prim = ξ - ξ1_prim - ξ2_prim - ξ3_prim - ξ4_prim;
            var η_prim = η - η1_prim - η2_prim - η3_prim - η4_prim;

            var β = Math.Asin(Sech(η_prim) * Math.Sin(ξ_prim));
            var l = Math.Asin(Math.Tanh(η_prim) / Math.Cos(β));

            var Q = Asinh(Math.Tan(β));
            var Q_prim = Q + ConversionParams.e * Atanh(ConversionParams.e * Math.Tanh(Q));
            for (int i = 0; i < 3; ++i) // 3 iterations should be enough according to the source material
            {
                Q_prim = Q + ConversionParams.e * Atanh(ConversionParams.e * Math.Tanh(Q_prim));
            }

            var φ = Math.Atan(Math.Sinh(Q_prim));
            var λ = ConversionParams.λ0_rad + l;

            // Convert to degrees
            var latitude = φ * 180 / Math.PI;
            var longitude = λ * 180 / Math.PI;

            return new Etrs89Point(latitude, longitude);
        }
    }
}
