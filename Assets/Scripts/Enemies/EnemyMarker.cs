using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyMarker : MonoBehaviour
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private bool _randomizeposition;
        [SerializeField] private float _xSpawnBorder;
        [SerializeField] private float _zSpawnBorder;

        public EnemyType EnemyType => _enemyType;

        private void Awake()
        {
            if (!_randomizeposition)
            {
                var xPosition = Random.Range(-_xSpawnBorder, _xSpawnBorder);
                var zPosition = Random.Range(-_zSpawnBorder, _zSpawnBorder);

                transform.position = new Vector3(xPosition, transform.position.y, zPosition);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}