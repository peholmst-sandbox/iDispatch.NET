using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iDispatch.MapTiles.Importers
{

    /// <summary>
    /// TODO document me
    /// </summary>
    public static class WorldFileDirectoryScanner
    {
        public delegate void ProcessWorldFileDelegate(WorldFile worldFile);
        public static void ScanDirectory(DirectoryInfo directory, ProcessWorldFileDelegate processWorldFileDelegate)
        {

        }

    }
}
