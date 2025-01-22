using System;
using System.Threading;
using BundleTest.Project.Scripts.MainScreen;
using BundleTest.Project.Scripts.MainScreen.Counter;
using BundleTest.Project.Scripts.MainScreen.TittleText;
using Cysharp.Threading.Tasks;
using Project.Scripts.LoaderScreen.Configs;
using Project.Scripts.MainScreen.UI;
using Object = UnityEngine.Object;

namespace Project.Scripts.MainScreen.UIControllers
{
    public class MainScreenUIController : IDisposable
    {
        private readonly MainScreenCounterService _mainScreenCounterService;
        private readonly MainScreenTitleTextService _mainScreenTitleTextService;
        private readonly MainScreenButtonBgService _mainScreenButtonBgService;
        private readonly CancellationTokenSource _tokenSource = new();

        private MainScreenUI _mainScreenUI;
        private ScreenContainer _screenContainer;

        public MainScreenUIController(ScreenContainer screenContainer,
            MainScreenCounterService mainScreenCounterService,
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
            SetDataInMainScreen();

            _mainScreenUI.UpCounterButton.onClick.AddListener(UpCounterButtonClick);
            _mainScreenUI.RefreshContentButton.onClick.AddListener(RefreshContentButtonClick);

            _screenContainer = null;
        }

        private void SetDataInMainScreen()
        {
            _mainScreenUI.SetData(_mainScreenTitleTextService.TitleText, _mainScreenCounterService.CurrentValue,
                _mainScreenButtonBgService.BackgroundSprite);

            SetInteractableButtons(true);
        }

        private void SetInteractableButtons(bool interactable)
        {
            _mainScreenUI.UpCounterButton.interactable = interactable;
            _mainScreenUI.RefreshContentButton.interactable = interactable;
        }

        private void UpCounterButtonClick()
        {
            _mainScreenCounterService.IncrementValue();
            _mainScreenUI.UpCounterButton.SetText(_mainScreenCounterService.CurrentValue.ToString());
        }

        private void RefreshContentButtonClick()
        {
            SetInteractableButtons(false);
            LoadNewData().Forget();
        }

        private async UniTask LoadNewData()
        {
            await UniTask.WhenAll
            (
                _mainScreenCounterService.LoadValue(_tokenSource.Token, true),
                _mainScreenButtonBgService.LoadButtonBg(_tokenSource.Token),
                _mainScreenTitleTextService.LoadTitleText(_tokenSource.Token)
            ).AttachExternalCancellation(_tokenSource.Token);

            SetDataInMainScreen();
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}