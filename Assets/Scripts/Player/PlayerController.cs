using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        // parameters
        [SerializeField] private float _playerSpeed = 2.0f;
        [SerializeField] private float _rotationSpeed = 4f;

        // references
        private CharacterController _characterController;

        // internal variables
        private Vector2 _direction;
        private Vector3 _playerVelocity;

        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
        }

        private void FixedUpdate()
        {
            MovePlayer();
            RotatePlayer();
        }

        // movement control
        private void MovePlayer()
        {
            Vector3 move = new Vector3(_direction.x, 0, _direction.y);

            _characterController.Move(move * Time.deltaTime * _playerSpeed);
        }

        // rotation depending on direction
        private void RotatePlayer()
        {
            if (_direction != Vector2.zero)
            {
                float targetAngle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);
            }
        }

        // direction set (input and enemy targeting)
        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }
    }
}
