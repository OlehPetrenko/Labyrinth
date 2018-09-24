using System.Collections.Generic;
using Assets.Classes;

namespace Assets.Interfaces
{
    /// <summary>
    /// Represents a object to find a path between two points.
    /// </summary>
    public interface IPathFinder
    {
        LinkedList<Point> FindPath(bool[,] field, Point start, Point goal);
    }
}
