using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Classes;
using Assets.Classes.PathFinder;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        private enum EscapeType
        {
            Dead,
            Interrupted
        }

        private EscapeType _escapeType;

        private DateTime _startTime;
        private DateTime _finishTime;

        [SerializeField] private MazeBuilder _mazeBuilder;

        [SerializeField] private int _mazeRowsSize;
        [SerializeField] private int _mazeColsSize;

        [SerializeField] private Player _player;

        [SerializeField] private MovableEnemy _zombie;
        [SerializeField] private MovableEnemy _mummy;

        [SerializeField] private int _maxCoinsCountInMaze;
        [SerializeField] private int _coinAppearanceIntervalSec;

        [SerializeField] private Coin _coin;
        [SerializeField] private Text _score;


        private void Awake()
        {
            InitializeGameSession();

            _player.OnDestroyEvent += PlayerDied;
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

        private void OnDestroy()
        {
            FinishGame();
        }

        private void CreateMaze()
        {
            _mazeBuilder.GenerateNewMaze(_mazeRowsSize, _mazeColsSize);
            GameSessionData.Instance.Maze = _mazeBuilder.Data;
        }

        private void InitializeGameSession()
        {
            CreateMaze();

            GameSessionData.Instance.InitializeSession();
            GameSessionData.Instance.PathFinder = new RandomPathFinder();

            CreateEnemy(_zombie);

            if (GameSessionData.Instance.Coins != null)
                GameSessionData.Instance.Coins.ForEach(coin => coin.OnDisableEvent += UpdateScore);

            StartCoroutine("CreateCoin");
        }

        public void UpdateScore()
        {
            GameSessionData.Instance.CoinCount++;

            UpdateGameBehaviour();

            _score.text = GameSessionData.Instance.CoinCount.ToString();
        }

        private void UpdateGameBehaviour()
        {
            if (GameSessionData.Instance.CoinCount == 5)
                CreateEnemy(_zombie);
            else if (GameSessionData.Instance.CoinCount == 10)
                CreateEnemy(_mummy);
            else if (GameSessionData.Instance.CoinCount == 20)
                GameSessionData.Instance.PathFinder = new AStarPathFinder();
            else if (GameSessionData.Instance.CoinCount > 20)
                GameSessionData.Instance.Enemies.ForEach(enemy => enemy.IncreaseSpeed());
        }

        private void AddGameInfo()
        {
            var gameDuration = _finishTime - _startTime;

            var scoreItem = new ScoreItemDto
            {
                Name = GameSessionData.Instance.UserName,
                Score = GameSessionData.Instance.CoinCount.ToString(),
                Date = DateTime.Today.ToShortDateString(),
                Duration = gameDuration.ToString(),
                Result = _escapeType.ToString()
            };

            GameSessionData.Instance.Scores.AddFirst(scoreItem);
        }

        private void CreateEnemy(MovableEnemy enemy)
        {
            var groundPoints = GameSessionData.Instance.GroundPoints;

            var rand = new System.Random();
            var point = groundPoints[rand.Next(0, groundPoints.Count)];

            var createdEnemy = Instantiate(enemy, new Vector3(point.X, point.Y), transform.rotation);
            GameSessionData.Instance.Enemies.Add(createdEnemy);
        }

        IEnumerator CreateCoin()
        {
            if (GameSessionData.Instance.Coins == null)
                GameSessionData.Instance.Coins = new List<Coin>(_maxCoinsCountInMaze);

            while (true)
            {
                //
                // Try to use alredy created coin.
                //
                var availableCoin = GameSessionData.Instance.Coins.FirstOrDefault(coin => !coin.gameObject.activeSelf);
                if (availableCoin != null)
                {
                    availableCoin.Show();
                    yield return new WaitForSeconds(_coinAppearanceIntervalSec);
                }

                //
                // Create a new one if all coins are active.
                //
                if (GameSessionData.Instance.Coins.Count < _maxCoinsCountInMaze)
                {
                    var coin = Instantiate(_coin);
                    coin.OnDisableEvent += UpdateScore;
                    coin.Show();

                    GameSessionData.Instance.Coins.Add(coin);
                }

                yield return new WaitForSeconds(_coinAppearanceIntervalSec);
            }
        }

        private void PlayerDied()
        {
            _escapeType = EscapeType.Dead;

            SceneManager.LoadScene("FinishScene");
        }

        private void FinishGame()
        {
            _finishTime = DateTime.Now;

            if (GameSessionData.Instance.Coins != null)
            {
                GameSessionData.Instance.Coins.ForEach(coin => coin.OnDisableEvent -= UpdateScore);
                GameSessionData.Instance.Coins.ForEach(coin => coin.PickUp());
            }

            StopCoroutine("CreateCoin");
            _player.OnDestroyEvent -= PlayerDied;

            AddGameInfo();
        }
    }
}
