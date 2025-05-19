using System.IO;
using UnityEditor;

public class CreateAssetsBundles
{
    [MenuItem("Assets/Create Asset Bundles")]   
    public static void BuildAllAssetBundles()
    {
        string bundlePath = "Assets/AssetBundle";
        if (!Directory.Exists(bundlePath))
        {
            Directory.CreateDirectory(bundlePath);
        }
        BuildPipeline.BuildAssetBundles(bundlePath,
            BuildAssetBundleOptions.None,
            BuildTarget.StandaloneWindows);
    }
}
