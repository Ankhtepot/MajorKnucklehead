using Utilities.Managers;
using Utilities.ObjectPool;

namespace Zenject
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameManager>().FromComponentInChildren().AsSingle();
        }
    }
}