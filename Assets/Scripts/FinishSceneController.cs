using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class FinishSceneController : MonoBehaviour
    {
        private TextMeshProUGUI _score;

        public void RestartGame()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void OpenMainMenu()
        {
            SceneManager.LoadScene("StartScene");
        }

        private void Awake()
        {
            _score = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();

            _score.text = string.Format("SCORE: {0}", GameSessionData.Instance.CoinCount);

        }
    }
}
