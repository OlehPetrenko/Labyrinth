using UnityEngine;

namespace Assets.Scripts
{
    public class GameSaver : MonoBehaviour
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
