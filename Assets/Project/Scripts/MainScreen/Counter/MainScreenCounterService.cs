using System.Threading;
using BundleTest.Project.Scripts.JsonUtility;
using Cysharp.Threading.Tasks;
using Project.Scripts.JsonUtility;
using Project.Scripts.SaveLoad;

namespace BundleTest.Project.Scripts.MainScreen.Counter
{
    public class CounterStartValue
    {
        public int StartingNumber;
    }

    public class MainScreenCounterService
    {
        public int CurrentValue { get; private set; }

        private const string SaveKey = "MainScreenCounter";
        private readonly SaveService _saveService;
        private readonly RemoteJsonLoader _remoteJsonLoader;

        public MainScreenCounterService(SaveService saveService, RemoteJsonLoader remoteJsonLoader)
        {
            _saveService = saveService;
            _remoteJsonLoader = remoteJsonLoader;
        }

        public async UniTask LoadValue(CancellationToken token, bool onlyRemote = false)
        {
            var loadModel = _saveService.LoadEntryWithDefault(SaveKey, new CounterStartValue());

            if (onlyRemote || loadModel.StartingNumber == 0)
            {
                loadModel = await _remoteJsonLoader.DownloadAsset<CounterStartValue>(JsonFilesNames.SETTINGS, token);
            }

            CurrentValue = loadModel.StartingNumber;
            SaveValue();
        }

        public void IncrementValue()
        {
            CurrentValue++;
            SaveValue();
        }

        private void SaveValue()
        {
            var saveModel = new CounterStartValue { StartingNumber = CurrentValue };
            _saveService.FlashChanges(SaveKey, saveModel);
        }
    }
}