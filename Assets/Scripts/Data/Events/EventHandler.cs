using System;

namespace Assets.Scripts.Data.Events
{
    public class EventHandler
    {
        public event Action OnCoinsChanged;

        public void UpdateCoins()
        {
            OnCoinsChanged?.Invoke();
        }
    }
}
