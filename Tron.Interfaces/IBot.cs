using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tron.Tests
{
    public interface IBot
    {
        int GridXDimension { get; set; }
        int GridYDimension { get; set; }
        Location Location { get; set; }
        string Name { get; set; }
        bool IsAlive { get; set; }
        Direction GetNewDirection(List<Location> occupiedLocations); 
    }
}
