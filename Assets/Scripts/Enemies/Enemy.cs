using Assets.Scripts.Data;
using Assets.Scripts.Enemies.Movement;
using Assets.Scripts.Player;
using Assets.Scripts.Weapons;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Assets.Scripts.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float _shootingDuration = 1f;
        [SerializeField] private int _coinValue = 1;
        [Header("Weapon")]
        [SerializeField] private Weapon _weapon;
        [Header("Movement script")]
        [SerializeField] private MovementBase _mover;

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
            _gameStateHandler.OnAfterStateEnter += OnGameStarted;
            _mover.OnMovementFinished += BeginShooting;
        }

        private void OnGameStarted(GameState gameState)
        {
            if (gameState != GameState.Gameplay)
                return;

            StartState(_mover.Moving());
        }

        // call to change state
        private void StartState(IEnumerator coroutine)
        {
            if (_currentState != null)
                StopCoroutine(_currentState);

            _currentState = coroutine;
            StartCoroutine(coroutine);
        }

        private void BeginShooting()
        {
            StartState(Shooting());
        }

        private IEnumerator Shooting()
        {
            _agent.velocity = Vector3.zero;

            RotateTowardsPlayer();

            _weapon.StartFiring(transform.forward);

            yield return new WaitForSeconds(_shootingDuration);

            _weapon.StopFiring();

            StartState(_mover.Moving());
        }

        private void RotateTowardsPlayer()
        {
            _agent.updateRotation = false;
            var direction = (_player.transform.position - transform.position).normalized;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);

            transform.rotation = rotation;
        }


        private void OnDestroy()
        {
            _gameStateHandler.OnAfterStateEnter -= OnGameStarted;
            _mover.OnMovementFinished += BeginShooting;

            // add coins and remove from enemy list
            _sessionData.AddCoins(_coinValue);
            _enemyCounter.Remove(gameObject);
        }
    }
}