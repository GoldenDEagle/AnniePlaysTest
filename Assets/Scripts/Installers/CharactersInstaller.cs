using Assets.Scripts.Enemies;
using Assets.Scripts.Player;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class CharactersInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;

        public override void InstallBindings()
        {
            BindEnemyCounter();
            BindPlayerController();
            BindEnemyFactory();
        }

        private void BindPlayerController()
        {
            Container.Bind<PlayerController>().FromComponentInNewPrefab(_playerPrefab).AsSingle();
        }

        private void BindEnemyFactory()
        {
            Container.Bind<EnemyFactory>().AsSingle().NonLazy();
        }

        private void BindEnemyCounter()
        {
            Container.Bind<EnemyCounter>().AsSingle().NonLazy();
        }
    }
}