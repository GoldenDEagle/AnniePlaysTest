using Assets.Scripts.Data;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class BootstrapInstaller : MonoInstaller, IInitializable
    {
        public override void InstallBindings()
        {
            BindInterfaces();
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

        public void Initialize()
        {
            SceneManager.LoadSceneAsync("MainMenu");
        }
    }
}