using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tron.Interface;

namespace TronBot
{
    public class GridPopulator
    {
        public bool[,] SetCurrentOccupiedLocations(int GridXDimension, int GridYDimension, List<Location> occupiedLocations)
        {
            var newGrid = new bool[GridXDimension, GridYDimension];
            foreach (var location in occupiedLocations)
                newGrid[location.X - 1, location.Y - 1] = true;
            return newGrid;
        }
    }
}
