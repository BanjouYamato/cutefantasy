using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSave : SaveLoadControler
{
    public override void OnLoad()
    {
        GameControler.Instance.LoadGold(
            PlayerPrefs.GetInt(GameConstant.GAME_SAVE));
    }

    public override void OnNewGame()
    {
        GameControler.Instance.LoadGold(100);
    }

    public override void Onsave()
    {
        PlayerPrefs.SetInt(GameConstant.GAME_SAVE, GameControler.Instance.gold);
    }
}
