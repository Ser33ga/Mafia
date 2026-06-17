using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using VContainer;
public class MainMenuUIManager
{
    
    [Inject]public UIManager uIManager;
    public VisualElement MainMenu;
    public Button StartGameBTN;
    public Button LeaveGameBTN;
    public Button LanguageBTN;
    public Button SettingsBTN;
    public void Initialize(VisualElement el)
    {
        MainMenu=el;
        StartGameBTN=MainMenu.Q<Button>("StartGameBTN");
        StartGameBTN.clicked+= () => uIManager.ChooseScreen(uIManager.PreparingGameMenu);
        LeaveGameBTN=MainMenu.Q<Button>("LeaveGameBTN");
        LeaveGameBTN.clicked+= () => uIManager.ChooseScreen(uIManager.LeavingGameMenu);
        LanguageBTN=MainMenu.Q<Button>("LanguageBTN");
        LanguageBTN.clicked+= () => uIManager.ChooseScreen(uIManager.LanguageMenu);
        SettingsBTN=MainMenu.Q<Button>("SettingsBTN");
        SettingsBTN.clicked+= () => uIManager.ChooseScreen(uIManager.SettingsMenu);
    }
}