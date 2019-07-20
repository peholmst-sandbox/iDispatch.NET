using iDispatch.MapTiles.Files;
using iDispatch.MapTiles.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                var mapTiles = GetCoveringMapTiles(rasterBounds);
                foreach (var mapTile in mapTiles) {
                    var existing = _repository.GetMapTileImage(_backgroundMap, mapTile);
                    if (existing == null)
                    {
                        Console.Out.WriteLine(" - Creating map tile {0} for background map {1}", mapTile, _backgroundMap);
                        _repository.SaveMapTileImage(_backgroundMap, CreateMapTileImage(mapTile, raster, rasterBounds));
                    }
                    else
                    {
                        Console.Out.WriteLine(" - Updating map tile {0} for background map {1}", mapTile, _backgroundMap);
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

        private MapTile[] GetCoveringMapTiles(GeoRect<EtrsTm35FinPoint> bounds)
        {
            throw new NotImplementedException(); // TODO Implement me!
        }

        private MapTileImage CreateMapTileImage(MapTile mapTile, BitmapSource raster, GeoRect<EtrsTm35FinPoint> rasterBounds)
        {
            throw new NotImplementedException(); // TODO Implement me!
        }
        
        private MapTileImage UpdateMapTileImage(MapTileImage existing, BitmapSource raster, GeoRect<EtrsTm35FinPoint> rasterBounds)
        {
            throw new NotImplementedException(); // TODO Implement me!
        }
    }
}
