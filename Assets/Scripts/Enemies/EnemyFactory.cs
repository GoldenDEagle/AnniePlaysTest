using System;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Enemies
{
    public class EnemyFactory
    {
        private const string GroundEnemyPath = "Enemies/enemy_ground";
        private const string FlyingEnemyPath = "Enemies/enemy_flying";

        private readonly DiContainer _diContainer;
        
        private Object _groundEnemyPrefab;
        private Object _flyingEnemyPrefab;

        public EnemyFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void Load()
        {
            _groundEnemyPrefab = Resources.Load(GroundEnemyPath);
            _flyingEnemyPrefab = Resources.Load(FlyingEnemyPath);
        }

        public GameObject Create(EnemyType enemyType, Vector3 position)
        {
            GameObject spawned = null;
            switch (enemyType)
            {
                case EnemyType.Ground:
                    spawned = _diContainer.InstantiatePrefab(_groundEnemyPrefab, position, Quaternion.identity, null);
                    break;
                case EnemyType.Flying:
                    spawned = _diContainer.InstantiatePrefab(_flyingEnemyPrefab, position, Quaternion.identity, null);
                    break;
                default:
                    throw new ArgumentException("No such enemy type");
            }
            return spawned;
        }
    }

    public enum EnemyType
    {
        Ground = 0,
        Flying = 1
    }
}