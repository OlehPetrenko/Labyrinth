using System.Collections.Generic;
using Assets.Classes;

namespace Assets.Interfaces
{
    public interface IPathFinder
    {
        LinkedList<Point> FindPath(bool[,] field, Point start, Point goal);
    }
}
