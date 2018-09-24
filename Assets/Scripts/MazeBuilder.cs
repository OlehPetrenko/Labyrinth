using Assets.Classes;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// The object to build a maze.
    /// </summary>
    public sealed class MazeBuilder : MonoBehaviour
    {
        [SerializeField] private GameObject _floor;
        [SerializeField] private GameObject _wall;

        private MazeDataGenerator _dataGenerator;

        public bool[,] Data { get; private set; }

        public float HallWidth { get; private set; }
        public float HallHeight { get; private set; }

        public int StartRow { get; private set; }
        public int StartCol { get; private set; }


        private void Awake()
        {
            //
            // Default to walls surrounding a single empty cell.
            //
            Data = new[,]
            {
                {true, true, true},
                {true, false, true},
                {true, true, true}
            };
        }

        public void GenerateNewMaze(int sizeRows, int sizeCols)
        {
            _dataGenerator = new MazeDataGenerator();

            DisposeOldMaze();

            Data = _dataGenerator.GenerateMaze(sizeRows, sizeCols);

            FindStartPosition();

            HallWidth = 1;
            HallHeight = 1;

            DisplayMaze();
        }

        private void DisplayMaze()
        {
            var newObject = new GameObject
            {
                name = "Maze Item",
                tag = "GeneratedMazeObject",
            };

            newObject.transform.SetParent(transform);

            var maze = Data;
            var rMax = maze.GetUpperBound(0);
            var cMax = maze.GetUpperBound(1);

            //
            // Loop top to bottom, left to right.
            //
            for (var i = rMax; i >= 0; i--)
            {
                for (var j = 0; j <= cMax; j++)
                {
                    if (maze[i, j] == false)
                    {
                        newObject = Instantiate(_floor, transform);
                        newObject.transform.position = new Vector3(i, j, 0.5f);
                    }
                    else
                    {
                        newObject = Instantiate(_wall, transform);
                        newObject.transform.position = new Vector2(i, j);
                    }
                }
            }
        }

        private void FindStartPosition()
        {
            var maze = Data;
            var rMax = maze.GetUpperBound(0);
            var cMax = maze.GetUpperBound(1);

            for (var i = 0; i <= rMax; i++)
            {
                for (var j = 0; j <= cMax; j++)
                {
                    if (maze[i, j] == false)
                    {
                        StartRow = i;
                        StartCol = j;
                        return;
                    }
                }
            }
        }

        public void DisposeOldMaze()
        {
            var mazeObjects = GameObject.FindGameObjectsWithTag("GeneratedMazeObject");

            foreach (var obj in mazeObjects)
            {
                Destroy(obj);
            }
        }
    }
}