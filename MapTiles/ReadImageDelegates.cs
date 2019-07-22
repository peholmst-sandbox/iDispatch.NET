using System.IO;
using System.Windows.Media.Imaging;

namespace iDispatch.MapTiles.Importers
{
    /// <summary>
    /// TODO document me!
    /// </summary>
    public static class ReadImageDelegates
    {
        public static BitmapSource ReadPng(Stream imageStream)
        {
            var decoder = new PngBitmapDecoder(imageStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            return decoder.Frames[0];
        }
    }
}
