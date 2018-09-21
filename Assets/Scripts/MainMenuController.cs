using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField _nameField;

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
            GameSessionData.Instance.Save();

            Application.Quit();
        }
    }
}
