using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tron.Interface;

namespace TronBot
{
    public class Bot : IBot
    {
        public Bot()
        {
            Name = "HP";
        }
        private AiLogic _aiLogic = new AiLogic();
        private GridPopulator _gridPopulator = new GridPopulator();
        private bool[,] Grid { get; set; }
        public int GridXDimension { get; set; }
        public int GridYDimension { get; set; }
        public Location Location { get; set; }
        public string Name { get; set; }
        public bool IsAlive { get; set; }
        public Direction GetNewDirection(List<Location> occupiedLocations)
        {
            Grid = _gridPopulator.SetCurrentOccupiedLocations
                    (
                        GridXDimension, GridYDimension, occupiedLocations
                    );
            return _aiLogic.FindBestDirection
                    (
                        GridXDimension, GridYDimension, Grid, Location
                    );
        }
    }
}
