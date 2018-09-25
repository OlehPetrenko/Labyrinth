using System.Collections.Generic;
using System.IO;
using Assets.Interfaces;
using UnityEngine;

namespace Assets.Classes
{
    /// <summary>
    /// Provides logic to manage JSON data.
    /// Lightweight kind of repository.
    /// </summary>
    public sealed class JsonToFileDataManager<T> : IDataManager<T>
    {
        private readonly string _path;


        public JsonToFileDataManager(string path = null)
        {
            //
            // Set default path if not specified.
            //
            _path = path ?? string.Format("{0}\\Data\\data{1}.txt", Application.streamingAssetsPath, typeof(T));
        }

        public List<T> Load()
        {
            if (!File.Exists(_path))
            {
                Debug.Log("Incorrect file path with scores data.");
                return new List<T>();
            }

            var json = File.ReadAllText(_path);

            if(string.IsNullOrEmpty(json))
            {
                Debug.Log("Incorrect file with scores data.");
                return new List<T>();
            }

            return JsonHelper.FromJson<T>(json);
        }

        public void Save(List<T> items)
        {
            var json = JsonHelper.ToJson(items);

            File.WriteAllText(_path, json);
        }
    }
}
