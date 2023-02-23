using Assets.Scripts.Data;
using Assets.Scripts.Data.Definitions;
using Assets.Scripts.Enemies;
using Assets.Scripts.Weapons;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        // parameters
        [SerializeField] private float _playerSpeed = 2.0f;
        [SerializeField] private float _rotationSpeed = 4f;
        [SerializeField] private Weapon _weapon;

        // references
        private CharacterController _characterController;
        private EnemyCounter _enemyCounter;
        private GameStateHandler _gameStateHandler;

        // internal variables
        private Vector2 _direction;
        private bool _isMoving;

        [Inject]
        private void Construct(EnemyCounter enemyCounter, GameStateHandler gameStateHandler)
        {
            _enemyCounter = enemyCounter;
            _gameStateHandler = gameStateHandler;
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            _gameStateHandler.SwitchState(GameState.SpawnEnemies);
        }

        private void FixedUpdate()
        {
            if (!(_gameStateHandler.State == GameState.Gameplay || _gameStateHandler.State == GameState.PostGame)) return;

            CheckMovementStatus();

            if (_isMoving)
            {
                StopFiring();

                MovePlayer();
                RotatePlayer(_direction);
            }
            else
            {
                if (_gameStateHandler.State != GameState.Gameplay) return;
                LockOnTarget();
            }
        }

        // if firing -> stop
        private void StopFiring()
        {
            if (!_weapon.IsFiring)
                return;

            _weapon.StopFiring();
        }

        private void CheckMovementStatus()
        {
            if (_direction != Vector2.zero)
            {
                _isMoving = true;
            }
            else
            {
                _isMoving = false;
            }
        }

        // movement control
        private void MovePlayer()
        {
            Vector3 move = new Vector3(_direction.x, 0, _direction.y);

            _characterController.Move(move * Time.deltaTime * _playerSpeed);
        }

        // rotation depending on direction
        private void RotatePlayer(Vector2 rotationDirection)
        {
            if (rotationDirection != Vector2.zero)
            {
                float targetAngle = Mathf.Atan2(rotationDirection.x, rotationDirection.y) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);
            }
        }

        private void LockOnTarget()
        {
            // find and rotate towards nearest enemy
            var enemy = _enemyCounter.FindClosestEnemy(transform.position);
            var direction = (enemy.transform.position - transform.position).normalized;
            RotatePlayer(new Vector2(direction.x, direction.z));

            // fire weapon
            _weapon.StartFiring(direction);
        }

        // direction set from input
        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }
    }
}
