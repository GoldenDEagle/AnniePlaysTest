using Assets.Scripts.Data.Events;
using Zenject;

namespace Assets.Scripts.Data
{
    public class SessionData
    {
        private int _coins;
        public int Coins => _coins;

        private EventHandler _eventHandler;

        [Inject]
        private void Construct(EventHandler eventHandler)
        {
            _eventHandler = eventHandler;
        }

        public void AddCoins(int coinsToAdd)
        {
            _coins += coinsToAdd;
            _eventHandler.UpdateCoins();
        }

        public void ClearCoins()
        {
            _coins = 0;
        }
    }
}
