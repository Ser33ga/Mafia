using UnityEngine.UIElements;
using VContainer;

public class GameMenuUIManager
{
    [Inject]public UIManager uIManager;
    public VisualElement GameMenu;
    private bool _isSyncing;
    public void Initialize(VisualElement el)
    {
        GameMenu=el;
        var fixedScroll = GameMenu.Q<ScrollView>("fixed-scroll");
        var innerVScroll = GameMenu.Q<ScrollView>("inner-v-scroll");

        // Синхронизация: fixed → inner
        fixedScroll.verticalScroller.valueChanged += val =>
        {
            if (!_isSyncing)
            {
                _isSyncing = true;
                innerVScroll.verticalScroller.value = val;
                _isSyncing = false;
            }
        };

        innerVScroll.verticalScroller.valueChanged += val =>
        {
            if (!_isSyncing)
            {
                _isSyncing = true;
                fixedScroll.verticalScroller.value = val;
                _isSyncing = false;
            }
        };
        var hScroll = GameMenu.Q<ScrollView>("h-scroll");
        var scrollHeader = GameMenu.Q<ScrollView>("scroll-header");

        hScroll.horizontalScroller.valueChanged += val =>
        scrollHeader.horizontalScroller.value = val;
    }
}