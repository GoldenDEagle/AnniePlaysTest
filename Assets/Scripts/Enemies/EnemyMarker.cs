using UnityEngine;

namespace Assets.Scripts.Enemies
{
    public class EnemyMarker : MonoBehaviour
    {
        [SerializeField] private EnemyType _enemyType;
        public EnemyType EnemyType => _enemyType;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
    }
}