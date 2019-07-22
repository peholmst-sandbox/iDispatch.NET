using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace iDispatch.MapTiles.Media
{
    /// <summary>
    /// TODO Document me
    /// </summary>
    public class MapTileImage
    {
        public MapTile MapTile { get; }
        public BitmapSource Image { get; }

        public MapTileImage(MapTile mapTile, BitmapSource image)
        {
            MapTile = mapTile;
            Image = image;
        }
    }
}
