using UnityEngine;

namespace Assets.Classes
{
    /// <summary>
    /// Provides functionality to generate data for a maze.
    /// </summary>
    public sealed class MazeDataGenerator
    {
        private const float PlacementThreshold = .3f;


        public bool[,] GenerateMaze(int sizeRows, int sizeCols)
        {
            var maze = new bool[sizeRows, sizeCols];

            var rMax = maze.GetUpperBound(0);
            var cMax = maze.GetUpperBound(1);

            for (var i = 0; i <= rMax; i++)
            {
                for (var j = 0; j <= cMax; j++)
                {
                    //
                    // Outside wall.
                    //
                    if (i == 0 || j == 0 || i == rMax || j == cMax)
                        maze[i, j] = true;

                    // 
                    // Every other inside space.
                    //
                    else if (i % 2 == 0 && j % 2 == 0)
                    {
                        if (Random.value > PlacementThreshold)
                        {
                            maze[i, j] = true;

                            //
                            // In addition to this spot, randomly place adjacent.
                            //
                            FillAdjacentSpot(maze, i, j);
                        }
                    }
                }
            }

            return maze;
        }

        private static void FillAdjacentSpot(bool[,] maze, int row, int column)
        {
            var rowShift = Random.value < .2 ? 0 : (Random.value < .2 ? -1 : 1);
            var columnShift = rowShift != 0 ? 0 : (Random.value < .2 ? -1 : 1);

            maze[row + rowShift, column + columnShift] = true;
        }
    }
}
