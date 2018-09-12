using System.Collections.Generic;
using Assets.Classes;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameSessionData
    {
        private static readonly GameSessionData Instance = new GameSessionData();

        public bool[,] Maze { get; set; }
        public List<Coin> Coins { get; private set; }
        public int CoinCount { get; set; }

        public List<MovableEnemy> Enemies { get; private set; }

        public GameController Game { get; private set; }

        private GameSessionData()
        {
            Coins = new List<Coin>(10);
            Enemies = new List<MovableEnemy>();

            Game = GameObject.Find("Game").GetComponent<GameController>();
        }

        public static GameSessionData GetInstance()
        {
            return Instance;
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

