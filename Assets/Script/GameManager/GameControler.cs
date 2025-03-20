using BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControler : SingleTon<GameControler>
{
    public bool unDead;
    public RunTimeData runTimeData;
    public bool isMainMapScene;
    public List<int> bossStatus = new List<int>();
    
    public List<string> loadedSceneList = new List<string>();
    bool isNewGame;

    public int gold { get; private set; }

    public event Action<int> onGoldChanged;

    public void SetGold(int val)
    {
        gold += val;
        gold = Mathf.Clamp(gold, 0, gold);
        onGoldChanged?.Invoke(gold);
    }
    public void LoadGold(int val)
    {
        gold = val;
        onGoldChanged?.Invoke(gold);
    }
    public bool IsNewGame
    {   
        get { return isNewGame; }
        set { isNewGame = value; }
    }
    bool isChangScene;
    public bool IsChangScene
    {
        get { return isChangScene; }
        set {  isChangScene = value; }
    }
    public void ResetData()
    {   
        isChangScene = false;
        bossStatus.Clear();
    }
    public bool IsFirstTimeLoad(string sceneName)
    {
        if (!loadedSceneList.Contains(sceneName))
        {
            loadedSceneList.Add(sceneName);
            return true;
        }
        return false;
    }
    public void SetBossDefeated(int bossID)
    {
        bossStatus.Add(bossID);
    }
    public bool IsBossDefeated(int bossID)
    {
        return bossStatus.Contains(bossID);
    }
}
