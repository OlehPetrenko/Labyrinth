using Assets.Classes;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// The singleton object for processing data saving before the game is closed.
    /// </summary>
    public sealed class GameSaver : MonoBehaviour
    {
        public void Awake()
        {
            DontDestroyOnLoad(this);

            if (FindObjectsOfType(GetType()).Length > 1)
            {
                Destroy(gameObject);
            }
        }

        private void OnApplicationQuit()
        {
            GameSessionData.Instance.Save();
        }
    }
}
