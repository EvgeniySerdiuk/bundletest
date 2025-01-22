using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace BundleTest.Project.Scripts.JsonUtility
{
    public class RemoteJsonLoader
    {
        private const string URL = "https://raw.githubusercontent.com/EvgeniySerdiuk/Json/main/";

        public async UniTask<T> DownloadAsset<T>(string name, CancellationToken token)
        {
            try
            {
                using (var request = UnityWebRequest.Get(URL + name))
                {
                    await request.SendWebRequest().WithCancellation(token);

                    if (request.result == UnityWebRequest.Result.Success)
                    {
                        var json = request.downloadHandler.text;
                        return JsonConvert.DeserializeObject<T>(json);
                    }
                    else
                    {
                        Debug.LogError($"Error downloading asset: {request.error}");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

            return default;
        }
    }
}