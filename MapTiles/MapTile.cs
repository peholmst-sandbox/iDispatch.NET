using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iDispatch.MapTiles
{
    public sealed class MapTile
    {
        public IImmutableSet<MapLeaf> GetRequiredMapLeaves()
        {
            return null;
        }
    }
}
