using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SaveLoadControler : MonoBehaviour
{   
    protected virtual void Start()
    {   
        Observer.Instance.AddToList(GameConstant.GAME_SAVE, Onsave);
        if (GameControler.Instance.IsChangScene)
            return;
        if (GameControler.Instance.IsNewGame)
            OnNewGame();
        else
            OnLoad();
    }
    protected void OnDisable()
    {
        Observer.Instance.RemoveToList(GameConstant.GAME_SAVE, Onsave);
    }
    public abstract void OnNewGame();
    public abstract void Onsave();
    public abstract void OnLoad();
}
