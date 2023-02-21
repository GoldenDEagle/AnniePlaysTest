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
        private Coroutine _coroutine;

        [Inject]
        private void Construct(EnemyFactory enemyFactory, EnemyCounter enemyCounter)
        {
            _enemyFactory = enemyFactory;
            _enemyCounter = enemyCounter;
        }

        private void Start()
        {
            SpawnEnemies();
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
                var enemy = _enemyFactory.Create(marker.EnemyType, marker.transform.position);
                _enemyCounter.Add(enemy);
                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        private void OnDestroy()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }
    }
}