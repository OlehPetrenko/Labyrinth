using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Classes;

namespace Assets.Interfaces
{
    public interface IPathFinder
    {
        List<Point> FindPath(bool[,] field, Point start, Point goal);
    }
}
