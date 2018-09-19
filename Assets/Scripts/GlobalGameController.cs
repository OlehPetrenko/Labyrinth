using UnityEngine;

namespace Assets.Scripts
{
    public class GlobalGameController : MonoBehaviour
    {

        void Awake()
        {
           
            DontDestroyOnLoad(this.gameObject);

        }

        void Update()
        {
            if (Input.GetKey("escape"))
            {
                Application.Quit();
            }
        }


        void OnApplicationQuit()
        {
            GameSessionData.Instance.Save();
        }
    }
}


