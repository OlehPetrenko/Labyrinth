using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Classes;
using Assets.Classes.PathFinder;
using Assets.Scripts.Units;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    /// <summary>
    /// Provides main game logic.
    /// </summary>
    public sealed class GameController : MonoBehaviour
    {
        private enum EscapeType
        {
            Dead,
            Interrupted
        }

        private EscapeType _escapeType;

        private DateTime _startTime;
        private DateTime _finishTime;

        private List<MovableEnemy> _enemies;

        [SerializeField] private MazeBuilder _mazeBuilder;

        [SerializeField] private int _mazeRowsSize;
        [SerializeField] private int _mazeColsSize;

        [SerializeField] private Player _player;

        [SerializeField] private MovableEnemy _zombie;
        [SerializeField] private MovableEnemy _mummy;

        [SerializeField] private int _maxCoinsCountInMaze;
        [SerializeField] private int _coinAppearanceIntervalSec;

        [SerializeField] private Text _score;


        private void Awake()
        {
            InitializeGameSession();
        }

        private void Start()
        {
            _startTime = DateTime.Now;
        }

        private void Update()
        {
            if (Input.GetKey("escape"))
            {
                _escapeType = EscapeType.Interrupted;
                FinishGame();
            }
        }

        private void InitializeGameSession()
        {
            _enemies = new List<MovableEnemy>();
            _escapeType = EscapeType.Dead;

            CreateMaze();
            CreatePlayer();

            GameSessionData.Instance.InitializeSession();
            GameSessionData.Instance.PathFinder = new RandomPathFinder();

            CreateEnemy(_zombie);

            GameSessionData.Instance.Player.OnDestroyEvent += PlayerDied;

            StartCoroutine("CreateCoin");
        }

        private void CreateMaze()
        {
            _mazeBuilder.GenerateNewMaze(_mazeRowsSize, _mazeColsSize);
            GameSessionData.Instance.Maze = _mazeBuilder.Data;
        }

        public void UpdateScore()
        {
            GameSessionData.Instance.Score++;

            UpdateGameBehaviour();

            if (!_score.IsDestroyed())
                _score.text = GameSessionData.Instance.Score.ToString();
        }

        private void UpdateGameBehaviour()
        {
            if (GameSessionData.Instance.Score == 5)
                CreateEnemy(_zombie);
            else if (GameSessionData.Instance.Score == 10)
                CreateEnemy(_mummy);
            else if (GameSessionData.Instance.Score == 20)
                GameSessionData.Instance.PathFinder = new AStarPathFinder();
            else if (GameSessionData.Instance.Score > 20)
                _enemies.ForEach(enemy => enemy.IncreaseSpeed());
        }

        private void AddGameInfo()
        {
            var gameDuration = _finishTime - _startTime;

            var scoreItem = new ScoreItemDto
            {
                Name = GameSessionData.Instance.UserName,
                Score = GameSessionData.Instance.Score.ToString(),
                Date = DateTime.Today.ToShortDateString(),
                Duration = gameDuration.ToString(),
                Result = _escapeType.ToString()
            };

            GameCommonData.Instance.Scores.AddFirst(scoreItem);
        }

        private void CreatePlayer()
        {
            //
            // Put Player to the first available point.
            //
            var point = GameSessionData.Instance.GroundPoints.First();
            var player = Instantiate(_player, new Vector3(point.X, point.Y), transform.rotation);

            GameSessionData.Instance.Player = player;
        }

        private void CreateEnemy(MovableEnemy enemy)
        {
            List<Point> groundPoints;

            //
            // Remove first 5 points to create the first enemy at a distance from the Player.
            //
            if (!_enemies.Any())
            {
                groundPoints = new List<Point>(GameSessionData.Instance.GroundPoints);
                groundPoints.RemoveRange(0, 5);
            }
            else
            {
                groundPoints = GameSessionData.Instance.GroundPoints;
            }

            var rand = new System.Random();

            var point = groundPoints[rand.Next(0, groundPoints.Count)];
            var createdEnemy = Instantiate(enemy, new Vector3(point.X, point.Y), transform.rotation);

            _enemies.Add(createdEnemy);
        }

        IEnumerator CreateCoin()
        {
            while (true)
            {
                if (GameCommonData.Instance.CoinPool.ActiveCoinsCount >= _maxCoinsCountInMaze)
                {
                    yield return new WaitForSeconds(_coinAppearanceIntervalSec);
                    continue;
                }

                var coin = GameCommonData.Instance.CoinPool.GetCoin();

                //
                // Remove the event if it has been already added, this prevents multiple firing of the event.
                //
                coin.OnDisableEvent -= UpdateScore;
                coin.OnDisableEvent += UpdateScore;

                coin.Show();

                yield return new WaitForSeconds(_coinAppearanceIntervalSec);
            }
        }

        private void PlayerDied()
        {
            FinishGame();
        }

        private void FinishGame()
        {
            _finishTime = DateTime.Now;

            StopCoroutine("CreateCoin");

            ResetCoins();
            AddGameInfo();

            GameSessionData.Instance.Player.OnDestroyEvent -= PlayerDied;

            SceneManager.LoadScene("FinishScene");
        }

        private void ResetCoins()
        {
            if (GameCommonData.Instance.CoinPool == null)
                return;

            foreach (var coin in GameCommonData.Instance.CoinPool.Coins)
            {
                if (coin == null)
                    continue;

                coin.OnDisableEvent -= UpdateScore;
                coin.PickUp();
            }
        }
    }
}
