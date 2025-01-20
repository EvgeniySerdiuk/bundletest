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
        private LoaderScreenData _loaderScreenData;

        public LoaderScreenUIController(LoaderScreenData loaderScreenData)
        {
            _loaderScreenData = loaderScreenData;
        }

        public void Initialize()
        {
            _loaderScreenUI = Object.Instantiate(_loaderScreenData.LoaderScreenUI);
            _loaderScreenUI.ProgressBar.Construct(_loaderScreenData.LoadingTime);

            _loaderScreenData = null;
        }

        public async UniTask StartLoading(CancellationToken token)
        {
            await _loaderScreenUI.ProgressBar.StartFillingBar().AsyncWaitForCompletion();
        }
    }
}