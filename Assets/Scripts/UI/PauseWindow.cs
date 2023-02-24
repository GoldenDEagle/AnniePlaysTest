using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI
{
    public class PauseWindow : MonoBehaviour
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

        public void OnContinue()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            Time.timeScale = _defaultTimeScale;
        }
    }
}