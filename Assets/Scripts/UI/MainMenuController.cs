using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button _startButton;

        public void OnStartClicked()
        {
            SceneManager.LoadScene("Game");
        }
    }
}