using Assets.Scripts.Data;
using Assets.Scripts.Data.Events;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            BindInstallerInterfaces();
            BindEventHandler();
            BindSessionData();
            BindGameStatesHandler();
        }

        private void BindInstallerInterfaces()
        {
            Container.BindInterfacesTo<BootstrapInstaller>()
                .FromInstance(this)
                .AsSingle();
        }

        private void BindGameStatesHandler()
        {
            Container.Bind<GameStateHandler>().AsSingle().NonLazy();
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
            SceneManager.LoadScene("MainMenu");
        }
    }
}