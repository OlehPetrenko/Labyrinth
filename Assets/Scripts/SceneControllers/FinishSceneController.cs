using Assets.Classes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.SceneControllers
{
    /// <summary>
    /// Provides main logic for the finish scene.
    /// </summary>
    public sealed class FinishSceneController : MonoBehaviour
    {
        [SerializeField]
        public TextMeshProUGUI _scoreField;


        private void Awake()
        {
            _scoreField.text = string.Format("SCORE: {0}", GameSessionData.Instance.Score);
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
