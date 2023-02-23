using Assets.Scripts.Data;
using Assets.Scripts.Player;
using Assets.Scripts.Weapons;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _maxMovementDistance = 1f;
        [SerializeField] private float _shootingDuration = 1f;
        [SerializeField] private int _coinValue = 1;
        [SerializeField] private Weapon _weapon;

        private EnemyCounter _enemyCounter;
        private SessionData _sessionData;
        private NavMeshAgent _agent;
        private IEnumerator _currentState;
        private PlayerController _player;
        private GameStateHandler _gameStateHandler;

        [Inject]
        private void Construct(EnemyCounter enemyCounter, SessionData sessionData, PlayerController playerController, GameStateHandler gameStateHandler)
        {
            _enemyCounter = enemyCounter;
            _sessionData = sessionData;
            _player = playerController;
            _gameStateHandler = gameStateHandler;
        }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _gameStateHandler.OnStartGame += OnGameStarted;
        }

        private void OnGameStarted()
        {
            StartState(Moving());
        }

        private void StartState(IEnumerator coroutine)
        {
            if (_currentState != null)
                StopCoroutine(_currentState);

            _currentState = coroutine;
            StartCoroutine(coroutine);
        }

        private IEnumerator Moving()
        {
            _agent.updateRotation = true;
            _agent.SetDestination(RandomNavmeshLocation(_maxMovementDistance));

            while (true)
            {
                if (!_agent.pathPending && _agent.remainingDistance < 0.1f)
                {
                    StartState(Shooting());
                    break;
                }
                yield return null;
            }
        }

        private IEnumerator Shooting()
        {
            _agent.velocity = Vector3.zero;

            RotateTowardsPlayer();

            _weapon.StartFiring(transform.forward);

            yield return new WaitForSeconds(_shootingDuration);

            _weapon.StopFiring();

            StartState(Moving());
        }

        private void RotateTowardsPlayer()
        {
            _agent.updateRotation = false;
            var direction = (_player.transform.position - transform.position).normalized;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);

            transform.rotation = rotation;
        }

        // get random point on navmesh within radius
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
            _gameStateHandler.OnStartGame -= OnGameStarted;

            // add coins and remove from enemy list
            _sessionData.AddCoins(_coinValue);
            _enemyCounter.Remove(gameObject);
        }
    }
}