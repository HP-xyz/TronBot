using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tron.Interface;

namespace TronBot
{
    public class AiLogic
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Grid CurrentGrid = new Grid();
        private Dictionary<Direction, int> _StepCounter = new Dictionary<Direction, int>();
        public Direction BestDirection
        {
            get
            {
                ResetCounters();
                DoGetBestPathAlgorithm(CurrentGrid.GetPossibleDirections(), CurrentGrid, null);
                return _StepCounter.FirstOrDefault(x => x.Value == _StepCounter.Values.Max()).Key;
            }
        }
        private void ResetCounters()
        {
            if(!_StepCounter.Keys.Contains(Direction.Down))
                _StepCounter.Add(Direction.Down, 0);
            if (!_StepCounter.Keys.Contains(Direction.Left))
                _StepCounter.Add(Direction.Left, 0);
            if (!_StepCounter.Keys.Contains(Direction.Right))
                _StepCounter.Add(Direction.Right, 0);
            if (!_StepCounter.Keys.Contains(Direction.Up))
                _StepCounter.Add(Direction.Up, 0);
        }
        private void DoGetBestPathAlgorithm(List<Direction> possibleDirections, Grid currentGrid, Direction? previousDirection)
        {
            currentGrid.LogCurrentGridState();
            if (possibleDirections.Contains(Direction.Down))
            {
                if (previousDirection == null) previousDirection = Direction.Down;
                Grid newGrid = new Grid(currentGrid);
                newGrid.TakeStepInDirection(Direction.Down);
                _StepCounter[previousDirection.Value]++;
                DoGetBestPathAlgorithm(newGrid.GetPossibleDirections(), newGrid, previousDirection);
            }
            if (possibleDirections.Contains(Direction.Left))
            {
                if (previousDirection == null) previousDirection = Direction.Left;
                Grid newGrid = new Grid(currentGrid);
                newGrid.TakeStepInDirection(Direction.Left);
                _StepCounter[previousDirection.Value]++;
                DoGetBestPathAlgorithm(newGrid.GetPossibleDirections(), newGrid, previousDirection);
            }
            if (possibleDirections.Contains(Direction.Right))
            {
                if (previousDirection == null) previousDirection = Direction.Right;
                Grid newGrid = new Grid(currentGrid);
                newGrid.TakeStepInDirection(Direction.Right);
                _StepCounter[previousDirection.Value]++;
                DoGetBestPathAlgorithm(newGrid.GetPossibleDirections(), newGrid, previousDirection);
            }
            if (possibleDirections.Contains(Direction.Up))
            {
                if (previousDirection == null) previousDirection = Direction.Up;
                Grid newGrid = new Grid(currentGrid);
                newGrid.TakeStepInDirection(Direction.Up);
                _StepCounter[previousDirection.Value]++;
                DoGetBestPathAlgorithm(newGrid.GetPossibleDirections(), newGrid, previousDirection);
            }
        }

        public Direction FindBestDirection(int GridXDimension, int GridYDimension, bool[,] currentGrid, Location currentLocation)
        {
            CurrentGrid.GridXDimension = GridXDimension;
            CurrentGrid.GridYDimension = GridYDimension;
            CurrentGrid.CurrentGrid = currentGrid;
            CurrentGrid.CurrentLocation = currentLocation;
            return BestDirection;
        }
    }
}
