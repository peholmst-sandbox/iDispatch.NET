using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iDispatch.MapTiles.Media
{
    /// <summary>
    /// TODO document me!
    /// </summary>
    public class BackgroundMap
    {
        public string SymbolicName { get; }

        //private Map<string, string> _humanReadableNames;

        public BackgroundMap(string symbolicName)
        {
            SymbolicName = symbolicName;
        }

        public string GetHumanReadableName(CultureInfo cultureInfo)
        {
            return SymbolicName;
        }
    }
}
