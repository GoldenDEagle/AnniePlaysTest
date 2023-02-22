namespace Assets.Scripts.Data
{
    public class SessionData
    {
        private int _coins;
        public int Coins => _coins;

        public void AddCoins(int coinsToAdd)
        {
            _coins += coinsToAdd;
        }
    }
}
