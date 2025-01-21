using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Project.Scripts.SaveLoad
{
    public class SaveService
    {
        public const string SaveFile = "save.json";

        public string SavePath => Path.Combine("saves", SaveFile);
        public string SaveFilePath => Path.Combine(Application.persistentDataPath, SavePath);
        public string SaveDirectory => Path.Combine(Application.persistentDataPath, "saves");

        private readonly Dictionary<string, object> _saveEntries = new();
        private Dictionary<string, JObject> _loadedEntries;

        public SaveService()
        {
            Load();
        }

        public T LoadEntryWithDefault<T>(string key, T defaultValue) where T : class
        {
            if (_loadedEntries == null && _saveEntries.Count == 0)
            {
                return defaultValue;
            }

            if (_loadedEntries.TryGetValue(key, out var json))
            {
                return json.ToObject<T>();
            }

            if (_saveEntries.TryGetValue(key, out var model))
            {
                return model as T;
            }

            return defaultValue;
        }

        public void FlashChanges(string key, object model, bool forceSave = true)
        {
            _saveEntries[key] = model;

            if (forceSave)
            {
                Save();
            }
        }

        private void Save()
        {
            var yamlStr = JsonConvert.SerializeObject(_saveEntries);

            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }

            File.WriteAllText(SaveFilePath, yamlStr);
        }

        private void Load()
        {
            if (!File.Exists(SaveFilePath))
            {
                _loadedEntries = new Dictionary<string, JObject>();
                return;
            }

            var json = File.ReadAllText(SaveFilePath);
            _loadedEntries = JsonConvert.DeserializeObject<Dictionary<string, JObject>>(json);

            foreach (var loadedEntries in _loadedEntries)
            {
                FlashChanges(loadedEntries.Key, loadedEntries.Value, false);
            }
        }
    }
}