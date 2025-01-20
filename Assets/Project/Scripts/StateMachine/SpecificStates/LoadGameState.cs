using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.AssetBundlesUtility;
using Project.Scripts.LoaderScreen.Configs;
using Project.Scripts.LoaderScreen.UIControllers;

namespace Project.Scripts.StateMachine.SpecificStates
{
    public class LoadGameState : State
    {
        public override Type NextState => typeof(GameState);

        private readonly LoaderScreenData _loadingData;
        private readonly LoaderScreenUIController _loaderScreenUIController;
        private readonly RemoteAssetBundleLoader _remoteBundleLoader;

        public LoadGameState(LoaderScreenData loadingData, LoaderScreenUIController loaderScreenUIController,
            RemoteAssetBundleLoader remoteBundleLoader)
        {
            _loadingData = loadingData;
            _loaderScreenUIController = loaderScreenUIController;
            _remoteBundleLoader = remoteBundleLoader;
        }

        public override async UniTask Enter(CancellationToken cancellationToken)
        {
            await UniTask.WhenAll
            (
                _remoteBundleLoader.DownloadAsset(_loadingData.LoadingBundleName),
                _loaderScreenUIController.StartLoading(cancellationToken)
            );
        }

        public override UniTask Exit(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}