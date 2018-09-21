using System;
using System.Collections;
using System.Linq;
using Assets.Classes;
using Assets.Classes.PathFinder;
using Assets.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        private const int MaxCoinCount = 10;

        private MazeBuilder _mazeBuilder;
        [SerializeField]
        private Coin _coin;

        [SerializeField]
        private Text _score;

        [SerializeField]
        private MovableEnemy _zombie;
        [SerializeField]
        private MovableEnemy _mummy;

        [SerializeField]
        private Player _player;

        private DateTime _startTiem;
        private DateTime _finishTiem;

        void Awake()
        {
            _startTiem = DateTime.Now;

            //DontDestroyOnLoad(gameObject);

            //if (FindObjectsOfType(GetType()).Length > 1)
            //    Destroy(gameObject);

            //_coin = Resources.Load("Coin", typeof(Coin)) as Coin;

            //_zombie = Resources.Load("Zombie", typeof(MovableEnemy)) as MovableEnemy;
            //_mummy = Resources.Load("Mummy", typeof(MovableEnemy)) as MovableEnemy;

            //_score = GameObject.Find("CoinCount").GetComponent<Text>();

            //_player = GameObject.Find("Player").GetComponent<Player>();

            //_player.OnDestroyEvent += FinishGame;

            //InitializeGameSession();
        }



        private enum EscapeType
        {
            Dead,
            Interrupted
        }

        private EscapeType _escapeType = EscapeType.Dead;

        private void AddGameInfo()
        {
            var time = (_finishTiem - _startTiem);

            var scoreItem = new ScoreItemDto
            {
                Name = GameSessionData.Instance.UserName,
                Score = GameSessionData.Instance.CoinCount.ToString(),
                Date = DateTime.Today.ToShortDateString(),
                Duration = string.Format("{0}:{1}:{2}", time.Hours, time.Minutes, time.Seconds),
                Result = _escapeType.ToString()
            };

            GameSessionData.Instance.Scores.AddFirst(scoreItem);
        }

        private void InitializeGameSession()
        {
            GameSessionData.Instance.InitializeSession();

            GameSessionData.Instance.PathFinder = new RandomPathFinder();
        }

        private void FinishGame()
        {
            _finishTiem = DateTime.Now;


            GameSessionData.Instance.Coins.ForEach(coin => coin.PickUp());

            StopCoroutine("CreateCoin");
            _player.OnDestroyEvent -= LoadFinishScene;

            AddGameInfo();


        }

        private void LoadFinishScene()
        {
            SceneManager.LoadScene("FinishScene");

        }

        void OnDestroy()
        {
            FinishGame();
        }

        public void UpdateScore()
        {
            GameSessionData.Instance.CoinCount++;

            if (GameSessionData.Instance.CoinCount == 5)
                CreateEnemy(_zombie);
            else if (GameSessionData.Instance.CoinCount == 10)
                CreateEnemy(_mummy);
            else if (GameSessionData.Instance.CoinCount == 20)
                GameSessionData.Instance.PathFinder = new AStarPathFinder();
            else if (GameSessionData.Instance.CoinCount > 20)
                GameSessionData.Instance.Enemies.ForEach(enemy => enemy.IncreaseSpeed());

            _score.text = GameSessionData.Instance.CoinCount.ToString();
        }

        // Use this for initialization
        void Start()
        {
            Debug.Log("hhhhhhhhhhh");

            InitializeGameSession();



            _mazeBuilder = GameObject.Find("Maze").GetComponent<MazeBuilder>();
            _mazeBuilder.GenerateNewMaze(18, 10);

            GameSessionData.Instance.Maze = _mazeBuilder.Data;

            CreateEnemy(_zombie);

            StartCoroutine("CreateCoin");

            _player.OnDestroyEvent += LoadFinishScene;


        }

        private void Update()
        {
            if (Input.GetKey("escape"))
            {
                _escapeType = EscapeType.Interrupted;
                FinishGame();
            }
        }

        private void CreateEnemy(MovableEnemy enemy)
        {
            var rand = new System.Random();

            var groundPoints = GameSessionData.Instance.GetGroundPoints();
            //var activeCoins = GameSessionData.GetInstance().Coins.Where(coin => coin.gameObject.activeSelf);

            var point = groundPoints[rand.Next(0, groundPoints.Count)];

            //while (activeCoins.Any(coin => (int)coin.transform.position.x == point.X && (int)coin.transform.position.y == point.Y))
            //    point = groundPoints[rand.Next(0, groundPoints.Count)];

            var createdEnemy = Instantiate(enemy, new Vector3(point.X, point.Y), transform.rotation);
            GameSessionData.Instance.Enemies.Add(createdEnemy);
        }

        IEnumerator CreateCoin()
        {
            while (true)
            {
                var availableCoin = GameSessionData.Instance.Coins.FirstOrDefault(coin => !coin.gameObject.activeSelf);
                if (availableCoin != null)
                {
                    availableCoin.Show();
                    yield return new WaitForSeconds(5);
                }

                //var rand = new System.Random();

                if (GameSessionData.Instance.Coins.Count < MaxCoinCount)
                {
                    //var availablePoints = GameSessionData.GetInstance().GetGroundPoints();
                    //var point = availablePoints[rand.Next(0, availablePoints.Count)];

                    var coin = Instantiate(_coin);
                    coin.Show();

                    GameSessionData.Instance.Coins.Add(coin);
                }

                yield return new WaitForSeconds(5);
            }
        }
    }
}
