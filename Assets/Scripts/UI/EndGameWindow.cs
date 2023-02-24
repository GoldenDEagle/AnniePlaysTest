using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class EndGameWindow : MonoBehaviour
    {
        private float _defaultTimeScale;

        private void Start()
        {
            _defaultTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }

        public void OnExit()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void OnDestroy()
        {
            Time.timeScale = _defaultTimeScale;
        }
    }
}