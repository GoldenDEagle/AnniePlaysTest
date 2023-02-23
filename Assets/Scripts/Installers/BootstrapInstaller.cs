using Assets.Scripts.Data;
using Assets.Scripts.Data.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            BindInterfaces();
            BindEventHandler();
            BindSessionData();
        }

        private void BindInterfaces()
        {
            Container.BindInterfacesTo<BootstrapInstaller>()
                .FromInstance(this)
                .AsSingle();
        }

        private void BindSessionData()
        {
            Container.Bind<SessionData>().AsSingle().NonLazy();
        }

        private void BindEventHandler()
        {
            Container.Bind<EventHandler>().AsSingle().NonLazy();
        }

        public void Initialize()
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }
}