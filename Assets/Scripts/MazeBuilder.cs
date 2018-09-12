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
        public int GoalRow { get; private set; }
        public int GoalCol { get; private set; }

        private MazeDataGenerator _dataGenerator;

        public MazeBuilder(bool showDebug)
        {
            _showDebug = showDebug;
        }

        void Start()
        {
            _floor = Resources.Load("Ground") as GameObject;
            _wall = Resources.Load("Wall") as GameObject;
        }

        void Awake()
        {
            // default to walls surrounding a single empty cell
            Data = new[,]
            {
                {true, true, true},
                {true, false, true},
                {true, true, true}
            };
        }

        public void GenerateNewMaze(int sizeRows, int sizeCols)
        {
            if (sizeRows % 2 == 0 && sizeCols % 2 == 0)
            {
                Debug.LogError("Odd numbers work better for dungeon size.");
            }

            _dataGenerator = new MazeDataGenerator();

            DisposeOldMaze();

            Data = _dataGenerator.FromDimensions(sizeRows, sizeCols);

            FindStartPosition();

            HallWidth = 1;
            HallHeight = 1;

            DisplayMaze();
        }

        private void DisplayMaze()
        {
            var go = new GameObject();
            go.transform.position = Vector3.zero;
            go.name = "Procedural Maze";
            go.tag = "Generated";

            var maze = Data;
            var rMax = maze.GetUpperBound(0);
            var cMax = maze.GetUpperBound(1);

            //loop top to bottom, left to right
            for (var i = rMax; i >= 0; i--)
            {
                for (var j = 0; j <= cMax; j++)
                {
                    if (maze[i, j] == false)
                    {
                        go = Instantiate(_floor);
                        go.transform.position = new Vector3(i, j, 0.5f);
                    }
                    else
                    {
                        go = Instantiate(_wall);
                        go.transform.position = new Vector2(i, j);
                    }
                }
            }
        }

        public void DisposeOldMaze()
        {
            var objects = GameObject.FindGameObjectsWithTag("Generated");
            foreach (var go in objects)
            {
                Destroy(go);
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

        // top-down debug display
        //private void OnGUI()
        //{
        //    if (!_showDebug)
        //    {
        //        return;
        //    }

        //    var maze = Data;
        //    var rMax = maze.GetUpperBound(0);
        //    var cMax = maze.GetUpperBound(1);

        //    var msg = string.Empty;

        //    // loop top to bottom, left to right
        //    for (var i = rMax; i >= 0; i--)
        //    {
        //        for (var j = 0; j <= cMax; j++)
        //        {
        //            if (maze[i, j] == false)
        //            {
        //                msg += "....";
        //            }
        //            else
        //            {
        //                msg += "==";
        //            }
        //        }
        //        msg += "\n";
        //    }

        //    GUI.Label(new Rect(20, 20, 500, 500), msg);
        //}
    }
}