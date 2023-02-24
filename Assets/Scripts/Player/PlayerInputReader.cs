using Assets.Scripts.Data;
using Assets.Scripts.Enemies;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Assets.Scripts.Player
{
    // collecting all input

    [RequireComponent(typeof(PlayerController))]
    public class PlayerInputReader : MonoBehaviour
    {
        private PlayerController _playerController;

        private GameStateHandler _gameStateHandler;

        [Inject]
        private void Construct(GameStateHandler gameStateHandler)
        {
            _gameStateHandler = gameStateHandler;
        }

        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();

            _playerController.SetDirection(direction);
        }

        public void OnWeaponSwitch(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _playerController.SwitchWeapon();
            }
        }

        public void OnPausePressed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                WindowUtils.CreateWindow("PauseWindow");
            }
        }
    }
}