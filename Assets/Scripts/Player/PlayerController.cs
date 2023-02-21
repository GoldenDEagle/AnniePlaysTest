using Assets.Scripts.Enemies;
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
        [SerializeField] private float _fireInterval = 2f;

        // references
        private CharacterController _characterController;
        private EnemyCounter _enemyCounter;

        // internal variables
        private Vector2 _direction;
        private Coroutine _firingRoutine;
        private bool _isMoving;

        [Inject]
        private void Construct(EnemyCounter enemyCounter)
        {
            _enemyCounter = enemyCounter;
        }

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void FixedUpdate()
        {
            CheckMovementStatus();

            if (_isMoving)
            {
                StopFiring();

                MovePlayer();
                RotatePlayer(_direction);
            }
            else
            {
                Fire();
            }
        }

        // if firing => stop
        private void StopFiring()
        {
            if (_firingRoutine != null)
            {
                StopCoroutine(_firingRoutine);
                _firingRoutine = null;
            }
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

        private void Fire()
        {
            // find and rotate towards nearest enemy
            var enemy = _enemyCounter.FindClosestEnemy(transform.position);
            var direction = (enemy.transform.position - transform.position).normalized;
            RotatePlayer(new Vector2(direction.x, direction.z));

            // start firing if don't already
            if (_firingRoutine == null)
            {
                _firingRoutine = StartCoroutine(FiringRoutine());
            }
        }

        private IEnumerator FiringRoutine()
        {
            while (true)
            {
                Debug.Log("Fire");
                yield return new WaitForSeconds(_fireInterval);
            }
        }

        // direction set (input and enemy targeting)
        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }
    }
}
