using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Provides logic for pool of coins.
    /// </summary>
    /// <remarks>Don't need to destroy coin objects to use it for further game sessions.</remarks>
    public sealed class CoinPool : MonoBehaviour
    {
        [SerializeField] private Coin _coin;
        [SerializeField] private int _capacity;

        public List<Coin> Coins { get; private set; }

        public List<Coin> ActiveCoins
        {
            get { return Coins.Where(coin => coin.gameObject.activeSelf).ToList(); }
        }

        public int ActiveCoinsCount
        {
            get { return ActiveCoins.Count; }
        }


        public void Awake()
        {
            DontDestroyOnLoad(this);

            if (FindObjectsOfType(GetType()).Length > 1)
                Destroy(gameObject);

            Coins = new List<Coin>(_capacity == 0 ? 4 : _capacity);
        }

        public Coin GetCoin()
        {
            var availableCoin = Coins.FirstOrDefault(coin => !coin.gameObject.activeSelf);

            if (availableCoin != null)
                return availableCoin;

            var createdCoin = Instantiate(_coin, transform);
            Coins.Add(createdCoin);

            return createdCoin;
        }
    }
}
