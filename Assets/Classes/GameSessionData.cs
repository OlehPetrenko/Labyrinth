using System.Collections.Generic;
using Assets.Interfaces;
using Assets.Scripts.Units;

namespace Assets.Classes
{
    /// <summary>
    /// The singleton object with game data for session.
    /// </summary>
    public sealed class GameSessionData
    {
        private static readonly GameSessionData _instance = new GameSessionData();
        public static GameSessionData Instance { get { return _instance; } }

        private bool[,] _maze;
        private List<Point> _groundPoints;

        public string UserName { get; set; }
        public int Score { get; set; }

        public Player Player { get; set; }

        public IPathFinder PathFinder { get; set; }

        public bool[,] Maze
        {
            get { return _maze; }
            set
            {
                _groundPoints = null;
                _maze = value;
            }
        }

        public List<Point> GroundPoints
        {
            get
            {
                if (_groundPoints != null)
                    return _groundPoints;

                _groundPoints = new List<Point>();

                var rMax = Maze.GetUpperBound(0);
                var cMax = Maze.GetUpperBound(1);

                for (var i = 0; i < rMax; i++)
                {
                    for (var j = 0; j < cMax; j++)
                    {
                        if (!Maze[i, j])
                            _groundPoints.Add(new Point { X = i, Y = j });
                    }
                }

                return _groundPoints;
            }
        }


        public void InitializeSession()
        {
            _groundPoints = null;

            Score = 0;
            PathFinder = null;
        }
    }
}

