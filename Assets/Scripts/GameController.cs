using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private MazeBuilder _mazeBuilder;
    private Coin _coin;

    private Text _score;

    private MovableEnemy _zombie;
    private MovableEnemy _mummy;

    void Awake()
    {
        _coin = Resources.Load("Coin", typeof(Coin)) as Coin;

        _zombie = Resources.Load("Zombie", typeof(MovableEnemy)) as MovableEnemy;
        _mummy = Resources.Load("Mummy", typeof(MovableEnemy)) as MovableEnemy;

        _score = GameObject.Find("CoinCount").GetComponent<Text>();

        //GameSessionData.GetInstance().CoinCount = 18;
    }

    public void UpdateScore()
    {
        GameSessionData.GetInstance().CoinCount++;

        if (GameSessionData.GetInstance().CoinCount == 5)
            CreateEnemy(_zombie);
        else if (GameSessionData.GetInstance().CoinCount == 10)
            CreateEnemy(_mummy);
        else if(GameSessionData.GetInstance().CoinCount % 5 == 0)
            GameSessionData.GetInstance().Enemies.ForEach(enemy => enemy.IncreaseSpeed());

        _score.text = GameSessionData.GetInstance().CoinCount.ToString();
    }

    // Use this for initialization
    void Start()
    {
        _mazeBuilder = GameObject.Find("Maze").GetComponent<MazeBuilder>();
        _mazeBuilder.GenerateNewMaze(17, 8);

        GameSessionData.GetInstance().Maze = _mazeBuilder.Data;

        CreateEnemy(_zombie);

        StartCoroutine("CreateCoin");
    }

    private void FixedUpdate()
    {

    }

    private void CreateEnemy(MovableEnemy enemy)
    {
        var rand = new System.Random();

        var groundPoints = GameSessionData.GetInstance().GetGroundPoints();
        //var activeCoins = GameSessionData.GetInstance().Coins.Where(coin => coin.gameObject.activeSelf);

        var point = groundPoints[rand.Next(0, groundPoints.Count)];

        //while (activeCoins.Any(coin => (int)coin.transform.position.x == point.X && (int)coin.transform.position.y == point.Y))
        //    point = groundPoints[rand.Next(0, groundPoints.Count)];

        var createdEnemy = Instantiate(enemy, new Vector3(point.X, point.Y), transform.rotation);
        GameSessionData.GetInstance().Enemies.Add(createdEnemy);
    }

    IEnumerator CreateCoin()
    {
        while (true)
        {
            var availableCoin = GameSessionData.GetInstance().Coins.FirstOrDefault(coin => !coin.gameObject.activeSelf);
            if (availableCoin != null)
            {
                availableCoin.Show();
                yield return new WaitForSeconds(5);
            }

            //var rand = new System.Random();

            if (GameSessionData.GetInstance().Coins.Count < 10)
            {
                //var availablePoints = GameSessionData.GetInstance().GetGroundPoints();
                //var point = availablePoints[rand.Next(0, availablePoints.Count)];

                var coin = Instantiate(_coin);
                coin.Show();

                GameSessionData.GetInstance().Coins.Add(coin);
            }

            yield return new WaitForSeconds(5);
        }
    }
}
