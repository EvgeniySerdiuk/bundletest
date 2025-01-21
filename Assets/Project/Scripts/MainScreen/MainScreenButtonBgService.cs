using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Scripts.AssetBundlesUtility;
using Project.Scripts.LoaderScreen.Configs;
using UnityEngine;

namespace BundleTest.Project.Scripts.MainScreen
{
    public class MainScreenButtonBgService
    {
        public Sprite BackgroundSprite { get; private set; }
        
        private readonly RemoteAssetBundleLoader _bundleLoader;
        private readonly string _assetBundleName;

        public MainScreenButtonBgService(RemoteAssetBundleLoader bundleLoader, ScreenContainer screenContainer )
        {
            _bundleLoader = bundleLoader;
            _assetBundleName = AssetBundlesNames.BUTTONBG;
        }

        public async UniTask LoadButtonBg(CancellationToken token)
        {
            BackgroundSprite = await _bundleLoader.DownloadAsset<Sprite>(_assetBundleName, token);
        }
    }
}