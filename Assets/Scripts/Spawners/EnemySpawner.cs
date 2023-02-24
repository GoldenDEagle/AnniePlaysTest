using Assets.Scripts.Data;
using Assets.Scripts.Enemies;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Spawners
{
    public class EnemySpawner : MonoBehaviour
    {
        [Tooltip("Time between enemies spawn")]
        [SerializeField] private float _spawnInterval = 1f;
        [Header("Enemies to spawn")]
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
            foreach (var marker in _enemyMarkers)
            {
                yield return new WaitForSeconds(_spawnInterval);
                var enemy = _enemyFactory.Create(marker.EnemyType, marker.transform.position);
                _enemyCounter.Add(enemy);
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