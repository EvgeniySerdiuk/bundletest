using UnityEditor;

public class AssetBundleBuilder
{
    [MenuItem("Assets/Build AssetBundles")]
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