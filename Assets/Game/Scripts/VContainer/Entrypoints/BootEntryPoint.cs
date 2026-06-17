using Cysharp.Threading.Tasks;
using Cysharp.Threading;
using System.Threading;
using UnityEngine;
using VContainer.Unity;
using VContainer;

public class BootEntryPoint:IAsyncStartable
{
    [Inject] UIManager uIManager;
    [Inject] PreparingGameUIManager preparingGameUIManager;
    [Inject] MainMenuUIManager mainMenuUIManager;
    //Saves
    [Inject]public RolesOrderData rolesOrderData;
    [Inject]public RolesInGameData rolesInGameData;
    [Inject]public PlayersData playersData;
    [Inject] SaveSystem saveSystem;
    public async UniTask StartAsync(CancellationToken token = default)
    {
        saveSystem.Register(rolesInGameData);
        saveSystem.Register(playersData);
        saveSystem.Register(rolesOrderData);
        saveSystem.LoadAll();

        uIManager.Initialize();
        mainMenuUIManager.Initialize(uIManager.MainMenu);
        preparingGameUIManager.Initialize(uIManager.PreparingGameMenu);
        uIManager.StartGame();
        
    }
}