using Assets.Scripts.Data;
using Assets.Scripts.Enemies;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float _spawnInterval = 1f;
        [SerializeField] private EnemyMarker[] _enemyMarkers;

        private EnemyFactory _enemyFactory;
        private EnemyCounter _enemyCounter;
        private GameStateHandler _gameStateHandler;

        private Coroutine _coroutine;

        [Inject]
        private void Construct(EnemyFactory enemyFactory, EnemyCounter enemyCounter, GameStateHandler gameStateHandler)
        {
            _enemyFactory = enemyFactory;
            _enemyCounter = enemyCounter;
            _gameStateHandler = gameStateHandler;
        }

        private void Awake()
        {
            _gameStateHandler.OnSpawnEnemies += SpawnEnemies;
        }

        public void SpawnEnemies()
        {
            _enemyFactory.Load();
            _coroutine = StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            yield return new WaitForSeconds(1f);

            foreach (var marker in _enemyMarkers)
            {
                var enemy = _enemyFactory.Create(marker.EnemyType, marker.transform.position);
                _enemyCounter.Add(enemy);
                yield return new WaitForSeconds(_spawnInterval);
            }

            _gameStateHandler.SwitchState(GameState.Countdown);
        }

        private void OnDestroy()
        {
            _gameStateHandler.OnSpawnEnemies -= SpawnEnemies;

            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
    }
}