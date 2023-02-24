using Assets.Scripts.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Scripts.LevelManagement
{
    // used if GO needs to load a scene
    public class SceneTransitionComponent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;
        [SerializeField] private bool _switchGameState;
        [SerializeField] private GameState _newState;

        private GameStateHandler _gameStateHandler;

        [Inject]
        private void Construct(GameStateHandler gameStateHandler)
        {
            _gameStateHandler = gameStateHandler;
        }

        public void GoToScene()
        {
            if (_switchGameState)
            {
                _gameStateHandler.SwitchState(_newState);
            }

            SceneManager.LoadScene(_sceneName);
        }
    }
}