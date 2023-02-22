using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _maxMovementDistance = 1f;
        [SerializeField] private float _moveInterval = 1f;

        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            StartCoroutine(Moving());
        }

        private IEnumerator Moving()
        {
            while (true)
            {
                _agent.SetDestination(RandomNavmeshLocation(_maxMovementDistance));
                yield return new WaitForSeconds(_moveInterval);
            }
        }

        public Vector3 RandomNavmeshLocation(float radius)
        {
            Vector3 randomDirection = Random.onUnitSphere * radius;
            randomDirection.y = 0f;
            randomDirection += transform.position;
            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, 1))
            {
                finalPosition = hit.position;
            }
            return finalPosition;
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}