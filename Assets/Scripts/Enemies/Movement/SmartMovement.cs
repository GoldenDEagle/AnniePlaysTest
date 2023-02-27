using Assets.Scripts.Data;
using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Scripts.Enemies.Movement
{
    public class SmartMovement : MovementBase
    {
        [SerializeField] private float _maxMovementDistance = 1f;
        [SerializeField] private int _attemptsToFindPosition = 5;

        private Vector3 _nextPosition;
        private List<Vector3> _possiblePositions;

        public override event Action OnMovementFinished;

        private NavMeshAgent _agent;
        private PlayerController _player;

        [Inject]
        private void Construct(PlayerController playerController)
        {
            _player = playerController;
        }

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _possiblePositions = new List<Vector3>(_attemptsToFindPosition);
        }

        public override IEnumerator Moving()
        {
            _agent.updateRotation = true;

            // try to find position for visibility
            for (int i = 0; i < _attemptsToFindPosition; i++)
            {
                _nextPosition = RandomNavmeshLocation(_maxMovementDistance);
                _possiblePositions.Add(_nextPosition);

                if (CanSeePlayer(_nextPosition))
                {
                    _possiblePositions.Clear();
                    break;
                }
            }

            // if propper position found
            if (CanSeePlayer(_nextPosition))
            {
                _agent.SetDestination(_nextPosition);
            }
            // if no -> find closest position to player
            else
            {
                var closestPosition = FindClosestPosition();

                _agent.SetDestination(closestPosition);
            }

            // send a callback when destination reached
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

        // finds closest point to the player from the list
        private Vector3 FindClosestPosition()
        {
            int closestIndex = 0;
            var minDistance = (_player.transform.position - _possiblePositions[closestIndex]).magnitude;
            for (int j = 0; j < _possiblePositions.Count; j++)
            {
                var distance = (_player.transform.position - _possiblePositions[j]).magnitude;
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestIndex = j;
                }
            }

            return _possiblePositions[closestIndex];
        }

        // check if player is visible from position
        bool CanSeePlayer(Vector3 position)
        {
            if (Physics.Raycast(position, _player.transform.position - position, out RaycastHit hit))
            {
                if (hit.transform == _player.transform)
                {
                    return true;
                }
            }
            return false;
        }
    }
}