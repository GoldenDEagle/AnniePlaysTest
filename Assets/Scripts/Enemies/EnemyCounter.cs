using Assets.Scripts.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Enemies
{
    // holds list of active enemies

    public class EnemyCounter
    {
        private List<GameObject> _activeEnemies = new List<GameObject>();

        private GameStateHandler _gameStateHandler;

        [Inject]
        private EnemyCounter(GameStateHandler gameStateHandler)
        {
            _gameStateHandler = gameStateHandler;
        }

        public void Add(GameObject enemyObject)
        {
            _activeEnemies.Add(enemyObject);
        }

        public void Remove(GameObject enemyObject)
        {
            _activeEnemies.Remove(enemyObject);

            // end game if no enemies left
            if (!_activeEnemies.Any())
            {
                _gameStateHandler.SwitchState(GameState.PostGame);
            }
        }

        /// <summary>
        /// Find enemy closest to the given position
        /// </summary>
        /// <param name="givenPosition"></param>
        public GameObject FindClosestEnemy(Vector3 givenPosition)
        {
            // check for empty list
            if (!_activeEnemies.Any())
                return null;

            float minDistance = (_activeEnemies[0].transform.position - givenPosition).magnitude;
            GameObject closestEnemy = _activeEnemies[0];

            // iterate to find the nearest
            foreach (var enemy in _activeEnemies)
            {
                var distance = (enemy.transform.position - givenPosition).magnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestEnemy = enemy;
                }
            }

            return closestEnemy;
        }

        public void Clear()
        { 
            _activeEnemies.Clear();
        }
    }
}