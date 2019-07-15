using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace iDispatch.MapControl
{
    internal enum TileState { Pending, Loading, Loaded }

    internal sealed class Tile : Image
    {
        public const int TileSize = 256;
        public int Zoom { get; private set; }
        public int Column { get; private set; }
        public int Row { get; private set; }
        public TileState State { get; private set; } = TileState.Pending;

        private ITileGenerator tileGenerator;

        public Tile(int zoom, int column, int row)
        {
            Zoom = zoom;
            Column = column;
            Row = row;
            Width = TileSize;
            Height = TileSize;
        }

        public void ScheduleLoadTile(ITileGenerator tileGenerator, BackgroundWorker backgroundWorker)
        {
            if (State == TileState.Pending)
            {
                this.tileGenerator = tileGenerator;
                State = TileState.Loading;
                backgroundWorker.DoWork += LoadTileInBackground;
            }
        }

        private void LoadTileInBackground(object sender, DoWorkEventArgs e)
        {
            var image = tileGenerator?.GetTileImage(Zoom, Column, Row) ?? null;
            if (image == null)
            {
                image = PaintUnknownTile();
            }
            Dispatcher.BeginInvoke(new Action(() => {
                Source = image;
                State = TileState.Loaded;
            }));
        }

        private ImageSource PaintUnknownTile()
        {
            var drawing = new DrawingGroup();
            using (var drawingContext = drawing.Open())
            {
                var rect = new Rect(new Point(0, 0), new Point(TileSize, TileSize));
                drawingContext.DrawRectangle(Brushes.LightGray, null, rect);
            }

            return new DrawingImage(drawing);
        }
    }
}
