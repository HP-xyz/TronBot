using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tron.Interface;

namespace TronBot.Tests
{
    [TestFixture]
    public class UnitTests
    {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Location GetNewLocation(Bot bot, Direction newDirection)
        {
            if (newDirection == Direction.Down)
                return new Location() { X = bot.Location.X - 1, Y = bot.Location.Y - 2};
            if (newDirection == Direction.Left)
                return new Location() { X = bot.Location.X - 2, Y = bot.Location.Y - 1 };
            if (newDirection == Direction.Right)
                return new Location() { X = bot.Location.X, Y = bot.Location.Y - 1 };
            if (newDirection == Direction.Up)
                return new Location() { X = bot.Location.X - 1, Y = bot.Location.Y };
            return null;
        }
        private static List<Location> GetOccupiedLocationsOn2x2GridWithTopOccupied()
        {
            return new List<Location>()
                    {
                        new Location()
                        {
                            X = 2,
                            Y = 2
                        },
                        new Location()
                        {
                            X = 1,
                            Y = 2
                        }
                    };
        }
        private static Bot GetBotWith2x2GridAndLocationAtBottomLeft()
        {
            return new Bot() { GridXDimension = 2, GridYDimension = 2, Location = new Location() { X = 1, Y = 1 } };
        }
        [Test]
        public void GetNewDirection_GivenOccupiedLocations_ShouldReturnNewDirection()
        {
            List<Location> occupiedLocations = new List<Location>()
            {
                new Location()
                {
                    X = 1,
                    Y = 1
                },
                new Location()
                {
                    X = 1,
                    Y = 2
                },
                new Location()
                {
                    X = 1,
                    Y = 3
                }
            };
            Bot bot = new Bot() { GridXDimension = 10, GridYDimension = 10, Location = new Location() {X = 5, Y = 4 } };
            var newDirection = bot.GetNewDirection(occupiedLocations);
            Assert.That(newDirection, Is.EqualTo(Direction.Down).Or.EqualTo(Direction.Left).Or.EqualTo(Direction.Right).Or.EqualTo(Direction.Up));
        }
        //[Test]
        //public void GetNewDirection_GivenOccupiedLocations_ShouldReturnNewDirectionThatIsNotOccupied()
        //{
        //    List<Location> occupiedLocations = GetOccupiedLocationsOn2x2GridWithTopOccupied();
        //    Bot bot = GetBotWith2x2GridAndLocationAtBottomLeft();
        //    var newDirection = bot.GetNewDirection(occupiedLocations);
        //    var newLocation = GetNewLocation(bot, newDirection);
        //    Assert.That(bot.Grid[newLocation.X, newLocation.Y], Is.False);
        //}
     /*   [Test]
        public void GetPossibleDirections_GivenCurrentLocationAndOccupiedPositions_ShouldReturnPossibleDirections()
        {
            List<Location> occupiedLocations = GetOccupiedLocationsOn2x2GridWithTopOccupied();
            AiLogic ai = new AiLogic() 
            {
                CurrentGrid = new GridPopulator().SetCurrentOccupiedLocations(2,2, occupiedLocations),
                CurrentLocation = new Location(){X = 1, Y = 1},
                GridXDimension = 2,
                GridYDimension = 2
            };
            var possibleDirections = ai.GetPossibleDirections();
            Assert.That(possibleDirections[0], Is.EqualTo(Direction.Right));
        }
        [Test]
        public void GuessNumberOfStepsBeforeDeath_GivenDirectionUpAndPossibleLocationsAndGridWithNoOtherPlayers_ShouldReturnDirectionWithMostPossibleGuessedSteps()
        {
            List<Location> occupiedLocations = GetOccupiedLocationsOn2x2GridWithTopOccupied();
            AiLogic ai = new AiLogic()
            {
                CurrentGrid = new GridPopulator().SetCurrentOccupiedLocations(2, 2, occupiedLocations),
                CurrentLocation = new Location() { X = 1, Y = 1 },
                GridXDimension = 2,
                GridYDimension = 2
            };
            var numberOfSteps = ai.GuessNumberOfStepsBeforeDeath(Direction.Up);
            Assert.That(numberOfSteps, Is.EqualTo(3));
        }*/
        [Test]
        public void GetBestDirection_Given2x2GridAndTopLocationsOccupied_ShouldRecurseAndReturnRight()
        {
            _log.Debug("Test Started");
            List<Location> occupiedLocations = GetOccupiedLocationsOn2x2GridWithTopOccupied();
            Grid grid = new Grid()
            {
                CurrentGrid = new GridPopulator().SetCurrentOccupiedLocations(2, 2, occupiedLocations),
                CurrentLocation = new Location() { X = 1, Y = 1 },
                GridXDimension = 2,
                GridYDimension = 2
            };
            AiLogic ai = new AiLogic()
            {
                CurrentGrid = grid
            };
            var bestDirection = ai.BestDirection;
            Assert.That(bestDirection, Is.EqualTo(Direction.Right));
        }
        [Test]
        public void GetBestDirection_Given10x10GridAndTopLeft2LocationsOccupied_ShouldRecurseAndReturnRight()
        {
            _log.Debug("Test Started");
            List<Location> occupiedLocations = GetOccupiedLocationsOn2x2GridWithTopOccupied();
            Grid grid = new Grid()
            {
                CurrentGrid = new GridPopulator().SetCurrentOccupiedLocations(10, 10, occupiedLocations),
                CurrentLocation = new Location() { X = 1, Y = 1 },
                GridXDimension = 10,
                GridYDimension = 10
            };
            AiLogic ai = new AiLogic()
            {
                CurrentGrid = grid
            };
            var bestDirection = ai.BestDirection;
            Assert.That(bestDirection, Is.EqualTo(Direction.Right));
        }
    }
}
