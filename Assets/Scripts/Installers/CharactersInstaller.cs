using Assets.Scripts.Enemies;
using Assets.Scripts.Player;
using Assets.Scripts.Projectiles;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class CharactersInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _playerStartPosition;

        public override void InstallBindings()
        {
            BindEnemyCounter();
            BindPlayerController();
            BindEnemyFactory();
        }

        private void BindEnemyFactory()
        {
            Container.Bind<EnemyFactory>().AsSingle().NonLazy();
        }

        private void BindEnemyCounter()
        {
            Container.Bind<EnemyCounter>().AsSingle().NonLazy();
        }

        private void BindPlayerController()
        {
            PlayerController playerController = Container
                .InstantiatePrefabForComponent<PlayerController>(_playerPrefab, _playerStartPosition.position, Quaternion.identity, null);

            Container.Bind<PlayerController>().FromInstance(playerController).AsSingle();
        }
    }
}