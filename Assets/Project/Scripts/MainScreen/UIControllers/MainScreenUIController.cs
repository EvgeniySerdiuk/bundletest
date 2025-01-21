using System;
using BundleTest.Project.Scripts.MainScreen;
using BundleTest.Project.Scripts.MainScreen.Counter;
using BundleTest.Project.Scripts.MainScreen.TittleText;
using Project.Scripts.LoaderScreen.Configs;
using Project.Scripts.MainScreen.UI;
using Object = UnityEngine.Object;

namespace Project.Scripts.MainScreen.UIControllers
{
    public class MainScreenUIController
    {
        private readonly MainScreenCounterService _mainScreenCounterService;
        private readonly MainScreenTitleTextService _mainScreenTitleTextService;
        private readonly MainScreenButtonBgService _mainScreenButtonBgService;

        private MainScreenUI _mainScreenUI;
        private ScreenContainer _screenContainer;
        private Action _onUpCounterButtonClick;

        public MainScreenUIController(ScreenContainer screenContainer, MainScreenCounterService mainScreenCounterService,
            MainScreenTitleTextService mainScreenTitleTextService, MainScreenButtonBgService mainScreenButtonBgService)
        {
            _screenContainer = screenContainer;
            _mainScreenCounterService = mainScreenCounterService;
            _mainScreenTitleTextService = mainScreenTitleTextService;
            _mainScreenButtonBgService = mainScreenButtonBgService;
        }

        public void CreateScreen()
        {
            _mainScreenUI = Object.Instantiate(_screenContainer.MainScreenUI);
            _mainScreenUI.Construct(_mainScreenTitleTextService.TitleText, _mainScreenCounterService.CurrentValue,
                _mainScreenButtonBgService.BackgroundSprite);

            _mainScreenUI.UpCounterButton.onClick.AddListener(UpCounterButtonClick);
            _mainScreenUI.RefreshContentButton.onClick.AddListener(RefreshContentButtonClick);

            _mainScreenCounterService.SubscribeAction(_onUpCounterButtonClick);

            _screenContainer = null;
        }

        private void UpCounterButtonClick()
        {
            _onUpCounterButtonClick?.Invoke();
            _mainScreenUI.UpCounterButton.SetText(_mainScreenCounterService.CurrentValue.ToString());
        }

        private void RefreshContentButtonClick()
        {
        }
    }
}