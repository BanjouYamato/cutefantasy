using System.Collections;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class AssetBundleManager : MonoBehaviour
{
    private AssetBundle loadedAssetBundle;

    private string localBundlePath => Path.Combine(Application.persistentDataPath, "AssetBundles");

    [SerializeField]
    private string bundleUrl = "https://yourserver.com/AssetBundles/gameobject";

    public GameObject gameObjectPrefab;

    private void Start()
    {
#if UNITY_EDITOR
        Debug.Log("Running in Editor - Using local prefab");
        Instantiate(gameObjectPrefab);
#else
        StartCoroutine(LoadAssetBundleFromFile("gameobject"));
#endif
    }
    public IEnumerator LoadAssetBundleFromFile(string bundleName)
    {
        string bundlePath = Path.Combine(localBundlePath, bundleName);

        if (!File.Exists(bundlePath))
        {
            Debug.LogWarning("Asset Bundle file does not exist locally. Downloading from server...");
            yield return StartCoroutine(DownloadAssetBundle(bundleUrl, bundleName));
            yield break;
        }

        var assetBundle = AssetBundle.LoadFromFile(bundlePath);
        if (assetBundle == null)
        {
            Debug.LogError("Failed to load Asset Bundle from file.");
            yield break;
        }

        Debug.Log($"Successfully loaded Asset Bundle: {bundleName}");
        loadedAssetBundle = assetBundle;
        LoadAndInstantiatePrefab("Cube");
    }
    public IEnumerator DownloadAssetBundle(string url, string bundleName)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to download Asset Bundle: " + www.error);
            yield break;
        }

        string savePath = Path.Combine(localBundlePath, bundleName);
        Directory.CreateDirectory(localBundlePath);
        File.WriteAllBytes(savePath, www.downloadHandler.data);

        Debug.Log("Asset Bundle downloaded and saved to: " + savePath);

        StartCoroutine(LoadAssetBundleFromFile(bundleName));
    }

    private void LoadAndInstantiatePrefab(string prefabName)
    {
        if (loadedAssetBundle == null)
        {
            Debug.LogError("Asset Bundle is not loaded.");
            return;
        }

        var prefab = loadedAssetBundle.LoadAsset<GameObject>(prefabName);
        if (prefab != null)
        {
            Instantiate(prefab);
            Debug.Log($"Successfully instantiated prefab: {prefabName}");
        }
        else
        {
            Debug.LogError($"Failed to load prefab: {prefabName} from Asset Bundle.");
        }
    }
}
