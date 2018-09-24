using Assets.Classes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    /// <summary>
    /// Provides main logic for the finish scene.
    /// </summary>
    public class FinishSceneController : MonoBehaviour
    {
        [SerializeField]
        public TextMeshProUGUI _scoreField;

        private void Awake()
        {
            _scoreField.text = string.Format("SCORE: {0}", GameSessionData.Instance.CoinCount);
        }

        public void RestartGame()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void OpenMainMenu()
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
