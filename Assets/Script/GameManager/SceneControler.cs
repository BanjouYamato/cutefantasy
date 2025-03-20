using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneControler : SingleTon<SceneControler>
{
    public string mainMenuScene;
    public string uiScene;
    public string TransitionName { get; private set; }

    [Header("Map Scene")]
    public List<string> mapScenes = new List<string>();
    public string currentMapScene { get; private set; }

    public void StartGame(string option)
    {   
        SceneManager.LoadScene(uiScene);
        MoveToScene(LoadSceneData(option));
    }

    public void MoveToScene(string newScene)
    {
        if (GameControler.Instance.isMainMapScene)
            Observer.Instance.Notify<bool>(GameConstant.UI_LOADSCENE, true);
        if (!GameControler.Instance.isMainMapScene)
            GameControler.Instance.isMainMapScene = true;
        StartCoroutine(ChangeScene(newScene));
    }
    private IEnumerator ChangeScene(string newScene)
    {

        AsyncOperation loadOp = SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
        while (!loadOp.isDone)
        {
            yield return null;
        }

        if (!string.IsNullOrEmpty(currentMapScene))
        {
            GameObject oldCamera = GameObject.FindWithTag("MainCamera");
            if (oldCamera != null)
            {
                AudioListener oldListener = oldCamera.GetComponent<AudioListener>();
                if (oldListener != null)
                {
                    oldListener.enabled = false;
                }
            }

            AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(currentMapScene));
            while (!unloadOp.isDone)
            {
                yield return null;
            }
        }

        currentMapScene = newScene;

        yield return new WaitForSeconds(0.5f);
        Observer.Instance.Notify<bool>(GameConstant.UI_LOADSCENE, false);
    }

    public void LoadMainMenu()
    {
        currentMapScene = null;
        GameControler.Instance.isMainMapScene = false;
        GameControler.Instance.runTimeData.ResetListData();
        GameControler.Instance.ResetData();
        SceneManager.LoadScene(mainMenuScene, LoadSceneMode.Single);
    }

    public void SetTransName(string transName)
    {
        this.TransitionName = transName;
    }
    public void SaveScene()
    {
        PlayerPrefs.SetString(GameConstant.GAME_SAVE_SCENE, currentMapScene);
        PlayerPrefs.Save();
    }
    public string LoadSceneData(string option)
    {
        
        if (option == GameConstant.NEW_GAME)
            return mapScenes[0];
        else
        {
            return PlayerPrefs.GetString(GameConstant.GAME_SAVE_SCENE);
        }
    }
}
