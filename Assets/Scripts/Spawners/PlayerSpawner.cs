using UnityEngine;
using Zenject;

namespace Assets.Scripts.Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _playerStartPosition;

        private DiContainer _container;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }

        void Awake()
        {
          //  _container.InstantiatePrefab(_playerPrefab, _playerStartPosition.position, Quaternion.identity, null);
        }
    }
}