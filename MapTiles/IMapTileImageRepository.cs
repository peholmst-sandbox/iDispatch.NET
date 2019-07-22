using System;
using System.Collections.Generic;

namespace iDispatch.MapTiles.Media
{
    /// <summary>
    /// A repository for retrieving and storing <seealso cref="MapTileImage"/>s.
    /// </summary>
    public interface IMapTileImageRepository
    {
        /// <summary>
        /// All the available <seealso cref="BackgroundMap"/>s that the user can choose from.
        /// </summary>
        IReadOnlyCollection<BackgroundMap> BackgroundMaps { get; }

        /// <summary>
        /// Gets the corresponding <seealso cref="MapTileImage"/> for the given <seealso cref="MapTile"/> on the given <seealso cref="BackgroundMap"/>.
        /// </summary>
        /// <param name="backgroundMap">the background map from which to retrieve the image</param>
        /// <param name="mapTile">the map tile of the image</param>
        /// <returns>the image or null if the tile was not found</returns>
        MapTileImage GetMapTileImage(BackgroundMap backgroundMap, MapTile mapTile);

        /// <summary>
        /// Saves the given <seealso cref="MapTileImage"/> to the given <seealso cref="BackgroundMap"/>.
        /// </summary>
        /// <param name="backgroundMap">the background map to which the image should be saved</param>
        /// <param name="mapTileImage">the image to save</param>
        void SaveMapTileImage(BackgroundMap backgroundMap, MapTileImage mapTileImage);

        /// <summary>
        /// Gets the timestamp for the given <seealso cref="MapTile"/> on the given <seealso cref="BackgroundMap"/>. 
        /// This can be used to determine whether a particular map tile needs to be updated or not when importing new tiles.
        /// </summary>
        /// <param name="backgroundMap">the background map from which to retrieve the timestamp</param>
        /// <param name="mapTile">the map tile to retrieve the timestamp for</param>
        /// <returns>the timestamp or null if the tile was not found</returns>
        DateTime GetMapTileTimestamp(BackgroundMap backgroundMap, MapTile mapTile);
    }
}
