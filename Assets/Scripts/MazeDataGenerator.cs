using UnityEngine;

namespace Assets.Scripts
{
    public class MazeDataGenerator 
    {
        public float PlacementThreshold;

        public MazeDataGenerator()
        {
            PlacementThreshold = .3f;
        }

        public bool[,] FromDimensions(int sizeRows, int sizeCols)
        {
            var maze = new bool[sizeRows, sizeCols];

            var rMax = maze.GetUpperBound(0);
            var cMax = maze.GetUpperBound(1);

            for (var i = 0; i <= rMax; i++)
            {
                for (var j = 0; j <= cMax; j++)
                {
                    // outside wall
                    if (i == 0 || j == 0 || i == rMax || j == cMax)
                    {
                        maze[i, j] = true;
                    }

                    // every other inside space
                    else if (i % 2 == 0 && j % 2 == 0)
                    {
                        if (Random.value > PlacementThreshold)
                        {
                            maze[i, j] = true;

                            // in addition to this spot, randomly place adjacent
                            var a = Random.value < .2 ? 0 : (Random.value < .2 ? -1 : 1);
                            var b = a != 0 ? 0 : (Random.value < .2 ? -1 : 1);
                            maze[i + a, j + b] = true;
                        }
                    }
                }
            }

            return maze;
        }
    }
}
