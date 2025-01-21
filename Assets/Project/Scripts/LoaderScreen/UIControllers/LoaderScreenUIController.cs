using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Scripts.LoaderScreen.Configs;
using Project.Scripts.LoaderScreen.UI;
using UnityEngine;
using VContainer.Unity;

namespace Project.Scripts.LoaderScreen.UIControllers
{
    public class LoaderScreenUIController : IInitializable
    {
        private LoaderScreenUI _loaderScreenUI;
        private ScreenContainer _screenContainer;

        public LoaderScreenUIController(ScreenContainer screenContainer)
        {
            _screenContainer = screenContainer;
        }

        public void Initialize()
        {
            _loaderScreenUI = Object.Instantiate(_screenContainer.LoaderScreenUI);
            _loaderScreenUI.ProgressBar.Construct(_screenContainer.LoadingTime);

            _screenContainer = null;
        }

        public async UniTask StartLoading(CancellationToken token)
        {
            await _loaderScreenUI.ProgressBar.StartFillingBar(token);
        }

        public void CloseScreen()
        {
            Object.Destroy(_loaderScreenUI.gameObject);
        }
    }
}