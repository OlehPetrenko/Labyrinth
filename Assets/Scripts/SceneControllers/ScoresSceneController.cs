using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.SceneControllers
{
    /// <summary>
    /// Provides logic for the scene with scores.
    /// </summary>
    public sealed class ScoresSceneController : MonoBehaviour
    {
        public void OpenMainMenu()
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
