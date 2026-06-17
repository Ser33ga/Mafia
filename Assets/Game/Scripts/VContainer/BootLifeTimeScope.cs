using VContainer.Unity;
using VContainer;
public class BootLifeTimeScope: LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<BootEntryPoint>(Lifetime.Scoped);
        builder.Register<RolesOrderData>(Lifetime.Scoped);
        builder.Register<RolesInGameData>(Lifetime.Scoped);
        builder.Register<PlayersData>(Lifetime.Scoped);
        builder.Register<SaveSystem>(Lifetime.Singleton);
        builder.Register<DataProviderForUI>(Lifetime.Scoped);
        builder.Register<MainMenuUIManager>(Lifetime.Scoped);
        builder.Register<PreparingGameUIManager>(Lifetime.Scoped);
        UIManager uIManager=FindAnyObjectByType<UIManager>();
        builder.RegisterComponent<UIManager>(uIManager);
        
    }
}