using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;
using iDispatch.MapTiles.Media;
using iDispatch.MapTiles.Tests.TestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace iDispatch.MapTiles.Importers.Tests
{
    [TestClass]
    public class WorldFileImporterTest
    {
        private class TestRepository : IMapTileImageRepository
        {
            private string _rootDirectory;

            public TestRepository()
            {
                _rootDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                Directory.CreateDirectory(_rootDirectory);
                Console.Out.WriteLine("Storing map tiles in directory {0}", _rootDirectory);
            }

            public ISet<BackgroundMap> GetBackgroundMaps()
            {
                throw new NotImplementedException();
            }

            public MapTileImage GetMapTileImage(BackgroundMap backgroundMap, MapTile mapTile)
            {
                return null;
            }

            public void SaveMapTileImage(BackgroundMap backgroundMap, MapTileImage mapTileImage)
            {
                var destinationDirectory = Path.Combine(_rootDirectory, backgroundMap.SymbolicName, 
                    mapTileImage.MapTile.ZoomLevel.Zoom.ToString(),
                    mapTileImage.MapTile.Row.ToString());
                Directory.CreateDirectory(destinationDirectory);

                using (var fileStream = new FileStream(Path.Combine(destinationDirectory, mapTileImage.MapTile.Column + ".png"), FileMode.Create))
                {
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(mapTileImage.Image));
                    encoder.Save(fileStream);
                }
            }
        }


        [TestMethod]
        public void ImportRasterFile()
        {
            var rasterFile = new FileInfo(
                Path.Combine(
                    Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,
                    TestFiles.MapLeaf
                 )
            );

            var repository = new TestRepository();
            var backgroundMap = new BackgroundMap("test");

            var importer = new WorldFileImporter(backgroundMap, MapTileZoomLevels.Zoom10, repository, 
                new WorldFileImporter.ReadImageDelegate(ReadImageDelegates.ReadPng));
            importer.Import(new WorldFile(rasterFile));

            // TODO Verify mock
        }
    }
}
