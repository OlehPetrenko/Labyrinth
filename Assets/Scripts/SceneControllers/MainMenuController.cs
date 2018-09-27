using Assets.Classes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.SceneControllers
{
    /// <summary>
    /// Provides logic for the main menu of the game.
    /// </summary>
    public sealed class MainMenuController : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField _nameField;


        private void Awake()
        {
            _nameField.text = GameSessionData.Instance.UserName;
        }

        public void SetName()
        {
            GameSessionData.Instance.UserName = _nameField.text;
        }

        public void PlayGame()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void OpenScores()
        {
            SceneManager.LoadScene("ScoresScene");
        }

        public void CloseGame()
        {
            Application.Quit();
        }
    }
}
