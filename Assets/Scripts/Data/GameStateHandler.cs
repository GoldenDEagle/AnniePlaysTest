using System;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Data
{
    public class GameStateHandler
    {
        private GameState _state = GameState.MainMenu;
        public GameState State => _state;

        public Action OnSpawnEnemies;
        public Action OnStartCoundown;
        public Action OnStartGame;
        public Action OnLevelCleared;

        private SessionData _sessionData;

        [Inject]
        private GameStateHandler(SessionData sessionData)
        {
            _sessionData = sessionData;
        }

        public void SwitchState(GameState newState)
        {
            switch (newState)
            {
                case GameState.MainMenu:
                    break;
                case GameState.SpawnPlayer:
                    _sessionData.ClearCoins();
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
                    OnLevelCleared?.Invoke();
                    break;
                case GameState.Pause:
                    break;
                default:
                    break;
            }

            _state = newState;
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
