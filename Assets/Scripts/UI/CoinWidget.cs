using Assets.Scripts.Data;
using Assets.Scripts.Data.Events;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    public class CoinWidget : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private EventHandler _eventHandler;
        private SessionData _sessionData;

        [Inject]
        private void Construct(EventHandler eventHandler, SessionData sessionData)
        {
            _eventHandler = eventHandler;
            _sessionData = sessionData;
        }

        private void Awake()
        {
            UpdateCoins();
            _eventHandler.OnCoinsChanged += UpdateCoins;
        }

        private void UpdateCoins()
        {
            _text.text = $"Coins: {_sessionData.Coins}";
        }

        private void OnDestroy()
        {
            _eventHandler.OnCoinsChanged -= UpdateCoins;
        }
    }
}