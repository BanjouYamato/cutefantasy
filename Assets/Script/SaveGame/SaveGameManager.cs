using UnityEngine;

public class SaveGameManager : SingleTon<SaveGameManager> 
{
    public void Save(string key, string data)
    {
        PlayerPrefs.SetString(key, data);
        PlayerPrefs.Save();
    }
    public void Save<T>(string key, T data)
    {
        string jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, jsonData);
        PlayerPrefs.Save();
    }
    public string Load(string key, string defaultValue = null)
    {
        if (PlayerPrefs.HasKey(key))
        {
            return PlayerPrefs.GetString(key);
        }
        return defaultValue;
    }
    public T Load<T>(string key, T defaultValue = default)
    {
        if (PlayerPrefs.HasKey(key))
        {   
            string jsonData = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<T>(jsonData);
        }
        return defaultValue;
    }
    public bool HasSavedData(string key)
    {
        return PlayerPrefs.HasKey(key);
    }
    public void DeleteData(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.DeleteKey(key);
        }
    }
}
