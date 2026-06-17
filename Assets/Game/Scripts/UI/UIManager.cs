using VContainer;
using Cysharp.Threading;
using VContainer.Unity;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
public class UIManager:MonoBehaviour
{
    [Inject] public DataProviderForUI dataProviderForUI;
    [SerializeField] private UIDocument rootDoc;
    private VisualElement rootElement;
    public VisualElement MainMenu;
    public VisualElement SettingsMenu;
    public VisualElement LanguageMenu;
    public VisualElement LeavingGameMenu;
    public VisualElement PreparingGameMenu;
    public VisualElement GameMenu;
    public VisualElement currentOpenedMenu;
    //PreparingGame
    public VisualTreeAsset PlayerListPrefab;
    public VisualTreeAsset roleInGamePrefab;
    public VisualTreeAsset roleOrderPrefab;
    public void Initialize()
    {
        rootElement=rootDoc.rootVisualElement;
        MainMenu=rootElement.Q<TemplateContainer>("MainMenu");
        SettingsMenu=rootElement.Q<TemplateContainer>("SettingsMenu");
        LanguageMenu=rootElement.Q<TemplateContainer>("LanguageMenu");
        LeavingGameMenu=rootElement.Q<TemplateContainer>("LeavingGameMenu");
        PreparingGameMenu=rootElement.Q<TemplateContainer>("PreparingGameMenu");
        GameMenu=rootElement.Q<TemplateContainer>("GameMenu");
    }
    public void StartGame()
    {
        ChooseScreen(MainMenu);
    }
    public void ChooseScreen(VisualElement element)
    {
        if (currentOpenedMenu != null)
        {
            currentOpenedMenu.style.display = DisplayStyle.None;
        }
        currentOpenedMenu=element;
        element.style.display = DisplayStyle.Flex;
    }
}