using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Interfaces;

namespace Assets.Classes.PathFinder
{
    /// <summary>
    /// Provides logic to get a path between two points using the A* algorithm.
    /// </summary>
    public sealed class AStarPathFinder : IPathFinder
    {
        private const int DistanceBetweenNeighbours = 1;

        private readonly PathFinderHelper _helper;


        public AStarPathFinder()
        {
            _helper = new PathFinderHelper();
        }

        public LinkedList<Point> FindPath(bool[,] field, Point start, Point goal)
        {
            var closedSet = new List<PathNode>();
            var openSet = new List<PathNode>();

            var startNode = GetStartNode(start, goal);
            openSet.Add(startNode);

            while (openSet.Any())
            {
                var currentNode = openSet.OrderBy(node => node.EstimateFullPathLength).First();

                if (currentNode.Position == goal)
                    return GetPathForNode(currentNode);

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                foreach (var neighbourNode in GetNeighbours(currentNode, goal, field))
                {
                    if (closedSet.Any(node => node.Position == neighbourNode.Position))
                        continue;

                    var openNode = openSet.FirstOrDefault(node => node.Position == neighbourNode.Position);

                    if (openNode == null)
                    {
                        openSet.Add(neighbourNode);
                    }
                    else if (openNode.PathLengthFromStart > neighbourNode.PathLengthFromStart)
                    {
                        openNode.CameFrom = currentNode;
                        openNode.PathLengthFromStart = neighbourNode.PathLengthFromStart;
                    }
                }
            }

            return null;
        }

        private PathNode GetStartNode(Point start, Point goal)
        {
            return new PathNode
            {
                Position = start,
                CameFrom = null,
                PathLengthFromStart = 0,
                HeuristicEstimatePathLength = GetHeuristicPathLength(start, goal)
            };
        }

        private List<PathNode> GetNeighbours(PathNode pathNode, Point goal, bool[,] field)
        {
            var result = new List<PathNode>();

            var neighbourPoints = _helper.GetNeighbourPoints(pathNode.Position);

            foreach (var point in neighbourPoints)
            {
                if (!_helper.IsPointAvailableToMove(field, point))
                    continue;

                var neighbourNode = new PathNode
                {
                    Position = point,
                    CameFrom = pathNode,
                    PathLengthFromStart = pathNode.PathLengthFromStart + DistanceBetweenNeighbours,
                    HeuristicEstimatePathLength = GetHeuristicPathLength(point, goal)
                };

                result.Add(neighbourNode);
            }

            return result;
        }

        private int GetHeuristicPathLength(Point from, Point to)
        {
            return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
        }

        private LinkedList<Point> GetPathForNode(PathNode pathNode)
        {
            var result = new LinkedList<Point>();
            var currentNode = pathNode;

            while (currentNode != null)
            {
                result.AddFirst(currentNode.Position);
                currentNode = currentNode.CameFrom;
            }

            return result;
        }
    }
}
