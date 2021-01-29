using Utilities.ObjectPool;

namespace Zenject
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ObjectPool>().FromComponentInChildren().AsSingle();
        }
    }
}