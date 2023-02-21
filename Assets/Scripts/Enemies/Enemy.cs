using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _arrivalTreshold = 0.1f;

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
                _agent.SetDestination(RandomNavmeshLocation(4f));
                yield return new WaitForSeconds(2);
            }
        }

        public Vector3 RandomNavmeshLocation(float radius)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += transform.position;
            NavMeshHit hit;
            Vector3 finalPosition = Vector3.zero;
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
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