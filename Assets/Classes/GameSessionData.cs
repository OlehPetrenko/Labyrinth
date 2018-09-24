using System.Collections.Generic;
using System.Linq;
using Assets.Interfaces;
using Assets.Scripts;
using Assets.Scripts.Units;
using UnityEngine;

namespace Assets.Classes
{
    /// <summary>
    /// The singleton object with game data.
    /// </summary>
    public sealed class GameSessionData
    {
        private static readonly GameSessionData _instance = new GameSessionData();

        private readonly IDataManager<ScoreItemDto> _dataManager;

        private bool[,] _maze;
        private List<Point> _groundPoints;


        public static GameSessionData Instance { get { return _instance; } }

        public string UserName { get; set; }
        public int Score { get; set; }

        public List<Coin> Coins { get; set; }
        public LinkedList<ScoreItemDto> Scores { get; set; }
        public List<MovableEnemy> Enemies { get; private set; }

        public Player Player { get; private set; }

        public IPathFinder PathFinder { get; set; }

        public bool[,] Maze
        {
            get
            {
                return _maze;
            }
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


        private GameSessionData()
        {
            Enemies = new List<MovableEnemy>();

            _dataManager = new JsonToFileDataManager<ScoreItemDto>(string.Format("{0}\\Data\\scores.txt", Application.dataPath));
            Scores = new LinkedList<ScoreItemDto>(_dataManager.Load());
        }

        public void InitializeSession()
        {
            Player = GameObject.Find("Player").GetComponent<Player>();

            Score = 0;
            Enemies = new List<MovableEnemy>();
            PathFinder = null;
        }

        public void Save()
        {
            _dataManager.Save(Scores.ToList());
        }
    }
}

