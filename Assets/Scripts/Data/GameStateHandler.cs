using System;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Data
{
    public class GameStateHandler
    {
        private GameState _state = GameState.MainMenu;
        public GameState State => _state;

        public Action OnSpawnEnemies;
        public Action OnStartCoundown;
        public Action OnStartGame;

        public void SwitchState(GameState newState)
        {
            switch (newState)
            {
                case GameState.MainMenu:
                    break;
                case GameState.SpawnPlayer:
                    SceneManager.LoadScene("Game");
                    break;
                case GameState.SpawnEnemies:
                    OnSpawnEnemies?.Invoke();
                    break;
                case GameState.Countdown:
                    OnStartCoundown?.Invoke();
                    break;
                case GameState.Gameplay:
                    OnStartGame?.Invoke();
                    break;
                case GameState.PostGame:
                    break;
                case GameState.Pause:
                    break;
                default:
                    break;
            }
        }
    }

    public enum GameState
    {
        MainMenu = 0,
        SpawnPlayer,
        SpawnEnemies,
        Countdown,
        Gameplay,
        PostGame,
        Pause
    }
}
