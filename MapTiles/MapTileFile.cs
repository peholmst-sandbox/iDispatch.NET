using System.IO;

namespace iDispatch.MapTiles.Files
{
    /// <summary>
    /// A file in the file system that contains an image representing a single <seealso cref="MapTile"/>. The file must not exist,
    /// i.e. this class can also be used when creating new files.
    /// </summary>
    public class MapTileFile
    {
        /// <summary>
        /// The map tile that the file contains.
        /// </summary>
        public MapTile MapTile { get; }

        /// <summary>
        /// The file containing the map tile.
        /// </summary>
        public FileInfo File { get; }

        /// <summary>
        /// The mime type of the file.
        /// </summary>
        public IMimeType MimeType { get; }

        /// <summary>
        /// Creates a new map tile file. The file name will be generated based on the zoom level, the column and the row of the map tile.
        /// </summary>
        /// <param name="mapTile">the map tile that the file contains</param>
        /// <param name="rootDirectory">the root directory that will contain the tiles</param>
        /// <param name="mimeType">the mime type of the map tile</param>
        public MapTileFile(MapTile mapTile, DirectoryInfo rootDirectory, IMimeType mimeType)
        {
            var fileName = string.Format("{0}-{1}-{2}{3}",
                mapTile.ZoomLevel.Zoom, mapTile.Column, mapTile.Row, mimeType.Extension);
            var path = rootDirectory.FullName + Path.DirectorySeparatorChar
                + mapTile.ZoomLevel.Zoom + Path.DirectorySeparatorChar
                + PrefixDirectoryName(mapTile.Column.ToString()) + Path.DirectorySeparatorChar
                + PrefixDirectoryName(mapTile.Row.ToString()) + Path.DirectorySeparatorChar
                + fileName;
            File = new FileInfo(path);
            MimeType = mimeType;
            MapTile = mapTile;
        }

        private string PrefixDirectoryName(string directoryName)
        {
            if (directoryName.Length <= 2)
            {
                return directoryName;
            } else
            {
                return directoryName.Substring(0, 2) + Path.DirectorySeparatorChar + directoryName;
            }
        }
    }
}
