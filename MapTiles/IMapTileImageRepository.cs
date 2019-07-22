using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iDispatch.MapTiles.Media
{
    /// <summary>
    /// TODO Document me
    /// </summary>
    public interface IMapTileImageRepository
    {
        ISet<BackgroundMap> GetBackgroundMaps();

        MapTileImage GetMapTileImage(BackgroundMap backgroundMap, MapTile mapTile);

        void SaveMapTileImage(BackgroundMap backgroundMap, MapTileImage mapTileImage);

        DateTime GetTimestamp(BackgroundMap backgroundMap, MapTile mapTile);
    }
}
