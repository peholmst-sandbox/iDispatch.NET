using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace iDispatch.MapControl
{
    public interface ITileGenerator
    {
        Rect GetMaxBoundsForZoom(int zoom);

        int SanitizeZoom(int zoom);

        ImageSource GetTileImage(int zoom, int column, int row);
    }
}
