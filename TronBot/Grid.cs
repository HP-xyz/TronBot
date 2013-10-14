using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tron.Interface;

namespace TronBot
{
    public class Grid
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public bool[,] CurrentGrid { get; set; }
        private Location _CurrentLocation;
        public Location CurrentLocation 
        {
            get { return _CurrentLocation; }
            set
            {
                _CurrentLocation = value;
                SetCurrentLocationAsOccupied(_CurrentLocation.X, _CurrentLocation.Y);
            }
        }
        public int GridXDimension { get; set; }
        public int GridYDimension { get; set; }
        protected bool MoveLeftIsInBounds
        {
            get
            {
                return CurrentLocation.X - 2 >= 0;
            }
        }
        protected bool PositionLeftIsNotOccupied
        {
            get
            {
                return CurrentGrid[CurrentLocation.X - 2, CurrentLocation.Y - 1] == false;
            }
        }
        protected bool MoveLeftIsPossible
        {
            get
            {
                return MoveLeftIsInBounds && PositionLeftIsNotOccupied;
            }
        }
        protected bool MoveRightIsInBounds
        {
            get
            {
                return CurrentLocation.X < GridXDimension;
            }
        }
        protected bool PositionRightIsNotOccupied
        {
            get
            {
                return CurrentGrid[CurrentLocation.X, CurrentLocation.Y - 1] == false;
            }
        }
        protected bool MoveRightIsPossible
        {
            get
            {
                return MoveRightIsInBounds && PositionRightIsNotOccupied;
            }
        }
        protected bool MoveDownIsInBounds
        {
            get
            {
                return CurrentLocation.Y - 2 >= 0;
            }
        }
        protected bool PositionDownIsNotOccupied
        {
            get
            {
                return CurrentGrid[CurrentLocation.X - 1, CurrentLocation.Y - 2] == false;
            }
        }
        protected bool MoveDownIsPossible
        {
            get
            {
                return MoveDownIsInBounds && PositionDownIsNotOccupied;
            }
        }
        protected bool MoveUpIsInBounds
        {
            get
            {
                return CurrentLocation.Y < GridYDimension;
            }
        }
        protected bool PositionUpIsNotOccupied
        {
            get
            {
                return CurrentGrid[CurrentLocation.X - 1, CurrentLocation.Y] == false;
            }
        }
        protected bool MoveUpIsPossible
        {
            get
            {
                return MoveUpIsInBounds && PositionUpIsNotOccupied;
            }
        }
        public Grid() { }
        public Grid(Grid grid)
        {
            CurrentGrid = grid.CurrentGrid;
            CurrentLocation = grid.CurrentLocation;
            GridXDimension = grid.GridXDimension;
            GridYDimension = grid.GridYDimension;
        }
        public List<Direction> GetPossibleDirections()
        {
            List<Direction> possibleDirections = new List<Direction>();
            if (MoveLeftIsPossible)
                possibleDirections.Add(Direction.Left);
            if (MoveRightIsPossible)
                possibleDirections.Add(Direction.Right);
            if (MoveDownIsPossible)
                possibleDirections.Add(Direction.Down);
            if (MoveUpIsPossible)
                possibleDirections.Add(Direction.Up);
            return possibleDirections;
        }
        public bool TakeStepInDirection(Direction direction)
        {
            if (!GetPossibleDirections().Contains(direction)) return false;
            int x = CurrentLocation.X - 1;
            int y = CurrentLocation.Y - 1;
            if (direction == Direction.Down)
                y = y - 1;
            if (direction == Direction.Left)
                x = x - 1;
            if (direction == Direction.Right)
                x = CurrentLocation.X;
            if (direction == Direction.Up)
                y = CurrentLocation.Y;
            _log.Debug(String.Format("Grid at '{0}','{1}' is now populated", x, y));
            CurrentGrid[x, y] = true;
            CurrentLocation = new Location() { X = x + 1, Y = y + 1 };
            return true;
        }
        private void SetCurrentLocationAsOccupied(int x, int y)
        {
            CurrentGrid[x - 1, y - 1] = true;
        }
        public void LogCurrentGridState()
        {
            for(int i = 0; i < GridXDimension; i++)
            {
                string logline = "";
                for(int j = 0; j < GridYDimension; j++)
                {
                    if (CurrentGrid[i,j] == false)
                        logline += "[O]";
                    else
                        logline += "[X]";
                }
                _log.Debug(logline);
            }
        }
    }
}
