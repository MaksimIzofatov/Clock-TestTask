using Zenject;

public class ClockInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<TimeService>().AsSingle().NonLazy();
    }
}
