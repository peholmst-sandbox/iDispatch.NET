using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace iDispatch.MapTiles.Importers
{
    /// <summary>
    /// A world file that is used to georeference a raster map image. This implementation ignores the rotation (skew) parameters
    /// since the rasters provided by NLS always have a rotation of 0. It also expects the world file to conform to the 8.3 file naming convention,
    /// so that e.g. the world file for a .png file becomes .pgw.
    /// </summary>
    public sealed class WorldFile
    {
        /// <summary>
        /// Pixel width in map units (typically meters per pixel).
        /// </summary>
        public double ScaleX { get; }
        /// <summary>
        /// Pixel height in map units (typically meters per pixel and negative).
        /// </summary>
        public double ScaleY { get; }
        /// <summary>
        /// X-coordinate of the center of the raster map image's upper left pixel in map units (typically meters).
        /// </summary>
        public double UpperLeftX { get; }
        /// <summary>
        /// Y-coordinate of the center of the raster map image's upper left pixel in map units (typically meters).
        /// </summary>
        public double UpperLeftY { get; }
        /// <summary>
        /// The name of the raster map image file.
        /// </summary>
        public string RasterFileName { get; }

        private FileInfo _rasterFile;

        /// <summary>
        /// Parses the world file of the given raster image file.
        /// </summary>
        /// <param name="rasterFile">the file containing the raster map image</param>
        public WorldFile(FileInfo rasterFile)
        {
            var worldFile = new FileInfo(ExtractWorldFileName(rasterFile));
            if (!worldFile.Exists)
            {
                throw new ArgumentException("World file " + worldFile.FullName + " does not exist");
            }

            var worldFileLines = File.ReadLines(worldFile.FullName, Encoding.ASCII).ToArray();
            if (worldFileLines.Length < 6)
            {
                throw new ArgumentException("World file doest not contain enough information");
            }

            ScaleX = Double.Parse(worldFileLines[0].Trim(), CultureInfo.InvariantCulture);
            ScaleY = Double.Parse(worldFileLines[3].Trim(), CultureInfo.InvariantCulture);
            UpperLeftX = Double.Parse(worldFileLines[4].Trim(), CultureInfo.InvariantCulture);
            UpperLeftY = Double.Parse(worldFileLines[5].Trim(), CultureInfo.InvariantCulture);

            _rasterFile = rasterFile;
            RasterFileName = rasterFile.Name;
        }

        /// <summary>
        /// Opens a stream that reads the raster map image.
        /// </summary>
        /// <returns>a stream.</returns>
        public Stream ReadRaster()
        {
            return _rasterFile.OpenRead();
        }

        // TODO Refactor WorldFile so that you can create WorldFiles without actually having to touch the file system (use a separate parser for that).

        private static string ExtractWorldFileName(FileInfo rasterFile)
        {
            var rasterFileName = rasterFile.FullName;
            var rasterFileExtension = rasterFile.Extension;
            string worldFileExtension;
            if (rasterFileExtension.Length == 4)
            {
                worldFileExtension = rasterFileExtension.Substring(0, 2) + rasterFileExtension[3] + 'w';
            }
            else
            {
                worldFileExtension = rasterFileExtension + 'w';
            }
            return rasterFileName.Substring(0, rasterFileName.Length - rasterFileExtension.Length) + worldFileExtension;
        }
    }
}
