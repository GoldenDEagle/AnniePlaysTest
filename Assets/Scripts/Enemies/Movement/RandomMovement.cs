using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Enemies.Movement
{
    public class RandomMovement : MovementBase
    {
        [SerializeField] private float _maxMovementDistance = 1f;

        private NavMeshAgent _agent;

        public override event Action OnMovementFinished;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public override IEnumerator Moving()
        {
            _agent.updateRotation = true;
            _agent.SetDestination(RandomNavmeshLocation(_maxMovementDistance));

            while (true)
            {
                if (!_agent.pathPending && _agent.remainingDistance < 0.1f)
                {
                    OnMovementFinished?.Invoke();
                    break;
                }
                yield return null;
            }
        }
    }
}