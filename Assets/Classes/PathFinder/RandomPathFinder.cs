using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Interfaces;
using Assets.Scripts;

namespace Assets.Classes.PathFinder
{
    /// <summary>
    /// Provides logic to get a valid random point for further moving.
    /// </summary>
    public sealed class RandomPathFinder : IPathFinder
    {
        private readonly PathFinderHelper _helper;


        public RandomPathFinder()
        {
            _helper = new PathFinderHelper();
        }

        public List<Point> FindPath(bool[,] field, Point start, Point goal)
        {
            var neighbourPoints = _helper.GetNeighbourPoints(start);

            var availablePoints = neighbourPoints.Where(point => _helper.IsPointAvailableToMove(field, point)).ToList();

            var rand = new Random();

            var availablePoint = availablePoints[rand.Next(0, availablePoints.Count)];

            return new List<Point> { availablePoint };
        }
    }
}
