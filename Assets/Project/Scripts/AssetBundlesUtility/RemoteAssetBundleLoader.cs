using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

namespace Project.Scripts.AssetBundlesUtility
{
    public class RemoteAssetBundleLoader
    {
        private const string URL = "https://raw.githubusercontent.com/EvgeniySerdiuk/Bundles/main/";

        public async UniTask<T> DownloadAsset<T>(string name, CancellationToken token) where T : UnityEngine.Object
        {
            try
            {
                using (UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(URL + name))
                {
                    await request.SendWebRequest().ToUniTask(cancellationToken: token);

                    var bundle = DownloadHandlerAssetBundle.GetContent(request);
                    var asset = await bundle.LoadAssetAsync<T>(name).ToUniTask(cancellationToken: token);

                    bundle.Unload(false);

                    return asset as T;
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }

            return null;
        }
    }
}