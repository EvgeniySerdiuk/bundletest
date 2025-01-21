using System;
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

    public class MainScreenCounterService : IDisposable
    {
        public int CurrentValue { get; private set; }

        private const string SaveKey = "MainScreenCounter";
        private readonly SaveService _saveService;
        private readonly RemoteJsonLoader _remoteJsonLoader;

        private Action _action;

        public MainScreenCounterService(SaveService saveService, RemoteJsonLoader remoteJsonLoader)
        {
            _saveService = saveService;
            _remoteJsonLoader = remoteJsonLoader;
        }

        public void SubscribeAction(Action action)
        {
            _action = action;
            _action += IncrementValue;
        }

        private void UnSubscribeAction()
        {
            _action -= IncrementValue;
        }

        public async UniTask LoadValue(CancellationToken token)
        {
            var loadModel = _saveService.LoadEntryWithDefault(SaveKey, new CounterStartValue());

            if (loadModel.StartingNumber == 0)
            {
                loadModel = await _remoteJsonLoader.DownloadAsset<CounterStartValue>(JsonFilesNames.SETTINGS, token);
            }

            CurrentValue = loadModel.StartingNumber;
        }

        public void SaveValue()
        {
            var saveModel = new CounterStartValue { StartingNumber = CurrentValue };
            _saveService.FlashChanges(SaveKey, saveModel);
        }

        private void IncrementValue()
        {
            CurrentValue++;
            SaveValue();
        }

        public void Dispose()
        {
            UnSubscribeAction();
        }
    }
}