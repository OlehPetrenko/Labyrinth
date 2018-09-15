using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Interfaces;
using Assets.Scripts;

namespace Assets.Classes.PathFinder
{
    public sealed class RandomPathFinder : IPathFinder
    {
        public List<Point> FindPath(bool[,] field, Point start, Point goal)
        {
            var availablePoints = new List<Point>();

            if (!GameSessionData.Instance.Maze[start.X, start.Y - 1])
                availablePoints.Add(new Point { X = start.X, Y = start.Y - 1 });

            if (!GameSessionData.Instance.Maze[start.X, start.Y + 1])
                availablePoints.Add(new Point { X = start.X, Y = start.Y + 1 });

            if (!GameSessionData.Instance.Maze[start.X - 1, start.Y])
                availablePoints.Add(new Point { X = start.X - 1, Y = start.Y });

            if (!GameSessionData.Instance.Maze[start.X + 1, start.Y])
                availablePoints.Add(new Point { X = start.X + 1, Y = start.Y });

            var rand = new Random();

            var point = availablePoints[rand.Next(0, availablePoints.Count)];

            return new List<Point> { point };
        }
    }
}
