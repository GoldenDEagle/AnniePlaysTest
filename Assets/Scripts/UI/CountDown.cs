using Assets.Scripts.Data;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    public class CountDown : MonoBehaviour
    {
        [Tooltip("Seconds until start")]
        [SerializeField] private int _secondsToStart = 3;
        [SerializeField] private TextMeshProUGUI _text;

        private const string GoText = "Go!";

        private int _counter;

        private GameStateHandler _gameStateHandler;

        [Inject]
        private void Construct(GameStateHandler gameStateHandler)
        {
            _gameStateHandler = gameStateHandler;
        }

        private void Awake()
        {
            _gameStateHandler.OnStartCoundown += Run;
            _counter = _secondsToStart;
        }

        private void Run()
        {
            _text.gameObject.SetActive(true);
            StartCoroutine(Counting());
        }

        private IEnumerator Counting()
        {
            // update every second
            while (_counter > 0)
            {
                yield return new WaitForSeconds(1f);
                _counter--;
                UpdateCount();
            }

            // final message
            _text.text = GoText;
            yield return new WaitForSeconds(0.5f);

            _gameStateHandler.SwitchState(GameState.Gameplay);
            _text.gameObject.SetActive(false);
        }

        private void UpdateCount()
        {
            _text.text = _counter.ToString();
        }

        private void OnDisable()
        {
            _gameStateHandler.OnStartCoundown -= Run;
        }
    }
}