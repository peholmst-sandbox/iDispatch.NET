using iDispatch.MapTiles.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace iDispatch.MapTiles.Importers
{
    /// <summary>
    /// TODO Document me!
    /// </summary>
    public sealed class WorldFileImporter
    {
        public delegate BitmapSource ReadImageDelegate(Stream imageStream);

        private BackgroundMap _backgroundMap;
        private MapTileZoomLevel _zoomLevel;
        private IMapTileImageRepository _repository;
        private ReadImageDelegate _readImageDelegate;

        public WorldFileImporter(BackgroundMap backgroundMap, MapTileZoomLevel zoomLevel,
            IMapTileImageRepository repository, ReadImageDelegate readImageDelegate)
        {
            _backgroundMap = backgroundMap;
            _zoomLevel = zoomLevel;
            _repository = repository;
            _readImageDelegate = readImageDelegate;
        }

        public void Import(WorldFile worldFile)
        {
            Console.Out.WriteLine("Importing raster file {0}", worldFile.RasterFileName);
            using (var stream = worldFile.OpenRasterStream())
            {
                var raster = _readImageDelegate(stream);
                var rasterBounds = GetBounds(worldFile, raster);
                Console.Out.WriteLine("- Raster file bounds are {0}", rasterBounds);
                var mapTiles = GetCoveringMapTiles(rasterBounds);
                Console.Out.WriteLine("- Raster file is covered by {0} tiles", mapTiles.Count);
                foreach (var mapTile in mapTiles)
                {
                    var existing = _repository.GetMapTileImage(_backgroundMap, mapTile);
                    // TODO Add a mechanism for checking if a tile needs to be updated based on a timestamp
                    if (existing == null)
                    {
                        Console.Out.WriteLine("-- Creating map tile {0} for background map {1}", mapTile, _backgroundMap);
                        _repository.SaveMapTileImage(_backgroundMap, CreateMapTileImage(mapTile, raster, rasterBounds));
                    }
                    else
                    {
                        Console.Out.WriteLine("-- Updating map tile {0} for background map {1}", mapTile, _backgroundMap);
                        _repository.SaveMapTileImage(_backgroundMap, UpdateMapTileImage(existing, raster, rasterBounds));
                    }
                }
            }
        }

        private GeoRect<EtrsTm35FinPoint> GetBounds(WorldFile worldFile, BitmapSource raster)
        {
            var widthInMeters = Math.Abs(raster.PixelWidth * worldFile.ScaleX);
            var heightInMeters = Math.Abs(raster.PixelHeight * worldFile.ScaleY);

            var northWest = new EtrsTm35FinPoint(worldFile.UpperLeftY, worldFile.UpperLeftX);
            var southEast = new EtrsTm35FinPoint(northWest.Northing - heightInMeters, northWest.Easting + widthInMeters);

            return new GeoRect<EtrsTm35FinPoint>(northWest, southEast);
        }

        private List<MapTile> GetCoveringMapTiles(GeoRect<EtrsTm35FinPoint> bounds)
        {
            // We know the order of the points from the getBounds method above
            var northWest = MapTile.FindMapTileContainingPoint(bounds.FirstPoint, _zoomLevel);
            var southEast = MapTile.FindMapTileContainingPoint(bounds.SecondPoint, _zoomLevel);

            var tiles = new List<MapTile>();
            for (var c = northWest.Column; c <= southEast.Column; ++c)
            {
                for (var r = southEast.Row; r <= northWest.Row; ++r)
                {
                    tiles.Add(new MapTile(_zoomLevel, c, r));
                }
            }

            return tiles;
        }

        // TODO Try to make the image creation faster by manulating bits in the bitmap directly

        private MapTileImage CreateMapTileImage(MapTile mapTile, BitmapSource raster, GeoRect<EtrsTm35FinPoint> rasterBounds)
        {
            var visual = new DrawingVisual();
            using (var dc = visual.RenderOpen())
            {
                dc.DrawRectangle(Brushes.Transparent, null, new Rect(0, 0, MapTile.TileSizeInPixels, MapTile.TileSizeInPixels));
                RenderIntoDrawingContext(mapTile, raster, rasterBounds, dc);
            }
            var tileImage = new RenderTargetBitmap(MapTile.TileSizeInPixels, MapTile.TileSizeInPixels, 96, 96, PixelFormats.Pbgra32);
            tileImage.Render(visual);
            return new MapTileImage(mapTile, tileImage);
        }

        private void RenderIntoDrawingContext(MapTile mapTile, BitmapSource raster, GeoRect<EtrsTm35FinPoint> rasterBounds, DrawingContext dc)
        {
            int rasterOffsetX = _zoomLevel.MetersToPixelsOnXAxis(mapTile.Bounds.West - rasterBounds.West);
            // When calculating the Y offset, remember that origo is in the top-left corner on images and in the bottom-left corner in world coordinates.
            int rasterOffsetY = _zoomLevel.MetersToPixelsOnYAxis(rasterBounds.North - mapTile.Bounds.North);

            int width = MapTile.TileSizeInPixels;
            int height = MapTile.TileSizeInPixels;

            int tileOffsetX = 0;
            int tileOffsetY = 0;

            if (rasterOffsetX < 0)
            {
                tileOffsetX = -rasterOffsetX;
                width += rasterOffsetX;
                rasterOffsetX = 0;
            }
            if (rasterOffsetX + width > raster.PixelWidth)
            {
                width = raster.PixelWidth - rasterOffsetX;
            }

            if (rasterOffsetY < 0)
            {
                tileOffsetY = -rasterOffsetY;
                height += rasterOffsetY;
                rasterOffsetY = 0;
            }
            if (rasterOffsetY + height > raster.PixelHeight)
            {
                height = raster.PixelHeight - rasterOffsetY;
            }

            var rasterTile = new CroppedBitmap(raster, new Int32Rect(rasterOffsetX, rasterOffsetY, width, height));
            dc.DrawImage(rasterTile, new Rect(tileOffsetX, tileOffsetY, width, height));
        }

        private MapTileImage UpdateMapTileImage(MapTileImage existing, BitmapSource raster, GeoRect<EtrsTm35FinPoint> rasterBounds)
        {
            throw new NotImplementedException(); // TODO Implement me!
        }
    }
}
