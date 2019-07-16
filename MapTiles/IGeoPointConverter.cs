using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iDispatch.MapTiles.Converters
{
    interface IGeoPointConverter<S, D> 
        where S: IGeoPoint 
        where D: IGeoPoint
    {
        D Convert(S source);
    }
}
