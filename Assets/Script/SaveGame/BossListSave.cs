using System.Collections.Generic;

[System.Serializable]
public class IntListWrapper
{
    public List<int> list;
    public IntListWrapper(List<int> list)
    {
        this.list = list;
    }
}
public class BossListSave : SaveLoadControler
{   
    public override void OnLoad()
    {
        IntListWrapper intList = SaveGameManager.Instance.Load<IntListWrapper>(GameConstant.GAME_SAVE);
        GameControler.Instance.bossStatus = intList.list;
    }

    public override void OnNewGame()
    {
        if (GameControler.Instance.bossStatus == null)
            GameControler.Instance.bossStatus = new List<int>();
        GameControler.Instance.bossStatus.Clear();
    }

    public override void Onsave()
    {   
        IntListWrapper intListWrapper = new IntListWrapper(GameControler.Instance.bossStatus);
        SaveGameManager.Instance.Save<IntListWrapper>(GameConstant.GAME_SAVE, intListWrapper);
    }
}
