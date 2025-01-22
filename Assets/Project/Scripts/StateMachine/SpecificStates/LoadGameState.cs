using System;
using System.Threading;
using BundleTest.Project.Scripts.MainScreen;
using BundleTest.Project.Scripts.MainScreen.Counter;
using BundleTest.Project.Scripts.MainScreen.TittleText;
using Cysharp.Threading.Tasks;
using Project.Scripts.LoaderScreen.UIControllers;
using Project.Scripts.MainScreen.UIControllers;
using UnityEngine;

namespace Project.Scripts.StateMachine.SpecificStates
{
    public class LoadGameState : State
    {
        public override Type NextState => typeof(GameState);

        private readonly MainScreenUIController _mainScreenUIController;
        private readonly LoaderScreenUIController _loaderScreenUIController;
        private readonly MainScreenButtonBgService _mainScreenButtonBgService;
        private readonly MainScreenTitleTextService _mainScreenTitleTextService;
        private readonly MainScreenCounterService _mainScreenCounterService;

        public LoadGameState(LoaderScreenUIController loaderScreenUIController,
            MainScreenButtonBgService mainScreenButtonBgService, MainScreenTitleTextService mainScreenTitleTextService,
            MainScreenCounterService mainScreenCounterService, MainScreenUIController mainScreenUIController)
        {
            _loaderScreenUIController = loaderScreenUIController;
            _mainScreenButtonBgService = mainScreenButtonBgService;
            _mainScreenTitleTextService = mainScreenTitleTextService;
            _mainScreenCounterService = mainScreenCounterService;
            _mainScreenUIController = mainScreenUIController;
        }

        public override async UniTask Enter(CancellationToken token)
        {
            await UniTask.WhenAll
            (
                _loaderScreenUIController.StartLoading(token),
                _mainScreenButtonBgService.LoadButtonBg(token),
                _mainScreenTitleTextService.LoadTitleText(token),
                _mainScreenCounterService.LoadValue(token)
            );

            Debug.Log("<color=yellow>Load button bg:</color> " + _mainScreenButtonBgService.BackgroundSprite.name);
            Debug.Log("<color=yellow>Load TITLE:</color> " + _mainScreenTitleTextService.TitleText);
            Debug.Log("<color=yellow>Load Counter bg:</color> " + _mainScreenCounterService.CurrentValue);

            Complete();
        }

        public override UniTask Exit(CancellationToken cancellationToken)
        {
            _mainScreenUIController.CreateScreen();
            return UniTask.CompletedTask;
        }
    }
}