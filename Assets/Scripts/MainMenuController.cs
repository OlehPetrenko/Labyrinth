using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class MainMenuController : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void OpenScores()
        {
            SceneManager.LoadScene("ScoresScene");
        }
    }
}
