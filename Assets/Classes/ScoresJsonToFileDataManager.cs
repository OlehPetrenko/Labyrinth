using System.Collections.Generic;
using System.IO;
using Assets.Interfaces;
using UnityEngine;

namespace Assets.Classes
{
    public sealed class ScoresJsonToFileDataManager : IScoresDataManager
    {
        private readonly string _path;

        public ScoresJsonToFileDataManager()
        {
            _path = string.Format("{0}\\Data\\scores.txt", Application.dataPath);
        }

        public List<ScoreItemDto> Load()
        {
            if (!File.Exists(_path))
            {
                Debug.Log("Incorrect file path with scores data.");
                return new List<ScoreItemDto>();
            }

            var json = File.ReadAllText(_path);
            var scores = JsonHelper.FromJson<ScoreItemDto>(json);

            return scores;
        }

        public void Save(List<ScoreItemDto> items)
        {
            var json = JsonHelper.ToJson(items);

            File.WriteAllText(_path, json);
        }
    }
}
