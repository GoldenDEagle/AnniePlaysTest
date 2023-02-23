using Assets.Scripts.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] private Button _startButton;

        private GameStateHandler _gameStateHandler;

        [Inject]
        private void Construct(GameStateHandler gameStateHandler)
        {
            _gameStateHandler = gameStateHandler;
        }

        public void OnStartClicked()
        {
            _gameStateHandler.SwitchState(GameState.SpawnPlayer);
        }
    }
}