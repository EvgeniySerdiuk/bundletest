using System;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

namespace Project.Scripts.AssetBundlesUtility
{
    public class RemoteAssetBundleLoader
    {
        private const string URL = "https://raw.githubusercontent.com/EvgeniySerdiuk/Bundles/main/";
        
        public async UniTask<AssetBundleRequest> DownloadAsset(string name)
        {
            try
            {
                using (UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(URL + name))
                {
                    await request.SendWebRequest();

                    AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
                    var loadAsset = bundle.LoadAssetAsync<Sprite>(name);
                    await loadAsset;
                    
                    return loadAsset;
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