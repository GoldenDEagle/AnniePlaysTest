using System;

namespace Assets.Scripts.Data
{
    public class GameStateManager
    {
        public Action OnStateChanged;

        public void SwitchState(GameState newState)
        {
            switch (newState)
            {
                case GameState.MainMenu:
                    break;
                case GameState.PreparingLevel:
                    break;
                case GameState.Gameplay:
                    break;
                case GameState.EndGame:
                    break;
                case GameState.Pause:
                    break;
                default:
                    break;
            }

            OnStateChanged?.Invoke();
        }
    }

    public enum GameState
    {
        MainMenu = 0,
        PreparingLevel,
        Gameplay,
        EndGame,
        Pause
    }
}
