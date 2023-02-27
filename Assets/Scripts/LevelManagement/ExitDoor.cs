using Assets.Scripts.Data;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.LevelManagement
{
    public class ExitDoor : MonoBehaviour
    {
        private GameStateHandler _gameStateHandler;

        [Inject]
        private void Construct(GameStateHandler gameStateHandler)
        {
            _gameStateHandler = gameStateHandler;
        }

        private void Start()
        {
            _gameStateHandler.OnAfterStateEnter += OpenDoor;
        }
        
        // called when level cleared
        private void OpenDoor(GameState gameState)
        {
            if (gameState != GameState.PostGame)
                return;

            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _gameStateHandler.OnAfterStateEnter -= OpenDoor;
        }
    }
}