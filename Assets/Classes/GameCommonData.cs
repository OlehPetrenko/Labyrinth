using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Interfaces;
using Assets.Scripts;
using UnityEngine;

namespace Assets.Classes
{
    /// <summary>
    /// The singleton object with common game data.
    /// </summary>
    public sealed class GameCommonData
    {
        private static readonly GameCommonData _instance = new GameCommonData();
        public static GameCommonData Instance { get { return _instance; } }

        private readonly IDataManager<ScoreItemDto> _dataManager;
        private CoinPool _coinPool;

        public LinkedList<ScoreItemDto> Scores { get; set; }

        public CoinPool CoinPool
        {
            get
            {
                if (_coinPool != null)
                    return _coinPool;

                return _coinPool = GameObject.Find("CoinPool").GetComponent<CoinPool>();
            }
        }


        private GameCommonData()
        {
            _dataManager = new JsonToFileDataManager<ScoreItemDto>(string.Format("{0}\\Data\\scores.txt", Application.streamingAssetsPath));
            Scores = new LinkedList<ScoreItemDto>(_dataManager.Load());
        }

        public void Save()
        {
            _dataManager.Save(Scores.ToList());
        }
    }
}
