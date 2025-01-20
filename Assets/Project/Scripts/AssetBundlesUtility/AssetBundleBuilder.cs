using UnityEditor;

namespace Project.Scripts.AssetBundlesUtility
{
    public class AssetBundleBuilder
    {
        [MenuItem("Tools/Build Asset Bundles")]
        public static void BuildAllAssetBundles()
        {
            var outputPath = "Assets/AssetBundles";
        
            if (!System.IO.Directory.Exists(outputPath))
            {
                System.IO.Directory.CreateDirectory(outputPath);
            }

            BuildPipeline.BuildAssetBundles(outputPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        }
    }
}