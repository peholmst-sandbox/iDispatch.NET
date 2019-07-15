using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace iDispatch.MapControl
{
    public sealed class MapCanvas : Canvas
    {

        public ITileGenerator TileGenerator { get; set; }

        public Rect GeoViewport { get; private set; }

        public Rect GeoBounds { get;  }

        public Point GeoCenter { get; private set; }

        public Point GeoNorthWest { get { return GeoViewport.TopLeft; } }

        public Point GeoSouthEast { get { return GeoViewport.BottomRight; } }

        public bool CenterCrossVisible { get; set; }

        public bool ScaleIndicatorVisible { get; set; }

        public int Zoom { get; set; }

        private BackgroundWorker backgroundWorker = new BackgroundWorker();

        public MapCanvas()
        {
            
        }

        public void CenterOn(double latitude, double longitude)
        {

        }

        

        private void CalculateGeoCoordinates()
        {

        }

    }
}
