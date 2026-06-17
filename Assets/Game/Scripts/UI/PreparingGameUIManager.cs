using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
public class PreparingGameUIManager
{
    [Inject]public UIManager uIManager;
    public VisualElement PreparingGameMenu;
    //service
    public ScrollView InteractiveElements;
    //выход в меню
    public Button MainMenuBTN;
    //добавить игроков
    public VisualElement PlayersEL;
    public ListView PlayersList;
    public VisualTreeAsset PlayersListPrefab;
    public Button AddEmptyPlayerBTN;
    public Button SavePlayersBTN;
    public Button ResetPlayersBTN;
    //выбрать роли и их количество
    public VisualElement RolesEL;
    public ListView RolesList;
    public VisualTreeAsset RolesInGamePrefab;
    public Button SaveRolesInGameBTN;
    public Button ResetRolesInGameBTN;
    //сделать порядок ролей
    public VisualElement RolesOrderEL;
    public ListView RolesOrderList;
    public VisualTreeAsset RoleOrderPrefab; 
    public Button SaveRolesOrderBTN;
    public Button ResetRolesOrderBTN;

    public void Initialize(VisualElement el)
    {
        PreparingGameMenu=el;
        
        MainMenuBTN=el.Q<Button>("MenuBTN");
        MainMenuBTN.clicked+=() => uIManager.ChooseScreen(uIManager.MainMenu);

        InteractiveElements=el.Q<ScrollView>("InteractiveElements");
        
        PlayersList=el.Q<ListView>("PlayersList");
        PlayersList.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
        PlayersList.selectionType = SelectionType.None;
        PlayersEL=el.Q<VisualElement>("PlayersEL");
        PlayersListPrefab=uIManager.PlayerListPrefab;
        PlayersList.itemsSource=uIManager.dataProviderForUI.players;
        if (uIManager.dataProviderForUI.players.Count==0)
        {
            PlayersList.style.display=DisplayStyle.None;
        }
        uIManager.dataProviderForUI.OnPlayersChanged += () => PlayersList.Rebuild();
        PlayersList.makeItem = () =>
        {
            return PlayersListPrefab.CloneTree();
        };
        PlayersList.bindItem = (element, index) =>
        {
            var playerName = element.Q<TextField>("PlayerNameField");

            if (playerName.userData is EventCallback<ChangeEvent<string>> old)
                playerName.UnregisterValueChangedCallback(old);
            playerName.SetValueWithoutNotify(uIManager.dataProviderForUI.players[index].name);

            EventCallback<ChangeEvent<string>> callback = evt =>
                uIManager.dataProviderForUI.players[index].name = evt.newValue;
            playerName.userData = callback;
            playerName.RegisterValueChangedCallback(callback);

            var removePlayer = element.Q<Button>("RemovePlayerBTN");

            if (removePlayer.userData is System.Action oldRemove)
                removePlayer.clicked -= oldRemove;

            System.Action removeCallback = () =>
            {
                uIManager.dataProviderForUI.RemoveFromPlayers(uIManager.dataProviderForUI.players[index]);
                if (uIManager.dataProviderForUI.players.Count == 0)
                {
                    PlayersList.style.display = DisplayStyle.None;
                }
            };
            removePlayer.userData = removeCallback;
            removePlayer.clicked += removeCallback;
        };

        PlayersList.unbindItem = (element, index) =>
        {
            var playerName = element.Q<TextField>("PlayerNameField");

            if (playerName.userData is EventCallback<ChangeEvent<string>> callback)
            {
                playerName.UnregisterValueChangedCallback(callback);
                playerName.userData = null;
            }

            var removePlayer = element.Q<Button>("RemovePlayerBTN");

            if (removePlayer.userData is System.Action removeCallback)
            {
                removePlayer.clicked -= removeCallback;
                removePlayer.userData = null;
            }
        };
        AddEmptyPlayerBTN=el.Q<Button>("AddEmptyPlayerBTN");
        AddEmptyPlayerBTN.clicked += () =>
        {
            if (uIManager.dataProviderForUI.players.Count==0)
                {
                    PlayersList.style.display=DisplayStyle.Flex;
                }
            uIManager.dataProviderForUI.AddEmptyForPlayers();
        };
        ResetPlayersBTN=el.Q<Button>("ResetPlayersBTN");
        ResetPlayersBTN.clicked += () =>
        {
            uIManager.dataProviderForUI.ResetPlayers();
            PlayersList.Rebuild();
            if (uIManager.dataProviderForUI.players.Count == 0)
                {
                    PlayersList.style.display = DisplayStyle.None;
                }
        };
        SavePlayersBTN=el.Q<Button>("SavePlayersBTN");
        SavePlayersBTN.clicked += () =>
        {
            uIManager.dataProviderForUI.SavePlayers();
        };
        
        RolesList=el.Q<ListView>("RolesList");
        RolesEL=el.Q<VisualElement>("RolesEL");

        RolesInGamePrefab=uIManager.roleInGamePrefab;
        RolesList.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
        RolesList.selectionType = SelectionType.None;
        RolesList.itemsSource=uIManager.dataProviderForUI.rolesInGame;
        uIManager.dataProviderForUI.OnRoleInGameChanged += () => RolesList.Rebuild();
        RolesList.makeItem = () =>
        {
            return RolesInGamePrefab.CloneTree();
        };
        RolesList.bindItem = (element, index) =>
        {
            var roleName = element.Q<Label>("RoleName");
            var roleAmount = element.Q<IntegerField>("RoleAmountINT");
            roleName.text=uIManager.dataProviderForUI.rolesInGame[index].name;

            var roleInGame=element.Q<Toggle>("IsRoleInGameTOGGLE");
            roleInGame.value=uIManager.dataProviderForUI.rolesInGame[index].isInGame;
            // отписка старого, если bind пришёл без unbind
            if (roleInGame.userData is EventCallback<ChangeEvent<bool>> oldCallback)
            roleInGame.UnregisterValueChangedCallback(oldCallback);

            // создаём новый, сохраняем ссылку
            EventCallback<ChangeEvent<bool>> newCallback = evt =>
            {
                uIManager.dataProviderForUI.rolesInGame[index].isInGame=evt.newValue;
                roleAmount.SetEnabled(evt.newValue);
            };
            roleInGame.userData = newCallback;
            roleInGame.RegisterValueChangedCallback(newCallback);

            if (roleAmount.userData is EventCallback<ChangeEvent<int>> old)
                roleAmount.UnregisterValueChangedCallback(old);

            roleAmount.SetValueWithoutNotify(uIManager.dataProviderForUI.rolesInGame[index].amount);

            EventCallback<ChangeEvent<int>> callback = evt =>
                uIManager.dataProviderForUI.rolesInGame[index].amount = evt.newValue;
            roleAmount.userData = callback;
            roleAmount.RegisterValueChangedCallback(callback);
            };
            

        RolesList.unbindItem = (element, index) =>
        {
            var roleAmount = element.Q<IntegerField>("RoleAmountINT");

            if (roleAmount.userData is EventCallback<ChangeEvent<int>> callback)
            {
                roleAmount.UnregisterValueChangedCallback(callback);
                roleAmount.userData = null;
            }

            var roleInGame=element.Q<Toggle>("IsRoleInGameTOGGLE");

            if (roleInGame.userData is EventCallback<ChangeEvent<bool>> newcallback)
            {
                roleInGame.UnregisterValueChangedCallback(newcallback);
                roleInGame.userData = null;
            }
        };

        ResetRolesInGameBTN=el.Q<Button>("ResetRolesInGameBTN");
        ResetRolesInGameBTN.clicked += () =>
        {
            uIManager.dataProviderForUI.ResetRolesInGame();
            RolesList.Rebuild();
        };
        SaveRolesInGameBTN=el.Q<Button>("SaveRolesInGameBTN");
        SaveRolesInGameBTN.clicked += () =>
        {
            uIManager.dataProviderForUI.SaveRolesInGame();
        };



        RolesOrderList=el.Q<ListView>("RolesOrderList");
        RolesOrderEL=el.Q<VisualElement>("RolesOrderEL");
        RoleOrderPrefab=uIManager.roleOrderPrefab;
        RolesOrderList.virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight;
        RolesOrderList.selectionType = SelectionType.None;
        RolesOrderList.itemsSource=uIManager.dataProviderForUI.rolesOrder;
        RolesOrderList.makeItem = () =>
        {
            return RoleOrderPrefab.CloneTree();
        };
        RolesOrderList.bindItem = (element, index) =>
        {
            var orderList = uIManager.dataProviderForUI.rolesOrder;

            var roleName = element.Q<Label>("RoleOrderName");
            roleName.text = orderList[index].role.name;

            var priority = element.Q<Label>("PriorityLabel");
            priority.text = orderList[index].priority.ToString();

            var upBTN = element.Q<Button>("UpBTN");

            if (upBTN.userData is System.Action oldUp)
                upBTN.clicked -= oldUp;

            System.Action upCallback = () =>
            {
                if (index > 0)
                {
                    (orderList[index - 1], orderList[index]) = (orderList[index], orderList[index - 1]);
                    orderList[index - 1].priority = index;       // 1 — самый высокий
                    orderList[index].priority = index + 1;
                    RolesOrderList.Rebuild();
                }
            };
            upBTN.userData = upCallback;
            upBTN.clicked += upCallback;

            var downBTN = element.Q<Button>("DownBTN");

            if (downBTN.userData is System.Action oldDown)
                downBTN.clicked -= oldDown;

            System.Action downCallback = () =>
            {
                if (index < orderList.Count - 1)
                {
                    (orderList[index + 1], orderList[index]) = (orderList[index], orderList[index + 1]);
                    orderList[index].priority = index + 1;
                    orderList[index + 1].priority = index + 2;
                    RolesOrderList.Rebuild();
                }
            };
            downBTN.userData = downCallback;
            downBTN.clicked += downCallback;
        };

        RolesOrderList.unbindItem = (element, index) =>
        {
            var upBTN = element.Q<Button>("UpBTN");
            if (upBTN.userData is System.Action upCallback)
            {
                upBTN.clicked -= upCallback;
                upBTN.userData = null;
            }

            var downBTN = element.Q<Button>("DownBTN");
            if (downBTN.userData is System.Action downCallback)
            {
                downBTN.clicked -= downCallback;
                downBTN.userData = null;
            }
        };

        ResetRolesOrderBTN=el.Q<Button>("ResetRolesOrderBTN");
        ResetRolesOrderBTN.clicked += () =>
        {
            uIManager.dataProviderForUI.ResetRolesOrder();
            RolesOrderList.Rebuild();
        };
        SaveRolesOrderBTN=el.Q<Button>("SaveRolesOrderBTN");
        SaveRolesOrderBTN.clicked += () =>
        {
            uIManager.dataProviderForUI.SaveRolesOrder();
        };




        InteractiveElements.Add(PlayersEL);
        InteractiveElements.Add(RolesEL);
        InteractiveElements.Add(RolesOrderEL);

    } 
}