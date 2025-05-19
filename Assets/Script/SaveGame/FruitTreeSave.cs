using System.Collections.Generic;

[System.Serializable]
public class FruitTreeList
{
    public List<FruitTreeData> list;
    public FruitTreeList(List<FruitTreeData> list)
    {
        this.list = list;
    }
}
public class FruitTreeSave : SaveLoadControler
{
    public override void OnLoad()
    {
        FruitTreeList list = SaveGameManager.Instance
            .Load<FruitTreeList>(GameConstant.FARMTILES_DATA);
        GameControler.Instance.runTimeData.fruitTreeDataList = list.list;
    }

    public override void OnNewGame()
    {
        GameControler.Instance.runTimeData.fruitTreeDataList.Clear();
    }

    public override void Onsave()
    {   
        FruitTreeList list = new FruitTreeList(GameControler.Instance.runTimeData.fruitTreeDataList);
        SaveGameManager.Instance.Save<FruitTreeList>(GameConstant.FARMTILES_DATA, list);
    }
}
