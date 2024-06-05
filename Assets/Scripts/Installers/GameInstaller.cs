using IndustrRazvlProj.EventBus;
using Zenject;

public class GameInstaller : MonoInstaller
{
    private CustomEventBus _eventBus;
    public override void InstallBindings()
    {
        _eventBus = new CustomEventBus();
        Container.Bind<CustomEventBus>().FromInstance(_eventBus).AsSingle();
    }
}