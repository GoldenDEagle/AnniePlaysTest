using System;

namespace Assets.Scripts.Data.Events
{
    // global events handler

    public class EventHandler
    {
        public event Action OnCoinsChanged;

        public void UpdateCoins()
        {
            OnCoinsChanged?.Invoke();
        }
    }
}
