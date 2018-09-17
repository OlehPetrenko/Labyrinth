﻿using System.Collections;
using System.Linq;
using System.Runtime.Remoting;
using UnityEngine;

namespace Assets.Scripts
{
    public class Coin : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void PickUp()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            var rand = new System.Random();

            var groundPoints = GameSessionData.Instance.GetGroundPoints();
            var activeCoins = GameSessionData.Instance.Coins.Where(coin => coin.gameObject.activeSelf);

            var point = groundPoints[rand.Next(0, groundPoints.Count)];

            while (activeCoins.Any(coin => (int)coin.transform.position.x == point.X && (int)coin.transform.position.y == point.Y))
                point = groundPoints[rand.Next(0, groundPoints.Count)];

            transform.position = new Vector3(point.X, point.Y);

            gameObject.SetActive(true);
        }
    }
}
