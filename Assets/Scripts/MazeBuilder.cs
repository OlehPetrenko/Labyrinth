using Assets.Classes;
using UnityEngine;

namespace Assets.Scripts
{
    public class MazeBuilder : MonoBehaviour
    {
        [SerializeField] private bool _showDebug;

        [SerializeField] private GameObject _floor;
        [SerializeField] private GameObject _wall;

        public bool[,] Data { get; private set; }

        public float HallWidth { get; private set; }
        public float HallHeight { get; private set; }

        public int StartRow { get; private set; }
        public int StartCol { get; private set; }

        private MazeDataGenerator _dataGenerator;

        void Awake()
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

        void Start()
        {
            //_floor = Resources.Load("Ground") as GameObject;
            //_wall = Resources.Load("Wall") as GameObject;
        }

        public void GenerateNewMaze(int sizeRows, int sizeCols)
        {
            _dataGenerator = new MazeDataGenerator();

            DisposeOldMaze();

            Data = _dataGenerator.Generate(sizeRows, sizeCols);

            FindStartPosition();

            HallWidth = 1;
            HallHeight = 1;

            DisplayMaze();
        }

        private void DisplayMaze()
        {
            var newObject = new GameObject
            {
                name = "Procedural Maze",
                tag = "GeneratedMazeObject"
            };

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
                        newObject = Instantiate(_floor);
                        newObject.transform.position = new Vector3(i, j, 0.5f);
                    }
                    else
                    {
                        newObject = Instantiate(_wall);
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