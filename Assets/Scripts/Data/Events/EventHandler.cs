using System;

namespace Assets.Scripts.Data.Events
{
    public class EventHandler
    {
        public event Action OnCoinsChanged;
        public event Action<GameState> OnGameStateChanged;

        public void UpdateCoins()
        {
            OnCoinsChanged?.Invoke();
        }

        public void SwitchGameState(GameState newState)
        {
            OnGameStateChanged?.Invoke(newState);
        }
    }
}
