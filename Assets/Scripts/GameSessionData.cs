using System.Collections.Generic;
using Assets.Classes;
using Assets.Classes.PathFinder;
using Assets.Interfaces;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameSessionData
    {
        private static readonly GameSessionData _instance = new GameSessionData();

        public bool[,] Maze { get; set; }
        public List<Coin> Coins { get; private set; }
        public int CoinCount { get; set; }

        public List<MovableEnemy> Enemies { get; private set; }

        public GameController Game { get; private set; }

        public IPathFinder PathFinder { get; set; }

        private GameSessionData()
        {
            Coins = new List<Coin>(10);
            Enemies = new List<MovableEnemy>();

            Game = GameObject.Find("Game").GetComponent<GameController>();

            PathFinder = new RandomPathFinder();
        }

        public static GameSessionData Instance
        {
            get
            {
                return _instance;
            }
        }

        public List<Point> GetGroundPoints()
        {
            var availablePoints = new List<Point>();

            var rMax = Maze.GetUpperBound(0);
            var cMax = Maze.GetUpperBound(1);

            for (var i = 0; i < rMax; i++)
            {
                for (var j = 0; j < cMax; j++)
                {
                    if (!Maze[i, j])
                        availablePoints.Add(new Point { X = i, Y = j });
                }

            }

            return availablePoints;
        }
    }
}

