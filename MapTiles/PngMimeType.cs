using iDispatch.MapTiles.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iDispatch.MapTiles.Files.MimeTypes
{
    public sealed class PngMimeType: IMimeType
    {
        public static IMimeType Instance = new PngMimeType();

        public string Extension { get; } = ".png";
    }
}
