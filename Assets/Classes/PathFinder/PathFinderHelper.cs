using System;

namespace Assets.Classes.PathFinder
{
    /// <summary>
    /// Provides common functionality for path finders.
    /// </summary>
    public sealed class PathFinderHelper
    {
        public Point[] GetNeighbourPoints(Point point)
        {
            //
            // Neighboring points are adjacent points around.
            //
            var neighbourPoints = new Point[4];

            neighbourPoints[0] = new Point(point.X + 1, point.Y);
            neighbourPoints[1] = new Point(point.X - 1, point.Y);
            neighbourPoints[2] = new Point(point.X, point.Y + 1);
            neighbourPoints[3] = new Point(point.X, point.Y - 1);

            return neighbourPoints;
        }

        public bool IsPointAvailableToMove(bool[,] field, Point point)
        {
            //
            // Check that not exceeded the boundaries of the map.
            //
            if (point.X < 0 || point.X >= field.GetLength(0))
                return false;
            if (point.Y < 0 || point.Y >= field.GetLength(1))
                return false;

            //
            // Check that point is empty.
            //
            return !field[point.X, point.Y];
        }
    }
}
