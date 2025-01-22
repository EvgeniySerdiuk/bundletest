using System.Threading;
using BundleTest.Project.Scripts.JsonUtility;
using Cysharp.Threading.Tasks;
using Project.Scripts.JsonUtility;

namespace BundleTest.Project.Scripts.MainScreen.TittleText
{
    public class TitleStartText
    {
        public string Text;
    }

    public class MainScreenTitleTextService
    {
        public string TitleText { get; private set; }

        private readonly RemoteJsonLoader _remoteJsonLoader;

        public MainScreenTitleTextService(RemoteJsonLoader remoteJsonLoader)
        {
            _remoteJsonLoader = remoteJsonLoader;
        }

        public async UniTask LoadTitleText(CancellationToken token)
        {
            var result = await _remoteJsonLoader.DownloadAsset<TitleStartText>(JsonFilesNames.TEXTS, token);
            TitleText = result.Text;
        }
    }
}